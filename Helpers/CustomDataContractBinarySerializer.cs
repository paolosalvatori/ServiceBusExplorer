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
using System.IO;
using System.Runtime.Serialization;
using System.Xml; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class CustomDataContractBinarySerializer : XmlObjectSerializer
    {
        // Fields
        private readonly DataContractSerializer dataContractSerializer;

        // Methods
        public CustomDataContractBinarySerializer(Type type)
        {
            dataContractSerializer = new DataContractSerializer(type);
        }

        public override bool IsStartObject(XmlDictionaryReader reader)
        {
            return dataContractSerializer.IsStartObject(reader);
        }

        public override object ReadObject(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            return ReadObject(XmlDictionaryReader.CreateBinaryReader(stream, XmlDictionaryReaderQuotas.Max));
        }

        public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName)
        {
            return dataContractSerializer.ReadObject(reader, verifyObjectName);
        }

        public override void WriteEndObject(XmlDictionaryWriter writer)
        {
            dataContractSerializer.WriteEndObject(writer);
        }

        public override void WriteObject(Stream stream, object graph)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            var writer = XmlDictionaryWriter.CreateBinaryWriter(stream, null, null, false);
            WriteObject(writer, graph);
            writer.Flush();
        }

        public override void WriteObject(XmlDictionaryWriter writer, object graph)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            dataContractSerializer.WriteObject(writer, graph);
        }

        public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
        {
            dataContractSerializer.WriteObjectContent(writer, graph);
        }

        public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
        {
            dataContractSerializer.WriteStartObject(writer, graph);
        }
    }
}
