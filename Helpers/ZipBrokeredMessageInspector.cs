#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
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
using System.IO.Compression;
using Microsoft.ServiceBus.Messaging;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class ZipBrokeredMessageInspector : IBrokeredMessageInspector, IDisposable
    {
        #region IBrokeredMessageInspector Methods
        public BrokeredMessage BeforeSendMessage(BrokeredMessage message, WriteToLogDelegate writeToLog = null)
        {
            if (message == null)
            {
                return null;
            }
            var stream = message.Clone().GetBody<Stream>();
            if (stream == null)
            {
                return null;
            }
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
            message.SetBodyStream(Compress(stream));
            return message;
        }

        public BrokeredMessage AfterReceiveMessage(BrokeredMessage message, WriteToLogDelegate writeToLog = null)
        {
            if (message == null)
            {
                return null;
            }
            var stream = message.Clone().GetBody<Stream>();
            if (stream == null)
            {
                return null;
            }
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
            message.SetBodyStream(Decompress(stream));
            return message;
        } 
        #endregion

        #region IDisposable Methods
        public void Dispose()
        {
        }
        #endregion

        #region Private Static Methods
        private static Stream Compress(Stream inputStream)
        {
            var outputStream = new MemoryStream();
            using (var zipStream = new GZipStream(outputStream, CompressionMode.Compress, true))
            {
                inputStream.CopyTo(zipStream);
            }
            outputStream.Position = 0;
            return outputStream;
        }
        private static Stream Decompress(Stream inputStream)
        {
            var outputStream = new MemoryStream();
            using (var zipStream = new GZipStream(inputStream, CompressionMode.Decompress, true))
            {
                try
                {
                    zipStream.CopyTo(outputStream);
                }
                catch (Exception)
                {
                    return inputStream;
                }
            }
            outputStream.Position = 0;
            return outputStream;
        }
        #endregion
    }
}
