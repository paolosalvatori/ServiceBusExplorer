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
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

#endregion


namespace ServiceBusExplorer.Utilities.Helpers
{
    public class ConversionHelper
    {
        #region Private Constants
        private const string TypeNotSupported = "Type not supported: {0}";
        #endregion

        #region Static Fields
        private static readonly Dictionary<Type, string> clrToEDMMappingDictionary = new Dictionary<Type, string>();
        #endregion

        #region Static Constructor
        static ConversionHelper()
        {
            clrToEDMMappingDictionary.Add(typeof(byte), "Edm.Byte");
            clrToEDMMappingDictionary.Add(typeof(short), "Edm.Int16");
            clrToEDMMappingDictionary.Add(typeof(int), "Edm.Int32");
            clrToEDMMappingDictionary.Add(typeof(long), "Edm.Int64");
            clrToEDMMappingDictionary.Add(typeof(float), "Edm.Single");
            clrToEDMMappingDictionary.Add(typeof(double), "Edm.Double");
            clrToEDMMappingDictionary.Add(typeof(decimal), "Edm.Decimal");
            clrToEDMMappingDictionary.Add(typeof(byte[]), "Edm.Binary");
            clrToEDMMappingDictionary.Add(typeof(Guid), "Edm.Guid");
            clrToEDMMappingDictionary.Add(typeof(DateTime), "Edm.DateTime");
            clrToEDMMappingDictionary.Add(typeof(bool), "Edm.Boolean");
        }
        #endregion

        #region Public Static Methods
        public static object MapEDMTypeToCLRType(string type, string value, bool isNull)
        {
            if (isNull)
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(type))
            {
                return value;
            }
            switch (type)
            {
                case "Edm.String":
                    return value;
                case "Edm.Byte":
                    return Convert.ChangeType(value, typeof(byte));
                case "Edm.SByte":
                    return Convert.ChangeType(value, typeof(sbyte));
                case "Edm.Int16":
                    return Convert.ChangeType(value, typeof(short));
                case "Edm.Int32":
                    return Convert.ChangeType(value, typeof(int));
                case "Edm.Int64":
                    return Convert.ChangeType(value, typeof(long));
                case "Edm.Single":
                    return Convert.ChangeType(value, typeof(float));
                case "Edm.Double":
                    return Convert.ChangeType(value, typeof(double));
                case "Edm.Boolean":
                    return Convert.ChangeType(value, typeof(bool));
                case "Edm.Decimal":
                    return Convert.ChangeType(value, typeof(decimal));
                case "Edm.DateTime":
                    return XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
                case "Edm.Binary":
                    return Convert.FromBase64String(value);
                case "Edm.Guid":
                    return new Guid(value);
            }
            throw new NotSupportedException(string.Format(TypeNotSupported, type));
        }

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
                case "Byte":
                    return Convert.ChangeType(value, typeof(byte));
                case "Int16":
                    return Convert.ChangeType(value, typeof(short));
                case "Int32":
                    return Convert.ChangeType(value, typeof(int));
                case "Int64":
                    return Convert.ChangeType(value, typeof(long));
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

        public static string MapCLRTypeToEDMType(Type type)
        {
            if (type != null &&
                clrToEDMMappingDictionary.ContainsKey(type))
            {
                return clrToEDMMappingDictionary[type];
            }
            return null;
        }
        #endregion
    }
}
