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
    public class DeletedDlqMessagesResult
    {
        #region Public constructor
        public DeletedDlqMessagesResult(bool timedOut, List<long> deletedSequenceNumbers)
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
        readonly QueueDescription sourceQueueDescription;
        readonly SubscriptionWrapper subscriptionWrapper;
        readonly ServiceBusHelper serviceBusHelper;
        readonly WriteToLogDelegate writeToLog;
        #endregion

        #region Public Constructors
        public DeadLetterMessageHandler(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper, QueueDescription queueDescription)
        {
            this.writeToLog = writeToLog;
            this.serviceBusHelper = serviceBusHelper;
            this.sourceQueueDescription = queueDescription;
        }

        public DeadLetterMessageHandler(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper, SubscriptionWrapper subscriptionWrapper)
        {
            this.writeToLog = writeToLog;
            this.serviceBusHelper = serviceBusHelper;
            this.subscriptionWrapper = subscriptionWrapper;
        }
        #endregion

        #region Public methods
        public async Task<DeletedDlqMessagesResult> DeleteMessages(int receiveTimeout, List<long> sequenceNumbers)
        {
            var sequenceNumbersToDeleteList = new List<long>();
            foreach (var number in sequenceNumbers)
            {
                sequenceNumbersToDeleteList.Add(number);
            }

            var timedOut = false;

            var messageReceiver = await serviceBusHelper.MessagingFactory.CreateMessageReceiverAsync(
                QueueClient.FormatDeadLetterPath(sourceQueueDescription.Path),
                ReceiveMode.PeekLock).ConfigureAwait(false);

            var done = false;
            var lockedMessages = new Dictionary<long, BrokeredMessage>(1000);
            var deletedSequenceNumbers = new List<long>();
            var maxTimeInSeconds = (int)sourceQueueDescription.LockDuration.TotalSeconds - 3; // Allocate three seconds for final operations;

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

        public async Task<DeletedDlqMessagesResult> MoveMessages(MessageSender messageSender, int receiveTimeout,
            List<long> sequenceNumbers, List<BrokeredMessage> messagesToSend = null)
        {
            if (messagesToSend != null)
            {
                if (sequenceNumbers.Count != messagesToSend.Count)
                {
                    throw new ArgumentException("The number of items in the parameter sequenceNumbers must be the same as the number of " +
                        "items in the parameter messagesToSend", "messagesToSend");
                }
            }

            var messageReceiver = serviceBusHelper.MessagingFactory.CreateMessageReceiver(
                QueueClient.FormatDeadLetterPath(sourceQueueDescription.Path),
                ReceiveMode.PeekLock);

            var timedOut = false;
            var done = false;
            var lockedMessages = new List<BrokeredMessage>(1000);
            var movedSequenceNumbers = new List<long>();

            var maxTimeInSeconds = (int)sourceQueueDescription.LockDuration.TotalSeconds - 3; // Allocate three seconds for final operations;

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
                    var message = await messageReceiver.ReceiveAsync(TimeSpan.FromSeconds(receiveTimeout));

                    if (message != null)
                    {
                        if (sequenceNumbers.Contains(message.SequenceNumber))
                        {
                            //using (var scope = new TransactionScope())  // Stay on the thread during the transaction
                            //{

                            try
                            {
                                if (messagesToSend == null)
                                {
                                    await messageSender.SendAsync(message).ConfigureAwait(false);
                                }
                                else
                                {
                                    var index = sequenceNumbers.IndexOf(message.SequenceNumber);
                                    await messageSender.SendAsync(messagesToSend[index]).ConfigureAwait(false);
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

                            //scope.Complete();
                            message.Dispose();
                            //}
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
                    await message.AbandonAsync();
                    message.Dispose();
                }

                await messageSender.CloseAsync();
                await messageReceiver.CloseAsync();

                stopwatch.Stop();
            }

            return new DeletedDlqMessagesResult(timedOut, movedSequenceNumbers);
        }


        #endregion

        #region Private methods
        //async Task SendMessage(MessageSender messageSender, BrokeredMessage message)
        //{
        //    await messageSender.SendAsync(message).ConfigureAwait(false);
        //}

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
