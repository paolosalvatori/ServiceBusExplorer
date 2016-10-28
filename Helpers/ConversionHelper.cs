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
using System.Xml;
using System.IO;
using System.Collections.Generic;
#endregion


namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
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
                case "Guid":
                    return new Guid(value as string);
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
