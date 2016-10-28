#region Copyright
//=======================================================================================
// Windows Azure Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on the community
// blog at http://www.appfabriccat.com/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright © 2011 Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF 
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. YOU BEAR THE RISK OF USING IT.
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
        private static string messageFilePath = Path.Combine(Environment.CurrentDirectory, MessageFileName);
        private static string propertiesFilePath = Path.Combine(Environment.CurrentDirectory, PropertiesFileName);
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
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Reads a message from an XML file in the current directory.
        /// </summary>
        /// <returns>The message read from the XML file.</returns>
        public static  List<MessagePropertyInfo> ReadProperties()
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
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
