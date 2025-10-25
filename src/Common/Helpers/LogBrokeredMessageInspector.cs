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

using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.IO;
using System.Linq;
//using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

#endregion

namespace ServiceBusExplorer.Helpers
{
    using Enums;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text.Json;

    public class LogBrokeredMessageInspector : IBrokeredMessageInspector, IDisposable
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
        private const string LogFileNameFormat = "ServiceBusMessage {0}.txt";
        private const int MaxBufferSize = 262144; // 256 KB
        #endregion

        #region Private Instance Fields
        private readonly Task writeTask = Task.Run(
            () =>
            {
                messageCollection = new BlockingCollection<Tuple<MessageDirection, LogMessageWrapper>>(int.MaxValue);
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
        private static BlockingCollection<Tuple<MessageDirection, LogMessageWrapper>> messageCollection;
        private static StreamWriter writer;
        private static readonly string line = new string('-', 100);
        #endregion

        #region IServiceBusMessageInspector Methods
        public ServiceBusMessage BeforeSendMessage(ServiceBusMessage message)
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
        private static ServiceBusMessage LogMessage(MessageDirection direction, ServiceBusMessage message)
        {
            try
            {
                if (message != null)
                {
                    messageCollection.TryAdd(
                        new Tuple<MessageDirection, LogMessageWrapper>(
                            direction, 
                            LogMessageWrapper.Create(Clone(message))));
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
            return message;
        }

        private static ServiceBusReceivedMessage LogMessage(MessageDirection direction, ServiceBusReceivedMessage message)
        {
            try
            {
                if (message != null)
                {
                    messageCollection.TryAdd(
                        new Tuple<MessageDirection, LogMessageWrapper>(
                            direction, 
                            LogMessageWrapper.Create(Clone(message))));
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
            return message;
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
                                string.IsNullOrWhiteSpace(message.Label) ? NullValue : message.Label,
                                message.Size));
                            builder.AppendLine(MessagePayloadHeader);
                            var messageText = GetMessageText(message);
                            builder.AppendLine(string.Format(MessageTextFormat,
                                messageText.Contains('\n')
                                    ? messageText
                                    : messageText.Substring(0, Math.Min(messageText.Length, 128)) +
                                      (messageText.Length >= 128 ? "..." : "")));
                            if (message.Properties.Any())
                            {
                                builder.AppendLine(MessagePropertiesHeader);
                                foreach (var p in message.Properties)
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

        /// <summary>
        /// Reads the content of the ServiceBusMessage passed as argument.
        /// </summary>
        /// <param name="messageToRead">The ServiceBusMessage to read.</param>
        /// <returns>The content of the ServiceBusMessage.</returns>
        private static string GetMessageText(LogMessageWrapper messageToRead)
        {
            string messageText = string.Empty;

            try
            {
                using StreamReader reader = new(messageToRead.Body);
                messageText = reader.ReadToEnd(); //TODO: Max buffer size what if content too big used to be 256kb 
            }
            catch (Exception)
            {
                messageText = UnableToReadMessageBody;
            }

            return messageText;
        }
        #endregion

        private static T Clone<T>(T obj)
        {
            var raw = JsonSerializer.Serialize(obj);
            return JsonSerializer.Deserialize<T>(raw);
        }

        private class LogMessageWrapper
        {
            public string MessageId { get; private set; }
            public string SessionId { get; private set; }
            public string Label { get; private set; }
            public long Size { get; private set; }

            public Stream Body { get; private set; }
            public IReadOnlyDictionary<string, object> Properties { get; private set; }

            private LogMessageWrapper() { } 

            public static LogMessageWrapper Create(ServiceBusMessage message)
            {
                var props = new ReadOnlyDictionary<string, object>(message.ApplicationProperties);
                var size = GetSize(message.Body, props);
                return Create(message.MessageId, message.SessionId, message.Subject, size, message.Body, props);
            }

            public static LogMessageWrapper Create(ServiceBusReceivedMessage message)
            {
                var size = GetSize(message.Body, message.ApplicationProperties);
                return Create(message.MessageId, message.SessionId, message.Subject, size, message.Body, message.ApplicationProperties); 
            }

            private static LogMessageWrapper Create(
                string messageId,
                string sessionId, 
                string subject,
                long size, 
                BinaryData body, 
                IReadOnlyDictionary<string, object> properties)
            {
                return new()
                {
                    MessageId = messageId,
                    SessionId = sessionId,
                    Label = subject,
                    Size = size,
                    Body = body.ToStream(),
                    Properties = properties,
                };
            }

            private static long GetSize(BinaryData body, IReadOnlyDictionary<string, object> properties)
            {
                var bodySize = body.ToMemory().Length;

                int propertiesSize = 0;
                foreach (var prop in properties)
                {
                    propertiesSize += Encoding.UTF8.GetByteCount(prop.Key);
                    propertiesSize += Encoding.UTF8.GetByteCount(prop.Value?.ToString() ?? "");
                }

                return bodySize + propertiesSize;
            }
        }
    }
}
