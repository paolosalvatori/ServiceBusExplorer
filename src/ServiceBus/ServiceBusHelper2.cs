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
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.ServiceBus.Management;
using Microsoft.Azure.ServiceBusExplorer.Helpers;
using Microsoft.Azure.ServiceBusExplorer.Enums;
#endregion

// ReSharper disable CheckNamespace
namespace Microsoft.Azure.ServiceBusExplorer
// ReSharper restore CheckNamespace
{


    public class ServiceBusHelper2 
    {
        public string ConnectionString { get; set; }

       
    }
}
