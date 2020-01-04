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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.UIHelpers;
using ServiceBusExplorer.Utilities.Helpers;

#endregion

namespace ServiceBusExplorer.Controls
{

    internal partial class StandardValueEditorUI : UserControl
    {
        private class TagItem
        {
            public bool SetByCode = false;
            public StandardValueAttribute Item = null;
            public TagItem(StandardValueAttribute item)
            {
                Item = item;
            }
        }
        private Type m_PropertyType = Type.Missing.GetType();
        private Type m_EnumDataType = Type.Missing.GetType();
        private object m_Value = null;
        IWindowsFormsEditorService m_editorService = null;

        private bool m_bFlag = false;
        public StandardValueEditorUI()
        {
            InitializeComponent();
        }

        public void SetData(ITypeDescriptorContext context, IWindowsFormsEditorService editorService, object value)
        {
            m_editorService = editorService;

            m_PropertyType = context.PropertyDescriptor.PropertyType;
            if (m_PropertyType.IsEnum)
            {
                m_EnumDataType = Enum.GetUnderlyingType(m_PropertyType);
                m_bFlag = (m_PropertyType.GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0);
            }

            m_Value = value;

            listViewEnum.Items.Clear();
            listViewEnum.CheckBoxes = m_bFlag;

            if (!(context.PropertyDescriptor is CustomPropertyDescriptor))
            {
                throw new Exception("PropertyDescriptorManager has not been installed on this instance.");
            }

            var customPropertyDescriptor = context.PropertyDescriptor as CustomPropertyDescriptor;

            // create list view items for the visible Enum items
            foreach (var standardValueAttribute in customPropertyDescriptor.StandardValues)
            {
                if (standardValueAttribute.Visible)
                {
                    var lvi = new ListViewItem();
                    lvi.Text = standardValueAttribute.DisplayName;
                    lvi.ForeColor = (standardValueAttribute.Enabled ? lvi.ForeColor : Color.FromKnownColor(KnownColor.GrayText));
                    lvi.Tag = new TagItem(standardValueAttribute);
                    listViewEnum.Items.Add(lvi);

                }
            }

            UpdateCheckState();

            // make initial selection
            if (m_bFlag)
            {
                // select the first checked one
                foreach (ListViewItem lvi in listViewEnum.CheckedItems)
                {
                    lvi.Selected = true;
                    lvi.EnsureVisible();
                    break;
                }
            }
            else
            {
                foreach (ListViewItem lvi in listViewEnum.Items)
                {
                    var ti = lvi.Tag as TagItem;
                    if (ti != null && ti.Item.Value.Equals(m_Value))
                    {
                        lvi.Selected = true;
                        lvi.EnsureVisible();
                        break;
                    }
                }
            }

        }

        public object GetValue()
        {
            if (m_PropertyType.IsEnum)
            {
                return Enum.ToObject(m_PropertyType, m_Value);
            }
            if (m_PropertyType == typeof(bool))
            {
                return Convert.ToBoolean(m_Value);
            }
            return m_Value;
        }

        private void listViewEnum_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var ti = listViewEnum.Items[e.Index].Tag as TagItem;

            if (ti.SetByCode)
            {
                ti.SetByCode = false;
                return;
            }
            if (!ti.Item.Enabled)
            {
                e.NewValue = e.CurrentValue;
                return;
            }

            if (e.NewValue == CheckState.Checked)
            {
                if (IsZero(m_EnumDataType, ti.Item.Value))  // user is chekcing the item with zero value ( None )
                {
                    m_Value = 0;
                }
                else
                {
                    AddBits(m_EnumDataType, ref m_Value, ti.Item.Value);
                }

            }
            else
            {
                var _copyValue = m_Value;
                RemoveBits(m_EnumDataType, ref m_Value, ti.Item.Value);

                if (IsZeroValueSituation())
                {
                    m_Value = _copyValue;
                }
            }

            e.NewValue = e.CurrentValue;
            UpdateCheckState(); // this will change the check box on the list view item
        }

        private bool IsZeroValueSituation()
        {
            if (IsZero(m_EnumDataType, m_Value))
            {
                if (!Enum.IsDefined(m_PropertyType, m_Value))
                {
                    return true;
                }
            }
            return false;
        }

        private void listViewEnum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewEnum.SelectedItems.Count > 0)
            {
                var listView = (ListView)sender;
                var ti = listView.SelectedItems[0].Tag as TagItem;

                lblDispName.Text = ti.Item.DisplayName;
                lblDesc.Text = ti.Item.Description;

                if (!m_bFlag)
                {
                    if (!ti.Item.Enabled)
                    {
                        return;
                    }
                    m_Value = ti.Item.Value;
                }
            }
        }

        private void listViewEnum_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            m_editorService.CloseDropDown();
        }

        private void listViewEnum_SizeChanged(object sender, EventArgs e)
        {
            listViewEnum.Columns[0].Width = listViewEnum.Width - 20;
            listViewEnum.Invalidate();
            lblDesc.Invalidate();
            this.Invalidate();
        }

        void UpdateCheckState()
        {
            if (!m_bFlag)
            {
                return;
            }

            foreach (ListViewItem lvi in listViewEnum.Items)
            {
                var ti = lvi.Tag as TagItem;
                var bitExist = DoBitsExist(Enum.GetUnderlyingType(m_PropertyType), m_Value, ti.Item.Value);
                if (lvi.Checked != DoBitsExist(Enum.GetUnderlyingType(m_PropertyType), m_Value, ti.Item.Value))
                {
                    ti.SetByCode = true;
                    lvi.Checked = bitExist;
                }
            }

        }

        private bool DoBitsExist(Type enumDataType, object value, object bits)
        {
            // zero needs special treatment, because you cannot do bitwise operations using zeros
            var valueIsZero = IsZero(enumDataType, value);
            var bitsIsZero = IsZero(enumDataType, bits);

            if (valueIsZero && bitsIsZero)
                return true;
            if (valueIsZero)
                return false;
            if (bitsIsZero)
                return false;

            // otherwise (!valueIsZero && !bitsIsZero)

            if (enumDataType == typeof(short))
            {
                var _value = Convert.ToInt16(value);
                var _bits = Convert.ToInt16(bits);
                return (_value & _bits) == _bits;
            }
            if (enumDataType == typeof(ushort))
            {
                var _value = Convert.ToUInt16(value);
                var _bits = Convert.ToUInt16(bits);
                return (_value & _bits) == _bits;
            }
            if (enumDataType == typeof(int))
            {
                var _value = Convert.ToInt32(value);
                var _bits = Convert.ToInt32(bits);
                return (_value & _bits) == _bits;
            }
            if (enumDataType == typeof(uint))
            {
                var _value = Convert.ToUInt32(value);
                var _bits = Convert.ToUInt32(bits);
                return (_value & _bits) == _bits;
            }
            if (enumDataType == typeof(long))
            {
                var _value = Convert.ToInt64(value);
                var _bits = Convert.ToInt64(bits);
                return (_value & _bits) == _bits;
            }
            if (enumDataType == typeof(ulong))
            {
                var _value = Convert.ToUInt64(value);
                var _bits = Convert.ToUInt64(bits);
                return (_value & _bits) == _bits;
            }
            if (enumDataType == typeof(sbyte))
            {
                var _value = Convert.ToSByte(value);
                var _bits = Convert.ToSByte(bits);
                return (_value & _bits) == _bits;
            }
            if (enumDataType == typeof(byte))
            {
                var _value = Convert.ToByte(value);
                var _bits = Convert.ToByte(bits);
                return (_value & _bits) == _bits;
            }
            return false;
        }
        private void RemoveBits(Type enumDataType, ref object value, object bits)
        {
            if (enumDataType == typeof(short))
            {
                var _value = Convert.ToInt32(value);
                var _bits = Convert.ToInt32(bits);
                _value &= ~_bits;
                value = _value;
            }
            else if (enumDataType == typeof(ushort))
            {
                var _value = Convert.ToUInt32(value);
                var _bits = Convert.ToUInt32(bits);
                _value &= ~_bits;
                value = _value;
            }
            else if (enumDataType == typeof(int))
            {
                var _value = Convert.ToInt32(value);
                var _bits = Convert.ToInt32(bits);
                _value &= ~_bits;
                value = _value;
            }
            else if (enumDataType == typeof(uint))
            {
                var _value = Convert.ToUInt32(value);
                var _bits = Convert.ToUInt32(bits);
                _value &= ~_bits;
                value = _value;
            }
            else if (enumDataType == typeof(long))
            {
                var _value = Convert.ToInt64(value);
                var _bits = Convert.ToInt64(bits);
                _value &= ~_bits;
                value = _value;
            }
            else if (enumDataType == typeof(ulong))
            {
                var _value = Convert.ToUInt64(value);
                var _bits = Convert.ToUInt64(bits);
                _value &= ~_bits;
                value = _value;
            }
            else if (enumDataType == typeof(sbyte))
            {
                var _value = Convert.ToInt32(value);
                var _bits = Convert.ToInt32(bits);
                _value &= ~_bits;
                value = _value;
            }
            else if (enumDataType == typeof(byte))
            {
                var _value = Convert.ToInt32(value);
                var _bits = Convert.ToInt32(bits);
                _value &= ~_bits;
                value = _value;
            }
        }
        private void AddBits(Type enumDataType, ref object value, object bits)
        {
            if (enumDataType == typeof(short))
            {
                var _value = Convert.ToInt32(value);
                var _bits = Convert.ToInt32(bits);
                _value |= _bits;
                value = _value;
            }
            else if (enumDataType == typeof(ushort))
            {
                var _value = Convert.ToUInt32(value);
                var _bits = Convert.ToUInt32(bits);
                _value |= _bits;
                value = _value;
            }
            else if (enumDataType == typeof(int))
            {
                var _value = Convert.ToInt32(value);
                var _bits = Convert.ToInt32(bits);
                _value |= _bits;
                value = _value;
            }
            else if (enumDataType == typeof(uint))
            {
                var _value = Convert.ToUInt32(value);
                var _bits = Convert.ToUInt32(bits);
                _value |= _bits;
                value = _value;
            }
            else if (enumDataType == typeof(long))
            {
                var _value = Convert.ToInt64(value);
                var _bits = Convert.ToInt64(bits);
                _value |= _bits;
                value = _value;
            }
            else if (enumDataType == typeof(ulong))
            {
                var _value = Convert.ToUInt64(value);
                var _bits = Convert.ToUInt64(bits);
                _value |= _bits;
                value = _value;
            }
            else if (enumDataType == typeof(sbyte))
            {
                var _value = Convert.ToInt32(value);
                var _bits = Convert.ToInt32(bits);
                _value |= _bits;
                value = _value;
            }
            else if (enumDataType == typeof(byte))
            {
                var _value = Convert.ToInt32(value);
                var _bits = Convert.ToInt32(bits);
                _value |= _bits;
                value = _value;
            }
        }
        private bool IsZero(Type enumDataType, object value)
        {
            if (enumDataType == typeof(Int16))
            {
                var _value = Convert.ToInt16(value);
                return (_value == 0);
            }
            if (enumDataType == typeof(UInt16))
            {
                var _value = Convert.ToUInt16(value);
                return (_value == 0);
            }
            if (enumDataType == typeof(Int32))
            {
                var _value = Convert.ToInt32(value);
                return (_value == 0);
            }
            if (enumDataType == typeof(UInt32))
            {
                var _value = Convert.ToUInt32(value);
                return (_value == 0);
            }
            if (enumDataType == typeof(Int64))
            {
                var _value = Convert.ToInt64(value);
                return (_value == 0);
            }
            if (enumDataType == typeof(UInt64))
            {
                var _value = Convert.ToUInt64(value);
                return (_value == 0);
            }
            if (enumDataType == typeof(SByte))
            {
                var _value = Convert.ToSByte(value);
                return (_value == 0);
            }
            if (enumDataType == typeof(Byte))
            {
                var _value = Convert.ToByte(value);
                return (_value == 0);
            }
            return false;

        }
    }

}
