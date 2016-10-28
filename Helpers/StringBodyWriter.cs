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
using System.ServiceModel.Channels;
using System.Xml;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    class StringBodyWriter : BodyWriter
    {
        #region Private Fields
        private readonly string content; 
        #endregion

        #region Public Constructor
        public StringBodyWriter(string content)
            : base(true)
        {
            this.content = content;
        } 
        #endregion

        #region Protected Methods
        protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
        {
            writer.WriteRaw(content);
        } 
        #endregion
    }
}
