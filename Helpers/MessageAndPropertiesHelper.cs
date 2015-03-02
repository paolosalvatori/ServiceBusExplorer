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
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public static class MessageAndPropertiesHelper
    {
        #region Private Constants
        //***************************
        // Constants
        //***************************
        private const string MessageFileName = "Message.xml";
        private const string RelayMessageFileName = "RelayMessage.xml";
        private const string PropertiesFileName = "Properties.xml";
        private const string Namespace = @"http://schemas.microsoft.com/servicebusexplorer";
        private const string Message = "Message";
        private const string Date = "Date";
        private const string Content = "Content";
        private const string Properties = "Properties";
        private const string Property = "Property";
        private const string Key = "Key";
        private const string Type = "Type";
        private const string Value = "Value";
        #endregion

        #region Private Static Fields
        private static readonly string messageFilePath = Path.Combine(Environment.CurrentDirectory, MessageFileName);
        private static readonly string relayMessageFilePath = Path.Combine(Environment.CurrentDirectory, RelayMessageFileName);
        private static readonly string propertiesFilePath = Path.Combine(Environment.CurrentDirectory, PropertiesFileName);
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Write a message to an XML file in the current directory.
        /// </summary>
        /// <param name="message">The message to save.</param>
        public static void WriteMessage(string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    return;
                }
                using (var memoryStream = new MemoryStream())
                {
                    using (var stringWriter = new StreamWriter(memoryStream, Encoding.ASCII))
                    {
                        var settings = new XmlWriterSettings { Indent = true };
                        using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                        {
                            xmlWriter.WriteStartElement(Message, Namespace);
                            xmlWriter.WriteStartElement(Date, Namespace);
                            var now = DateTime.Now;
                            xmlWriter.WriteString(now.ToLongDateString() + " " + now.ToLongTimeString());
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteStartElement(Content, Namespace);
                            xmlWriter.WriteCData(message);
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteEndElement();
                        }
                    }
                    var xml = Encoding.UTF8.GetString(memoryStream.ToArray());
                    WriteFile(messageFilePath, xml);
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Write a relay message to an XML file in the current directory.
        /// </summary>
        /// <param name="message">The message to save.</param>
        public static void WriteRelayMessage(string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    return;
                }
                using (var memoryStream = new MemoryStream())
                {
                    using (var stringWriter = new StreamWriter(memoryStream, Encoding.ASCII))
                    {
                        var settings = new XmlWriterSettings { Indent = true };
                        using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                        {
                            xmlWriter.WriteStartElement(Message, Namespace);
                            xmlWriter.WriteStartElement(Date, Namespace);
                            var now = DateTime.Now;
                            xmlWriter.WriteString(now.ToLongDateString() + " " + now.ToLongTimeString());
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteStartElement(Content, Namespace);
                            xmlWriter.WriteCData(message);
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteEndElement();
                        }
                    }
                    var xml = Encoding.UTF8.GetString(memoryStream.ToArray());
                    WriteFile(relayMessageFilePath, xml);
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Reads a message from an XML file in the current directory.
        /// </summary>
        /// <returns>The message read from the XML file.</returns>
        public static string ReadMessage()
        {
            try
            {
                if (!File.Exists(messageFilePath))
                {
                    return null;
                }

                using (var reader = new StreamReader(messageFilePath))
                {
                    using (var xmlReader = XmlReader.Create(reader))
                    {
                        var root = XElement.Load(xmlReader);
                        var cdata = root.DescendantNodes().OfType<XCData>().FirstOrDefault();
                        if (cdata != null)
                        {
                            return cdata.Value;
                        }
                    }
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
            return null;
        }

        /// <summary>
        /// Reads a relay message from an XML file in the current directory.
        /// </summary>
        /// <returns>The message read from the XML file.</returns>
        public static string ReadRelayMessage()
        {
            try
            {
                if (!File.Exists(relayMessageFilePath))
                {
                    return null;
                }

                using (var reader = new StreamReader(messageFilePath))
                {
                    using (var xmlReader = XmlReader.Create(reader))
                    {
                        var root = XElement.Load(xmlReader);
                        var cdata = root.DescendantNodes().OfType<XCData>().FirstOrDefault();
                        if (cdata != null)
                        {
                            return cdata.Value;
                        }
                    }
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
            return null;
        }

        /// <summary>
        /// Write a message to an XML file in the current directory.
        /// </summary>
        public static void WriteProperties()
        {
            try
            {
                if (MessagePropertyInfo.Properties == null || MessagePropertyInfo.Properties.Count == 0)
                {
                    return;
                }
                using (var memoryStream = new MemoryStream())
                {
                    using (var stringWriter = new StreamWriter(memoryStream, Encoding.ASCII))
                    {
                        var settings = new XmlWriterSettings { Indent = true };
                        using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                        {
                            xmlWriter.WriteStartElement(Properties, Namespace);
                            foreach (var property in MessagePropertyInfo.Properties)
                            {
                                xmlWriter.WriteStartElement(Property, Namespace);
                                xmlWriter.WriteAttributeString(Key, property.Key);
                                xmlWriter.WriteAttributeString(Type, property.Type);
                                xmlWriter.WriteAttributeString(Value, property.Value.ToString());
                                xmlWriter.WriteEndElement();
                            }
                            xmlWriter.WriteEndElement();
                        }
                    }
                    var xml = Encoding.UTF8.GetString(memoryStream.ToArray());
                    WriteFile(propertiesFilePath, xml);
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Reads a message from an XML file in the current directory.
        /// </summary>
        /// <returns>The message read from the XML file.</returns>
        public static List<MessagePropertyInfo> ReadProperties()
        {
            try
            {
                if (!File.Exists(propertiesFilePath))
                {
                    return null;
                }

                using (var reader = new StreamReader(propertiesFilePath))
                {
                    using (var xmlReader = XmlReader.Create(reader))
                    {
                        XNamespace ns = Namespace;
                        var root = XElement.Load(xmlReader);
                        var elements = root.Elements(ns + Property);
                        return elements.Where(element => !string.IsNullOrWhiteSpace(element.Attribute(Key).Value.Trim()) &&
                                                  !string.IsNullOrWhiteSpace(element.Attribute(Type).Value.Trim()) &&
                                                  !string.IsNullOrWhiteSpace(element.Attribute(Value).Value.Trim())).
                        Select(element => new MessagePropertyInfo
                        {
                            Key = element.Attribute(Key).Value.Trim(),
                            Type = element.Attribute(Type).Value.Trim(),
                            Value = ConversionHelper.MapStringTypeToCLRType(element.Attribute(Type).Value.Trim(), element.Attribute(Value).Value.Trim())
                        }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
            return null;
        }
        #endregion

        #region Private Static Methods
        private static void WriteFile(string path, string content)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(content))
                {
                    return;
                }

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                using (var writer = new StreamWriter(path))
                {
                    writer.Write(content);
                    writer.Flush();
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
