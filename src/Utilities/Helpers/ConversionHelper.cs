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
using System.Globalization;

#endregion


namespace ServiceBusExplorer.Utilities.Helpers
{
    public class ConversionHelper
    {
        #region Private Constants
        private const string TypeNotSupported = "Type not supported: {0}";
        #endregion

        #region Public Static Methods
        public static object MapStringTypeToCLRType(string type, object value)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return value;
            }
            switch (type)
            {
                case "String":
                    var s = value as string;
                    return s != null ? s.Trim() : null;
                case "Char":
                    return Convert.ChangeType(value, typeof(char));
                case "SByte":
                    return Convert.ChangeType(value, typeof(sbyte));
                case "Byte":
                    return Convert.ChangeType(value, typeof(byte));
                case "Int16":
                    return Convert.ChangeType(value, typeof(short));
                case "UInt16":
                    return Convert.ChangeType(value, typeof(ushort));
                case "Int32":
                    return Convert.ChangeType(value, typeof(int));
                case "UInt32":
                    return Convert.ChangeType(value, typeof(uint));
                case "Int64":
                    return Convert.ChangeType(value, typeof(long));
                case "UInt64":
                    return Convert.ChangeType(value, typeof(ulong)); 
                case "Single":
                    return Convert.ChangeType(value, typeof(float));
                case "Double":
                    return Convert.ChangeType(value, typeof(double));
                case "Boolean":
                    return Convert.ChangeType(value, typeof(bool));
                case "Decimal":
                    return Convert.ChangeType(value, typeof(decimal));
                case "DateTime":
                    return Convert.ChangeType(value, typeof(DateTime));
                case "TimeSpan":
                    if (value is TimeSpan t) return t;
                    if (value is string st) return TimeSpan.Parse(st, CultureInfo.InvariantCulture);
                    return TimeSpan.Parse((string)value);
                case "Guid":
                    return new Guid(value.ToString());
            }
            throw new NotSupportedException(string.Format(TypeNotSupported, type));
        }
        #endregion
    }
}
