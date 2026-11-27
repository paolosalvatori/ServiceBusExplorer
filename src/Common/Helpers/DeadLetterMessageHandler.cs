using QueueDescription = ServiceBusExplorer.Helpers.QueueInfo;
using SubscriptionDescription = ServiceBusExplorer.Helpers.SubscriptionInfo;
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
        public async Task<DeletedDlqMessagesResult> DeleteMessages(List<long?> sequenceNumbers,
            bool TransferDLQ)
        {
            var sequenceNumbersToDeleteList = new List<long?>();
            foreach (var number in sequenceNumbers)
            {
                sequenceNumbersToDeleteList.Add(number);
            }

            var timedOut = false;
            var dlqEntityPath = TransferDLQ ? GetTransferDlqEntityPath() : GetDlqEntityPath();

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
            List<long> sequenceNumbers, bool transferDLQ, List<BrokeredMessage> messagesToSend = null)
        {
            if (messagesToSend != null)
            {
                if (sequenceNumbers.Count != messagesToSend.Count)
                {
                    throw new ArgumentException("The number of items in the parameter sequenceNumbers must be the same as the number of " +
                        "items in the parameter messagesToSend", nameof(messagesToSend));
                }
            }

            try
            {
                return await MoveMessagesCore(sequenceNumbers, transferDLQ,
                    (index, locked) => messageSender.SendAsync(messagesToSend == null ? locked : messagesToSend[index])).ConfigureAwait(false);
            }
            finally
            {
                await messageSender.CloseAsync().ConfigureAwait(false);
            }
        }

        public Task<DeletedDlqMessagesResult> MovePreparedMessages(List<long> sequenceNumbers, bool transferDLQ,
            IReadOnlyList<BrokeredMessage> messages, Func<Azure.Messaging.ServiceBus.ServiceBusMessage, Task> send)
        {
            if (send == null) throw new ArgumentNullException(nameof(send));
            if (messages == null || messages.Count != sequenceNumbers.Count)
                throw new ArgumentException("Provide one prepared message for each source sequence number.", nameof(messages));

            // Convert the entire selection before taking a lock or sending the first replacement.
            var prepared = messages.Select(ServiceBusMessageAdapter.ToReplayMessage).ToArray();
            return MoveMessagesCore(sequenceNumbers, transferDLQ, (index, _) => send(prepared[index]));
        }

        private async Task<DeletedDlqMessagesResult> MoveMessagesCore(List<long> sequenceNumbers, bool transferDLQ, Func<int, BrokeredMessage, Task> send)
        {
            // Reject duplicate source rows and pin each prepared message to its source before acquiring a lock.
            var indices = sequenceNumbers.Select((number, index) => new { number, index }).ToDictionary(item => item.number, item => item.index);
            var timedOut = false;
            var done = false;
            var lockedMessages = new List<BrokeredMessage>(1000);
            var movedSequenceNumbers = new List<long>();
            var maxTimeInSeconds = GetMaxOperationTimeInSeconds();

            if (maxTimeInSeconds < 1)
            {
                throw new LockDurationTooLowException();
            }

            var dlqEntityPath = transferDLQ ? GetTransferDlqEntityPath() : GetDlqEntityPath();

            var messageReceiver = serviceBusHelper.MessagingFactory.CreateMessageReceiver(
                dlqEntityPath,
                ReceiveMode.PeekLock);

            var stopwatch = Stopwatch.StartNew();

            try
            {
                do
                {
                    var message = await messageReceiver.ReceiveAsync(TimeSpan.FromSeconds(receiveTimeoutInSeconds)).ConfigureAwait(false);

                    if (message != null)
                    {
                        if (indices.TryGetValue(message.SequenceNumber, out var index))
                        {
                            using (message)
                            {
                                var sequence = message.SequenceNumber;
                                await SendAndSettleAsync(() => send(index, message), () => message.CompleteAsync(),
                                    () => message.AbandonAsync()).ConfigureAwait(false);
                                indices.Remove(sequence);
                                movedSequenceNumbers.Add(sequence);
                                done = indices.Count == 0;
                            }
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
                try
                {
                    foreach (var message in lockedMessages)
                    {
                        using (message) await message.AbandonAsync().ConfigureAwait(false);
                    }
                }
                finally
                {
                    await messageReceiver.CloseAsync().ConfigureAwait(false);
                }
                stopwatch.Stop();
            }

            return new DeletedDlqMessagesResult(timedOut, movedSequenceNumbers);
        }

        internal static async Task SendAndSettleAsync(Func<Task> send, Func<Task> complete, Func<Task> abandon)
        {
            try
            {
                await send().ConfigureAwait(false);
                await complete().ConfigureAwait(false);
            }
            catch (Exception failure)
            {
                try
                {
                    await abandon().ConfigureAwait(false);
                }
                catch (MessageLockLostException)
                {
                    // The expired lock already released the source. Preserve the delivery failure.
                }
                catch (Exception abandonFailure)
                {
                    throw new AggregateException("Delivery or settlement failed and the source lock could not be released.", failure, abandonFailure);
                }
                throw;
            }
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

        string GetTransferDlqEntityPath()
        {
            if (sourceQueueDescription != null)
            {
                return QueueClient.FormatTransferDeadLetterPath(sourceQueueDescription.Path);
            }

            throw new Exception("It is currently not supported getting a Transfer Dead-letter queue for a subscription.");
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
