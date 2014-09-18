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
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    internal class StandardValuesConverter : TypeConverter
    {
        #region Private Static Fields
        private static int count; 
        #endregion

        #region Public Methods
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            if (context != null && context.PropertyDescriptor is CustomPropertyDescriptor)
            {
                var cpd = context.PropertyDescriptor as CustomPropertyDescriptor;
                var enu = cpd.GetValue(context.Instance) as IEnumerable;
                if (enu != null && cpd.PropertyFlags != PropertyFlags.None && (cpd.PropertyFlags & PropertyFlags.ExpandIEnumerable) > 0)
                {
                    return true;
                }
            }
            return base.GetPropertiesSupported(context);

        }
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            if (value == null)
            {
                return base.GetProperties(context, value, attributes);
            }

            var propType = Type.Missing.GetType();
            if (context != null && context.PropertyDescriptor is CustomPropertyDescriptor)
            {
                var cpd = context.PropertyDescriptor as CustomPropertyDescriptor;
                UpdateEnumDisplayText(cpd);
                propType = cpd.PropertyType;
            }
            var pdl = new List<CustomPropertyDescriptor>();
            int nIndex = -1;
            if (pdl.Count == 0)
            {
                var en = value as IEnumerable;
                if (en != null)
                {
                    var enu = en.GetEnumerator();
                    enu.Reset();
                    while (enu.MoveNext())
                    {
                        nIndex++;
                        var sPropName = enu.Current.ToString();

                        var comp = enu.Current as IComponent;
                        if (comp != null && comp.Site != null && !String.IsNullOrWhiteSpace(comp.Site.Name))
                        {
                            sPropName = comp.Site.Name;
                        }
                        else if (propType.IsArray)
                        {
                            sPropName = "[" + nIndex.ToString() + "]";
                        }
                        pdl.Add(new CustomPropertyDescriptor(null, sPropName, enu.Current.GetType(), enu.Current));
                    }
                }
            }
            if (pdl.Count > 0)
            {
                return new PropertyDescriptorCollection(pdl.ToArray());
            }
            return base.GetProperties(context, value, attributes);
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            if (context != null && context.PropertyDescriptor is CustomPropertyDescriptor)
            {
                var cpd = context.PropertyDescriptor as CustomPropertyDescriptor;

                if (cpd.PropertyFlags != PropertyFlags.None && (cpd.PropertyFlags & PropertyFlags.SupportStandardValues) > 0)
                {
                    return true;
                }
            }
            return base.GetStandardValuesSupported(context);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            if (context != null && context.PropertyDescriptor is CustomPropertyDescriptor)
            {
                var cpd = context.PropertyDescriptor as CustomPropertyDescriptor;
                if (cpd.PropertyType == typeof(bool) || cpd.PropertyType.IsEnum)
                {
                    return true;
                }
                if (cpd.PropertyFlags != PropertyFlags.None && ((cpd.PropertyFlags & PropertyFlags.ExclusiveStandardValues) > 0))
                {
                    return true;
                }
                return false;
            }

            return base.GetStandardValuesExclusive(context);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            //WriteContext("ConvertFrom", context, value, Type.Missing.GetType());

            ICollection<StandardValueAttribute> col = null;
            Type propType = Type.Missing.GetType();
            if (context != null && context.PropertyDescriptor is CustomPropertyDescriptor)
            {
                CustomPropertyDescriptor cpd = context.PropertyDescriptor as CustomPropertyDescriptor;
                UpdateEnumDisplayText(cpd);
                col = cpd.StandardValues;
                propType = cpd.PropertyType;
            }
            if (value == null)
            {
                return null;
            }
            else if (value is string)
            {
                if (propType.IsEnum)
                {
                    var sInpuValue = value as string;
                    string[] arrDispName = sInpuValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    var sb = new StringBuilder(1000);
                    foreach (string sDispName in arrDispName)
                    {
                        string sTrimValue = sDispName.Trim();
                        foreach (StandardValueAttribute sva in col)
                        {
                            if (String.Compare(sva.Value.ToString(), sTrimValue, true) == 0 ||
                                String.Compare(sva.DisplayName, sTrimValue, true) == 0)
                            {
                                if (sb.Length > 0)
                                {
                                    sb.Append(",");
                                }
                                sb.Append(sva.Value.ToString());
                            }
                        }

                    }  // end of foreach..loop
                    return Enum.Parse(propType, sb.ToString(), true);
                }
                foreach (StandardValueAttribute sva in col)
                {
                    if (String.Compare(value.ToString(), sva.DisplayName, true, culture) == 0 ||
                        String.Compare(value.ToString(), sva.Value.ToString(), true, culture) == 0)
                    {
                        return sva.Value;

                    }
                }
                var tc = TypeDescriptor.GetConverter(propType);
                if (tc != null)
                {
                    object convertedValue = null;
                    try
                    {
                        convertedValue = tc.ConvertFrom(context, culture, value);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    if (tc.IsValid(convertedValue))
                    {
                        return convertedValue;
                    }
                }
            }
            else if (value.GetType() == propType)
            {
                return value;
            }
            else if (value is StandardValueAttribute)
            {
                return (value as StandardValueAttribute).Value;
            }

            return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            //WriteContext("ConvertTo", context, value, destinationType);

            ICollection<StandardValueAttribute> col = null;
            var propType = Type.Missing.GetType();
            if (context != null && context.PropertyDescriptor is CustomPropertyDescriptor)
            {
                var cpd = context.PropertyDescriptor as CustomPropertyDescriptor;
                UpdateEnumDisplayText(cpd);
                col = cpd.StandardValues;
                propType = cpd.PropertyType;
            }
            if (value == null)
            {
                return null;
            }
            else if (value is string)
            {
                if (destinationType == typeof(string))
                {
                    return value;
                }
                else if (destinationType == propType)
                {
                    return ConvertFrom(context, culture, value);
                }
                else if (destinationType == typeof(StandardValueAttribute))
                {
                    foreach (StandardValueAttribute sva in col)
                    {
                        if (String.Compare(value.ToString(), sva.DisplayName, true, culture) == 0 ||
                            String.Compare(value.ToString(), sva.Value.ToString(), true, culture) == 0)
                        {
                            return sva;
                        }
                    }
                }
            }
            else if (value.GetType() == propType)
            {
                if (destinationType == typeof(string))
                {
                    if (propType.IsEnum)
                    {
                        var sDelimitedValues = Enum.Format(propType, value, "G");
                        var arrValue = sDelimitedValues.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        var sb = new StringBuilder(1000);
                        foreach (string sDispName in arrValue)
                        {
                            string sTrimValue = sDispName.Trim();
                            foreach (StandardValueAttribute sva in col)
                            {
                                if (String.Compare(sva.Value.ToString(), sTrimValue, true) == 0 ||
                                    String.Compare(sva.DisplayName, sTrimValue, true) == 0)
                                {
                                    if (sb.Length > 0)
                                    {
                                        sb.Append(", ");
                                    }
                                    sb.Append(sva.DisplayName);
                                }
                            }

                        }  // end of foreach..loop
                        return sb.ToString();
                    }
                    foreach (StandardValueAttribute sva in col)
                    {
                        if (sva.Value.Equals(value))
                        {
                            return sva.DisplayName;
                        }
                    }
                    TypeConverter tc = TypeDescriptor.GetConverter(propType);
                    if (tc != null)
                    {
                        object convertedValue = null;
                        try
                        {
                            convertedValue = tc.ConvertTo(context, culture, value, destinationType);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        if (tc.IsValid(convertedValue))
                        {
                            return convertedValue;
                        }
                    }
                }
                else if (destinationType == typeof(StandardValueAttribute))
                {
                    foreach (var sva in col)
                    {
                        if (sva.Value.Equals(value))
                        {
                            return sva;
                        }
                    }

                }
                else if (destinationType == propType)
                {
                    return value;
                }
            }
            else if (value is StandardValueAttribute)
            {
                if (destinationType == typeof(string))
                {
                    return (value as StandardValueAttribute).DisplayName;
                }
                else if (destinationType == typeof(StandardValueAttribute))
                {
                    return value;
                }
                else if (destinationType == propType)
                {
                    return (value as StandardValueAttribute).Value;
                }
            }
            return base.ConvertFrom(context, culture, value);

        }
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            ICollection<StandardValueAttribute> col = null;
            if (context != null && context.PropertyDescriptor is CustomPropertyDescriptor)
            {
                var cpd = context.PropertyDescriptor as CustomPropertyDescriptor;
                UpdateEnumDisplayText(cpd);
                col = cpd.StandardValues;
            }

            var list = new List<StandardValueAttribute>();
            foreach (var sva in col)
            {
                if (sva.Visible)
                {
                    list.Add(sva);
                }
            }
            if (list.Count > 0)
            {
                var svc = new StandardValuesCollection(list);
                return svc;
            }

            return base.GetStandardValues(context);
        } 
        #endregion

        #region Private Methods
        private void UpdateEnumDisplayText(CustomPropertyDescriptor cpd)
        {
            if (!(cpd.PropertyType.IsEnum || cpd.PropertyType == typeof(bool)))
            {
                return;
            }
            if ((cpd.PropertyFlags & PropertyFlags.LocalizeEnumerations) <= 0)
            {
                return;
            }
            string prefix = String.Empty;
            ResourceManager rm = null;
            StandardValueAttribute sva = null;

            sva = cpd.StandardValues.FirstOrDefault() as StandardValueAttribute;

            // first try property itself
            if (cpd.ResourceManager != null)
            {
                string keyName = cpd.KeyPrefix + cpd.Name + "_" + sva.Value.ToString() + "_Name";
                string valueName = cpd.ResourceManager.GetString(keyName);
                if (!String.IsNullOrWhiteSpace(valueName))
                {
                    rm = cpd.ResourceManager;
                    prefix = cpd.KeyPrefix + cpd.Name;
                }
            }

            // now try class level
            if (rm == null && cpd.ResourceManager != null)
            {
                string keyName = cpd.KeyPrefix + cpd.PropertyType.Name + "_" + sva.Value.ToString() + "_Name";
                string valueName = cpd.ResourceManager.GetString(keyName);
                if (!String.IsNullOrWhiteSpace(valueName))
                {
                    rm = cpd.ResourceManager;
                    prefix = cpd.KeyPrefix + cpd.PropertyType.Name;
                }
            }

            // try the enum itself if still null
            if (rm == null && cpd.PropertyType.IsEnum)
            {
                var attr = (EnumResourceAttribute)cpd.AllAttributes.FirstOrDefault(a => a is EnumResourceAttribute);
                if (attr != null)
                {
                    try
                    {
                        if (String.IsNullOrWhiteSpace(attr.AssemblyFullName) == false)
                        {
                            rm = new ResourceManager(attr.BaseName, Assembly.ReflectionOnlyLoad(attr.AssemblyFullName));
                        }
                        else
                        {
                            rm = new ResourceManager(attr.BaseName, cpd.PropertyType.Assembly);
                        }
                        prefix = attr.KeyPrefix + cpd.PropertyType.Name;
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
            }

            if (rm != null)
            {
                foreach (StandardValueAttribute sv in cpd.StandardValues)
                {
                    string keyName = prefix + "_" + sv.Value.ToString() + "_Name";  // display name
                    string keyDesc = prefix + "_" + sv.Value.ToString() + "_Desc"; // description
                    string dispName = String.Empty;
                    string description = String.Empty;

                    try
                    {
                        dispName = rm.GetString(keyName);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    if (String.IsNullOrWhiteSpace(dispName) == false)
                    {
                        sv.DisplayName = dispName;
                    }

                    try
                    {
                        description = rm.GetString(keyDesc);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    if (String.IsNullOrWhiteSpace(description) == false)
                    {
                        sv.Description = description;
                    }
                }
            }
        }

        private void WriteContext(string prefix, ITypeDescriptorContext ctx, object value, Type destinationType)
        {
            count++;
            var sb = new StringBuilder(1024);

            if (ctx != null)
            {
                if (ctx.Instance != null)
                {
                    sb.Append("ctx.Instance is " + ctx.Instance.ToString() + ". ");
                }

                if (ctx.PropertyDescriptor != null)
                {
                    sb.Append("ctx.PropertyDescriptor is " + ctx.PropertyDescriptor.ToString() + ". ");
                }
            }
            else
            {
                sb.Append("ctx is null. ");
            }

            if (value == null)
            {
                sb.AppendLine("Value is null. ");
            }
            else
            {
                sb.AppendLine("Value is " + value.ToString() + ", " + value.GetType().ToString() + ". ");
            }
            sb.AppendLine(destinationType.ToString());
            Console.WriteLine(count.ToString() + " " + prefix + ": " + sb.ToString());
        } 
        #endregion
    }
}