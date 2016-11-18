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