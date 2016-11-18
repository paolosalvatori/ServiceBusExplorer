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
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public static class JsonTimestampHelper
    {
        #region Private Static Methods
        private static DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0); 
        #endregion

        #region Extension methods
        public static double FromDateTimeToJsonTimestamp(DateTime dateTime)
        {
            var universalTime = dateTime.ToUniversalTime();
            var timeSpan = new TimeSpan(universalTime.Ticks - origin.Ticks);
            return timeSpan.TotalMilliseconds;
        }

        public static DateTime FromJsonTimestampToDateTime(double milliseconds)
        {
            return origin.AddMilliseconds(milliseconds);
        }

        public static DateTime FromJsonTimestampToDateTime(string timestamp)
        {
            if (string.IsNullOrWhiteSpace(timestamp))
            {
                throw new ArgumentException("The timestamp argument cannot be null or empty.");
            }
            double value;
            if (!double.TryParse(timestamp, out value))
            {
                throw new ArgumentException("The timestamp argument must be a number.");
            }
            return timestamp.Length > 10 ? origin.AddMilliseconds(value) : origin.AddSeconds(value);
        }
        #endregion
    }
}
