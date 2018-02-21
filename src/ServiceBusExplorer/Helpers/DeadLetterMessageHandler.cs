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

using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Transactions;

using System.Linq;

#endregion

namespace Microsoft.Azure.ServiceBusExplorer.Helpers
{
    public class DeleteDlqMessagesResult
    {
        #region Public constructor
        public DeleteDlqMessagesResult(bool timedOut, List<long> deletedSequenceNumbers)
        {
            this.TimedOut = timedOut;
            this.DeletedSequenceNumbers = deletedSequenceNumbers;
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
        private readonly QueueDescription targetQueueDescription;
        private readonly SubscriptionWrapper subscriptionWrapper;
        private readonly ServiceBusHelper serviceBusHelper;
        readonly WriteToLogDelegate writeToLog;
        #endregion

        #region Public Constructors
        public DeadLetterMessageHandler(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper, QueueDescription queueDescription)
        {
            this.writeToLog = writeToLog;
            this.serviceBusHelper = serviceBusHelper;
            this.targetQueueDescription = queueDescription;
        }

        public DeadLetterMessageHandler(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper, SubscriptionWrapper subscriptionWrapper)
        {
            this.writeToLog = writeToLog;
            this.serviceBusHelper = serviceBusHelper;
            this.subscriptionWrapper = subscriptionWrapper;
        }
        #endregion

        #region Public methods
        public async Task<DeleteDlqMessagesResult> DeleteMessages(List<long> sequenceNumbersToDelete, int receiveTimeout)
        {
            var sequenceNumbersToDeleteList = new List<long>();
            foreach (var number in sequenceNumbersToDelete)
            {
                sequenceNumbersToDeleteList.Add(number);
            }

            var timedOut = false;

            var messageReceiver = await serviceBusHelper.MessagingFactory.CreateMessageReceiverAsync(
                QueueClient.FormatDeadLetterPath(targetQueueDescription.Path),
                ReceiveMode.PeekLock).ConfigureAwait(false);

            var done = false;
            var lockedMessages = new Dictionary<long, BrokeredMessage>(1000);
            var deletedSequenceNumbers = new List<long>();
            var maxTimeInSeconds = (int)targetQueueDescription.LockDuration.TotalSeconds - 3; // Allocate three seconds for final operations;

            if (maxTimeInSeconds < 1)
            {
                throw new LockDurationTooLowException();
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                do
                {
                    var message = await messageReceiver.ReceiveAsync(TimeSpan.FromSeconds(receiveTimeout))
                        .ConfigureAwait(false);

                    if (message != null)
                    {
                        if (sequenceNumbersToDelete.Contains(message.SequenceNumber))
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

            return new DeleteDlqMessagesResult(timedOut, deletedSequenceNumbers);
        }

        public async void MoveMessageFromDLQ(MessageSender messageSender, List<long> sequenceNumbers, int receiveTimeout,
           BrokeredMessage messageToSend = null) //async Task 
        {
            var messageReceiver = serviceBusHelper.MessagingFactory.CreateMessageReceiver(
                QueueClient.FormatDeadLetterPath(targetQueueDescription.Path),
                ReceiveMode.PeekLock);

            var done = false;
            var lockedMessages = new List<BrokeredMessage>(1000);
            var sentMessagesCount = 0;

            try
            {
                do
                {
                    var message = messageReceiver.Receive(TimeSpan.FromSeconds(receiveTimeout));

                    if (message != null)
                    {
                        if (sequenceNumbers.Contains(message.SequenceNumber))
                        {
                            using (var scope = new TransactionScope())
                            {
                                message.Complete();

                                // Send message
                                if (messageToSend == null)
                                {
                                    // Send the message
                                    await SendMessage(messageSender, message).ConfigureAwait(false);
                                }
                                else
                                {
                                    await SendMessage(messageSender, messageToSend).ConfigureAwait(false);
                                }

                                ++sentMessagesCount;
                                if (sentMessagesCount >= sequenceNumbers.Count)
                                {
                                    done = true;
                                }

                                scope.Complete();
                                message.Dispose();
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

                } while (!done);
            }
            finally
            {
                foreach (var message in lockedMessages)
                {
                    message.Abandon();
                    message.Dispose();
                }

                messageSender.Close();
                messageReceiver.Close();
            }
        }


        #endregion

        #region Private methods
        async Task SendMessage(MessageSender messageSender, BrokeredMessage message)
        {
            await messageSender.SendAsync(message).ConfigureAwait(false);
        }

        void WriteToLog(string message)
        {
            if (writeToLog != null &&
                !string.IsNullOrWhiteSpace(message))
            {
                writeToLog(message);
            }
        }
        #endregion
    }
}
