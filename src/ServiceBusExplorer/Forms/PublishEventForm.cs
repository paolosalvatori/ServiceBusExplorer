using ServiceBusExplorer.Utilities.Helpers;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace ServiceBusExplorer.Forms
{
    public partial class PublishEventForm : Form
    {
        #region Private Constants
        private const string ExceptionFormat = "Exception: {0}";
        private readonly WriteToLogDelegate writeToLog = default!;
        #endregion

        #region Public Fields
        public string EventSource = string.Empty;
        public string EventType = string.Empty;
        public string EventInfo = string.Empty;
        #endregion

        public PublishEventForm(WriteToLogDelegate writeToLog)
        {
            InitializeComponent();
            this.writeToLog = writeToLog;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try 
            {
                EventSource = txtEventSource.Text.Trim();
                EventType = txtEventType.Text.Trim();
                EventInfo = txtEventInfo.Text.Trim();

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        private void HandleException(Exception? ex)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            if (string.IsNullOrWhiteSpace(ex?.Message))
            {
                return;
            }

            writeToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex?.Message));
        }
    }
}
