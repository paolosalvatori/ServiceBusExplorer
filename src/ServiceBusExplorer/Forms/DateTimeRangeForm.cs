#region Using Directives

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ServiceBusExplorer.Forms
{
    public partial class DateTimeRangeForm : Form
    {
        #region Public Constructor
        public DateTimeRangeForm(DateTime? DateTimeFrom, DateTime? DateTimeTo)
        {
            InitializeComponent();
            if (DateTimeFrom != null && DateTime.TryParse(DateTimeFrom.ToString(), out _))
            {
                dateFromTimePicker.Checked = true;
                dateFromTimePicker.Value = DateTime.Parse(DateTimeFrom.ToString());
            }
            else
            {
                dateFromTimePicker.Checked = false;
            }
            if (DateTimeTo != null && DateTime.TryParse(DateTimeTo.ToString(), out _))
            {
                dateToTimePicker.Checked = true;
                dateToTimePicker.Value = DateTime.Parse(DateTimeTo.ToString());
            }
            else
            {
                dateToTimePicker.Checked = false;
            }            
        }
        #endregion

        #region Event Handlers
        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            if (dateFromTimePicker.Checked)
            {
                DateTimeFrom = dateFromTimePicker.Value;
                DateTimeFrom = new DateTime(DateTimeFrom.Value.Year, DateTimeFrom.Value.Month, DateTimeFrom.Value.Day, DateTimeFrom.Value.Hour, DateTimeFrom.Value.Minute, 0);
            }
            else
            {
                DateTimeFrom = null;
            }
            if (dateToTimePicker.Checked)
            {
                DateTimeTo = dateToTimePicker.Value;
                DateTimeTo = new DateTime(DateTimeTo.Value.Year, DateTimeTo.Value.Month, DateTimeTo.Value.Day, DateTimeTo.Value.Hour, DateTimeTo.Value.Minute, 59);
            }
            else
            {
                DateTimeTo = null;
            }

            if ((DateTimeFrom ?? DateTime.MinValue).Ticks > (DateTimeTo ?? DateTime.MaxValue).Ticks)
            {
                MessageBox.Show("From date time cannot be after To date time!", "Invalid date combination",MessageBoxButtons.OK, MessageBoxIcon.Hand);
                DialogResult = DialogResult.None;
                return;
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null)
            {
                control.ForeColor = Color.White;
            }
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null)
            {
                control.ForeColor = SystemColors.ControlText;
            }
        }
        #endregion

        #region Public Properties
        public DateTime? DateTimeFrom { get; private set; }
        public DateTime? DateTimeTo { get; private set; }
        #endregion
    }
}
