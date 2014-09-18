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
using System.Collections;
using System.Collections.Generic;
using System.Reflection; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class StandardValueAttribute : Attribute
    {
        #region Private Constants
        private const string EnumInstanceIsNull = "The enumInstance parameter is null.";
        private const string EnumInstanceIsNotEnumType = "The enumInstance parameter is not an Enum type.";
        #endregion

        #region Private Fields
        private string displayName = String.Empty;
        private bool visible = true;
        private bool enabled = true;
        private string description = String.Empty;
        private object value;
        #endregion

        #region Public Constructors
        public StandardValueAttribute(object value)
        {
            this.value = value;
        }

        public StandardValueAttribute(string displayName, string description)
        {
            this.displayName = displayName;
            this.description = description;
        } 
        #endregion

        #region Public Properties
        public string DisplayName
        {
            get
            {
                if (String.IsNullOrWhiteSpace(displayName))
                {
                    if (Value != null)
                    {
                        return Value.ToString();
                    }
                }
                return displayName;
            }
            set
            {
                displayName = value;
            }
        }

        public bool Visible
        {
            get
            {
                return visible;
            }
        }

        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        public object Value
        {
            get
            {
                return value;
            }
        } 
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return DisplayName;
        } 
        #endregion

        #region Internal Methods
        internal static IEnumerable<StandardValueAttribute> GetEnumItems(Type enumType)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException(EnumInstanceIsNull);
            }

            if (!enumType.IsEnum)
            {
                throw new ArgumentException(EnumInstanceIsNotEnumType);
            }

            var attributeArrayList = new ArrayList();
            var fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var fi in fields)
            {
                var attr = fi.GetCustomAttributes(typeof(StandardValueAttribute), false) as StandardValueAttribute[];

                if (attr != null && attr.Length > 0)
                {
                    attr[0].value = fi.GetValue(null);
                    attributeArrayList.Add(attr[0]);
                }
                else
                {
                    var atr = new StandardValueAttribute(fi.GetValue(null));
                    attributeArrayList.Add(atr);
                }
            }
            var retAttr = attributeArrayList.ToArray(typeof(StandardValueAttribute)) as StandardValueAttribute[];
            return retAttr;
        } 
        #endregion
    }
}