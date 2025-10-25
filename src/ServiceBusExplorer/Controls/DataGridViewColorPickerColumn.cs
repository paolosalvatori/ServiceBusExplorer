using System;
using System.Windows.Forms;

namespace ServiceBusExplorer.Controls
{
    using System.ComponentModel;
    using System.Drawing;

    public class DataGridViewColorPickerColumn : DataGridViewColumn
    {
        public DataGridViewColorPickerColumn()
            : base(new ColorCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get => base.CellTemplate;
            set
            {
                if (value != null && 
                    !value.GetType().IsAssignableFrom(typeof(ColorCell)))
                {
                    throw new InvalidCastException("Must be a ColorCell");
                }
                base.CellTemplate = value;
            }
        }
    }

    public class ColorCell : DataGridViewTextBoxCell
    {
        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            if (!(DataGridView.EditingControl is ColorEditingControl colorPicker))
            {
                return;
            }
            colorPicker.Value = (Color)Value;
        }

        public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
        {
            return formattedValue;
        }

        public override Type EditType => typeof(ColorEditingControl);

        public override Type ValueType => typeof(Color);

        public override object DefaultNewRowValue => Color.Empty;

        protected override void Paint(Graphics graphics,
            Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates elementState, object value,
            object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            paintParts &= ~DataGridViewPaintParts.Focus;
            var actualColor = GetActualColor(value);
            var colorText = actualColor?.Name ?? string.Empty;
            var actualCellStyle = actualColor == null
                ? cellStyle
                : new DataGridViewCellStyle(cellStyle)
                {
                    BackColor = actualColor.Value,
                    SelectionBackColor = actualColor.Value
                };
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, colorText, errorText, actualCellStyle, advancedBorderStyle, paintParts);
        }

        private static Color? GetActualColor(object value)
        {
            Color? actualColor;
            try
            {
                actualColor = value.Equals(Color.Empty) ? (Color?) null : (Color) value;
            }
            catch (Exception)
            {
                actualColor = null;
            }
            return actualColor;
        }
    }

    class ColorEditingControl : Button, IDataGridViewEditingControl
    {
        public Color Value
        {
            get => BackColor;
            set
            {
                var actualColor = value.Equals(Color.Empty) ? (Color?) null : (Color)value;
                if (actualColor.HasValue)
                {
                    BackColor = actualColor.Value;
                    Text = actualColor.Value.Name;
                }
            }
        }

        public ColorEditingControl()
        {
            TextAlign = ContentAlignment.MiddleLeft;
            Click += ColorEditingControl_Click;
        }

        void ColorEditingControl_Click(object sender, EventArgs e)
        {
            var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                if (Value != colorDialog.Color)
                {
                    Value = colorDialog.Color;
                    EditingControlValueChanged = true;
                    EditingControlDataGridView.NotifyCurrentCellDirty(true);
                    EditingControlDataGridView.EndEdit();
                }
            }
        }

        #region Public Methods

        // Implements the IDataGridViewEditingControl.EditingControlFormattedValue  
        // property. 
        public object EditingControlFormattedValue
        {
            get => Value;
            set
            {
                if (value != null)
                {
                    Value = (Color) value;
                }
            }
        }

        // Implements the  
        // IDataGridViewEditingControl.GetEditingControlFormattedValue method. 
        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        // Implements the  
        // IDataGridViewEditingControl.ApplyCellStyleToEditingControl method. 
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            Font = dataGridViewCellStyle.Font;
        }

        // Implements the IDataGridViewEditingControl.EditingControlWantsInputKey  
        // method. 
        public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the ColorPicker handle the keys listed. 
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }

        // Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit  
        // method. 
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            // No preparation needs to be done.
        }

        #endregion

        #region Public Properties

        // Implements the IDataGridViewEditingControl.EditingControlRowIndex  
        // property. 
        public int EditingControlRowIndex { get; set; }

        // Implements the IDataGridViewEditingControl 
        // .RepositionEditingControlOnValueChange property. 
        public bool RepositionEditingControlOnValueChange => false;

        // Implements the IDataGridViewEditingControl 
        // .EditingControlDataGridView property. 
        public DataGridView EditingControlDataGridView { get; set; }

        // Implements the IDataGridViewEditingControl 
        // .EditingControlValueChanged property. 
        public bool EditingControlValueChanged { get; set; }
        
        // Implements the IDataGridViewEditingControl 
        // .EditingPanelCursor property. 
        public Cursor EditingPanelCursor => base.Cursor;

        #endregion
    }
}
