using ServiceBusExplorer.Utilities.Helpers;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace ServiceBusExplorer.Forms
{
    public partial class ReceiveEventForm : Form
    {
        #region Private Constants
        private const string ExceptionFormat = "Exception: {0}";
        private readonly WriteToLogDelegate writeToLog = default!;
        #endregion

        #region Public Fields
        public int EventCount;
        public bool GetMax = false;
        #endregion

        public ReceiveEventForm(WriteToLogDelegate writeToLog)
        {
            this.writeToLog = writeToLog;
            InitializeComponent();
        }

        private void receiveEvents_CheckedChanged(object sender, EventArgs e)
        {
            txtEventCount.Enabled = btnTop.Checked;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                GetMax = btnMax.Checked;
                EventCount = txtEventCount.Text != string.Empty ? int.Parse(txtEventCount.Text) : 0;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void HandleException(Exception ex)
        {
            if (string.IsNullOrWhiteSpace(ex?.Message))
            {
                return;
            }

            writeToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex?.Message));
        }
    }
}
