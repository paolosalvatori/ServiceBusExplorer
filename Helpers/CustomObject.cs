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
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.CompilerServices;

#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    [TypeConverter(typeof(CustomObjectConverter))]
    public class CustomObject : INotifyPropertyChanged
    {
        #region Private Fields
        private List<CustomProperty> propertyList = new List<CustomProperty>();
        private readonly Dictionary<string, object> valueDictionary = new Dictionary<string, object>();
        #endregion

        #region Public Events
        public event PropertyChangedEventHandler PropertyChanged; 
        #endregion

        #region Public Properties
        [Browsable(false)]
        public List<CustomProperty> Properties
        {
            get
            {
                return propertyList;
            }
            set
            {
                propertyList = value;
                NotifyPropertyChanged();
            }
        }

        public object this[string name]
        {
            get
            {
                object val;
                valueDictionary.TryGetValue(name, out val);
                return val;
            }
            set
            {
                valueDictionary[name] = value;
                NotifyPropertyChanged();
            }
        } 
        #endregion

        #region Private Classes
        private class CustomObjectConverter : ExpandableObjectConverter
        {
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                var standardProperties = base.GetProperties(context, value, attributes);
                var obj = value as CustomObject;
                var customProps = obj == null ? null : obj.Properties;
                var props = new PropertyDescriptor[standardProperties.Count + (customProps == null ? 0 : customProps.Count)];
                standardProperties.CopyTo(props, 0);
                if (customProps != null)
                {
                    var index = standardProperties.Count;
                    foreach (var property in customProps)
                    {
                        props[index++] = new CustomPropertyDescriptor(property);
                    }
                }
                return new PropertyDescriptorCollection(props);
            }
        }

        private class CustomPropertyDescriptor : PropertyDescriptor
        {
            private readonly CustomProperty property;
            public CustomPropertyDescriptor(CustomProperty property)
                : base(property.Name, null)
            {
                this.property = property;
            }
            public override string Category { get { return "Parameters"; } }
            public override string Description { get { return property.Description; } }
            public override string Name { get { return property.Name; } }
            public override bool ShouldSerializeValue(object component) { return ((CustomObject)component)[property.Name] != null; }
            public override void ResetValue(object component) { ((CustomObject)component)[property.Name] = null; }
            public override bool IsBrowsable { get { return property.IsBrowsable; } }
            public override bool IsReadOnly { get { return property.IsReadOnly; } }
            public override bool DesignTimeOnly { get { return property.DesignTimeOnly; } }
            public override Type PropertyType { get { return property.Type; } }
            public override bool CanResetValue(object component) { return true; }
            public override Type ComponentType { get { return typeof(CustomObject); } }
            public override void SetValue(object component, object value) { ((CustomObject)component)[property.Name] = value; }
            public override object GetValue(object component) { return ((CustomObject)component)[property.Name] ?? property.DefaultValue; }
            public override object GetEditor(Type editorBaseType)
            {
                return property.Editor ?? base.GetEditor(editorBaseType);
            }
        } 
        #endregion

        #region Private Methods
        // This method is called by the Set accessor of each property. 
        // The CallerMemberName attribute that is applied to the optional propertyName 
        // parameter causes the property name of the caller to be substituted as an argument. 
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        } 
        #endregion
    }


    public class CustomProperty
    {
        #region Private Fields
        private Type type;
        #endregion

        #region Public Constructor
        public CustomProperty()
        {
            IsBrowsable = true;
            IsReadOnly = false;
            DesignTimeOnly = false;
        }
        #endregion

        #region Public Properties
        public string Name { get; set; }
        public string Description { get; set; }
        public object DefaultValue { get; set; }
        public bool IsBrowsable { get; set; }
        public bool IsReadOnly { get; set; }
        public bool DesignTimeOnly { get; set; }
        public UITypeEditor Editor { get; set; }
        public Type Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                if (type != typeof(string) && type.IsPrimitive)
                {
                    DefaultValue = Activator.CreateInstance(value);
                }
            }
        }
        #endregion
    }
}
