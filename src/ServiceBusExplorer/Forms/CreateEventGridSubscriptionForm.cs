using ServiceBusExplorer.Utilities.Helpers;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace ServiceBusExplorer.Forms
{
    public partial class CreateEventGridSubscriptionForm : Form
    {
        #region Private Constants
        private const string ExceptionFormat = "Exception: {0}";
        private readonly WriteToLogDelegate writeToLog = default!;
        #endregion

        #region Public Fields
        public string SubscriptionName;
        #endregion

        public CreateEventGridSubscriptionForm(WriteToLogDelegate writeToLog)
        {
            this.writeToLog = writeToLog;
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                SubscriptionName = txtSubscriptionName.Text;
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

        private void HandleException(Exception? ex)
        {
            if (string.IsNullOrWhiteSpace(ex?.Message))
            {
                return;
            }

            writeToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex?.Message));
        }

        private void CreateEventGridSubscriptionForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grouperMessages_Load(object sender, EventArgs e)
        {

        }
    }
}
