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
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class HandleRelayControl : UserControl
    {
        #region DllImports
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        #endregion

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
        private const string MetricsTabPage = "tabPageMetrics";

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
        private const string DeleteTooltip = "Delete the row.";

        //***************************
        // Property Labels
        //***************************
        private const string CreatedAt = "Created At";
        private const string UpdatedAt = "Updated At";
        private const string CollectionName = "Collection Name";
        private const string IsReadOnly = "Is ReadOnly";
        private const string IsDynamic = "Is Dynamic";
        private const string ListenerCount = "Listener Count";

        //***************************
        // Metrics Constants
        //***************************
        private const string MetricProperty = "Metric";
        private const string GranularityProperty = "Granularity";
        private const string TimeFilterOperator = "Operator";
        private const string TimeFilterValue = "Value";
        private const string TimeFilterOperator1Name = "FilterOperator1";
        private const string TimeFilterOperator2Name = "FilterOperator2";
        private const string TimeFilterValue1Name = "FilterValue1";
        private const string TimeFilterValue2Name = "FilterValue2";
        private const string DisplayNameProperty = "DisplayName";
        private const string NameProperty = "Name";
        private const string RelayEntity = "Relay";
        private const string Unknown = "Unkown";
        private const string DeleteName = "Delete";

        //***************************
        // Metrics Formats
        //***************************
        private const string MetricTabPageKeyFormat = "MetricTabPage{0}";
        private const string GrouperFormat = "Metric: [{0}] Unit: [{1}]";
        #endregion

        #region Private Fields
        private RelayDescription relayDescription;
        private readonly ServiceBusHelper serviceBusHelper;
        private readonly WriteToLogDelegate writeToLog;
        private readonly string path;
        private readonly BindingSource dataPointBindingSource = new BindingSource();
        private readonly BindingList<MetricDataPoint> dataPointBindingList;
        private readonly List<TabPage> hiddenPages = new List<TabPage>();
        private readonly List<string> metricTabPageIndexList = new List<string>();
        private readonly ManualResetEvent metricsManualResetEvent = new ManualResetEvent(false);
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
            dataPointBindingList = new BindingList<MetricDataPoint>
            {
                AllowNew = true,
                AllowEdit = true,
                AllowRemove = true
            };
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
            dataPointDataGridView.EnableHeadersVisualStyles = false;

            // Set the selection background color for all the cells.
            dataPointDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
            dataPointDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

            // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
            // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
            dataPointDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

            // Set the background color for all rows and for alternating rows.  
            // The value for alternating rows overrides the value for all rows. 
            dataPointDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
            dataPointDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;
            //filtersDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            //filtersDataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

            // Set the row and column header styles.
            dataPointDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            dataPointDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
            dataPointDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            dataPointDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

            // Initialize the DataGridView.
            dataPointBindingSource.DataSource = dataPointBindingList;
            dataPointDataGridView.AutoGenerateColumns = false;
            dataPointDataGridView.AutoSize = true;
            dataPointDataGridView.DataSource = dataPointBindingSource;
            dataPointDataGridView.ForeColor = SystemColors.WindowText;

            if (relayDescription != null)
            {
                MetricInfo.GetMetricInfoListAsync(serviceBusHelper.Namespace,
                                             RelayEntity,
                                             relayDescription.Path).ContinueWith(t => metricsManualResetEvent.Set());
            }

            if (dataPointDataGridView.Columns.Count == 0)
            {
                // Create the Metric column
                var metricColumn = new DataGridViewComboBoxColumn
                {
                    DataSource = MetricInfo.EntityMetricDictionary.ContainsKey(RelayEntity) ?
                                 MetricInfo.EntityMetricDictionary[RelayEntity] :
                                 null,
                    DataPropertyName = MetricProperty,
                    DisplayMember = DisplayNameProperty,
                    ValueMember = NameProperty,
                    Name = MetricProperty,
                    Width = 144,
                    DropDownWidth = 250,
                    FlatStyle = FlatStyle.Flat,
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
                };
                dataPointDataGridView.Columns.Add(metricColumn);

                // Create the Time Granularity column
                var timeGranularityColumn = new DataGridViewComboBoxColumn
                {
                    DataSource = timeGranularityList,
                    DataPropertyName = GranularityProperty,
                    Name = GranularityProperty,
                    Width = 72,
                    FlatStyle = FlatStyle.Flat
                };
                dataPointDataGridView.Columns.Add(timeGranularityColumn);

                // Create the Time Operator 1 column
                var operator1Column = new DataGridViewComboBoxColumn
                {
                    DataSource = operators,
                    DataPropertyName = TimeFilterOperator1Name,
                    HeaderText = TimeFilterOperator,
                    Name = TimeFilterOperator1Name,
                    Width = 72,
                    FlatStyle = FlatStyle.Flat
                };
                dataPointDataGridView.Columns.Add(operator1Column);

                // Create the Time Value 1 column
                var value1Column = new DataGridViewDateTimePickerColumn
                {
                    DataPropertyName = TimeFilterValue1Name,
                    HeaderText = TimeFilterValue,
                    Name = TimeFilterValue1Name,
                    Width = 136
                };
                dataPointDataGridView.Columns.Add(value1Column);

                // Create the Time Operator 1 column
                var operator2Column = new DataGridViewComboBoxColumn
                {
                    DataSource = operators,
                    DataPropertyName = TimeFilterOperator2Name,
                    HeaderText = TimeFilterOperator,
                    Name = TimeFilterOperator2Name,
                    Width = 72,
                    FlatStyle = FlatStyle.Flat
                };
                dataPointDataGridView.Columns.Add(operator2Column);

                // Create the Time Value 1 column
                var value2Column = new DataGridViewDateTimePickerColumn
                {
                    DataPropertyName = TimeFilterValue2Name,
                    HeaderText = TimeFilterValue,
                    Name = TimeFilterValue2Name,
                    Width = 136
                };
                dataPointDataGridView.Columns.Add(value2Column);

                // Create delete column
                var deleteButtonColumn = new DataGridViewButtonColumn
                {
                    Name = DeleteName,
                    CellTemplate = new DataGridViewDeleteButtonCell(),
                    HeaderText = string.Empty,
                    Width = 22
                };
                deleteButtonColumn.CellTemplate.ToolTipText = DeleteTooltip;
                deleteButtonColumn.UseColumnTextForButtonValue = true;
                dataPointDataGridView.Columns.Add(deleteButtonColumn);
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
            //authorizationRulesDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            //authorizationRulesDataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

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
                // Tab pages
                if (serviceBusHelper.IsCloudNamespace)
                {
                    EnablePage(MetricsTabPage);
                }
                else
                {
                    DisablePage(MetricsTabPage);
                }

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
                    btnMetrics.Location = btnRefresh.Location;
                    btnCloseTabs.Location = btnCreateDelete.Location;
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
                HideCaret(textBox.Handle);
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

        private void dataPointDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridViewColumn = dataPointDataGridView.Columns[DeleteName];
            if (dataGridViewColumn != null &&
                e.ColumnIndex == dataGridViewColumn.Index &&
                e.RowIndex > -1 &&
               !dataPointDataGridView.Rows[e.RowIndex].IsNewRow)
            {
                dataPointDataGridView.Rows.RemoveAt(e.RowIndex);
                return;
            }
            dataPointDataGridView.NotifyCurrentCellDirty(true);
        }

        private void dataPointDataGridView_Resize(object sender, EventArgs e)
        {
            CalculateLastColumnWidth();
            btnMetrics.Enabled = dataPointDataGridView.Rows.Count > 1;
        }

        private void dataPointDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalculateLastColumnWidth();
            btnMetrics.Enabled = dataPointDataGridView.Rows.Count > 1;
        }

        private void dataPointDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculateLastColumnWidth();
            btnMetrics.Enabled = dataPointDataGridView.Rows.Count > 1;
        }

        private void dataPointDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void CalculateLastColumnWidth()
        {
            if (dataPointDataGridView.Columns.Count < 5)
            {
                return;
            }
            var otherColumnsWidth = 0;
            for (var i = 1; i < dataPointDataGridView.Columns.Count; i++)
            {
                otherColumnsWidth += dataPointDataGridView.Columns[i].Width;
            }
            var width = dataPointDataGridView.Width - dataPointDataGridView.RowHeadersWidth - otherColumnsWidth;
            var verticalScrollbar = dataPointDataGridView.Controls.OfType<VScrollBar>().First();
            if (verticalScrollbar.Visible)
            {
                width -= verticalScrollbar.Width;
            }
            dataPointDataGridView.Columns[0].Width = width;
        }

        // ReSharper disable once FunctionComplexityOverflow
        private void btnMetrics_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MetricInfo.EntityMetricDictionary.ContainsKey(RelayEntity))
                {
                    return;
                }
                if (metricTabPageIndexList.Count > 0)
                {
                    for (var i = 0; i < metricTabPageIndexList.Count; i++)
                    {
                        mainTabControl.TabPages.RemoveByKey(metricTabPageIndexList[i]);
                    }
                    metricTabPageIndexList.Clear();
                }
                Cursor.Current = Cursors.WaitCursor;
                if (dataPointBindingList.Count == 0)
                {
                    return;
                }
                foreach (var item in dataPointBindingList)
                {
                    item.Entity = relayDescription.Path;
                    item.Type = RelayEntity;
                }
                BindingList<MetricDataPoint> pointBindingList;
                var allDataPoint = dataPointBindingList.FirstOrDefault(m => string.Compare(m.Metric, "all", StringComparison.OrdinalIgnoreCase) == 0);
                if (allDataPoint != null)
                {
                    pointBindingList = new BindingList<MetricDataPoint>();
                    foreach (var item in MetricInfo.EntityMetricDictionary[RelayEntity])
                    {
                        if (string.Compare(item.Name, "all", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            continue;
                        }
                        pointBindingList.Add(new MetricDataPoint
                        {
                            Entity = allDataPoint.Entity,
                            FilterOperator1 = allDataPoint.FilterOperator1,
                            FilterOperator2 = allDataPoint.FilterOperator2,
                            FilterValue1 = allDataPoint.FilterValue1,
                            FilterValue2 = allDataPoint.FilterValue2,
                            Granularity = allDataPoint.Granularity,
                            Graph = allDataPoint.Graph,
                            Metric = item.Name,
                            Type = allDataPoint.Type
                        });
                    }
                }
                else
                {
                    pointBindingList = dataPointBindingList;
                }
                var uris = MetricHelper.BuildUriListForDataPointMetricQueries(MainForm.SingletonMainForm.SubscriptionId,
                    serviceBusHelper.Namespace,
                    pointBindingList);
                var uriList = uris as IList<Uri> ?? uris.ToList();
                if (uris == null || !uriList.Any())
                {
                    return;
                }
                var metricData = MetricHelper.ReadMetricDataUsingTasks(uriList,
                    MainForm.SingletonMainForm.CertificateThumbprint);
                var metricList = metricData as IList<IEnumerable<MetricValue>> ?? metricData.ToList();
                if (metricData == null && metricList.Count == 0)
                {
                    return;
                }
                for (var i = 0; i < metricList.Count; i++)
                {
                    if (metricList[i] == null || !metricList[i].Any())
                    {
                        continue;
                    }
                    var key = string.Format(MetricTabPageKeyFormat, i);
                    var metricInfo = MetricInfo.EntityMetricDictionary[RelayEntity].FirstOrDefault(m => m.Name == pointBindingList[i].Metric);
                    var friendlyName = metricInfo != null ? metricInfo.DisplayName : pointBindingList[i].Metric;
                    var unit = metricInfo != null ? metricInfo.Unit : Unknown;
                    mainTabControl.TabPages.Add(key, friendlyName);
                    metricTabPageIndexList.Add(key);
                    var tabPage = mainTabControl.TabPages[key];
                    tabPage.BackColor = Color.FromArgb(215, 228, 242);
                    tabPage.ForeColor = SystemColors.ControlText;
                    var control = new MetricValueControl(writeToLog,
                        () => mainTabControl.TabPages.RemoveByKey(key),
                        metricList[i],
                        pointBindingList[i],
                        metricInfo)
                    {
                        Location = new Point(0, 0),
                        Dock = DockStyle.Fill,
                        Tag = string.Format(GrouperFormat, friendlyName, unit)
                    };
                    mainTabControl.TabPages[key].Controls.Add(control);
                    btnCloseTabs.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnCloseTabs_Click(object sender, EventArgs e)
        {
            if (metricTabPageIndexList.Count > 0)
            {
                for (var i = 0; i < metricTabPageIndexList.Count; i++)
                {
                    mainTabControl.TabPages.RemoveByKey(metricTabPageIndexList[i]);
                }
                metricTabPageIndexList.Clear();
                btnCloseTabs.Enabled = false;
            }
        }

        private void mainTabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (string.Compare(e.TabPage.Name, MetricsTabPage, StringComparison.InvariantCultureIgnoreCase) != 0)
            {
                return;
            }
            Task.Run(() =>
            {
                metricsManualResetEvent.WaitOne();
                var dataGridViewComboBoxColumn = (DataGridViewComboBoxColumn) dataPointDataGridView.Columns[MetricProperty];
                if (dataGridViewComboBoxColumn != null)
                {
                    dataGridViewComboBoxColumn.DataSource = MetricInfo.EntityMetricDictionary.ContainsKey(RelayEntity)
                        ? MetricInfo.EntityMetricDictionary[RelayEntity]
                        : null;
                }
            });
        }
        #endregion
    }
}
