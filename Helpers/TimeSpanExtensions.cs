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
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public static class TimeSpanExtensions
    {
        public static bool IsMaxValue(this TimeSpan timeSpan)
        {
            var maxValue = TimeSpan.MaxValue;
            return maxValue.Days == timeSpan.Days &&
                   maxValue.Hours == timeSpan.Hours &&
                   maxValue.Minutes == timeSpan.Minutes &&
                   maxValue.Seconds == timeSpan.Seconds;
        }
    }
}
