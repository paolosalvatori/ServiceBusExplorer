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
using Azure.Messaging.ServiceBus;
using Common.Contracts;
using Common.Models;

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
        // Either QueueMetadata or subscriptionWrapper is used - but never both.
        readonly QueueMetadata sourceQueueMetadata;
        //readonly SubscriptionWrapper sourceSubscriptionWrapper; TODO: 
        readonly int receiveTimeoutInSeconds;
        readonly IServiceBusService _serviceBusHelper;
        readonly WriteToLogDelegate writeToLog;
        #endregion

        #region Public Constructors
        public DeadLetterMessageHandler(WriteToLogDelegate writeToLog, IServiceBusService serviceBusHelper,
            int receiveTimeoutInSeconds, QueueMetadata QueueMetadata)
            : this(writeToLog, serviceBusHelper, receiveTimeoutInSeconds)
        {
            sourceQueueMetadata = QueueMetadata;
        }

        //public DeadLetterMessageHandler(WriteToLogDelegate writeToLog, IServiceBusService serviceBusHelper,
        //    int receiveTimeoutInSeconds /*SubscriptionWrapper subscriptionWrapper*/) //TODO: 
        //     : this(writeToLog, serviceBusHelper, receiveTimeoutInSeconds)
        //{
        //    //sourceSubscriptionWrapper = subscriptionWrapper;
        //}
        #endregion

        #region Private Constructor
        DeadLetterMessageHandler(WriteToLogDelegate writeToLog, IServiceBusService serviceBusHelper,
            int receiveTimeoutInSeconds)
        {
            this.writeToLog = writeToLog;
            this._serviceBusHelper = serviceBusHelper;
            this.receiveTimeoutInSeconds = receiveTimeoutInSeconds;
        }
        #endregion

        #region Public methods
        public async Task<DeletedDlqMessagesResult> DeleteMessages(List<long?> sequenceNumbers,
            bool TransferDLQ)
        {
            var sequenceNumbersToDeleteList = new List<long?>();
            foreach (var number in sequenceNumbers)
            {
                sequenceNumbersToDeleteList.Add(number);
            }

            var timedOut = false;
            //var dlqEntityPath = TransferDLQ ? GetTransferDlqEntityPath() : GetDlqEntityPath(); //TODO: 
            var dlqEntityPath = "queue-name"; 

            var messageReceiver = _serviceBusHelper.CreateReceiver(
                dlqEntityPath,
                ServiceBusReceiveMode.PeekLock);

            var done = false;
            var lockedMessages = new Dictionary<long, ServiceBusReceivedMessage>(1000);
            var deletedSequenceNumbers = new List<long>();
            //var maxTimeInSeconds = GetMaxOperationTimeInSeconds(); TODO: 
            var maxTimeInSeconds = 180; 

            if (maxTimeInSeconds < 1)
            {
                throw new LockDurationTooLowException();
            }

            var stopwatch = Stopwatch.StartNew();

            try
            {
                do
                {
                    var message = await messageReceiver.ReceiveMessageAsync(TimeSpan.FromSeconds(receiveTimeoutInSeconds))
                        .ConfigureAwait(false);

                    if (message != null)
                    {
                        if (sequenceNumbers.Contains(message.SequenceNumber))
                        {
                            var sequenceNumber = message.SequenceNumber;
                            await messageReceiver.CompleteMessageAsync(message).ConfigureAwait(false);
                            await messageReceiver.DisposeAsync().ConfigureAwait(false);
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
            catch (ServiceBusException ex) when (ex.Reason == ServiceBusFailureReason.MessageLockLost)
            {
                WriteToLog($"Got a MessageLockLostException after {stopwatch.ElapsedMilliseconds / 1000} seconds.");
                timedOut = true;
            }
            finally
            {
                foreach (var (_, message) in lockedMessages)
                {
                    await messageReceiver.AbandonMessageAsync(message).ConfigureAwait(false);
                }

                await messageReceiver.CloseAsync().ConfigureAwait(false);
                stopwatch.Stop();
            }

            return new DeletedDlqMessagesResult(timedOut, deletedSequenceNumbers);
        }

        public async Task<DeletedDlqMessagesResult> MoveMessages(ServiceBusSender messageSender,
            List<long> sequenceNumbers, bool transferDLQ, List<ServiceBusMessage> messagesToSend = null)
        {
            if (messagesToSend != null)
            {
                if (sequenceNumbers.Count != messagesToSend.Count)
                {
                    throw new ArgumentException("The number of items in the parameter sequenceNumbers must be the same as the number of " +
                        "items in the parameter messagesToSend", nameof(messagesToSend));
                }
            }

            //var dlqEntityPath = transferDLQ ? GetTransferDlqEntityPath() : GetDlqEntityPath(); //TODO: 
            var dlqEntityPath = "queue-name"; 

            var messageReceiver = _serviceBusHelper.CreateReceiver(
                dlqEntityPath,
                ServiceBusReceiveMode.PeekLock);

            var timedOut = false;
            var done = false;
            var lockedMessages = new List<ServiceBusReceivedMessage>(1000);
            var movedSequenceNumbers = new List<long>();
            //var maxTimeInSeconds = GetMaxOperationTimeInSeconds(); TODO: 
            var maxTimeInSeconds = 180;

            if (maxTimeInSeconds < 1)
            {
                throw new LockDurationTooLowException();
            }

            var stopwatch = Stopwatch.StartNew();

            try
            {
                do
                {
                    var message = await messageReceiver.ReceiveMessageAsync(TimeSpan.FromSeconds(receiveTimeoutInSeconds)).ConfigureAwait(false);

                    if (message != null)
                    {
                        if (sequenceNumbers.Contains(message.SequenceNumber))
                        {
                            try
                            {
                                if (messagesToSend == null)
                                {
                                    var toSend = _serviceBusHelper.ReceivedToMessage(message);
                                    await messageSender.SendMessageAsync(toSend).ConfigureAwait(false);
                                }
                                else
                                {
                                    var index = sequenceNumbers.IndexOf(message.SequenceNumber);
                                    await messageSender.SendMessageAsync(messagesToSend[index])
                                        .ConfigureAwait(false);
                                }

                                await messageReceiver.CompleteMessageAsync(message).ConfigureAwait(false);
                            }
                            catch
                            {
                                await messageReceiver.AbandonMessageAsync(message).ConfigureAwait(false);
                                throw;
                            }

                            movedSequenceNumbers.Add(message.SequenceNumber);
                            if (movedSequenceNumbers.Count >= sequenceNumbers.Count)
                            {
                                done = true;
                            }

                            await messageReceiver.DisposeAsync().ConfigureAwait(false);
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
            catch (ServiceBusException ex) when (ex.Reason == ServiceBusFailureReason.MessageLockLost)
            {
                WriteToLog($"Got a MessageLockLostException after {stopwatch.ElapsedMilliseconds / 1000} seconds.");
                timedOut = true;
            }
            finally
            {
                foreach (var message in lockedMessages)
                {
                    await messageReceiver.AbandonMessageAsync(message).ConfigureAwait(false);
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
                     $"The Lock duration for the queue is currently {100000/* */}" +
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
        //private double GetLockDurationInSeconds() TODO: 
        //{
        //    if (sourceQueueMetadata != null)
        //    {
        //        return sourceQueueMetadata.LockDuration.TotalSeconds;
        //    }

        //    return sourceSubscriptionWrapper.SubscriptionDescription.LockDuration.TotalSeconds;
        //}

        //private int GetMaxOperationTimeInSeconds()
        //{
        //    // Allocate three seconds for final operations;
        //    const int FinalActionsTime = 3;

        //    if (sourceQueueMetadata != null)
        //    {
        //        return (int)sourceQueueMetadata.LockDuration.TotalSeconds - FinalActionsTime;
        //    }

        //    return (int)sourceSubscriptionWrapper.SubscriptionDescription.LockDuration.TotalSeconds
        //        - FinalActionsTime;
        //}

        //string GetDlqEntityPath()
        //{
        //    if (sourceQueueMetadata != null)
        //    {
        //        return QueueClient.FormatDeadLetterPath(sourceQueueMetadata.Path);
        //    }

        //    return SubscriptionClient.FormatDeadLetterPath(
        //        sourceSubscriptionWrapper.SubscriptionDescription.TopicPath,
        //        sourceSubscriptionWrapper.SubscriptionDescription.Name);
        //}

        //string GetTransferDlqEntityPath()
        //{
        //    if (sourceQueueMetadata != null)
        //    {
        //        return QueueClient.FormatTransferDeadLetterPath(sourceQueueMetadata.Path);
        //    }

        //    throw new Exception("It is currently not supported getting a Transfer Dead-letter queue for a subscription.");
        //}

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
