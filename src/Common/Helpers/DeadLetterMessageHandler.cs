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
using System.Transactions;
using System.Threading.Tasks;
using ServiceBusExplorer.Utilities.Helpers;
using Azure.Messaging.ServiceBus;
using Microsoft.ServiceBus.Messaging;

#endregion

namespace ServiceBusExplorer.Helpers
{
    public class DeletedDlqMessagesResult
    {
        #region Public constructor
        public DeletedDlqMessagesResult(bool timedOut, List<long> deletedSequenceNumbers, string failure = null)
        {
            TimedOut = timedOut;
            Failure = failure;
            DeletedSequenceNumbers = deletedSequenceNumbers;
        }
        #endregion

        #region Public instance properties
        public bool TimedOut { get; }
        public string Failure { get; }
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
            if (sequenceNumbers == null || sequenceNumbers.Count == 0)
                return new DeletedDlqMessagesResult(false, new List<long>());
            if (sequenceNumbers.Any(number => !number.HasValue))
                throw new ArgumentException("Every selected message must have a broker sequence number.", nameof(sequenceNumbers));
            sequenceNumbers = sequenceNumbers.Distinct().ToList();
            var sequenceNumbersToDeleteList = new List<long?>();
            foreach (var number in sequenceNumbers)
            {
                sequenceNumbersToDeleteList.Add(number);
            }

            var maxTimeInSeconds = GetMaxOperationTimeInSeconds();
            if (maxTimeInSeconds < 1)
            {
                throw new LockDurationTooLowException();
            }

            string failure = null;
            var timedOut = false;
            var done = false;
            var lockedMessages = new Dictionary<long, ServiceBusReceivedMessage>(1000);
            var deletedSequenceNumbers = new List<long>();
            var stopwatch = Stopwatch.StartNew();
            var client = CreateClient(false);
            ServiceBusReceiver messageReceiver = null;
            try
            {
                messageReceiver = CreateReceiver(client, TransferDLQ);
                do
                {
                    var message = await messageReceiver.ReceiveMessageAsync(
                        ServiceBusHelper.GetModernReceiveWait(TimeSpan.FromSeconds(receiveTimeoutInSeconds))).ConfigureAwait(false);
                    if (message == null)
                    {
                        done = true;
                    }
                    else if (sequenceNumbersToDeleteList.Contains(message.SequenceNumber))
                    {
                        var sequenceNumber = message.SequenceNumber;
                        await messageReceiver.CompleteMessageAsync(message).ConfigureAwait(false);
                        sequenceNumbersToDeleteList.Remove(sequenceNumber);
                        deletedSequenceNumbers.Add(sequenceNumber);
                        done = sequenceNumbersToDeleteList.Count == 0;
                    }
                    else if (lockedMessages.ContainsKey(message.SequenceNumber))
                    {
                        timedOut = true;
                        done = true;
                    }
                    else
                    {
                        lockedMessages.Add(message.SequenceNumber, message);
                    }

                    if (stopwatch.ElapsedMilliseconds >= maxTimeInSeconds * 1000)
                    {
                        timedOut = true;
                        done = true;
                    }
                } while (!done);
            }
            catch (ServiceBusException exception) when (exception.Reason == ServiceBusFailureReason.MessageLockLost)
            {
                WriteToLog($"Got a message lock loss after {stopwatch.ElapsedMilliseconds / 1000} seconds.");
                timedOut = true;
            }
            catch (Exception exception) { failure = exception.Message; }
            finally
            {
                foreach (var message in lockedMessages.Values)
                {
                    await CleanupAsync(() => messageReceiver.AbandonMessageAsync(message)).ConfigureAwait(false);
                }
                await CleanupAsync(() => messageReceiver == null ? Task.CompletedTask : messageReceiver.DisposeAsync().AsTask()).ConfigureAwait(false);
                await CleanupAsync(() => client.DisposeAsync().AsTask()).ConfigureAwait(false);
                stopwatch.Stop();
            }

            return new DeletedDlqMessagesResult(timedOut, deletedSequenceNumbers, failure);
        }

    

        public async Task<DeletedDlqMessagesResult> MoveMessages(string targetEntityPath,
            List<long> sequenceNumbers, bool transferDLQ, List<BrokeredMessage> messagesToSend = null)
        {
            if (transferDLQ || sourceQueueDescription?.EnablePartitioning == true ||
                sourceSubscriptionWrapper?.TopicDescription?.EnablePartitioning == true)
                throw new NotSupportedException("Moving transfer dead-letter or partitioned-source messages is not supported safely by this stage. No messages were changed.");
            if (sequenceNumbers == null || sequenceNumbers.Count == 0)
                return new DeletedDlqMessagesResult(false, new List<long>());

            if (sequenceNumbers.Distinct().Count() != sequenceNumbers.Count)
                throw new ArgumentException("Select each broker sequence number only once.", nameof(sequenceNumbers));

            if (messagesToSend != null)
            {
                if (sequenceNumbers.Count != messagesToSend.Count)
                {
                    throw new ArgumentException("The number of items in the parameter sequenceNumbers must be the same as the number of " +
                        "items in the parameter messagesToSend", nameof(messagesToSend));
                }
            }

            string failure = null;
            var timedOut = false;
            var done = false;
            var lockedMessages = new List<ServiceBusReceivedMessage>(1000);
            var movedSequenceNumbers = new List<long>();
            var maxTimeInSeconds = GetMaxOperationTimeInSeconds();

            if (maxTimeInSeconds < 1)
            {
                throw new LockDurationTooLowException();
            }

            var stopwatch = Stopwatch.StartNew();
            var client = CreateClient(true);
            ServiceBusReceiver messageReceiver = null;
            ServiceBusSender messageSender = null;

            try
            {
                messageReceiver = CreateReceiver(client, transferDLQ);
                messageSender = client.CreateSender(targetEntityPath);
                do
                {
                    var message = await messageReceiver.ReceiveMessageAsync(
                        ServiceBusHelper.GetModernReceiveWait(TimeSpan.FromSeconds(receiveTimeoutInSeconds))).ConfigureAwait(false);

                    if (message != null)
                    {
                        if (sequenceNumbers.Contains(message.SequenceNumber))
                        {
                            try
                            {
                                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                                {
                                    if (messagesToSend == null)
                                    {
                                        var outbound = ServiceBusMessageAdapter.ToServiceBusMessageFromReceivedMessage(message);
                                        await messageSender.SendMessageAsync(outbound).ConfigureAwait(false);
                                    }
                                    else
                                    {
                                        var index = sequenceNumbers.IndexOf(message.SequenceNumber);
                                        var outbound = ServiceBusMessageAdapter.ToServiceBusMessage(messagesToSend[index]);
                                        await messageSender.SendMessageAsync(outbound).ConfigureAwait(false);
                                    }
                                    await messageReceiver.CompleteMessageAsync(message).ConfigureAwait(false);
                                    scope.Complete();
                                }
                            }
                            catch (Exception moveException)
                            {
                                try
                                {
                                    await messageReceiver.AbandonMessageAsync(message).ConfigureAwait(false);
                                }
                                catch (Exception abandonException)
                                {
                                    WriteToLog($"The move failed: {moveException.Message}. Releasing the source message also failed: {abandonException.Message}.");
                                }
                                throw;
                            }

                            movedSequenceNumbers.Add(message.SequenceNumber);
                            if (movedSequenceNumbers.Count >= sequenceNumbers.Count)
                            {
                                done = true;
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
            catch (ServiceBusException exception) when (exception.Reason == ServiceBusFailureReason.MessageLockLost)
            {
                WriteToLog($"Got a message lock loss after {stopwatch.ElapsedMilliseconds / 1000} seconds.");
                timedOut = true;
            }
            catch (Exception exception) { failure = exception.Message; }
            finally
            {
                foreach (var message in lockedMessages)
                {
                    await CleanupAsync(() => messageReceiver.AbandonMessageAsync(message)).ConfigureAwait(false);
                }

                await CleanupAsync(() => messageSender == null ? Task.CompletedTask : messageSender.DisposeAsync().AsTask()).ConfigureAwait(false);
                await CleanupAsync(() => messageReceiver == null ? Task.CompletedTask : messageReceiver.DisposeAsync().AsTask()).ConfigureAwait(false);
                await CleanupAsync(() => client.DisposeAsync().AsTask()).ConfigureAwait(false);
                stopwatch.Stop();
            }

            return new DeletedDlqMessagesResult(timedOut, movedSequenceNumbers, failure);
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

            if (result.Failure != null) return $"Only {result.DeletedSequenceNumbers.Count} of {targetMessageCount} messages were confirmed {verb}. The operation failed: {result.Failure}. Check broker state before retrying unconfirmed messages.";

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

        ServiceBusReceiver CreateReceiver(ServiceBusClient client, bool transferDeadLetterQueue)
        {
            var options = new ServiceBusReceiverOptions
            {
                ReceiveMode = ServiceBusReceiveMode.PeekLock,
                SubQueue = transferDeadLetterQueue ? SubQueue.TransferDeadLetter : SubQueue.DeadLetter
            };

            if (sourceQueueDescription != null)
            {
                return client.CreateReceiver(sourceQueueDescription.Path, options);
            }

            if (transferDeadLetterQueue)
            {
                throw new NotSupportedException("Transfer dead-letter queues are not supported for subscriptions.");
            }

            return client.CreateReceiver(
                sourceSubscriptionWrapper.SubscriptionDescription.TopicPath,
                sourceSubscriptionWrapper.SubscriptionDescription.Name,
                options);
        }

        protected virtual ServiceBusClient CreateClient(bool crossEntityTransactions) =>
            serviceBusHelper.CreateServiceBusClient(crossEntityTransactions);

        private async Task CleanupAsync(Func<Task> cleanup)
        {
            try { await cleanup().ConfigureAwait(false); }
            catch (Exception exception)
            {
                // A disposed UI logger must not prevent the remaining resources being released.
                try { WriteToLog("Dead-letter cleanup failed: " + exception.Message); }
                catch { }
            }
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
