#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// LICENSED UNDER THE APACHE LICENSE, VERSION 2.0 (THE "LICENSE"); YOU MAY NOT USE THESE 
// FILES EXCEPT IN COMPLIANCE WITH THE LICENSE. YOU MAY OBTAIN A COPY OF THE LICENSE AT 
// http://www.apache.org/licenses/LICENSE-2.0
// UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING, SOFTWARE DISTRIBUTED UNDER THE 
// LICENSE IS DISTRIBUTED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY 
// KIND, EITHER EXPRESS OR IMPLIED. SEE THE LICENSE FOR THE SPECIFIC LANGUAGE GOVERNING 
// PERMISSIONS AND LIMITATIONS UNDER THE LICENSE.
//=======================================================================================
#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ServiceBusExplorer.Utilities.Helpers;
using Microsoft.ServiceBus.Messaging;

#endregion

namespace ServiceBusExplorer.Helpers
{
    public class DeletedDlqMessagesResult
    {
        #region Public constructor
        public DeletedDlqMessagesResult(bool timedOut, List<long> deletedSequenceNumbers)
        {
            TimedOut = timedOut;
            DeletedSequenceNumbers = deletedSequenceNumbers;
        }
        #endregion

        #region Public instance properties
        public bool TimedOut { get; }
        public List<long> DeletedSequenceNumbers { get; }
        #endregion
    }

    public class DeadLetterMessageHandler
    {
        #region Private Fields
        // Either queueDescription or subscriptionWrapper is used - but never both.
        readonly QueueDescription sourceQueueDescription;
        readonly SubscriptionWrapper sourceSubscriptionWrapper;
        readonly int receiveTimeoutInSeconds;
        readonly ServiceBusHelper serviceBusHelper;
        readonly WriteToLogDelegate writeToLog;
        #endregion

        #region Public Constructors
        public DeadLetterMessageHandler(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper,
            int receiveTimeoutInSeconds, QueueDescription queueDescription)
            : this(writeToLog, serviceBusHelper, receiveTimeoutInSeconds)
        {
            sourceQueueDescription = queueDescription;
        }

        public DeadLetterMessageHandler(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper,
            int receiveTimeoutInSeconds, SubscriptionWrapper subscriptionWrapper)
             : this(writeToLog, serviceBusHelper, receiveTimeoutInSeconds)
        {
            sourceSubscriptionWrapper = subscriptionWrapper;
        }
        #endregion

        #region Private Constructor
        DeadLetterMessageHandler(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper, 
            int receiveTimeoutInSeconds)
        {
            this.writeToLog = writeToLog;
            this.serviceBusHelper = serviceBusHelper;
            this.receiveTimeoutInSeconds = receiveTimeoutInSeconds;
        }
        #endregion

        #region Public methods
        public async Task<DeletedDlqMessagesResult> DeleteMessages(List<long?> sequenceNumbers)
        {
            var sequenceNumbersToDeleteList = new List<long?>();
            foreach (var number in sequenceNumbers)
            {
                sequenceNumbersToDeleteList.Add(number);
            }

            var timedOut = false;
            var dlqEntityPath = GetDlqEntityPath();

            var messageReceiver = await serviceBusHelper.MessagingFactory.CreateMessageReceiverAsync(
                dlqEntityPath,
                ReceiveMode.PeekLock).ConfigureAwait(false);

            var done = false;
            var lockedMessages = new Dictionary<long, BrokeredMessage>(1000);
            var deletedSequenceNumbers = new List<long>();
            var maxTimeInSeconds = GetMaxOperationTimeInSeconds();

            if (maxTimeInSeconds < 1)
            {
                throw new LockDurationTooLowException();
            }

            var stopwatch = Stopwatch.StartNew();

            try
            {
                do
                {
                    var message = await messageReceiver.ReceiveAsync(TimeSpan.FromSeconds(receiveTimeoutInSeconds))
                        .ConfigureAwait(false);

                    if (message != null)
                    {
                        if (sequenceNumbers.Contains(message.SequenceNumber))
                        {
                            var sequenceNumber = message.SequenceNumber;
                            await message.CompleteAsync().ConfigureAwait(false);
                            message.Dispose();
                            sequenceNumbersToDeleteList.Remove(sequenceNumber);
                            deletedSequenceNumbers.Add(sequenceNumber);

                            if (sequenceNumbersToDeleteList.Count == 0)
                            {
                                done = true;
                            }
                        }
                        else
                        {
                            if (lockedMessages.ContainsKey(message.SequenceNumber))
                            {
                                timedOut = true;
                                done = true;
                            }
                            else
                            {
                                lockedMessages.Add(message.SequenceNumber, message);
                            }
                        }
                    }
                    else
                    {
                        done = true;
                    }

                    if (stopwatch.ElapsedMilliseconds >= maxTimeInSeconds * 1000)
                    {
                        timedOut = true;
                        done = true;
                    }

                } while (!done);
            }
            catch (MessageLockLostException)
            {
                WriteToLog($"Got a MessageLockLostException after {stopwatch.ElapsedMilliseconds / 1000} seconds.");
                timedOut = true;
            }
            finally
            {
                foreach (var pair in lockedMessages)
                {
                    await pair.Value.AbandonAsync().ConfigureAwait(false);
                    pair.Value.Dispose();
                }

                await messageReceiver.CloseAsync().ConfigureAwait(false);
                stopwatch.Stop();
            }

            return new DeletedDlqMessagesResult(timedOut, deletedSequenceNumbers);
        }

    

        public async Task<DeletedDlqMessagesResult> MoveMessages(MessageSender messageSender,
            List<long> sequenceNumbers, List<BrokeredMessage> messagesToSend = null)
        {
            if (messagesToSend != null)
            {
                if (sequenceNumbers.Count != messagesToSend.Count)
                {
                    throw new ArgumentException("The number of items in the parameter sequenceNumbers must be the same as the number of " +
                        "items in the parameter messagesToSend", nameof(messagesToSend));
                }
            }

            var dlqEntityPath = GetDlqEntityPath();

            var messageReceiver = serviceBusHelper.MessagingFactory.CreateMessageReceiver(
                dlqEntityPath,
                ReceiveMode.PeekLock);

            var timedOut = false;
            var done = false;
            var lockedMessages = new List<BrokeredMessage>(1000);
            var movedSequenceNumbers = new List<long>();
            var maxTimeInSeconds = GetMaxOperationTimeInSeconds();

            if (maxTimeInSeconds < 1)
            {
                throw new LockDurationTooLowException();
            }

            var stopwatch = Stopwatch.StartNew();

            try
            {
                do
                {
                    var message = await messageReceiver.ReceiveAsync(TimeSpan.FromSeconds(receiveTimeoutInSeconds)).ConfigureAwait(false);

                    if (message != null)
                    {
                        if (sequenceNumbers.Contains(message.SequenceNumber))
                        {
                            try
                            {
                                if (messagesToSend == null)
                                {
                                    await messageSender.SendAsync(message).ConfigureAwait(false);
                                }
                                else
                                {
                                    var index = sequenceNumbers.IndexOf(message.SequenceNumber);
                                    await messageSender.SendAsync(messagesToSend[index])
                                        .ConfigureAwait(false);
                                }

                                await message.CompleteAsync().ConfigureAwait(false);
                            }
                            catch
                            {
                                await message.AbandonAsync().ConfigureAwait(false);
                                throw;
                            }

                            movedSequenceNumbers.Add(message.SequenceNumber);
                            if (movedSequenceNumbers.Count >= sequenceNumbers.Count)
                            {
                                done = true;
                            }

                            message.Dispose();
                        }
                        else
                        {
                            lockedMessages.Add(message);
                        }
                    }
                    else
                    {
                        done = true;
                    }

                    if (stopwatch.ElapsedMilliseconds >= maxTimeInSeconds * 1000)
                    {
                        timedOut = true;
                        done = true;
                    }

                } while (!done);
            }
            catch (MessageLockLostException)
            {
                WriteToLog($"Got a MessageLockLostException after {stopwatch.ElapsedMilliseconds / 1000} seconds.");
                timedOut = true;
            }
            finally
            {
                foreach (var message in lockedMessages)
                {
                    await message.AbandonAsync().ConfigureAwait(false);
                    message.Dispose();
                }

                await messageSender.CloseAsync().ConfigureAwait(false);
                await messageReceiver.CloseAsync().ConfigureAwait(false);

                stopwatch.Stop();
            }

            return new DeletedDlqMessagesResult(timedOut, movedSequenceNumbers);
        }

        public string GetFailureExplanation(DeletedDlqMessagesResult result, int targetMessageCount, bool delete)
        {
            var verb = delete ? "deleted" : "moved";
            var singularis = targetMessageCount == 1;
            string messageText;

            if (!result.DeletedSequenceNumbers.Any())
            {
                messageText = singularis ? $"The message was not {verb}." : $"No messages were {verb}.";
            }
            else
            {
                messageText = $"Not all the selected messages were {verb}.";
            }

            if (result.TimedOut)
            {
                messageText += " This was caused by the time to go through the messages were longer than " +
                    "Lock duration for the queue. Either delete some messages in the dead letter subqueue or " +
                    "increase the Lock duration for the queue." +
                     Environment.NewLine + Environment.NewLine +
                     $"The Lock duration for the queue is currently {GetLockDurationInSeconds()}" +
                     " seconds.";
            }
            else
            {
                messageText += " This might have been caused by the Receive Timeout in the Options dialog is" +
                    " too low or by another process which deleted or locked the messages." +
                     Environment.NewLine + Environment.NewLine +
                    $"The Receive Timeout is currently set to {receiveTimeoutInSeconds} seconds.";
            }

            return messageText;
        }


        #endregion

        #region Private methods
        private double GetLockDurationInSeconds()
        {
            if (sourceQueueDescription != null)
            {
                return sourceQueueDescription.LockDuration.TotalSeconds;
            }

            return sourceSubscriptionWrapper.SubscriptionDescription.LockDuration.TotalSeconds;
        }

        private int GetMaxOperationTimeInSeconds()
        {
            // Allocate three seconds for final operations;
            const int FinalActionsTime = 3;

            if (sourceQueueDescription != null)
            {
                return (int)sourceQueueDescription.LockDuration.TotalSeconds - FinalActionsTime;
            }

            return (int)sourceSubscriptionWrapper.SubscriptionDescription.LockDuration.TotalSeconds
                - FinalActionsTime;
        }

        string GetDlqEntityPath()
        {
            if (sourceQueueDescription != null)
            {
                return QueueClient.FormatDeadLetterPath(sourceQueueDescription.Path);
            }

            return SubscriptionClient.FormatDeadLetterPath(
                sourceSubscriptionWrapper.SubscriptionDescription.TopicPath,
                sourceSubscriptionWrapper.SubscriptionDescription.Name);
        }
        void WriteToLog(string message)
        {
            if (writeToLog != null && !string.IsNullOrWhiteSpace(message))
            {
                writeToLog(message);
            }
        }
        #endregion
    }
}
