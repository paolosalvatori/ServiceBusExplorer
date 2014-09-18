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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Resources; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class CustomPropertyDescriptor : PropertyDescriptor
    {
        #region Private Fields
        private object owner;
        private readonly Type propType = Type.Missing.GetType();
        private readonly AttributeList attributes = new AttributeList();
        private readonly PropertyDescriptor propertyDescriptor;
        private readonly Collection<PropertyValueUIItem> colUIItem = new Collection<PropertyValueUIItem>();
        private string keyPrefix = String.Empty;
        private Image valueImage;
        private int tabAppendCount;
        private ResourceManager resourceManager;
        private object value;
        private readonly List<StandardValueAttribute> standardValues = new List<StandardValueAttribute>();
        #endregion

        #region Internal Constructors
        internal CustomPropertyDescriptor(object owner, string sName, Type type, object value, params Attribute[] attributes)
            : base(sName, attributes)
        {
            this.owner = owner;
            this.value = value;
            propType = type;
            this.attributes.AddRange(attributes);

            UpdateMemberData();
        }

        internal CustomPropertyDescriptor(object owner, PropertyDescriptor pd)
            : base(pd)
        {
            propertyDescriptor = pd;
            this.owner = owner;
            attributes = new AttributeList(pd.Attributes);
            UpdateMemberData();
        } 
        #endregion

        #region Public Properties
        public ICollection<StandardValueAttribute> StandardValues
        {
            get
            {
                if (PropertyType.IsEnum || PropertyType == typeof(bool))
                {
                    return standardValues.AsReadOnly();
                }
                return standardValues;
            }
        }


        public Image ValueImage
        {
            get
            {
                return valueImage;
            }
            set
            {
                valueImage = value;
            }
        }

        public PropertyFlags PropertyFlags
        {
            get
            {
                var attr = (PropertyStateFlagsAttribute)attributes.FirstOrDefault(a => a is PropertyStateFlagsAttribute);
                if (attr == null)
                {
                    attr = new PropertyStateFlagsAttribute();
                    attributes.Add(attr);
                    attr.Flags = PropertyFlags.Default;
                }

                return attr.Flags;
            }
            set
            {
                var attr = (PropertyStateFlagsAttribute)attributes.FirstOrDefault(a => a is PropertyStateFlagsAttribute);
                if (attr == null)
                {
                    attr = new PropertyStateFlagsAttribute();
                    attributes.Add(attr);
                    attr.Flags = PropertyFlags.Default;
                }
                attr.Flags = value;

            }
        }

        public ICollection<PropertyValueUIItem> StateItems
        {
            get
            {
                return colUIItem;
            }
        }

        public override TypeConverter Converter
        {
            get
            {
                var tca = (TypeConverterAttribute)attributes.FirstOrDefault(a => a is TypeConverterAttribute);

                if (tca != null)
                {
                    return base.Converter;
                }

                if (StandardValues.Count > 0)
                {
                    return new StandardValuesConverter();
                }
                var en = GetValue(owner) as IEnumerable;
                if (en != null && (PropertyFlags & PropertyFlags.ExpandIEnumerable) > 0)
                {
                    return new StandardValuesConverter();
                }
                return base.Converter;

            }
        }

        public override string Category
        {
            get
            {
                var sResult = String.Empty;
                if (ResourceManager != null && CategoryId != 0 && (PropertyFlags & PropertyFlags.LocalizeCategoryName) > 0)
                {
                    string sKey = KeyPrefix + "Cat" + CategoryId.ToString();
                    sResult = ResourceManager.GetString(sKey, CultureInfo.CurrentUICulture);
                    if (!String.IsNullOrWhiteSpace(sResult))
                    {
                        return sResult.PadLeft(sResult.Length + tabAppendCount, '\t');
                    }

                }
                var attr = (CategoryAttribute)attributes.FirstOrDefault(a => a is CategoryAttribute);
                if (attr != null)
                {
                    sResult = attr.Category;
                }
                if (String.IsNullOrWhiteSpace(sResult))
                {
                    sResult = base.Category;
                }
                return sResult.PadLeft(base.Category.Length + tabAppendCount, '\t');
            }
        }

        public override string Description
        {
            get
            {
                if (ResourceManager != null && (PropertyFlags & PropertyFlags.LocalizeDescription) > 0)
                {
                    string sKey = KeyPrefix + base.Name + "_Desc";
                    string sResult = ResourceManager.GetString(sKey, CultureInfo.CurrentUICulture);
                    if (!String.IsNullOrWhiteSpace(sResult))
                    {
                        return sResult;
                    }
                }
                var attr = (DescriptionAttribute)attributes.FirstOrDefault(a => a is DescriptionAttribute);
                return attr != null ? attr.Description : base.Description;
            }
        }

        public override Type ComponentType
        {
            get
            {
                return owner.GetType( );
            }
        }

        public override Type PropertyType
        {
            get
            {
                if (propertyDescriptor != null)
                {
                    return propertyDescriptor.PropertyType;
                }
                return propType;
            }
        }

        public override AttributeCollection Attributes
        {
            get
            {
                var ac = new AttributeCollection(attributes.ToArray());
                return ac;
            }
        }

        public IList<Attribute> AllAttributes
        {
            get
            {
                return attributes;
            }
        }

        public override bool IsLocalizable
        {
            get
            {
                var attr = (LocalizableAttribute)attributes.FirstOrDefault(a => a is LocalizableAttribute);
                return attr != null ? attr.IsLocalizable : base.IsLocalizable;
            }
        }

        public object DefaultValue
        {
            get
            {
                var attr = (DefaultValueAttribute)attributes.FirstOrDefault(a => a is DefaultValueAttribute);
                return attr != null ? attr.Value : null;
            }
            set
            {
                var attr = (DefaultValueAttribute)attributes.FirstOrDefault(a => a is DefaultValueAttribute);
                if (attr == null)
                {
                    attributes.RemoveAll(a => a is DefaultValueAttribute);
                }
                attr = new DefaultValueAttribute(value);
            }
        }

        public int PropertyId
        {
            get
            {
                var rsa = (IdAttribute)attributes.FirstOrDefault(a => a is IdAttribute);
                return rsa != null ? rsa.PropertyId : 0;
            }
            set
            {
                var rsa = (IdAttribute)attributes.FirstOrDefault(a => a is IdAttribute);
                if (rsa == null)
                {
                    rsa = new IdAttribute();
                    attributes.Add(rsa);
                }
                rsa.PropertyId = value;
            }
        }

        public int CategoryId
        {
            get
            {
                var rsa = (IdAttribute)attributes.FirstOrDefault(a => a is IdAttribute);
                return rsa != null ? rsa.CategoryId : 0;
            }
            set
            {
                var rsa = (IdAttribute)attributes.FirstOrDefault(a => a is IdAttribute);
                if (rsa == null)
                {
                    rsa = new IdAttribute();
                    attributes.Add(rsa);
                }
                rsa.CategoryId = value;
            }
        }
        #endregion

        #region Protected Properties
        protected override Attribute[] AttributeArray
        {
            get
            {
                return attributes.ToArray();
            }
            set
            {
                attributes.Clear();
                attributes.AddRange(value);
            }
        }

        public override bool IsBrowsable
        {
            get
            {
                var attr = (BrowsableAttribute)attributes.FirstOrDefault(a => a is BrowsableAttribute);
                return attr != null ? attr.Browsable : base.IsBrowsable;
            }
        }

        public override string DisplayName
        {
            get
            {

                if (ResourceManager != null && (PropertyFlags & PropertyFlags.LocalizeDisplayName) > 0)
                {
                    var sKey = KeyPrefix + base.Name + "_Name";

                    var sResult = ResourceManager.GetString(sKey, CultureInfo.CurrentUICulture);
                    if (!String.IsNullOrWhiteSpace(sResult))
                    {
                        return sResult;
                    }
                }
                var attr = (DisplayNameAttribute)attributes.FirstOrDefault(a => a is DisplayNameAttribute);
                return attr != null ? attr.DisplayName : base.DisplayName;
            }
        }
        #endregion

        #region Internal Properties
        internal string KeyPrefix
        {
            get
            {
                return keyPrefix;
            }
            set
            {
                keyPrefix = value;
            }
        }

        internal int TabAppendCount
        {
            get
            {
                return tabAppendCount;
            }
            set
            {
                tabAppendCount = value;
            }
        }

        internal ResourceManager ResourceManager
        {
            get
            {
                return resourceManager;
            }
            set
            {
                resourceManager = value;
            }
        }
        #endregion

        #region Public Methods
        public void SetIsLocalizable(bool isLocalizable)
        {
            var attr = (LocalizableAttribute)attributes.FirstOrDefault(a => a is LocalizableAttribute);
            if (attr != null)
            {
                attributes.RemoveAll(a => a is LocalizableAttribute);
            }
            attr = new LocalizableAttribute(isLocalizable);
            attributes.Add(attr);
        }

        public override bool IsReadOnly
        {
            get
            {
                var attr = (ReadOnlyAttribute)attributes.FirstOrDefault(a => a is ReadOnlyAttribute);
                return attr != null && attr.IsReadOnly;
            }
        }

        public void SetIsReadOnly(bool isReadOnly)
        {
            var attr = (ReadOnlyAttribute)attributes.FirstOrDefault(a => a is ReadOnlyAttribute);
            if (attr != null)
            {
                attributes.RemoveAll(a => a is ReadOnlyAttribute);
            }
            attr = new ReadOnlyAttribute(isReadOnly);
            attributes.Add(attr);
        } 
        
        public void SetIsBrowsable( bool isBrowsable )
        {
            var attr = (BrowsableAttribute)attributes.FirstOrDefault(a => a is BrowsableAttribute);
            if (attr != null)
            {
                attributes.RemoveAll(a => a is BrowsableAttribute);
            }
            attr = new BrowsableAttribute(isBrowsable);
            attributes.Add(attr);
        }
        
        public void SetDisplayName( string displayName )
        {
            var attr = (DisplayNameAttribute)attributes.FirstOrDefault(a => a is DisplayNameAttribute);
            if (attr != null)
            {
                attributes.RemoveAll(a => a is DisplayNameAttribute);
            }
            attr = new DisplayNameAttribute(displayName);
            attributes.Add(attr);
        }

        public void SetCategory( string category )
        {
            var attr = (CategoryAttribute)attributes.FirstOrDefault(a => a is CategoryAttribute);
            if (attr != null)
            {
                attributes.RemoveAll(a => a is CategoryAttribute);
            }
            attr = new CategoryAttribute(category);
            attributes.Add(attr);
        }
       
        public void SetDescription( string description )
        {
            var attr = (DescriptionAttribute)attributes.FirstOrDefault(a => a is DescriptionAttribute);
            if (attr != null)
            {
                attributes.RemoveAll(a => a is DescriptionAttribute);
            }
            attr = new DescriptionAttribute(description);
            attributes.Add(attr);
        }
        
        public override object GetValue( object component )
        {
            if (propertyDescriptor != null)
            {
                return propertyDescriptor.GetValue(component);
            }
            return value;
        }

        public override void SetValue( object component, object value )
        {
            if (value != null && value is StandardValueAttribute)
            {
                this.value = (value as StandardValueAttribute).Value;
            }
            else
            {
                this.value = value;
            }

            if (propertyDescriptor != null)
            {
                propertyDescriptor.SetValue(component, this.value);
                OnValueChanged(this, new EventArgs( ));

            }
            else
            {
                EventHandler eh = this.GetValueChangedHandler(owner);
                if (eh != null)
                {
                    eh.Invoke(this, new EventArgs( ));
                }
                OnValueChanged(this, new EventArgs( ));
            }
        }

        /// <summary>
        /// Abstract base members
        /// </summary>			
        public override void ResetValue( object component )
        {
            var dva = (DefaultValueAttribute)attributes.FirstOrDefault(a => a is DefaultValueAttribute);
            if (dva == null)
            {
                return;
            }
            SetValue(component, dva.Value);
        }

        public override bool CanResetValue( object component )
        {
            var dva = (DefaultValueAttribute)attributes.FirstOrDefault(a => a is DefaultValueAttribute);
            if (dva == null)
            {
                return false;
            }
            var bOk = (dva.Value.Equals(value));
            return !bOk;

        }

        public override bool ShouldSerializeValue( object component )
        {
            return CanResetValue(owner);
        }

        public override PropertyDescriptorCollection GetChildProperties( object instance, Attribute[] filter )
        {
            PropertyDescriptorCollection pdc = null;
            var tc = this.Converter;
            if (tc.GetPropertiesSupported(null) == false)
            {
                pdc = base.GetChildProperties(instance, filter);
            }
            else
            {

            }
            if (propertyDescriptor != null)
            {
                tc = propertyDescriptor.Converter;
            }
            else
            {
                //pdc = base.GetChildProperties(instance, filter);// this gives us a readonly collection, no good    
                tc = TypeDescriptor.GetConverter(instance, true);
            }
            if (pdc == null || pdc.Count == 0)
            {
                return pdc;
            }
            if (pdc[0] is CustomPropertyDescriptor)
            {
                return pdc;
            }
            // now wrap these properties with our CustomPropertyDescriptor
            var pdl = new PropertyDescriptorList( );

            foreach (PropertyDescriptor pd in pdc)
            {
                if (pd is CustomPropertyDescriptor)
                {
                    pdl.Add(pd as CustomPropertyDescriptor);
                }
                else
                {
                    pdl.Add(new CustomPropertyDescriptor(instance, pd));
                }
            }

            pdl.Sort(new PropertySorter( ));
            var pdcReturn = new PropertyDescriptorCollection(pdl.ToArray( ));
            pdcReturn.Sort( );
            return pdcReturn;
      
        }
        #endregion

        #region Protected Methods
        protected override void FillAttributes(IList attributeList)
        {
            foreach (var attr in attributes)
            {
                attributeList.Add(attr);
            }
        }

        protected override void OnValueChanged(object component, EventArgs e)
        {
            var md = component as MemberDescriptor;
            base.OnValueChanged(component, e);
        }
        #endregion

        #region Private Methods
        private void UpdateMemberData()
        {

            if (propertyDescriptor != null)
            {
                value = propertyDescriptor.GetValue(owner);
            }

            if (PropertyType.IsEnum)
            {
                var sva = StandardValueAttribute.GetEnumItems(PropertyType);
                standardValues.AddRange(sva);
            }
            else if (PropertyType == typeof(bool))
            {
                standardValues.Add(new StandardValueAttribute(true));
                standardValues.Add(new StandardValueAttribute(false));
            }
        }
        #endregion
    }
}