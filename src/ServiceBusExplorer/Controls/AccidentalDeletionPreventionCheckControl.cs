using System;
using System.Drawing;
using System.Windows.Forms;

namespace ServiceBusExplorer.Controls
{
    public partial class AccidentalDeletionPreventionCheckControl : UserControl
    {
        #region Private Constants
        private const string DeletionBlockedTitle = "Deletion Blocked";
        private const string DeletionBlockedMessage = "The deletion will not proceed unless you indicate your intention by typing the name of the item shown.";
        #endregion

        #region Public Constructors
        public AccidentalDeletionPreventionCheckControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handlers
        private void pnlFlowLayout_SizeChanged(object sender, EventArgs e)
        {
            this.ClientSize = new Size(
                this.ClientSize.Width,
                pnlFlowLayout.Height);
        }
        #endregion

        #region Public Properties
        public string EntityName
        {
            get => lblEntityName.Text;
            set => lblEntityName.Text = value;
        }
        #endregion

        #region Public Methods
        public bool CheckAcceptanceAndNotifyUser()
        {
            bool isAccepted = string.Equals(
                txtEntityNameCheck.Text.Trim(),
                lblEntityName.Text,
                StringComparison.InvariantCultureIgnoreCase);

            if (!isAccepted)
            {
                MessageBox.Show(
                    FindForm(),
                    DeletionBlockedMessage,
                    DeletionBlockedTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            return isAccepted;
        }
        #endregion
    }
}
