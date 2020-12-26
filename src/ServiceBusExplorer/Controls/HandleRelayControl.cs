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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.UIHelpers;
using ServiceBusExplorer.Utilities.Helpers;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

#endregion

namespace ServiceBusExplorer.Controls
{
    public partial class HandleRelayControl : UserControl
    {
        #region Private Constants
        //***************************
        // Formats
        //***************************
        private const string ExceptionFormat = "Exception: {0}";
        private const string InnerExceptionFormat = "InnerException: {0}";

        //***************************
        // Indexes
        //***************************
        private const int RequiresClientAuthorizationIndex = 0;
        private const int RequiresTransportSecurityIndex = 1;
        private const int IsAnonymousAccessibleIndex = 6;

        //***************************
        // Texts
        //***************************
        private const string DeleteText = "Delete";
        private const string CreateText = "Create";
        private const string UpdateText = "Update";
        private const string CancelText = "Cancel";
        private const string UserMetadata = "User Metadata";

        //***************************
        // Pages
        //***************************
        private const string AuthorizationPage = "tabPageAuthorization";

        //***************************
        // Messages
        //***************************
        private const string PathCannotBeNull = "The Path field cannot be null.";
        private const string AuthorizationRuleDeleteMessage = "The Authorization Rule will be permanently deleted";
        private const string KeyNameCannotBeNull = "Authorization Rule [{0}]: the KeyName cannot be null";

        //***************************
        // Tooltips
        //***************************
        private const string PathTooltip = "Gets or sets the relay path.";
        private const string UserMetadataTooltip = "Gets or sets the user metadata.";

        //***************************
        // Property Labels
        //***************************
        private const string CreatedAt = "Created At";
        private const string UpdatedAt = "Updated At";
        private const string CollectionName = "Collection Name";
        private const string IsReadOnly = "Is ReadOnly";
        private const string IsDynamic = "Is Dynamic";
        private const string ListenerCount = "Listener Count";
        private const string RelayEntity = "Relay";
        #endregion

        #region Private Fields
        private RelayDescription relayDescription;
        private readonly ServiceBusHelper serviceBusHelper;
        private readonly WriteToLogDelegate writeToLog;
        private readonly string path;
        private readonly List<TabPage> hiddenPages = new List<TabPage>();
        #endregion

        #region Private Static Fields
        private static readonly List<string> operators = new List<string> { "ge", "gt", "le", "lt", "eq", "ne" };
        private static readonly List<string> timeGranularityList = new List<string> { "PT5M", "PT1H", "P1D", "P7D" };
        private static readonly List<string> claimTypes = new List<string> { "NameIdentifier", "Upn", "Role", "SharedAccessKey" };
        #endregion

        #region Public Constructors
        public HandleRelayControl(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper, RelayDescription relayDescription, string path)
        {
            this.writeToLog = writeToLog;
            this.serviceBusHelper = serviceBusHelper;
            this.relayDescription = relayDescription;
            this.path = path;

            InitializeComponent();
            InitializeControls();
        } 
        #endregion

        #region Public Events
        public event Action OnCancel;
        public event Action OnRefresh;
        #endregion

        #region Public Methods
        public void RefreshData(RelayDescription relayService)
        {
            try
            {
                relayDescription = relayService;
                InitializeData();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        #endregion

        #region Private Methods
        // ReSharper disable once FunctionComplexityOverflow
        private void InitializeControls()
        {
            // RelayType
            if (cboRelayType.Items.Count == 0)
            {
                var values = Enum.GetNames(typeof (RelayType));
                // ReSharper disable once CoVariantArrayConversion
                cboRelayType.Items.AddRange(values);
            }
            if (cboRelayType.Items.Count > 0)
            {
                cboRelayType.SelectedIndex = 0;
            }
            
            // IsAnonymousAccessible
            if (serviceBusHelper.IsCloudNamespace)
            {
                if (checkedListBox.Items.Count > IsAnonymousAccessibleIndex)
                {
                    checkedListBox.Items.RemoveAt(IsAnonymousAccessibleIndex);
                }
            }

            // Set Grid style
            authorizationRulesDataGridView.EnableHeadersVisualStyles = false;

            // Set the selection background color for all the cells.
            authorizationRulesDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
            authorizationRulesDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

            // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
            // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
            authorizationRulesDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

            // Set the background color for all rows and for alternating rows.  
            // The value for alternating rows overrides the value for all rows. 
            authorizationRulesDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
            authorizationRulesDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

            // Set the row and column header styles.
            authorizationRulesDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            authorizationRulesDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
            authorizationRulesDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            authorizationRulesDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

            authorizationRulesDataGridView.AutoGenerateColumns = false;
            if (authorizationRulesDataGridView.Columns.Count == 0)
            {
                authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "IssuerName", DataPropertyName = "IssuerName" });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewComboBoxColumn { Name = "ClaimType", DataPropertyName = "ClaimType", DataSource = claimTypes, FlatStyle = FlatStyle.Flat });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "ClaimValue", DataPropertyName = "ClaimValue" });
                if (serviceBusHelper.IsCloudNamespace)
                {
                    authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "KeyName", DataPropertyName = "KeyName" });
                    authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrimaryKey", DataPropertyName = "PrimaryKey" });
                    authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "SecondaryKey", DataPropertyName = "SecondaryKey" });
                }
                authorizationRulesDataGridView.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Manage", DataPropertyName = "Manage", Width = 50 });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Send", DataPropertyName = "Send", Width = 50 });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Listen", DataPropertyName = "Listen", Width = 50 });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Revision", DataPropertyName = "Revision", Width = 50, ReadOnly = true });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "CreatedTime", DataPropertyName = "CreatedTime", ReadOnly = true });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "ModifiedTime", DataPropertyName = "ModifiedTime", ReadOnly = true });
            }

            if (relayDescription != null)
            {
                // Initialize buttons
                btnCreateDelete.Text = DeleteText;
                btnCancelUpdate.Text = UpdateText;
                btnRefresh.Visible = true;

                // Initialize textboxes
                txtPath.ReadOnly = true;
                txtPath.BackColor = SystemColors.Window;
                txtPath.ForeColor = SystemColors.ControlText;
                txtPath.GotFocus += textBox_GotFocus;

                // Initialize comboboxes
                cboRelayType.BackColor = SystemColors.Window;
                cboRelayType.Enabled = false;

                // EnableBatchedOperations
                checkedListBox.SetItemChecked(RequiresClientAuthorizationIndex, relayDescription.RequiresClientAuthorization);
                checkedListBox.SetItemChecked(RequiresTransportSecurityIndex, relayDescription.RequiresTransportSecurity);

                // Initialize Data
                InitializeData();

                checkedListBox.ItemCheck += checkedListBox_ItemCheck;

                toolTip.SetToolTip(txtPath, PathTooltip);
                toolTip.SetToolTip(txtUserMetadata, UserMetadataTooltip);

                if (relayDescription.IsDynamic)
                {
                    DisablePage(AuthorizationPage);
                    txtUserMetadata.ReadOnly = true;
                    txtUserMetadata.BackColor = SystemColors.Window;
                    txtUserMetadata.GotFocus += textBox_GotFocus;
                    
                    btnCreateDelete.Visible = false;
                    btnCancelUpdate.Visible = false;
                    //btnCloseTabs.Location = btnCreateDelete.Location;
                    btnRefresh.Location = btnCancelUpdate.Location;
                }
            }
            else
            {
                // Initialize buttons
                btnCreateDelete.Text = CreateText;
                btnCancelUpdate.Text = CancelText;
                btnRefresh.Visible = false;

                checkedListBox.SetItemChecked(RequiresClientAuthorizationIndex, true);
                checkedListBox.SetItemChecked(RequiresTransportSecurityIndex, true);

                // Create BindingList for Authorization Rules
                var bindingList = new BindingList<AuthorizationRuleWrapper>(new List<AuthorizationRuleWrapper>())
                {
                    AllowEdit = true,
                    AllowNew = true,
                    AllowRemove = true
                };
                bindingList.ListChanged += bindingList_ListChanged;
                authorizationRulesBindingSource.DataSource = bindingList;
                authorizationRulesDataGridView.DataSource = authorizationRulesBindingSource;

                if (!string.IsNullOrWhiteSpace(path))
                {
                    txtPath.Text = path;
                }
                txtPath.Focus();
            }
        }

        private void EnablePage(string pageName)
        {
            var page =
                hiddenPages.FirstOrDefault(
                    p => string.Compare(p.Name, pageName, StringComparison.InvariantCultureIgnoreCase) == 0);
            if (page == null)
            {
                return;
            }
            mainTabControl.TabPages.Add(page);
            hiddenPages.Remove(page);
        }

        private void DisablePage(string pageName)
        {
            var page = mainTabControl.TabPages[pageName];
            if (page == null)
            {
                return;
            }
            mainTabControl.TabPages.Remove(page);
            hiddenPages.Add(page);
        }

        private void bindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                if (relayDescription != null && 
                    relayDescription.Authorization.Count > 0 && 
                    relayDescription.Authorization.Count > e.NewIndex)
                {
                    var rule = relayDescription.Authorization.ElementAt(e.NewIndex);
                    if (rule != null)
                    {
                        relayDescription.Authorization.Remove(rule);
                    }
                }
            }
        }

        private void InitializeData()
        {
            // Authorization Rules
            BindingList<AuthorizationRuleWrapper> bindingList;
            if (relayDescription.Authorization.Count > 0)
            {
                var enumerable = relayDescription.Authorization.Select(r => new AuthorizationRuleWrapper(r));
                bindingList = new BindingList<AuthorizationRuleWrapper>(enumerable.ToList())
                {
                    AllowEdit = true,
                    AllowNew = true,
                    AllowRemove = true
                };

            }
            else
            {
                bindingList = new BindingList<AuthorizationRuleWrapper>(new List<AuthorizationRuleWrapper>())
                {
                    AllowEdit = true,
                    AllowNew = true,
                    AllowRemove = true
                };
            }
            bindingList.ListChanged += bindingList_ListChanged;
            authorizationRulesBindingSource.DataSource = new BindingList<AuthorizationRuleWrapper>(bindingList);
            authorizationRulesDataGridView.DataSource = authorizationRulesBindingSource;
            cboRelayType.SelectedItem = relayDescription.RelayType.ToString();

            // Initialize property grid
            var propertyList = new List<string[]>();

            propertyList.AddRange(new[]{new[]{IsReadOnly, relayDescription.IsReadOnly.ToString()},
                                            new[]{IsDynamic, relayDescription.IsDynamic.ToString()},
                                            new[]{ListenerCount, relayDescription.ListenerCount.ToString()},
                                            new[]{CreatedAt, relayDescription.CreatedAt.ToString(CultureInfo.CurrentCulture)},
                                            new[]{UpdatedAt, relayDescription.UpdatedAt.ToString(CultureInfo.CurrentCulture)},
                                            new[]{CollectionName, relayDescription.CollectionName}
                                            });

            propertyListView.Items.Clear();
            foreach (var array in propertyList)
            {
                propertyListView.Items.Add(new ListViewItem(array));
            }

            // Path
            if (!string.IsNullOrWhiteSpace(relayDescription.Path))
            {
                txtPath.Text = relayDescription.Path;
            }

            // UserMetadata
            if (!string.IsNullOrWhiteSpace(relayDescription.UserMetadata))
            {
                txtUserMetadata.Text = relayDescription.UserMetadata;
            }
        }

        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (relayDescription == null)
            {
                return;
            }
            if (e.Index == RequiresClientAuthorizationIndex)
            {
                e.NewValue = relayDescription.RequiresClientAuthorization ? CheckState.Checked : CheckState.Unchecked;
            }
            if (e.Index == RequiresTransportSecurityIndex)
            {
                e.NewValue = relayDescription.RequiresTransportSecurity ? CheckState.Checked : CheckState.Unchecked;
            }
        }

        // ReSharper disable once FunctionComplexityOverflow
        private async void btnCreateDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (serviceBusHelper == null)
                {
                    return;
                }
                if (btnCreateDelete.Text == DeleteText)
                {
                    using (var deleteForm = new DeleteForm(relayDescription.Path, RelayEntity.ToLower()))
                    {
                        if (deleteForm.ShowDialog() == DialogResult.OK)
                        {
                            await serviceBusHelper.NamespaceManager.DeleteRelayAsync(relayDescription.Path);
                            //serviceBusHelper.DeleteRelay(relayDescription.Path);
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(txtPath.Text))
                    {
                        writeToLog(PathCannotBeNull);
                        return;
                    }
                    var description = new RelayDescription(txtPath.Text, (RelayType)Enum.Parse(typeof(RelayType), cboRelayType.Text, true))
                    {
                        UserMetadata = txtUserMetadata.Text,
                        RequiresClientAuthorization = checkedListBox.GetItemChecked(RequiresClientAuthorizationIndex),
                        RequiresTransportSecurity = checkedListBox.GetItemChecked(RequiresTransportSecurityIndex)
                    };

                    var bindingList = authorizationRulesBindingSource.DataSource as BindingList<AuthorizationRuleWrapper>;
                    if (bindingList != null)
                    {
                        for (var i = 0; i < bindingList.Count; i++)
                        {
                            var rule = bindingList[i];
                            if (serviceBusHelper.IsCloudNamespace)
                            {
                                if (string.IsNullOrWhiteSpace(rule.KeyName))
                                {
                                    writeToLog(string.Format(KeyNameCannotBeNull, i));
                                    continue;
                                }
                            }
                            var rightList = new List<AccessRights>();
                            if (rule.Manage)
                            {
                                rightList.AddRange(new[] { AccessRights.Manage, AccessRights.Send, AccessRights.Listen });
                            }
                            else
                            {
                                if (rule.Send)
                                {
                                    rightList.Add(AccessRights.Send);
                                }
                                if (rule.Listen)
                                {
                                    rightList.Add(AccessRights.Listen);
                                }
                            }
                            if (serviceBusHelper.IsCloudNamespace)
                            {
                                if (string.IsNullOrWhiteSpace(rule.SecondaryKey))
                                {
                                    description.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                                                                                    rule.PrimaryKey ?? SharedAccessAuthorizationRule.GenerateRandomKey(),
                                                                                                    rightList));
                                }
                                else
                                {
                                    description.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                                                                                    rule.PrimaryKey ?? SharedAccessAuthorizationRule.GenerateRandomKey(),
                                                                                                    rule.SecondaryKey ?? SharedAccessAuthorizationRule.GenerateRandomKey(),
                                                                                                    rightList));
                                }
                            }
                            else
                            {
                                description.Authorization.Add(new AllowRule(rule.IssuerName,
                                                                            rule.ClaimType,
                                                                            rule.ClaimValue,
                                                                            rightList));
                            }
                        }
                    }
                    relayDescription = serviceBusHelper.CreateRelay(description);
                    InitializeControls();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void HandleException(Exception ex)
        {
            if (ex == null || string.IsNullOrWhiteSpace(ex.Message))
            {
                return;
            }
            writeToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex.Message));
            if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
            {
                writeToLog(string.Format(CultureInfo.CurrentCulture, InnerExceptionFormat, ex.InnerException.Message));
            }
        }

        // ReSharper disable once FunctionComplexityOverflow
        private void btnCancelUpdate_Click(object sender, EventArgs e)
        {
            if (btnCancelUpdate.Text == CancelText)
            {
                if (OnCancel != null)
                {
                    OnCancel();
                }
            }
            else
            {
                try
                {
                    relayDescription.UserMetadata = txtUserMetadata.Text;

                    var bindingList = authorizationRulesBindingSource.DataSource as BindingList<AuthorizationRuleWrapper>;
                    if (bindingList != null)
                    {
                        for (var i = 0; i < bindingList.Count; i++)
                        {
                            var rule = bindingList[i];

                            if (serviceBusHelper.IsCloudNamespace)
                            {
                                if (string.IsNullOrWhiteSpace(rule.KeyName))
                                {
                                    writeToLog(string.Format(KeyNameCannotBeNull, i));
                                    continue;
                                }
                            }
                            var rightList = new List<AccessRights>();
                            if (rule.Manage)
                            {
                                rightList.AddRange(new[] { AccessRights.Manage, AccessRights.Send, AccessRights.Listen });
                            }
                            else
                            {
                                if (rule.Send)
                                {
                                    rightList.Add(AccessRights.Send);
                                }
                                if (rule.Listen)
                                {
                                    rightList.Add(AccessRights.Listen);
                                }
                            }
                            if (serviceBusHelper.IsCloudNamespace)
                            {
                                if (string.IsNullOrWhiteSpace(rule.PrimaryKey) && string.IsNullOrWhiteSpace(rule.SecondaryKey))
                                {
                                    relayDescription.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                                                                                         rightList));
                                }
                                else if (string.IsNullOrWhiteSpace(rule.SecondaryKey))
                                {
                                    relayDescription.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                                                                                         rule.PrimaryKey ?? SharedAccessAuthorizationRule.GenerateRandomKey(),
                                                                                                         rightList));
                                }
                                else
                                {
                                    relayDescription.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                                                                                         rule.PrimaryKey ?? SharedAccessAuthorizationRule.GenerateRandomKey(),
                                                                                                         rule.SecondaryKey ?? SharedAccessAuthorizationRule.GenerateRandomKey(),
                                                                                                         rightList));
                                }
                            }
                            else
                            {
                                relayDescription.Authorization.Add(new AllowRule(rule.IssuerName,
                                                                                 rule.ClaimType,
                                                                                 rule.ClaimValue,
                                                                                 rightList));
                            }
                        }
                    }
                    relayDescription = serviceBusHelper.UpdateRelay(relayDescription);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                    relayDescription = serviceBusHelper.GetRelay(relayDescription.Path);
                }
                finally
                {
                    InitializeControls();
                }
            }
        }

        private static void textBox_GotFocus(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                NativeMethods.HideCaret(textBox.Handle);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (OnRefresh != null)
            {
                OnRefresh();
            }
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

        private void btnOpenUserMetadataForm_Click(object sender, EventArgs e)
        {
            using (var form = new TextForm(UserMetadata, txtUserMetadata.Text))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtUserMetadata.Text = form.Content;
                }
            }
        }

        private void propertyListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            var startX = e.ColumnIndex == 0 ? -1 : e.Bounds.X;
            var endX = e.Bounds.X + e.Bounds.Width - 1;
            // Background
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(215, 228, 242)), startX, -1, e.Bounds.Width + 1, e.Bounds.Height + 1);
            // Left vertical line
            e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight), startX, -1, startX, e.Bounds.Y + e.Bounds.Height + 1);
            // TopCount horizontal line
            e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight), startX, -1, endX, -1);
            // Bottom horizontal line
            e.Graphics.DrawLine(new Pen(SystemColors.ControlDark), startX, e.Bounds.Height - 1, endX, e.Bounds.Height - 1);
            // Right vertical line
            e.Graphics.DrawLine(new Pen(SystemColors.ControlDark), endX, -1, endX, e.Bounds.Height + 1);
            var roundedFontSize = (float)Math.Round(e.Font.SizeInPoints);
            var bounds = new RectangleF(e.Bounds.X + 4, (e.Bounds.Height - 8 - roundedFontSize) / 2, e.Bounds.Width, roundedFontSize + 6);
            e.Graphics.DrawString(e.Header.Text, e.Font, new SolidBrush(SystemColors.ControlText), bounds);
        }

        private void propertyListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
        }

        private void propertyListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
        }

        private void propertyListView_Resize(object sender, EventArgs e)
        {
            try
            {
                propertyListView.SuspendDrawing();
                propertyListView.SuspendLayout();
                var width = propertyListView.Width - propertyListView.Columns[0].Width - 4;
                var scrollbars = ScrollBarHelper.GetVisibleScrollbars(propertyListView);
                if (scrollbars == ScrollBars.Vertical || scrollbars == ScrollBars.Both)
                {
                    width -= 17;
                }
                propertyListView.Columns[1].Width = width;
            }
            finally
            {
                propertyListView.ResumeLayout();
                propertyListView.ResumeDrawing();
            }
        }

        private void mainTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(mainTabControl, e, null);
        }

        private void DrawTabControlTabs(TabControl tabControl, DrawItemEventArgs e, ImageList images)
        {
            // Get the bounding end of tab strip rectangles.
            var tabstripEndRect = tabControl.GetTabRect(tabControl.TabPages.Count - 1);
            var tabstripEndRectF = new RectangleF(tabstripEndRect.X + tabstripEndRect.Width, tabstripEndRect.Y - 5,
            tabControl.Width - (tabstripEndRect.X + tabstripEndRect.Width), tabstripEndRect.Height + 5);
            var leftVerticalLineRect = new RectangleF(2, tabstripEndRect.Y + tabstripEndRect.Height + 2, 2, tabControl.TabPages[tabControl.SelectedIndex].Height + 2);
            var rightVerticalLineRect = new RectangleF(tabControl.TabPages[tabControl.SelectedIndex].Width + 4, tabstripEndRect.Y + tabstripEndRect.Height + 2, 2, tabControl.TabPages[tabControl.SelectedIndex].Height + 2);
            var bottomHorizontalLineRect = new RectangleF(2, tabstripEndRect.Y + tabstripEndRect.Height + tabControl.TabPages[tabControl.SelectedIndex].Height + 2, tabControl.TabPages[tabControl.SelectedIndex].Width + 4, 2);
            RectangleF leftVerticalBarNearFirstTab = new Rectangle(0, 0, 2, tabstripEndRect.Height + 2);

            // First, do the end of the tab strip.
            // If we have an image use it.
            if (tabControl.Parent.BackgroundImage != null)
            {
                var src = new RectangleF(tabstripEndRectF.X + tabControl.Left, tabstripEndRectF.Y + tabControl.Top, tabstripEndRectF.Width, tabstripEndRectF.Height);
                e.Graphics.DrawImage(tabControl.Parent.BackgroundImage, tabstripEndRectF, src, GraphicsUnit.Pixel);
            }
            // If we have no image, use the background color.
            else
            {
                using (Brush backBrush = new SolidBrush(tabControl.Parent.BackColor))
                {
                    e.Graphics.FillRectangle(backBrush, tabstripEndRectF);
                    e.Graphics.FillRectangle(backBrush, leftVerticalLineRect);
                    e.Graphics.FillRectangle(backBrush, rightVerticalLineRect);
                    e.Graphics.FillRectangle(backBrush, bottomHorizontalLineRect);
                    if (mainTabControl.SelectedIndex != 0)
                    {
                        e.Graphics.FillRectangle(backBrush, leftVerticalBarNearFirstTab);
                    }
                }
            }

            // Set up the page and the various pieces.
            var page = tabControl.TabPages[e.Index];
            using (var backBrush = new SolidBrush(page.BackColor))
            {
                using (var foreBrush = new SolidBrush(page.ForeColor))
                {
                    var tabName = page.Text;

                    // Set up the offset for an icon, the bounding rectangle and image size and then fill the background.
                    var iconOffset = 0;
                    Rectangle tabBackgroundRect;

                    if (e.Index == mainTabControl.SelectedIndex)
                    {
                        tabBackgroundRect = e.Bounds;
                        e.Graphics.FillRectangle(backBrush, tabBackgroundRect);
                    }
                    else
                    {
                        tabBackgroundRect = new Rectangle(e.Bounds.X, e.Bounds.Y - 2, e.Bounds.Width,
                                                          e.Bounds.Height + 4);
                        e.Graphics.FillRectangle(backBrush, tabBackgroundRect);
                        var rect = new Rectangle(e.Bounds.X - 2, e.Bounds.Y - 2, 1, 2);
                        e.Graphics.FillRectangle(backBrush, rect);
                        rect = new Rectangle(e.Bounds.X - 1, e.Bounds.Y - 2, 1, 2);
                        e.Graphics.FillRectangle(backBrush, rect);
                        rect = new Rectangle(e.Bounds.X + e.Bounds.Width, e.Bounds.Y - 2, 1, 2);
                        e.Graphics.FillRectangle(backBrush, rect);
                        rect = new Rectangle(e.Bounds.X + e.Bounds.Width + 1, e.Bounds.Y - 2, 1, 2);
                        e.Graphics.FillRectangle(backBrush, rect);
                    }

                    // If we have images, process them.
                    if (images != null)
                    {
                        // Get sice and image.
                        var size = images.ImageSize;
                        Image icon = null;
                        if (page.ImageIndex > -1)
                            icon = images.Images[page.ImageIndex];
                        else if (page.ImageKey != "")
                            icon = images.Images[page.ImageKey];

                        // If there is an image, use it.
                        if (icon != null)
                        {
                            var startPoint =
                                new Point(tabBackgroundRect.X + 2 + ((tabBackgroundRect.Height - size.Height) / 2),
                                          tabBackgroundRect.Y + 2 + ((tabBackgroundRect.Height - size.Height) / 2));
                            e.Graphics.DrawImage(icon, new Rectangle(startPoint, size));
                            iconOffset = size.Width + 4;
                        }
                    }

                    // Draw out the label.
                    var labelRect = new Rectangle(tabBackgroundRect.X + iconOffset, tabBackgroundRect.Y + 5,
                                                  tabBackgroundRect.Width - iconOffset, tabBackgroundRect.Height - 3);
                    using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                    {
                        e.Graphics.DrawString(tabName, new Font(e.Font.FontFamily, 8.25F, e.Font.Style), foreBrush, labelRect, sf);
                    }
                }
            }
        }

        private void grouperAuthorizationRuleList_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    authorizationRulesDataGridView.Location.X - 1,
                                    authorizationRulesDataGridView.Location.Y - 1,
                                    authorizationRulesDataGridView.Size.Width + 1,
                                    authorizationRulesDataGridView.Size.Height + 1);
        }

        private void authorizationRulesDataGridView_Resize(object sender, EventArgs e)
        {
            try
            {
                authorizationRulesDataGridView.SuspendDrawing();
                authorizationRulesDataGridView.SuspendLayout();
                if (authorizationRulesDataGridView.Columns["IssuerName"] == null ||
                authorizationRulesDataGridView.Columns["ClaimType"] == null ||
                authorizationRulesDataGridView.Columns["ClaimValue"] == null ||
                authorizationRulesDataGridView.Columns["Manage"] == null ||
                authorizationRulesDataGridView.Columns["Send"] == null ||
                authorizationRulesDataGridView.Columns["Listen"] == null ||
                authorizationRulesDataGridView.Columns["Revision"] == null ||
                authorizationRulesDataGridView.Columns["CreatedTime"] == null ||
                authorizationRulesDataGridView.Columns["ModifiedTime"] == null)
                {
                    return;
                }

                var width = authorizationRulesDataGridView.Width -
                            authorizationRulesDataGridView.Columns["Manage"].Width -
                            authorizationRulesDataGridView.Columns["Send"].Width -
                            authorizationRulesDataGridView.Columns["Listen"].Width -
                            authorizationRulesDataGridView.Columns["Revision"].Width -
                            authorizationRulesDataGridView.RowHeadersWidth;
                var verticalScrollbar = authorizationRulesDataGridView.Controls.OfType<VScrollBar>().First();
                if (verticalScrollbar.Visible)
                {
                    width -= verticalScrollbar.Width;
                }
                int columnWidth;
                if (serviceBusHelper.IsCloudNamespace)
                {
                    columnWidth = width / 8;
                    authorizationRulesDataGridView.Columns["IssuerName"].Width = width - (7 * columnWidth);
                    if (authorizationRulesDataGridView.Columns["KeyName"] != null &&
                        authorizationRulesDataGridView.Columns["PrimaryKey"] != null &&
                        authorizationRulesDataGridView.Columns["SecondaryKey"] != null)
                    {
                        authorizationRulesDataGridView.Columns["KeyName"].Width = columnWidth;
                        authorizationRulesDataGridView.Columns["PrimaryKey"].Width = columnWidth;
                        authorizationRulesDataGridView.Columns["SecondaryKey"].Width = columnWidth;
                    }
                }
                else
                {
                    columnWidth = width / 5;
                    authorizationRulesDataGridView.Columns["IssuerName"].Width = width - (4 * columnWidth);
                }
                authorizationRulesDataGridView.Columns["ClaimType"].Width = columnWidth;
                authorizationRulesDataGridView.Columns["ClaimValue"].Width = columnWidth;
                authorizationRulesDataGridView.Columns["CreatedTime"].Width = columnWidth;
                authorizationRulesDataGridView.Columns["ModifiedTime"].Width = columnWidth;
            }
            finally
            {
                authorizationRulesDataGridView.ResumeLayout();
                authorizationRulesDataGridView.ResumeDrawing();
            }
        }

        private void authorizationRulesDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            authorizationRulesDataGridView_Resize(sender, null);
        }

        private void authorizationRulesDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            authorizationRulesDataGridView_Resize(sender, null);
        }

        private void authorizationRulesDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            using (var deleteForm = new DeleteForm(AuthorizationRuleDeleteMessage))
            {
                e.Cancel = deleteForm.ShowDialog() == DialogResult.Cancel;
            }
        }

        private void authorizationRulesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (authorizationRulesDataGridView.Columns[e.ColumnIndex].Name == "Manage")
            {
                if (!(bool)authorizationRulesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    authorizationRulesDataGridView.Rows[e.RowIndex].Cells["Manage"].Value = true;
                    authorizationRulesDataGridView.Rows[e.RowIndex].Cells["Send"].Value = true;
                    authorizationRulesDataGridView.Rows[e.RowIndex].Cells["Listen"].Value = true;
                }
                return;
            }
            if ((authorizationRulesDataGridView.Columns[e.ColumnIndex].Name == "Send" ||
                 authorizationRulesDataGridView.Columns[e.ColumnIndex].Name == "Listen"))
            {
                if ((bool)authorizationRulesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value &&
                    (bool)authorizationRulesDataGridView.Rows[e.RowIndex].Cells["Manage"].Value)
                {
                    authorizationRulesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                    authorizationRulesDataGridView.Rows[e.RowIndex].Cells["Manage"].Value = false;
                }
            }
        }

        private void authorizationRulesDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!serviceBusHelper.IsCloudNamespace &&
                e.RowIndex == authorizationRulesDataGridView.Rows.Count - 1 &&
                string.IsNullOrWhiteSpace(authorizationRulesDataGridView.Rows[e.RowIndex].Cells["IssuerName"].Value as string))
            {
                authorizationRulesDataGridView.Rows[e.RowIndex].Cells["IssuerName"].Value = serviceBusHelper.Namespace;
            }
        }

        private void authorizationRulesDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }

                for (var i = 0; i < Controls.Count; i++)
                {
                    Controls[i].Dispose();
                }

                base.Dispose(disposing);
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }
        }

        private void grouperRelayProperties_CustomPaint(PaintEventArgs e)
        {
            if (relayDescription != null)
            {
                return;
            }
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                   cboRelayType.Location.X - 1,
                                   cboRelayType.Location.Y - 1,
                                   cboRelayType.Size.Width + 1,
                                   cboRelayType.Size.Height + 1);
        }


        #endregion
    }
}
