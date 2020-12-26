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
    public enum EventDataDirection
    {
        Send,
        Receive
    }

    public class LogEventDataInspector : IEventDataInspector, IDisposable
    {
        #region Private Constants
        //***************************
        // Constants
        //***************************
        private const string DateFormat = "<{0,2:00}:{1,2:00}:{2,2:00}> {3}";
        private const string EventDataSuccessfullySent = "EventData Sent. PartitionKey=[{0}]";
        private const string EventDataSuccessfullyReceived = "EventData Received. PartitionKey=[{0}]";
        private const string UnableToReadMessageBody = "Unable to read the message body.";
        private const string NullValue = "NULL";
        private const string EventDataPropertiesHeader = "Properties:";
        private const string EventDataPayloadHeader = "Payload:";
        private const string EventDataTextFormat = "{0}";
        private const string EventDataPropertyFormat = " - Key=[{0}] Value=[{1}]";
        private const string LogFileNameFormat = "EventData {0}.txt";
        private const int MaxBufferSize = 262144; // 256 KB
        #endregion

        #region Private Instance Fields
        private readonly Task writeTask = Task.Run( 
            () =>
            {
                messageCollection = new BlockingCollection<Tuple<EventDataDirection, EventData>>(int.MaxValue);
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
        private static BlockingCollection<Tuple<EventDataDirection, EventData>> messageCollection;
        private static StreamWriter writer;
        private static readonly string line = new string('-', 100);
        #endregion

        #region IEventDataInspector Methods
        public EventData BeforeSendMessage(EventData eventData)
        {
            return LogEventData(EventDataDirection.Send, eventData);
        }

        public EventData AfterReceiveMessage(EventData eventData)
        {
            return LogEventData(EventDataDirection.Receive, eventData);
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
        private static EventData LogEventData(EventDataDirection direction, EventData eventData)
        {
            try
            {
                if (eventData != null)
                {
                    messageCollection.TryAdd(new Tuple<EventDataDirection, EventData>(direction, eventData.Clone()));
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
            return eventData;
        }

        /// <summary>
        /// Reads the content of the EventData passed as argument.
        /// </summary>
        /// <param name="eventDataToRead">The EventData to read.</param>
        /// <returns>The content of the EventData.</returns>
        public static string GetMessageText(EventData eventDataToRead)
        {
            string eventDataText = null;
            Stream stream = null;
            if (eventDataToRead == null)
            {
                return null;
            }
            var inboundMessage = eventDataToRead.Clone();
            try
            {
                stream = inboundMessage.GetBodyStream();
                if (stream != null)
                {
                    var element = new BinaryMessageEncodingBindingElement
                    {
                        ReaderQuotas = new XmlDictionaryReaderQuotas
                        {
                            MaxArrayLength = int.MaxValue,
                            MaxBytesPerRead = int.MaxValue,
                            MaxDepth = int.MaxValue,
                            MaxNameTableCharCount = int.MaxValue,
                            MaxStringContentLength = int.MaxValue
                        }
                    };
                    var encoderFactory = element.CreateMessageEncoderFactory();
                    var encoder = encoderFactory.Encoder;
                    var stringBuilder = new StringBuilder();
                    var eventData = encoder.ReadMessage(stream, MaxBufferSize);
                    using (var reader = eventData.GetReaderAtBodyContents())
                    {
                        // The XmlWriter is used just to indent the XML eventData
                        var settings = new XmlWriterSettings { Indent = true };
                        using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
                        {
                            xmlWriter.WriteNode(reader, true);
                        }
                    }
                    eventDataText = stringBuilder.ToString();
                }
            }
            catch (Exception)
            {
                inboundMessage = eventDataToRead.Clone();
                try
                {
                    stream = inboundMessage.GetBodyStream();
                    if (stream != null)
                    {
                        var element = new BinaryMessageEncodingBindingElement
                        {
                            ReaderQuotas = new XmlDictionaryReaderQuotas
                            {
                                MaxArrayLength = int.MaxValue,
                                MaxBytesPerRead = int.MaxValue,
                                MaxDepth = int.MaxValue,
                                MaxNameTableCharCount = int.MaxValue,
                                MaxStringContentLength = int.MaxValue
                            }
                        };
                        var encoderFactory = element.CreateMessageEncoderFactory();
                        var encoder = encoderFactory.Encoder;
                        var eventData = encoder.ReadMessage(stream, MaxBufferSize);
                        using (var reader = eventData.GetReaderAtBodyContents())
                        {
                            eventDataText = reader.ReadString();
                        }
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        if (stream != null)
                        {
                            try
                            {
                                stream.Seek(0, SeekOrigin.Begin);
                                var serializer = new CustomDataContractBinarySerializer(typeof(string));
                                eventDataText = serializer.ReadObject(stream) as string;
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    stream.Seek(0, SeekOrigin.Begin);
                                    using (var reader = new StreamReader(stream))
                                    {
                                        eventDataText = reader.ReadToEnd();
                                        if (eventDataText.ToCharArray().GroupBy(c => c).
                                            Where(g => char.IsControl(g.Key) && g.Key != '\t' && g.Key != '\n' && g.Key != '\r').
                                            Select(g => g.First()).Any())
                                        {
                                            stream.Seek(0, SeekOrigin.Begin);
                                            using (var binaryReader = new BinaryReader(stream))
                                            {
                                                var bytes = binaryReader.ReadBytes((int)stream.Length);
                                                eventDataText = BitConverter.ToString(bytes).Replace('-', ' ');
                                            }
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    eventDataText = UnableToReadMessageBody;
                                }
                            }
                        }
                        else
                        {
                            eventDataText = UnableToReadMessageBody;
                        }
                    }
                    catch (Exception)
                    {
                        eventDataText = UnableToReadMessageBody;
                    }
                }
            }
            return eventDataText;
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
                            var eventData = tuple.Item2;
                            var now = DateTime.Now;
                            var builder = new StringBuilder();
                            builder.AppendLine(string.Format(DateFormat,
                                now.Hour,
                                now.Minute,
                                now.Second,
                                line));
                            builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                direction == EventDataDirection.Send
                                    ? EventDataSuccessfullySent
                                    : EventDataSuccessfullyReceived,
                                string.IsNullOrWhiteSpace(eventData.PartitionKey) ? NullValue : eventData.PartitionKey));
                            builder.AppendLine(EventDataPayloadHeader);
                            var messageText = GetMessageText(eventData);
                            builder.AppendLine(string.Format(EventDataTextFormat,
                                messageText.Contains('\n')
                                    ? messageText
                                    : messageText.Substring(0, Math.Min(messageText.Length, 128)) +
                                      (messageText.Length >= 128 ? "..." : "")));
                            if (eventData.Properties.Any())
                            {
                                builder.AppendLine(EventDataPropertiesHeader);
                                foreach (var p in eventData.Properties)
                                {
                                    builder.AppendLine(string.Format(EventDataPropertyFormat,
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
