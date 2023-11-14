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
using System.Collections.Concurrent;
using System.Globalization;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.ServiceBus.Messaging;

#endregion

namespace ServiceBusExplorer.Helpers
{
    using Azure.Messaging.ServiceBus;
    using Enums;

    public class LogReceivedMessageInspector : IReceivedMessageInspector, IDisposable
    {
        #region Private Constants
        //***************************
        // Constants
        //***************************
        private const string DateFormat = "<{0,2:00}:{1,2:00}:{2,2:00}> {3}";
        private const string MessageSuccessfullySent = "Message Sent. MessageId=[{0}] SessionId=[{1}] Label=[{2}] Size=[{3}]";
        private const string MessageSuccessfullyReceived = "Message Received. MessageId=[{0}] SessionId=[{1}] Label=[{2}] Size=[{3}]";
        private const string UnableToReadMessageBody = "Unable to read the message body.";
        private const string NullValue = "NULL";
        private const string MessagePropertiesHeader = "Properties:";
        private const string MessagePayloadHeader = "Payload:";
        private const string MessageTextFormat = "{0}";
        private const string MessagePropertyFormat = " - Key=[{0}] Value=[{1}]";
        private const string LogFileNameFormat = "ReceivedMessage {0}.txt";
        private const int MaxBufferSize = 262144; // 256 KB
        #endregion

        #region Private Instance Fields
        private readonly Task writeTask = Task.Run( 
            () =>
            {
                messageCollection = new BlockingCollection<Tuple<MessageDirection, ServiceBusReceivedMessage>>(int.MaxValue);
                writer = new StreamWriter(new FileStream(Path.Combine(Environment.CurrentDirectory,
                                                                      string.Format(LogFileNameFormat,
                                                                                    DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace('/', '-').Replace(':', '-'))),
                                                         FileMode.Append,
                                                         FileAccess.Write,
                                                         FileShare.ReadWrite));
                WriteToLog();
            });
        #endregion

        #region Private Static Fields
        private static BlockingCollection<Tuple<MessageDirection, ServiceBusReceivedMessage>> messageCollection;
        private static StreamWriter writer;
        private static readonly string line = new string('-', 100);
        #endregion

        #region IReceivedMessageInspector Methods
        public ServiceBusReceivedMessage BeforeSendMessage(ServiceBusReceivedMessage message)
        {
            return LogMessage(MessageDirection.Send, message);
        }

        public ServiceBusReceivedMessage AfterReceiveMessage(ServiceBusReceivedMessage message)
        {
            return LogMessage(MessageDirection.Receive, message);
        } 
        #endregion

        #region IDisposable Methods
        public void Dispose()
        {
            messageCollection.CompleteAdding();
            writeTask.Wait();
            writer.Dispose();
        }
        #endregion

        #region Private Static Methods
        private static ServiceBusReceivedMessage LogMessage(MessageDirection direction, ServiceBusReceivedMessage message)
        {
            try
            {
                if (message != null)
                {
                    messageCollection.TryAdd(new Tuple<MessageDirection, ServiceBusReceivedMessage>(direction, message));
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
            return message;
        }

        /// <summary>
        /// Reads the content of the ReceivedMessage passed as argument.
        /// </summary>
        /// <param name="messageToRead">The ReceivedMessage to read.</param>
        /// <returns>The content of the ReceivedMessage.</returns>
        private static string GetMessageText(ServiceBusReceivedMessage messageToRead)
        {
            string messageText;

            if (messageToRead?.Body == null)
            {
                return null;
            }

            try
            {
                messageText = messageToRead.Body.ToString();
            }
            catch (Exception)
            {
                messageText = UnableToReadMessageBody;
            }
            return messageText;
        }

        private async static void WriteToLog()
        {
            try
            {
                foreach (var tuple in messageCollection.GetConsumingEnumerable())
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(50));
                    if (tuple != null && tuple.Item2 != null)
                    {
                        try
                        {
                            var direction = tuple.Item1;
                            var message = tuple.Item2;
                            var now = DateTime.Now;
                            var builder = new StringBuilder();
                            builder.AppendLine(string.Format(DateFormat,
                                now.Hour,
                                now.Minute,
                                now.Second,
                                line));
                            builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                direction == MessageDirection.Send
                                    ? MessageSuccessfullySent
                                    : MessageSuccessfullyReceived,
                                string.IsNullOrWhiteSpace(message.MessageId) ? NullValue : message.MessageId,
                                string.IsNullOrWhiteSpace(message.SessionId) ? NullValue : message.SessionId,
                                /*string.IsNullOrWhiteSpace(message.Label) ? */NullValue/* : message.Label,
                                message.Size*/));
                            builder.AppendLine(MessagePayloadHeader);
                            var messageText = GetMessageText(message);
                            builder.AppendLine(string.Format(MessageTextFormat,
                                messageText.Contains('\n')
                                    ? messageText
                                    : messageText.Substring(0, Math.Min(messageText.Length, 128)) +
                                      (messageText.Length >= 128 ? "..." : "")));
                            if (message.ApplicationProperties.Any())
                            {
                                builder.AppendLine(MessagePropertiesHeader);
                                foreach (var p in message.ApplicationProperties)
                                {
                                    builder.AppendLine(string.Format(MessagePropertyFormat,
                                        p.Key,
                                        p.Value));
                                }
                            }
                            if (writer != null)
                            {
                                await writer.WriteAsync(builder.ToString());
                                await writer.FlushAsync();
                            }
                            else
                            {
                                break;
                            }
                        }
                        // ReSharper disable once EmptyGeneralCatchClause
                        catch
                        {
                        }
                    }
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }
            finally
            {
                messageCollection.Dispose();
            }
        }
        #endregion
    }
}
