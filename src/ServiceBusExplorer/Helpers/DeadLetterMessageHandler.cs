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
        private readonly QueueDescription queueDescription;
        private readonly SubscriptionWrapper subscriptionWrapper;
        private readonly ServiceBusHelper serviceBusHelper;
        readonly WriteToLogDelegate writeToLog;
        #endregion

        #region Public Constructors
        public DeadLetterMessageHandler(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper, QueueDescription queueDescription)
        {
            this.writeToLog = writeToLog;
            this.serviceBusHelper = serviceBusHelper;
            this.queueDescription = queueDescription;
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
            var timedOut = false;

            var messageReceiver = await serviceBusHelper.MessagingFactory.CreateMessageReceiverAsync(
                QueueClient.FormatDeadLetterPath(queueDescription.Path),
                ReceiveMode.PeekLock).ConfigureAwait(false);

            var done = false;
            var lockedMessages = new Dictionary<long, BrokeredMessage>(1000);
            var deletedSequenceNumbers = new List<long>(sequenceNumbersToDelete.Count);
            var maxTimeInSeconds = (int)queueDescription.LockDuration.TotalSeconds - 3; // Allocate three seconds final operations;

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
                            sequenceNumbersToDelete.Remove(sequenceNumber);
                            deletedSequenceNumbers.Add(sequenceNumber);

                            if (sequenceNumbersToDelete.Count == 0)
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
        #endregion

        #region Private methods
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
