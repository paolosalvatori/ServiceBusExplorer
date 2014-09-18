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
