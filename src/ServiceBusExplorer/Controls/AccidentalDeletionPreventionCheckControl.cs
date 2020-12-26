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

        #region Public Properties
        public string DeletionScopePromptText
        {
            get => lblDeletionScopePromptText.Text;
            set => lblDeletionScopePromptText.Text = value;
        }

        public bool DisableFurtherChecks
        {
            get => chkDisableAccidentalDeletionPrevention.Checked;
            set => chkDisableAccidentalDeletionPrevention.Checked = value;
        }
        #endregion

        #region Public Methods
        public bool CheckAcceptanceAndNotifyUser()
        {
            bool isAccepted = string.Equals(
                txtDeletionScopePromptCheck.Text.Trim(),
                lblDeletionScopePromptText.Text,
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
