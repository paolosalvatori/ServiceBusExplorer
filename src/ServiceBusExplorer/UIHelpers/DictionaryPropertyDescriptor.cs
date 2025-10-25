namespace ServiceBusExplorer.UIHelpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;

    public class DictionaryPropertyDescriptor<TKey, TValue> : PropertyDescriptor
        where TValue : class
    {
        private readonly IDictionary<TKey, TValue> _dictionary;
        private readonly TKey _key;

        internal DictionaryPropertyDescriptor(IDictionary<TKey, TValue> dictionary, TKey key, bool isReadOnly)
            : base(key.ToString(), null)
        {
            _dictionary = dictionary;
            _key = key;
            IsReadOnly = isReadOnly;
        }

        public override Type PropertyType => _dictionary[_key]?.GetType();

        public override void SetValue(object component, object value)
        {
            _dictionary[_key] = value as TValue;
        }

        public override object GetValue(object component)
        {
            return _dictionary[_key];
        }

        public override bool IsReadOnly { get; }

        public override Type ComponentType => null;

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override void ResetValue(object component)
        {
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        public override object GetEditor(Type editorBaseType)
        {
            if (editorBaseType == typeof(UITypeEditor))
            {
                if (PropertyType == typeof(string))
                {
                    var value = _dictionary[_key] as string;
                    if (value != null && (value.Contains("\n") || value.Contains("\r")))
                    {
                        return new System.ComponentModel.Design.MultilineStringEditor();
                    }
                }
            }

            return base.GetEditor(editorBaseType);
        }
    }
}
