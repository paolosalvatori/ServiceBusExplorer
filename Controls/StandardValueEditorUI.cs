using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Diagnostics;
using System.Reflection;
using System.Drawing;

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{

  internal partial class StandardValueEditorUI : UserControl
  {
    private class TagItem
    {
      public bool SetByCode = false;
      public StandardValueAttribute Item = null;
      public TagItem( StandardValueAttribute item )
      {
        Item = item;
      }
    }
    private Type m_PropertyType = Type.Missing.GetType( );
    private Type m_EnumDataType = Type.Missing.GetType( );
    private object m_Value = null;
    IWindowsFormsEditorService m_editorService = null;

    private bool m_bFlag = false;
    public StandardValueEditorUI()
    {
      InitializeComponent( );
    }

    public void SetData( ITypeDescriptorContext context, IWindowsFormsEditorService editorService, object value )
    {
      m_editorService = editorService;

      m_PropertyType = context.PropertyDescriptor.PropertyType;
      if (m_PropertyType.IsEnum)
      {
        m_EnumDataType = Enum.GetUnderlyingType(m_PropertyType);
        m_bFlag = (m_PropertyType.GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0);
      }

      m_Value = value;

      listViewEnum.Items.Clear( );
      listViewEnum.CheckBoxes = m_bFlag;

      if (!(context.PropertyDescriptor is CustomPropertyDescriptor))
      {
        throw new Exception("PropertyDescriptorManager has not been installed on this instance.");
      }

      CustomPropertyDescriptor cpd = context.PropertyDescriptor as CustomPropertyDescriptor;

      // create list view items for the visible Enum items
      foreach (StandardValueAttribute sva in cpd.StandardValues)
      {
        if (sva.Visible)
        {
          ListViewItem lvi = new ListViewItem( );
          lvi.Text = sva.DisplayName;
          lvi.ForeColor = (sva.Enabled == true ? lvi.ForeColor : Color.FromKnownColor(KnownColor.GrayText));
          lvi.Tag = new TagItem(sva);
          listViewEnum.Items.Add(lvi);

        }
      }

      UpdateCheckState( );

      // make initial selection
      if (m_bFlag)
      {
        // select the first checked one
        foreach (ListViewItem lvi in listViewEnum.CheckedItems)
        {
          lvi.Selected = true;
          lvi.EnsureVisible( );
          break;
        }
      }
      else
      {
        foreach (ListViewItem lvi in listViewEnum.Items)
        {
          TagItem ti = lvi.Tag as TagItem;
          if (ti.Item.Value.Equals(m_Value))
          {
            lvi.Selected = true;
            lvi.EnsureVisible( );
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
      else if (m_PropertyType == typeof(bool))
      {
        return Convert.ToBoolean(m_Value);
      }
      return m_Value;

    }

    private void listViewEnum_ItemCheck( object sender, ItemCheckEventArgs e )
    {
      TagItem ti = listViewEnum.Items[e.Index].Tag as TagItem;

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
        object _copyValue = m_Value;
        RemoveBits(m_EnumDataType, ref m_Value, ti.Item.Value);

        if (IsZeroValueSituation( ))
        {
          m_Value = _copyValue;
        }
      }

      e.NewValue = e.CurrentValue;
      UpdateCheckState( ); // this will change the check box on the list view item
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

    private void listViewEnum_SelectedIndexChanged( object sender, EventArgs e )
    {
      if (listViewEnum.SelectedItems.Count > 0)
      {
        ListView listView = (ListView)sender;
        TagItem ti = listView.SelectedItems[0].Tag as TagItem;

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

    private void listViewEnum_MouseDoubleClick( object sender, MouseEventArgs e )
    {
      m_editorService.CloseDropDown( );
    }

    private void listViewEnum_SizeChanged( object sender, EventArgs e )
    {
      listViewEnum.Columns[0].Width = listViewEnum.Width - 20;
      listViewEnum.Invalidate( );
      lblDesc.Invalidate( );
      this.Invalidate( );
    }

    void UpdateCheckState()
    {
      if (!m_bFlag)
      {
        return;
      }

      foreach (ListViewItem lvi in listViewEnum.Items)
      {
        TagItem ti = lvi.Tag as TagItem;
        bool bitExist = DoBitsExist(Enum.GetUnderlyingType(m_PropertyType), m_Value, ti.Item.Value);
        if (lvi.Checked != DoBitsExist(Enum.GetUnderlyingType(m_PropertyType), m_Value, ti.Item.Value))
        {
          ti.SetByCode = true;
          lvi.Checked = bitExist;
        }
      }

    }

    private bool DoBitsExist( Type enumDataType, object value, object bits )
    {

      /// zero needs special treatment, because you cannot do bitwise operations using zeros
      bool valueIsZero = IsZero(enumDataType, value);
      bool bitsIsZero = IsZero(enumDataType, bits);

      if (valueIsZero && bitsIsZero)
      {
        return true;
      }
      else if (valueIsZero && !bitsIsZero)
      {
        return false;
      }
      else if (!valueIsZero && bitsIsZero)
      {
        return false;
      }

      // otherwise (!valueIsZero && !bitsIsZero)

      if (enumDataType == typeof(Int16))
      {
        Int16 _value = Convert.ToInt16(value);
        Int16 _bits = Convert.ToInt16(bits);
        return ((_value & _bits) == _bits);
      }
      else if (enumDataType == typeof(UInt16))
      {
        UInt16 _value = Convert.ToUInt16(value);
        UInt16 _bits = Convert.ToUInt16(bits);
        return ((_value & _bits) == _bits);
      }
      else if (enumDataType == typeof(Int32))
      {
        Int32 _value = Convert.ToInt32(value);
        Int32 _bits = Convert.ToInt32(bits);
        return ((_value & _bits) == _bits);
      }
      else if (enumDataType == typeof(UInt32))
      {
        UInt32 _value = Convert.ToUInt32(value);
        UInt32 _bits = Convert.ToUInt32(bits);
        return ((_value & _bits) == _bits);
      }
      else if (enumDataType == typeof(Int64))
      {
        Int64 _value = Convert.ToInt64(value);
        Int64 _bits = Convert.ToInt64(bits);
        return ((_value & _bits) == _bits);
      }
      else if (enumDataType == typeof(UInt64))
      {
        UInt64 _value = Convert.ToUInt64(value);
        UInt64 _bits = Convert.ToUInt64(bits);
        return ((_value & _bits) == _bits);
      }
      else if (enumDataType == typeof(SByte))
      {
        SByte _value = Convert.ToSByte(value);
        SByte _bits = Convert.ToSByte(bits);
        return ((_value & _bits) == _bits);
      }
      else if (enumDataType == typeof(Byte))
      {
        Byte _value = Convert.ToByte(value);
        Byte _bits = Convert.ToByte(bits);
        return ((_value & _bits) == _bits);
      }
      return false;
    }
    private void RemoveBits( Type enumDataType, ref object value, object bits )
    {
      if (enumDataType == typeof(Int16))
      {
        Int32 _value = Convert.ToInt32(value);
        Int32 _bits = Convert.ToInt32(bits);
        _value &= ~(_bits);
        value = _value;
      }
      else if (enumDataType == typeof(UInt16))
      {
        UInt32 _value = Convert.ToUInt32(value);
        UInt32 _bits = Convert.ToUInt32(bits);
        _value &= ~(_bits);
        value = _value;
      }
      else if (enumDataType == typeof(Int32))
      {
        Int32 _value = Convert.ToInt32(value);
        Int32 _bits = Convert.ToInt32(bits);
        _value &= ~(_bits);
        value = _value;
      }
      else if (enumDataType == typeof(UInt32))
      {
        UInt32 _value = Convert.ToUInt32(value);
        UInt32 _bits = Convert.ToUInt32(bits);
        _value &= ~(_bits);
        value = _value;
      }
      else if (enumDataType == typeof(Int64))
      {
        Int64 _value = Convert.ToInt64(value);
        Int64 _bits = Convert.ToInt64(bits);
        _value &= ~(_bits);
        value = _value;
      }
      else if (enumDataType == typeof(UInt64))
      {
        UInt64 _value = Convert.ToUInt64(value);
        UInt64 _bits = Convert.ToUInt64(bits);
        _value &= ~(_bits);
        value = _value;
      }
      else if (enumDataType == typeof(SByte))
      {
        Int32 _value = Convert.ToInt32(value);
        Int32 _bits = Convert.ToInt32(bits);
        _value &= ~(_bits);
        value = _value;
      }
      else if (enumDataType == typeof(Byte))
      {
        Int32 _value = Convert.ToInt32(value);
        Int32 _bits = Convert.ToInt32(bits);
        _value &= ~(_bits);
        value = _value;
      }
    }
    private void AddBits( Type enumDataType, ref object value, object bits )
    {
      if (enumDataType == typeof(Int16))
      {
        Int32 _value = Convert.ToInt32(value);
        Int32 _bits = Convert.ToInt32(bits);
        _value |= _bits;
        value = _value;
      }
      else if (enumDataType == typeof(UInt16))
      {
        UInt32 _value = Convert.ToUInt32(value);
        UInt32 _bits = Convert.ToUInt32(bits);
        _value |= _bits;
        value = _value;
      }
      else if (enumDataType == typeof(Int32))
      {
        Int32 _value = Convert.ToInt32(value);
        Int32 _bits = Convert.ToInt32(bits);
        _value |= _bits;
        value = _value;
      }
      else if (enumDataType == typeof(UInt32))
      {
        UInt32 _value = Convert.ToUInt32(value);
        UInt32 _bits = Convert.ToUInt32(bits);
        _value |= _bits;
        value = _value;
      }
      else if (enumDataType == typeof(Int64))
      {
        Int64 _value = Convert.ToInt64(value);
        Int64 _bits = Convert.ToInt64(bits);
        _value |= _bits;
        value = _value;
      }
      else if (enumDataType == typeof(UInt64))
      {
        UInt64 _value = Convert.ToUInt64(value);
        UInt64 _bits = Convert.ToUInt64(bits);
        _value |= _bits;
        value = _value;
      }
      else if (enumDataType == typeof(SByte))
      {
        Int32 _value = Convert.ToInt32(value);
        Int32 _bits = Convert.ToInt32(bits);
        _value |= _bits;
        value = _value;
      }
      else if (enumDataType == typeof(Byte))
      {
        Int32 _value = Convert.ToInt32(value);
        Int32 _bits = Convert.ToInt32(bits);
        _value |= _bits;
        value = _value;
      }
    }
    private bool IsZero( Type enumDataType, object value )
    {
      if (enumDataType == typeof(Int16))
      {
        Int16 _value = Convert.ToInt16(value);
        return (_value == 0);
      }
      else if (enumDataType == typeof(UInt16))
      {
        UInt16 _value = Convert.ToUInt16(value);
        return (_value == 0);
      }
      else if (enumDataType == typeof(Int32))
      {
        Int32 _value = Convert.ToInt32(value);
        return (_value == 0);
      }
      else if (enumDataType == typeof(UInt32))
      {
        UInt32 _value = Convert.ToUInt32(value);
        return (_value == 0);
      }
      else if (enumDataType == typeof(Int64))
      {
        Int64 _value = Convert.ToInt64(value);
        return (_value == 0);
      }
      else if (enumDataType == typeof(UInt64))
      {
        UInt64 _value = Convert.ToUInt64(value);
        return (_value == 0);
      }
      else if (enumDataType == typeof(SByte))
      {
        SByte _value = Convert.ToSByte(value);
        return (_value == 0);
      }
      else if (enumDataType == typeof(Byte))
      {
        Byte _value = Convert.ToByte(value);
        return (_value == 0);
      }
      return false;

    }
  }

}
