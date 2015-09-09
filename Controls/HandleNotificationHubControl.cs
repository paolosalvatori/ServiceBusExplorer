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
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.NotificationHubs.Messaging;
using Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Helpers;
using Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Properties;

#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class HandleNotificationHubControl : UserControl
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
        private const string NotificationSentHeader = "Notification sent to [{0}] notification hub. ";
        private const string BodyHeader = "Body:";
        private const string HeadersHeader = "Headers:";
        private const string HeaderFormat = " - Name=[{0}] Value=[{1}]";
        private const string TagsLogHeader = "Tags:";
        private const string TagsExpressionFormat = "Tags Expression: [{0}]";
        private const string ResultsHeader = "Results:";
        private const string TagFormat = "  - Tag[{0}]";
        private const string ResultFormat = "  - RegistrationId=[{0}] PnsHandle=[{1}] ApplicationPlatform=[{2}] Outcome=[{3}]";
        private const string OutcomeFormat = "State=[{0}] Success=[{1}] Failure=[{2}] TrackingId=[{3}].";

        //***************************
        // Texts
        //***************************
        private const string HeaderName = "Name";
        private const string HeaderValue = "Value";
        private const string DeleteText = "Delete";
        private const string CreateText = "Create";
        private const string UpdateText = "Update";
        private const string CancelText = "Cancel";
        private const string ETagHeader = "ETag";
        private const string ExpirationTimeHeader = "ExpirationTime";
        private const string RegistrationIdHeader = "RegistrationId";
        private const string TagsHeader = "Tags";
        private const string ChannelUriHeader = "PNSHandle";
        private const string ChannelUriValue = "ChannelUri";
        private const string TagName = "Tag";
        private const string PlatformTypeHeader = "PlatformType";
        private const string PlatformTypeValue = "PlatformType";
        private const string DeleteRegistration = "Delete Selected Registration";
        private const string DeleteRegistrations = "Delete Selected Registrations";
        private const string UpdateRegistration = "Update Selected Registration";
        private const string UpdateRegistrations = "Update Selected Registrations";
        private const string UserMetadata = "User Metadata";
        private const string DeviceToken = "DeviceToken";
        private const string ChannelUri = "ChannelUri";
        private const string GcmRegistrationId = "GcmRegistrationId";
        private const string BodyTemplate = "BodyTemplate";
        private const string Tags = "Tags";
        private const string MpnsHeaders = "MpnsHeaders";
        private const string WnsHeaders = "WnsHeaders";
        private const string Expressions = "Expressions";
        private const string ExpressionLengths = "ExpressionLengths";
        private const string ExpressionStartIndices = "ExpressionStartIndices";

        //***************************
        // Property Labels
        //***************************
        private const string DailyOperations = "Daily Operations";
        private const string DailyMaxActiveRegistrations  = "Daily Max Active Registrations";
        private const string DailyMaxActiveDevices = "Daily Max Active Devices";

        //***************************
        // Messages
        //***************************
        private const string SelectNotificationTemplate = "Select a Notification Template...";
        private const string ManualTemplate = "Manual";
        private const string PathCannotBeNull = "The Path field cannot be null.";
        private const string DuplicateDetectionHistoryTimeWindowDaysMustBeANumber = "The Days value of the DuplicateDetectionHistoryTimeWindow field must be a number.";
        private const string DuplicateDetectionHistoryTimeWindowHoursMustBeANumber = "The Hours value of the DuplicateDetectionHistoryTimeWindow field must be a number.";
        private const string DuplicateDetectionHistoryTimeWindowMinutesMustBeANumber = "The Minutes value of the DuplicateDetectionHistoryTimeWindow field must be a number.";
        private const string DuplicateDetectionHistoryTimeWindowSecondsMustBeANumber = "The Seconds value of the DuplicateDetectionHistoryTimeWindow field must be a number.";
        private const string DuplicateDetectionHistoryTimeWindowMillisecondsMustBeANumber = "The Milliseconds value of the DuplicateDetectionHistoryTimeWindow field must be a number.";
        private const string NotificationPayloadCannotBeNull = "The notification payload cannot be null.";
        private const string JsonPayloadTemplateCannotBeNull = "The json payload cannot be null.";
        private const string PayloadIsNotInJsonFormat = "The payload is not in json format.";
        private const string RegistrationsRetrievedFormat = "[{0}] registrations were retrieved from the notification hub [{1}].";
        private const string RegistrationsDeletedFormat = "[1] registration was deleted from the notification hub [{1}]: RegistrationId=[{0}]";
        private const string RegistrationsUpdatedFormat = "[1] registration was updated in the notification hub [{1}]: RegistrationId=[{0}]";
        private const string DeleteRegistrationWarningMessage = "The selected registration will be permanently deleted.";
        private const string DeleteRegistrationsWarningMessage = "The selected registrations will be permanently deleted.";
        private const string UpdateRegistrationsWarningMessage = "The selected registrations will be updated.";
        private const string RegistrationDeletedMessage = "[{0}] registrations were deleted from the notification hub [{1}]";
        private const string RegistrationUpdatedMessage = "[{0}] registrations were updated in the notification hub [{1}]";
        private const string RegistrationCreatedMessage = "[{0}] registration was created in the notification hub [{1}]";
        private const string RegistrationPageFormat = "Page {0} of {1}";
        private const string AuthorizationRuleDeleteMessage = "The Authorization Rule will be permanently deleted";
        private const string KeyNameCannotBeNull = "Authorization Rule [{0}]: the KeyName cannot be null";
        private const string FilterExpressionTitle = "Define Filter Expression";
        private const string FilterExpressionLabel = "Filter Expression";
        private const string FilterExpressionNotValidMessage = "The filter expression [{0}] is not valid.";
        private const string FilterExpressionAppliedMessage = "The filter expression [{0}] has been successfully applied. [{1}] registrations retrieved.";
        private const string FilterExpressionRemovedMessage = "The filter expression has been removed.";

        //***************************
        // Tooltips
        //***************************
        private const string PathTooltip = "Gets or sets the queue path.";
        private const string DuplicateDetectionHistoryTimeWindowTooltip = "Gets or sets the registration time to live.";
        private const string PackageSidTooltip = "A package SID is needed to send push notifications to Windows devices. You can get the package SID by registering your application with the Windows Store.";
        private const string ClientSecretTooltip = "Client Secret is a unique value that authenticates your Windows Store app with Windows Live. You can get the Client Secret by registering your application with the Windows Store.";
        private const string MpnsCertificateThumbprintTooltip = "The certificate authenticates your application with the Microsoft Push Notification Services used by Windows Phone devices.";
        private const string ApnsCertificateThumbprintTooltip = "This certificate authenticates your app to Apple Push Notification Services.";
        private const string GcmApiKeyTooltip = "The API key is a unique value that authenticates your Android app with Google Cloud Messaging. You can obtain an API key by registering your app in the Google APIs console.";
        private const string UserMetadataTooltip = "Gets or sets the user metadata.";
        private const string DeleteTooltip = "Delete the row.";

        //***************************
        // Pages
        //***************************
        private const string DescriptionPage = "tabPageDescription";
        private const string TemplateNotificationPage = "tabPageTemplateNotification";
        private const string WindowsPhoneNativeNotificationPage = "tabPageMpnsNativeNotification";
        private const string WindowsNativeNotificationPage = "tabPageWnsNativeNotification";
        private const string AppleNativeNotificationPage = "tabPageAppleNativeNotification";
        private const string GoogleNativeNotificationPage = "tabPageGoogleNativeNotification";
        private const string RegistrationsPage = "tabPageRegistrations";
        private const string AuthorizationPage = "tabPageAuthorization";
        private const string MetricsTabPage = "tabPageMetrics";

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
        private const string NotificationHubEntity = "Notification Hub";
        private const string Unknown = "Unkown";
        private const string DeleteName = "Delete";

        //***************************
        // Metrics Formats
        //***************************
        private const string MetricTabPageKeyFormat = "MetricTabPage{0}";
        private const string GrouperFormat = "Metric: [{0}] Unit: [{1}]";
        #endregion

        #region Private Fields
        private readonly List<CollectionQueryResult<RegistrationDescription>> registrationPageList = new List<CollectionQueryResult<RegistrationDescription>>();
        private int currentRegistrationPage = -1;
        private NotificationHubDescription notificationHubDescription;
        private readonly ServiceBusHelper serviceBusHelper;
        private readonly WriteToLogDelegate writeToLog;
        private List<MethodInfo> mpnsMethodInfoList;
        private List<MethodInfo> wnsMethodInfoList;
        private string mpnsPayload;
        private string wnsPayload;
        private string apnsPayload;
        private string gcmPayload;
        private readonly List<TabPage> hiddenPages = new List<TabPage>();
        private string mpnsCredentialCertificatePath;
        private string mpnsCredentialCertificateKey;
        private string apnsCredentialCertificatePath;
        private string apnsCredentialCertificateKey;
        private NotificationHubClient notificationHubClient;
        private GetRegistrationsMethod currentGetRegistrationsMethod = GetRegistrationsMethod.All;
        private int currentTopCount = 100;
        private string currentPnsHandle;
        private string currentTagName;
        private int currentRegistrationRowIndex;
        private bool sorting;
        private string registrationsFilterExpression;
        private readonly List<string> metricTabPageIndexList = new List<string>();
        private readonly BindingSource dataPointBindingSource = new BindingSource();
        private readonly BindingList<MetricDataPoint> dataPointBindingList;
        private readonly ManualResetEvent metricsManualResetEvent = new ManualResetEvent(false);
        #endregion

        #region Private Static Fields
        private static readonly List<string> operators = new List<string> { "ge", "gt", "le", "lt", "eq", "ne" };
        private static readonly List<string> timeGranularityList = new List<string> { "PT5M", "PT1H", "P1D", "P7D" };
        private static readonly List<string> claimTypes = new List<string> { "NameIdentifier", "Upn", "Role", "SharedAccessKey" };
        #endregion

        #region Public Constructors
        public HandleNotificationHubControl(WriteToLogDelegate writeToLog, 
                                            ServiceBusHelper serviceBusHelper, 
                                            NotificationHubDescription notificationHubDescription)
        {
            this.writeToLog = writeToLog;
            this.serviceBusHelper = serviceBusHelper;
            this.notificationHubDescription = notificationHubDescription;
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
        public void RefreshData(NotificationHubDescription hubDescription)
        {
            try
            {
                notificationHubDescription = hubDescription;
                InitializeData();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public async void GetRegistrations(bool showForm, string continuationToken)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (showForm)
                {

                    using (var registrationsForm = new RegistrationsForm(writeToLog))
                    {
                        if (registrationsForm.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }

                        if (notificationHubClient == null)
                        {
                            return;
                        }
                        currentGetRegistrationsMethod = registrationsForm.GetRegistrationsMethod;
                        currentTopCount = registrationsForm.TopCount;
                        currentTagName = registrationsForm.TagName;
                        currentPnsHandle = registrationsForm.PnsHandle;
                    }
                }
                CollectionQueryResult<RegistrationDescription> collectionQueryResult = null;
                if (string.IsNullOrWhiteSpace(continuationToken))
                {
                    registrationPageList.Clear();
                    currentRegistrationPage = -1;
                }
                switch (currentGetRegistrationsMethod)
                {
                    case GetRegistrationsMethod.All:
                        {
                            collectionQueryResult = string.IsNullOrWhiteSpace(continuationToken) ? 
                                                    await notificationHubClient.GetAllRegistrationsAsync(currentTopCount) :
                                                    await notificationHubClient.GetAllRegistrationsAsync(continuationToken, currentTopCount);
                        }
                        break;
                    case GetRegistrationsMethod.Tag:
                        {
                            collectionQueryResult = string.IsNullOrWhiteSpace(continuationToken) ?
                                                    await notificationHubClient.GetRegistrationsByTagAsync(currentTagName, currentTopCount) :
                                                    await notificationHubClient.GetRegistrationsByTagAsync(currentTagName, continuationToken, currentTopCount);
                        }
                        break;
                    case GetRegistrationsMethod.PnsHandle:
                        {
                            collectionQueryResult = string.IsNullOrWhiteSpace(continuationToken) ?
                                                    await notificationHubClient.GetRegistrationsByChannelAsync(currentPnsHandle, currentTopCount) :
                                                    await notificationHubClient.GetRegistrationsByChannelAsync(currentPnsHandle, continuationToken, currentTopCount);
                        }
                        break;
                }
                if (collectionQueryResult == null)
                {
                    return;
                }
                var count = collectionQueryResult.Count();
                if (count == 0)
                {
                    if (registrationPageList.Count > 0)
                    {
                        currentRegistrationPage = registrationPageList.Count - 1;
                        SetRegistrationPage();
                    }
                    else
                    {
                        if (mainTabControl.TabPages[RegistrationsPage] == null)
                        {
                            EnablePage(RegistrationsPage);
                            mainTabControl.SelectTab(RegistrationsPage);
                            registrationsBindingSource.DataSource = null;
                            registrationsDataGridView.DataSource = registrationsBindingSource;
                            writeToLog(string.Format(RegistrationsRetrievedFormat, 0, notificationHubDescription.Path));
                        }
                    }
                    return;
                }
                registrationPageList.Add(collectionQueryResult);
                currentRegistrationPage = registrationPageList.Count - 1;
                writeToLog(string.Format(RegistrationsRetrievedFormat, count, notificationHubDescription.Path));
                SetRegistrationPage();
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
        #endregion

        #region Private Methods
        private void InitializeControls()
        {
            // Hide the test button
            btnSend.Enabled = mainTabControl.SelectedTab.Name != DescriptionPage;

            // Hide tab pages
            DisablePage(RegistrationsPage);

            // Hide caret
            txtGcmEndpoint.GotFocus += textBox_GotFocus;
            txtApnsEndpoint.GotFocus += textBox_GotFocus;
            txtMpnsCredentialCertificateThumbprint.GotFocus += textBox_GotFocus;
            txtApnsCredentialCertificateThumbprint.GotFocus += textBox_GotFocus;

            // Initialize bindingSource
            //registrationsBindingSource.DataSource = RegistrationInfo.Registrations;

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

            if (notificationHubDescription != null)
            {
                MetricInfo.GetMetricInfoListAsync(serviceBusHelper.Namespace,
                                             NotificationHubEntity,
                                             notificationHubDescription.Path).ContinueWith(t => metricsManualResetEvent.Set());
            }

            if (dataPointDataGridView.Columns.Count == 0)
            {
                // Create the Metric column
                var metricColumn = new DataGridViewComboBoxColumn
                {
                    DataSource = MetricInfo.EntityMetricDictionary.ContainsKey(NotificationHubEntity) ?
                                 MetricInfo.EntityMetricDictionary[NotificationHubEntity] :
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

            if (notificationHubDescription != null)
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
                btnRefresh.Enabled = true;
                btnRegistrations.Visible = true;
                btnMetrics.Visible = true;
                btnCloseTabs.Visible = true;

                // Initialize textboxes
                txtPath.ReadOnly = true;
                txtPath.BackColor = SystemColors.Window;
                txtPath.GotFocus += textBox_GotFocus;

                toolTip.SetToolTip(txtPath, PathTooltip);
                toolTip.SetToolTip(txtRegistrationTimeToLiveWindowDays, DuplicateDetectionHistoryTimeWindowTooltip);
                toolTip.SetToolTip(txtRegistrationTimeToLiveWindowHours, DuplicateDetectionHistoryTimeWindowTooltip);
                toolTip.SetToolTip(txtRegistrationTimeToLiveWindowMinutes, DuplicateDetectionHistoryTimeWindowTooltip);
                toolTip.SetToolTip(txtRegistrationTimeToLiveWindowSeconds, DuplicateDetectionHistoryTimeWindowTooltip);
                toolTip.SetToolTip(txtRegistrationTimeToLiveWindowMilliseconds, DuplicateDetectionHistoryTimeWindowTooltip);
                toolTip.SetToolTip(txtPackageSid, PackageSidTooltip);
                toolTip.SetToolTip(txtClientSecret, ClientSecretTooltip);
                toolTip.SetToolTip(txtMpnsCredentialCertificateThumbprint, MpnsCertificateThumbprintTooltip);
                toolTip.SetToolTip(txtApnsCredentialCertificateThumbprint, ApnsCertificateThumbprintTooltip);
                toolTip.SetToolTip(txtGcmApiKey, GcmApiKeyTooltip);
                toolTip.SetToolTip(txtUserMetadata, UserMetadataTooltip);

                // Hide Caret
                txtMpnsPayload.GotFocus += textBox_GotFocus;
                txtWnsPayload.GotFocus += textBox_GotFocus;

                // Initialize the DataGridView.
                registrationsDataGridView.AutoGenerateColumns = false;
                registrationsDataGridView.AutoSize = true;
                //registrationsDataGridView.DataSource = registrationsBindingSource;
                registrationsDataGridView.ForeColor = SystemColors.WindowText;

                // Create the ETag column
                var textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = ETagHeader,
                    Name = ETagHeader,
                    Width = 35
                };
                registrationsDataGridView.Columns.Add(textBoxColumn);

                // Create the RegistrationId column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = RegistrationIdHeader,
                    Name = RegistrationIdHeader,
                    Width = 100
                };
                registrationsDataGridView.Columns.Add(textBoxColumn);

                // Create the ChannelUri column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = ChannelUriValue,
                    Name = ChannelUriHeader,
                    Width = 200
                };
                registrationsDataGridView.Columns.Add(textBoxColumn);

                // Create the ExpirationTimeHeader column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = ExpirationTimeHeader,
                    Name = ExpirationTimeHeader,
                    Width = 110
                };
                registrationsDataGridView.Columns.Add(textBoxColumn);

                // Create the ChannelUri column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = PlatformTypeValue,
                    Name = PlatformTypeHeader,
                    Width = 100
                };
                registrationsDataGridView.Columns.Add(textBoxColumn);

                // Create the TagsHeader  column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = TagsHeader,
                    Name = TagsHeader,
                    Width = 150
                };
                registrationsDataGridView.Columns.Add(textBoxColumn);

                // Set Grid style
                registrationsDataGridView.EnableHeadersVisualStyles = false;
                //registrationsDataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // Set the selection background color for all the cells.
                registrationsDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                registrationsDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                registrationsDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                registrationsDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                registrationsDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                registrationsDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                registrationsDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                registrationsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                registrationsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Initialize bindingSource
                templatePropertiesBindingSource.DataSource = NotificationInfo.TemplateProperties;

                // Initialize the DataGridView.
                templateNotificationDataGridView.AutoGenerateColumns = false;
                templateNotificationDataGridView.AutoSize = true;
                templateNotificationDataGridView.DataSource = templatePropertiesBindingSource;
                templateNotificationDataGridView.ForeColor = SystemColors.WindowText;

                // Create the Name column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = HeaderName,
                    Name = HeaderName,
                    Width = 80
                };
                templateNotificationDataGridView.Columns.Add(textBoxColumn);

                // Create the Value column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = HeaderValue,
                    Name = HeaderValue,
                    Width = 150
                };
                templateNotificationDataGridView.Columns.Add(textBoxColumn);

                // Set Grid style
                templateNotificationDataGridView.EnableHeadersVisualStyles = false;
                //templateNotificationDataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // Set the selection background color for all the cells.
                templateNotificationDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                templateNotificationDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                templateNotificationDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                templateNotificationDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                templateNotificationDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                templateNotificationDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                templateNotificationDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                templateNotificationDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                templateNotificationDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Initialize bindingSource
                templateHeadersBindingSource.DataSource = NotificationInfo.TemplateHeaders;

                // Initialize the DataGridView.
                templateHeadersDataGridView.AutoGenerateColumns = false;
                templateHeadersDataGridView.AutoSize = true;
                templateHeadersDataGridView.DataSource = templateHeadersBindingSource;
                templateHeadersDataGridView.ForeColor = SystemColors.WindowText;

                // Create the Name column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = HeaderName,
                    Name = HeaderName,
                    Width = 80
                };
                templateHeadersDataGridView.Columns.Add(textBoxColumn);

                // Create the Value column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = HeaderValue,
                    Name = HeaderValue,
                    Width = 150
                };
                templateHeadersDataGridView.Columns.Add(textBoxColumn);

                // Set Grid style
                templateHeadersDataGridView.EnableHeadersVisualStyles = false;
                //templateHeadersDataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // Set the selection background color for all the cells.
                templateHeadersDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                templateHeadersDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                templateHeadersDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                templateHeadersDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                templateHeadersDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                templateHeadersDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                templateHeadersDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                templateHeadersDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                templateHeadersDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Initialize bindingSource
                templateTagsBindingSource.DataSource = NotificationInfo.TemplateTags;

                // Initialize the DataGridView.
                templateTagsDataGridView.AutoGenerateColumns = false;
                templateTagsDataGridView.AutoSize = true;
                templateTagsDataGridView.DataSource = templateTagsBindingSource;
                templateTagsDataGridView.ForeColor = SystemColors.WindowText;

                // Create the TagName column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = TagName,
                    Name = TagName,
                    Width = 228
                };
                templateTagsDataGridView.Columns.Add(textBoxColumn);

                // Set Grid style
                templateTagsDataGridView.EnableHeadersVisualStyles = false;
                //templateTagsDataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // Set the selection background color for all the cells.
                templateTagsDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                templateTagsDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowTagsDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                templateTagsDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                templateTagsDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                templateTagsDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                templateTagsDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                templateTagsDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                templateTagsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                templateTagsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Initialize bindingSource
                wnsHeadersBindingSource.DataSource = NotificationInfo.WnsHeaders;

                // Initialize the DataGridView.
                wnsHeadersDataGridView.AutoGenerateColumns = false;
                wnsHeadersDataGridView.AutoSize = true;
                wnsHeadersDataGridView.DataSource = wnsHeadersBindingSource;
                wnsHeadersDataGridView.ForeColor = SystemColors.WindowText;

                // Create the Name column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = HeaderName,
                    Name = HeaderName,
                    Width = 80
                };
                wnsHeadersDataGridView.Columns.Add(textBoxColumn);

                // Create the Value column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = HeaderValue,
                    Name = HeaderValue,
                    Width = 150
                };
                wnsHeadersDataGridView.Columns.Add(textBoxColumn);

                // Set Grid style
                wnsHeadersDataGridView.EnableHeadersVisualStyles = false;
                //wnsHeadersDataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // Set the selection background color for all the cells.
                wnsHeadersDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                wnsHeadersDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                wnsHeadersDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                wnsHeadersDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                wnsHeadersDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                wnsHeadersDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                wnsHeadersDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                wnsHeadersDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                wnsHeadersDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Initialize bindingSource
                wnsTagsBindingSource.DataSource = NotificationInfo.WnsTags;

                // Initialize the DataGridView.
                wnsTagsDataGridView.AutoGenerateColumns = false;
                wnsTagsDataGridView.AutoSize = true;
                wnsTagsDataGridView.DataSource = wnsTagsBindingSource;
                wnsTagsDataGridView.ForeColor = SystemColors.WindowText;

                // Create the TagName column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = TagName,
                    Name = TagName,
                    Width = 228
                };
                wnsTagsDataGridView.Columns.Add(textBoxColumn);

                // Set Grid style
                wnsTagsDataGridView.EnableHeadersVisualStyles = false;
                //wnsTagsDataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // Set the selection background color for all the cells.
                wnsTagsDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                wnsTagsDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowTagsDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                wnsTagsDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                wnsTagsDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                wnsTagsDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                wnsTagsDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                wnsTagsDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                wnsTagsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                wnsTagsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Initialize bindingSource
                mpnsHeadersBindingSource.DataSource = NotificationInfo.MpnsHeaders;

                // Initialize the DataGridView.
                mpnsHeadersDataGridView.AutoGenerateColumns = false;
                mpnsHeadersDataGridView.AutoSize = true;
                mpnsHeadersDataGridView.DataSource = mpnsHeadersBindingSource;
                mpnsHeadersDataGridView.ForeColor = SystemColors.WindowText;

                // Create the Name column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = HeaderName,
                    Name = HeaderName,
                    Width = 80
                };
                mpnsHeadersDataGridView.Columns.Add(textBoxColumn);

                // Create the Value column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = HeaderValue,
                    Name = HeaderValue,
                    Width = 150
                };
                mpnsHeadersDataGridView.Columns.Add(textBoxColumn);

                // Set Grid style
                mpnsHeadersDataGridView.EnableHeadersVisualStyles = false;
                //mpnsHeadersDataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // Set the selection background color for all the cells.
                mpnsHeadersDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                mpnsHeadersDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                mpnsHeadersDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                mpnsHeadersDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                mpnsHeadersDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                mpnsHeadersDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                mpnsHeadersDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                mpnsHeadersDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                mpnsHeadersDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Initialize bindingSource
                mpnsTagsBindingSource.DataSource = NotificationInfo.MpnsTags;

                // Initialize the DataGridView.
                mpnsTagsDataGridView.AutoGenerateColumns = false;
                mpnsTagsDataGridView.AutoSize = true;
                mpnsTagsDataGridView.DataSource = mpnsTagsBindingSource;
                mpnsTagsDataGridView.ForeColor = SystemColors.WindowText;

                // Create the TagName column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = TagName,
                    Name = TagName,
                    Width = 228
                };
                mpnsTagsDataGridView.Columns.Add(textBoxColumn);

                // Set Grid style
                mpnsTagsDataGridView.EnableHeadersVisualStyles = false;
                //mpnsTagsDataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // Set the selection background color for all the cells.
                mpnsTagsDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                mpnsTagsDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowTagsDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                mpnsTagsDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                mpnsTagsDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                mpnsTagsDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                mpnsTagsDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                mpnsTagsDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                mpnsTagsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                mpnsTagsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Initialize bindingSource
                appleHeadersBindingSource.DataSource = NotificationInfo.ApnsHeaders;

                // Initialize the DataGridView.
                appleHeadersDataGridView.AutoGenerateColumns = false;
                appleHeadersDataGridView.AutoSize = true;
                appleHeadersDataGridView.DataSource = appleHeadersBindingSource;
                appleHeadersDataGridView.ForeColor = SystemColors.WindowText;

                // Create the Name column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = HeaderName,
                    Name = HeaderName,
                    Width = 80
                };
                appleHeadersDataGridView.Columns.Add(textBoxColumn);

                // Create the Value column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = HeaderValue,
                    Name = HeaderValue,
                    Width = 150
                };
                appleHeadersDataGridView.Columns.Add(textBoxColumn);

                // Set Grid style
                appleHeadersDataGridView.EnableHeadersVisualStyles = false;
                //appleHeadersDataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // Set the selection background color for all the cells.
                appleHeadersDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                appleHeadersDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                appleHeadersDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                appleHeadersDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                appleHeadersDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                appleHeadersDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                appleHeadersDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                appleHeadersDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                appleHeadersDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Initialize bindingSource
                appleTagsBindingSource.DataSource = NotificationInfo.ApnsTags;

                // Initialize the DataGridView.
                appleTagsDataGridView.AutoGenerateColumns = false;
                appleTagsDataGridView.AutoSize = true;
                appleTagsDataGridView.DataSource = appleTagsBindingSource;
                appleTagsDataGridView.ForeColor = SystemColors.WindowText;

                // Create the TagName column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = TagName,
                    Name = TagName,
                    Width = 228
                };
                appleTagsDataGridView.Columns.Add(textBoxColumn);

                // Set Grid style
                appleTagsDataGridView.EnableHeadersVisualStyles = false;
                //appleTagsDataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // Set the selection background color for all the cells.
                appleTagsDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                appleTagsDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowTagsDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                appleTagsDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                appleTagsDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                appleTagsDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                appleTagsDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                appleTagsDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                appleTagsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                appleTagsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                //--------------------------------
                // Initialize bindingSource
                gcmHeadersBindingSource.DataSource = NotificationInfo.GcmHeaders;

                // Initialize the DataGridView.
                gcmHeadersDataGridView.AutoGenerateColumns = false;
                gcmHeadersDataGridView.AutoSize = true;
                gcmHeadersDataGridView.DataSource = gcmHeadersBindingSource;
                gcmHeadersDataGridView.ForeColor = SystemColors.WindowText;

                // Create the Name column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = HeaderName,
                    Name = HeaderName,
                    Width = 80
                };
                gcmHeadersDataGridView.Columns.Add(textBoxColumn);

                // Create the Value column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = HeaderValue,
                    Name = HeaderValue,
                    Width = 150
                };
                gcmHeadersDataGridView.Columns.Add(textBoxColumn);

                // Set Grid style
                gcmHeadersDataGridView.EnableHeadersVisualStyles = false;
                //gcmHeadersDataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // Set the selection background color for all the cells.
                gcmHeadersDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                gcmHeadersDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                gcmHeadersDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                gcmHeadersDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                gcmHeadersDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                gcmHeadersDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                gcmHeadersDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                gcmHeadersDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                gcmHeadersDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Initialize bindingSource
                gcmTagsBindingSource.DataSource = NotificationInfo.GcmTags;

                // Initialize the DataGridView.
                gcmTagsDataGridView.AutoGenerateColumns = false;
                gcmTagsDataGridView.AutoSize = true;
                gcmTagsDataGridView.DataSource = gcmTagsBindingSource;
                gcmTagsDataGridView.ForeColor = SystemColors.WindowText;

                // Create the TagName column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = TagName,
                    Name = TagName,
                    Width = 228
                };
                gcmTagsDataGridView.Columns.Add(textBoxColumn);

                // Set Grid style
                gcmTagsDataGridView.EnableHeadersVisualStyles = false;
                //gcmTagsDataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // Set the selection background color for all the cells.
                gcmTagsDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                gcmTagsDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowTagsDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                gcmTagsDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                gcmTagsDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                gcmTagsDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                gcmTagsDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                gcmTagsDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                gcmTagsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                gcmTagsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set Grid style
                registrationsDataGridView.EnableHeadersVisualStyles = false;

                // Set the selection background color for all the cells.
                registrationsDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                registrationsDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                registrationsDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                registrationsDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                registrationsDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;
                //sessionsDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
                //sessionsDataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                registrationsDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                registrationsDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                registrationsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                registrationsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set splitter width
                templateSplitContainer.SplitterWidth = 16;
                registrationsSplitContainer.SplitterWidth = 16;
                templateSplitContainer1.SplitterWidth = 8;
                mpnsSplitContainer.SplitterWidth = 16;
                wnsSplitContainer.SplitterWidth = 16;
                appleSplitContainer.SplitterWidth = 8;
                gcmSplitContainer.SplitterWidth = 8;
                mpnsSplitContainer1.SplitterWidth = 8;
                wnsSplitContainer1.SplitterWidth = 8;
                appleLowerSplitContainer.SplitterWidth = 16;
                gcmLowerSplitContainer.SplitterWidth = 16;

                // Initialize cboMpnsNotificationTemplate ComboBox
                var mpnsType = typeof(WindowsPhoneNotificationXmlBuilder);
                var mpnsMethodInfos = mpnsType.GetMethods(BindingFlags.Public | BindingFlags.Static);
                mpnsMethodInfoList = new List<MethodInfo>(mpnsMethodInfos);
                cboMpnsNotificationTemplate.Items.Add(SelectNotificationTemplate);
                cboMpnsNotificationTemplate.Items.Add(ManualTemplate);
                foreach (var methodInfo in mpnsMethodInfos)
                {
                    var parameters = methodInfo.GetParameters();
                    if (parameters.Any(p => (p.ParameterType != typeof(string) && p.ParameterType != typeof(bool))))
                    {
                        continue;
                    }
                    cboMpnsNotificationTemplate.Items.Add(methodInfo.Name.StartsWith("Create") ?
                                                      methodInfo.Name.Substring(6) :
                                                      methodInfo.Name);
                }
                cboMpnsNotificationTemplate.SelectedIndex = 0;

                // Initialize cboWnsNotificationTemplate ComboBox
                var wnstype = typeof(WindowsNotificationXmlBuilder);
                var wnsMethodInfos = wnstype.GetMethods(BindingFlags.Public | BindingFlags.Static);
                wnsMethodInfoList = new List<MethodInfo>(wnsMethodInfos);
                cboWnsNotificationTemplate.Items.Add(SelectNotificationTemplate);
                cboWnsNotificationTemplate.Items.Add(ManualTemplate);
                foreach (var methodInfo in wnsMethodInfos)
                {
                    var parameters = methodInfo.GetParameters();
                    if (parameters.Any(p => (p.ParameterType != typeof(string))))
                    {
                        continue;
                    }
                    cboWnsNotificationTemplate.Items.Add(methodInfo.Name.StartsWith("Create") ?
                                                      methodInfo.Name.Substring(6) : 
                                                      methodInfo.Name);
                }
                cboWnsNotificationTemplate.SelectedIndex = 0;

                // Initialize Data
                InitializeData();
            }
            else
            {
                // Initialize buttons
                btnCreateDelete.Text = CreateText;
                btnCancelUpdate.Text = CancelText;
                btnRefresh.Enabled = false;
                btnRegistrations.Visible = false;
                btnMetrics.Visible = false;
                btnCloseTabs.Visible = false;
                txtPath.Focus();

                // Disable test pages
                DisablePage(MetricsTabPage);
                DisablePage(TemplateNotificationPage);
                DisablePage(WindowsPhoneNativeNotificationPage);
                DisablePage(WindowsNativeNotificationPage);
                DisablePage(AppleNativeNotificationPage);
                DisablePage(GoogleNativeNotificationPage);
                
                // Create BindingList for Authorization Rules
                var bindingList = new BindingList<NotificationHubAuthorizationRuleWrapper>(new List<NotificationHubAuthorizationRuleWrapper>())
                {
                    AllowEdit = true,
                    AllowNew = true,
                    AllowRemove = true
                };
                bindingList.ListChanged += bindingList_ListChanged;
                authorizationRulesBindingSource.DataSource = bindingList;
                authorizationRulesDataGridView.DataSource = authorizationRulesBindingSource;
            }
        }

        private void bindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                if (notificationHubDescription != null && 
                    notificationHubDescription.Authorization.Count > 0 && 
                    notificationHubDescription.Authorization.Count > e.NewIndex)
                {
                    var rule = notificationHubDescription.Authorization.ElementAt(e.NewIndex);
                    if (rule != null)
                    {
                        notificationHubDescription.Authorization.Remove(rule);
                    }
                }
            }
        }

        private void InitializeData()
        {
            // Get NotificationHubClient 
            notificationHubClient = GetNotificationHubClient();

            // Authorization Rules
            BindingList<NotificationHubAuthorizationRuleWrapper> bindingList;
            if (notificationHubDescription.Authorization.Count > 0)
            {
                var enumerable = notificationHubDescription.Authorization.Select(r => new NotificationHubAuthorizationRuleWrapper(r));
                bindingList = new BindingList<NotificationHubAuthorizationRuleWrapper>(enumerable.ToList())
                {
                    AllowEdit = true,
                    AllowNew = true,
                    AllowRemove = true
                };

            }
            else
            {
                bindingList = new BindingList<NotificationHubAuthorizationRuleWrapper>(new List<NotificationHubAuthorizationRuleWrapper>())
                {
                    AllowEdit = true,
                    AllowNew = true,
                    AllowRemove = true
                };
            }
            bindingList.ListChanged += bindingList_ListChanged;
            authorizationRulesBindingSource.DataSource = new BindingList<NotificationHubAuthorizationRuleWrapper>(bindingList);
            authorizationRulesDataGridView.DataSource = authorizationRulesBindingSource;

            // Initialize property grid
            var propertyList = new List<string[]>();
            propertyList.AddRange(new[]
                        {
                            new[] {DailyMaxActiveDevices, notificationHubDescription.DailyMaxActiveDevices.ToString("N0")},
                            new[] {DailyMaxActiveRegistrations, notificationHubDescription.DailyMaxActiveRegistrations.ToString("N0")},
                            new[] {DailyOperations, notificationHubDescription.DailyOperations.ToString("N0")}
                        });

            propertyListView.Items.Clear();
            foreach (var array in propertyList)
            {
                propertyListView.Items.Add(new ListViewItem(array));
            }

            // Path
            if (!string.IsNullOrWhiteSpace(notificationHubDescription.Path))
            {
                txtPath.Text = notificationHubDescription.Path;
            }

            // UserMetadata
            if (!string.IsNullOrWhiteSpace(notificationHubDescription.UserMetadata))
            {
                txtUserMetadata.Text = notificationHubDescription.UserMetadata;
            }

            // RegistrationTtl
            if (notificationHubDescription.RegistrationTtl != null)
            {
                txtRegistrationTimeToLiveWindowDays.Text = notificationHubDescription.RegistrationTtl.Value.Days.ToString(CultureInfo.InvariantCulture);
                txtRegistrationTimeToLiveWindowHours.Text = notificationHubDescription.RegistrationTtl.Value.Hours.ToString(CultureInfo.InvariantCulture);
                txtRegistrationTimeToLiveWindowMinutes.Text = notificationHubDescription.RegistrationTtl.Value.Minutes.ToString(CultureInfo.InvariantCulture);
                txtRegistrationTimeToLiveWindowSeconds.Text = notificationHubDescription.RegistrationTtl.Value.Seconds.ToString(CultureInfo.InvariantCulture);
                txtRegistrationTimeToLiveWindowMilliseconds.Text = notificationHubDescription.RegistrationTtl.Value.Milliseconds.ToString(CultureInfo.InvariantCulture);
            }

            if (notificationHubDescription.WnsCredential != null)
            {
                if (!string.IsNullOrWhiteSpace(notificationHubDescription.WnsCredential.PackageSid))
                {
                    txtPackageSid.Text = notificationHubDescription.WnsCredential.PackageSid;
                }
                if (!string.IsNullOrWhiteSpace(notificationHubDescription.WnsCredential.SecretKey))
                {
                    txtClientSecret.Text = notificationHubDescription.WnsCredential.SecretKey;
                }
            }
            else
            {
                txtPackageSid.Text = string.Empty;
                txtClientSecret.Text = string.Empty;
            }

            if (notificationHubDescription.GcmCredential != null)
            {
                if (!string.IsNullOrWhiteSpace(notificationHubDescription.GcmCredential.GoogleApiKey))
                {
                    txtGcmApiKey.Text = notificationHubDescription.GcmCredential.GoogleApiKey;
                }
                if (!string.IsNullOrWhiteSpace(notificationHubDescription.GcmCredential.GcmEndpoint))
                {
                    txtGcmEndpoint.Text = notificationHubDescription.GcmCredential.GcmEndpoint;
                }
            }
            else
            {
                txtGcmApiKey.Text = string.Empty;
            }

            if (notificationHubDescription.MpnsCredential != null)
            {
                if (!string.IsNullOrWhiteSpace(notificationHubDescription.MpnsCredential.MpnsCertificate) &&
                    !string.IsNullOrWhiteSpace(notificationHubDescription.MpnsCredential.CertificateKey))
                {
                    var certificate = new X509Certificate2(Convert.FromBase64String(notificationHubDescription.MpnsCredential.MpnsCertificate),
                                                           notificationHubDescription.MpnsCredential.CertificateKey);
                    if (!string.IsNullOrWhiteSpace(certificate.Thumbprint))
                    {
                        txtMpnsCredentialCertificateThumbprint.Text = certificate.Thumbprint;
                    }
                }
                else
                {
                    txtMpnsCredentialCertificateThumbprint.Text = string.Empty;
                    checkBoxEnableUnauthenticatedMpns.Checked = true;
                }
            }

            
            if (notificationHubDescription.ApnsCredential != null)
            {
                if (!string.IsNullOrWhiteSpace(notificationHubDescription.ApnsCredential.ApnsCertificate) &&
                !string.IsNullOrWhiteSpace(notificationHubDescription.ApnsCredential.CertificateKey))
                {
                    var certificate = new X509Certificate2(Convert.FromBase64String(notificationHubDescription.ApnsCredential.ApnsCertificate),
                                                           notificationHubDescription.ApnsCredential.CertificateKey);
                    if (!string.IsNullOrWhiteSpace(certificate.Thumbprint))
                    {
                        txtApnsCredentialCertificateThumbprint.Text = certificate.Thumbprint;
                    }
                }
                else
                {
                    txtApnsCredentialCertificateThumbprint.Text = string.Empty;
                }
                if (!string.IsNullOrWhiteSpace(notificationHubDescription.ApnsCredential.Endpoint))
                {
                    txtApnsEndpoint.Text = notificationHubDescription.ApnsCredential.Endpoint;
                }
            }

            // Show or hide test pages
            EnablePage(TemplateNotificationPage);
            if (notificationHubDescription.MpnsCredential == null &&
                (notificationHubDescription.WnsCredential == null ||
                string.IsNullOrWhiteSpace(notificationHubDescription.WnsCredential.WindowsLiveEndpoint)) &&
                (notificationHubDescription.ApnsCredential == null ||
                string.IsNullOrWhiteSpace(notificationHubDescription.ApnsCredential.Endpoint)) &&
                (notificationHubDescription.GcmCredential == null ||
                string.IsNullOrWhiteSpace(notificationHubDescription.GcmCredential.GcmEndpoint)))
            {
                DisablePage(TemplateNotificationPage);
            }
            else
            {
                EnablePage(TemplateNotificationPage);
            }
            if (notificationHubDescription.MpnsCredential == null)
            {
                DisablePage(WindowsPhoneNativeNotificationPage);
            }
            else
            {
                EnablePage(WindowsPhoneNativeNotificationPage);
            }
            if (notificationHubDescription.WnsCredential == null ||
                string.IsNullOrWhiteSpace(notificationHubDescription.WnsCredential.WindowsLiveEndpoint))
            {
                DisablePage(WindowsNativeNotificationPage);
            }
            else
            {
                EnablePage(WindowsNativeNotificationPage);
            }
            if (notificationHubDescription.ApnsCredential == null ||
                string.IsNullOrWhiteSpace(notificationHubDescription.ApnsCredential.Endpoint))
            {
                DisablePage(AppleNativeNotificationPage);
            }
            else
            {
                EnablePage(AppleNativeNotificationPage);
            }
            if (notificationHubDescription.GcmCredential == null ||
                string.IsNullOrWhiteSpace(notificationHubDescription.GcmCredential.GcmEndpoint))
            {
                DisablePage(GoogleNativeNotificationPage);
            }
            else
            {
                EnablePage(GoogleNativeNotificationPage);
            }
        }

        private void CalculateLastColumnWidth(object sender)
        {
            if (sorting)
            {
                return;
            }
            var dataGridView = sender as DataGridView;
            if (dataGridView == null || dataGridView.Columns.Count == 0)
            {
                return;
            }
            
            if (dataGridView.Columns.Count == 2)
            {
                var gridWidth = dataGridView.Parent is Grouper
                                    ? dataGridView.Parent.Size.Width - 32
                                    : dataGridView.Width;
                if (dataGridView.Columns[0].Width < 80)
                {
                    dataGridView.Columns[0].Width = 80;
                }
                var width = gridWidth - dataGridView.Columns[0].Width - dataGridView.RowHeadersWidth;
                var verticalScrollbar = dataGridView.Controls.OfType<VScrollBar>().First();
                if (verticalScrollbar.Visible)
                {
                    width -= verticalScrollbar.Width;
                }
                dataGridView.Columns[1].Width = width;
                return;
            }

            if (dataGridView == registrationsDataGridView)
            {
                var width = dataGridView.Width - dataGridView.RowHeadersWidth - dataGridView.Columns[0].Width - dataGridView.Columns[3].Width;
                var verticalScrollbar = dataGridView.Controls.OfType<VScrollBar>().First();
                if (verticalScrollbar.Visible)
                {
                    width -= verticalScrollbar.Width;
                }
                var columnWidth = width/4;
                dataGridView.Columns[1].Width = columnWidth - 5;
                dataGridView.Columns[2].Width = columnWidth - 5;
                dataGridView.Columns[4].Width = columnWidth + (width - (columnWidth * 4)) + 5;
                dataGridView.Columns[5].Width = columnWidth + 5;
            }

            if (dataGridView == mpnsTagsDataGridView ||
                dataGridView == wnsTagsDataGridView ||
                dataGridView == templateTagsDataGridView ||
                dataGridView == appleTagsDataGridView ||
                dataGridView == gcmTagsDataGridView)
            {
                var width = dataGridView.Width - dataGridView.RowHeadersWidth;
                var verticalScrollbar = dataGridView.Controls.OfType<VScrollBar>().First();
                if (verticalScrollbar.Visible)
                {
                    width -= verticalScrollbar.Width;
                }
                dataGridView.Columns[0].Width = width;
            }
        }

        private void mainTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(mainTabControl, e, null);
        }

        private void dataGridView_Resize(object sender, EventArgs e)
        {
            CalculateLastColumnWidth(sender);
        }

        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalculateLastColumnWidth(sender);
        }

        private void dataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculateLastColumnWidth(sender);
        }

        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //var textBox = (TextBox)e.Control;
            //textBox.Multiline = true;
            //textBox.ScrollBars = ScrollBars.Both;
        }

        private void cboWnsNotificationTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboWnsNotificationTemplate.SelectedIndex == 0)
            {
                wnsTemplatePropertyGrid.SelectedObject = null;
                SetGotFocusHandler(txtWnsPayload, textBox_GotFocus, true);
                return;
            }
            if (cboWnsNotificationTemplate.SelectedIndex == 1)
            {
                wnsTemplatePropertyGrid.SelectedObject = null;
                SetGotFocusHandler(txtWnsPayload, textBox_GotFocus, false);
            }
            else
            {
                var methodInfo = wnsMethodInfoList[cboWnsNotificationTemplate.SelectedIndex - 2];
                var parameterInfos = methodInfo.GetParameters();
                
                var notification = new CustomObject
                    {
                        Properties = parameterInfos.Select(p => new CustomProperty {Name = p.Name, Type = p.ParameterType}).ToList()
                    };

                wnsTemplatePropertyGrid.SelectedObject = notification;
                var values = notification.Properties.Select(p => notification[p.Name]).ToArray();
                // ReSharper disable CoVariantArrayConversion
                wnsPayload = methodInfo.Invoke(null, values) as string;
                // ReSharper restore CoVariantArrayConversion
                if (!string.IsNullOrWhiteSpace(wnsPayload))
                {
                    txtWnsPayload.Text = XmlHelper.Indent(wnsPayload);
                }
                SetGotFocusHandler(txtWnsPayload, textBox_GotFocus, true);
            }
        }

        private void wnsTemplatePropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            var methodInfo = wnsMethodInfoList[cboWnsNotificationTemplate.SelectedIndex - 2];
            var notification = wnsTemplatePropertyGrid.SelectedObject as CustomObject;
            if (notification == null)
            {
                return;
            }
            var values = notification.Properties.Select(p => notification[p.Name]).ToArray();
            // ReSharper disable CoVariantArrayConversion
            wnsPayload = methodInfo.Invoke(null, values) as string;
            // ReSharper restore CoVariantArrayConversion
            if (!string.IsNullOrWhiteSpace(wnsPayload))
            {
                txtWnsPayload.Text = XmlHelper.Indent(wnsPayload);
            }
        }

        private NotificationHubClient GetNotificationHubClient()
        {
            string connectionString = null;
            if (!string.IsNullOrWhiteSpace(serviceBusHelper.ConnectionString))
            {
                connectionString = serviceBusHelper.ConnectionString;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(serviceBusHelper.Namespace) &&
                !string.IsNullOrWhiteSpace(serviceBusHelper.IssuerName) &&
                !string.IsNullOrWhiteSpace(serviceBusHelper.IssuerSecret))
                {
                    var uri = ServiceBusEnvironment.CreateServiceUri("sb", serviceBusHelper.Namespace, string.Empty);
                    connectionString = ServiceBusConnectionStringBuilder.CreateUsingSharedSecret(uri,
                                                                                                 serviceBusHelper.IssuerName,
                                                                                                 serviceBusHelper.IssuerSecret);
                }
            }

            if (string.IsNullOrWhiteSpace(connectionString) || 
                notificationHubDescription == null || 
                string.IsNullOrWhiteSpace(notificationHubDescription.Path))
            {
                return null;
            }

            return NotificationHubClient.CreateClientFromConnectionString(connectionString, notificationHubDescription.Path, true);
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                btnRegistrations.Enabled = false;
                btnSend.Enabled = false;
                btnRefresh.Enabled = false;
                btnCreateDelete.Enabled = false;
                btnCancelUpdate.Enabled = false;
                if (notificationHubClient == null)
                {
                    return;
                }

                Notification notification = null;
                string[] tags = null;
                string tagExpression = null;
                switch (mainTabControl.SelectedTab.Name)
                {
                    case TemplateNotificationPage:
                        var properties = NotificationInfo.TemplateProperties.ToDictionary(p => p.Name, p => p.Value);
                        if (properties.Count > 0)
                        {
                            var headers = NotificationInfo.TemplateHeaders.ToDictionary(p => p.Name, p => p.Value);
                            tags = NotificationInfo.TemplateTags.Select(t => t.Tag).ToArray();
                            tagExpression = txtTemplateTagExpression.Text;
                            notification = new TemplateNotification(properties) {Headers = headers};
                        }
                        else
                        {
                            writeToLog(NotificationPayloadCannotBeNull, false);
                        }
                        break;
                    case WindowsPhoneNativeNotificationPage:
                        if (!string.IsNullOrWhiteSpace(mpnsPayload))
                        {
                            var headers = NotificationInfo.MpnsHeaders.ToDictionary(p => p.Name, p => p.Value);
                            tags = NotificationInfo.MpnsTags.Select(t => t.Tag).ToArray();
                            tagExpression = txtMpnsTagExpression.Text;
                            notification = new MpnsNotification(mpnsPayload, headers);
                        }
                        else
                        {
                            writeToLog(NotificationPayloadCannotBeNull, false);
                        }
                        break;
                    case WindowsNativeNotificationPage:
                        if (!string.IsNullOrWhiteSpace(wnsPayload))
                        {
                            var headers = NotificationInfo.WnsHeaders.ToDictionary(p => p.Name, p => p.Value);
                            tags = NotificationInfo.WnsTags.Select(t => t.Tag).ToArray();
                            tagExpression = txtWnsTagExpression.Text;
                            notification = new WindowsNotification(wnsPayload, headers);
                        }
                        else
                        {
                            writeToLog(NotificationPayloadCannotBeNull, false);
                        }
                        break;
                    case AppleNativeNotificationPage:
                        if (!string.IsNullOrWhiteSpace(apnsPayload))
                        {
                            var serializer = new JavaScriptSerializer();
                            try
                            {
                                serializer.Deserialize<dynamic>(apnsPayload);
                            }
                            catch (Exception)
                            {
                                writeToLog(PayloadIsNotInJsonFormat);
                                return;
                            }
                            var headers = NotificationInfo.ApnsHeaders.ToDictionary(p => p.Name, p => p.Value);
                            tags = NotificationInfo.ApnsTags.Select(t => t.Tag).ToArray();
                            tagExpression = txtAppleTagExpression.Text;
                            notification = new AppleNotification(apnsPayload) { Headers = headers };
                        }
                        else
                        {
                            writeToLog(JsonPayloadTemplateCannotBeNull, false);
                        }
                        break;
                    case GoogleNativeNotificationPage:
                        if (!string.IsNullOrWhiteSpace(gcmPayload))
                        {
                            var serializer = new JavaScriptSerializer();
                            try
                            {
                                serializer.Deserialize<dynamic>(gcmPayload);
                            }
                            catch (Exception)
                            {
                                writeToLog(PayloadIsNotInJsonFormat);
                                return;
                            }
                            var headers = NotificationInfo.GcmHeaders.ToDictionary(p => p.Name, p => p.Value);
                            tags = NotificationInfo.GcmTags.Select(t => t.Tag).ToArray();
                            tagExpression = txtGcmTagExpression.Text;
                            notification = new GcmNotification(gcmPayload) { Headers = headers };
                        }
                        else
                        {
                            writeToLog(JsonPayloadTemplateCannotBeNull, false);
                        }
                        break;
                }
                if (notification == null)
                {
                    return;
                }
                NotificationOutcome notificationOutcome;
                if (!string.IsNullOrWhiteSpace(tagExpression))
                {
                    notificationOutcome = await notificationHubClient.SendNotificationAsync(notification, tagExpression);
                    WriteNotificationToLog(notification, notificationOutcome, tagExpression, tags);
                    return;
                }
                if (tags.Any())
                {
                    notificationOutcome = await notificationHubClient.SendNotificationAsync(notification, tags);
                    WriteNotificationToLog(notification, notificationOutcome, tagExpression, tags);
                    return;
                }
                notificationOutcome = await notificationHubClient.SendNotificationAsync(notification);
                WriteNotificationToLog(notification, notificationOutcome, tagExpression, tags);
            }
            catch (Exception ex)
            {
                writeToLog(ex.Message);
            }
            finally
            {
                btnRegistrations.Enabled = true;
                btnSend.Enabled = true;
                btnRefresh.Enabled = true;
                btnCreateDelete.Enabled = true;
                btnCancelUpdate.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void WriteNotificationToLog(Notification notification, NotificationOutcome notificationOutcome, string tagExpression, string[] tags)
        {
            if (notification == null)
            {
                return;
            }
            var builder = new StringBuilder();
            builder.AppendFormat(NotificationSentHeader, notificationHubDescription.Path);
            if (notificationOutcome != null)
            {
                builder.AppendLine(string.Format(OutcomeFormat,
                                                 notificationOutcome.State,
                                                 notificationOutcome.Success,
                                                 notificationOutcome.Failure,
                                                 notificationOutcome.TrackingId));
                if (!string.IsNullOrWhiteSpace(tagExpression))
                {
                    builder.AppendLine(string.Format(TagsExpressionFormat, tagExpression));
                }
                else if (tags != null &&
                    tags.Any())
                {
                    builder.AppendLine(TagsLogHeader);
                    foreach (var tag in tags)
                    {
                        builder.AppendLine(string.Format(TagFormat, tag));
                    }
                }
                if (notificationOutcome.Results != null &&
                    notificationOutcome.Results.Count > 0)
                {
                    builder.AppendLine(ResultsHeader);
                    foreach (var item in notificationOutcome.Results)
                    {
                        builder.AppendLine(string.Format(ResultFormat,
                                                         item.RegistrationId,
                                                         item.PnsHandle,
                                                         item.ApplicationPlatform,
                                                         item.Outcome));
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(notification.Body))
            {
                builder.AppendLine(BodyHeader);
                builder.AppendLine(XmlHelper.Indent(notification.Body));
            }
            if (notification.Headers != null &&
                notification.Headers.Count > 0)
            {
                builder.AppendLine(HeadersHeader);
                foreach (var key in notification.Headers.Keys)
                {
                    builder.AppendLine(string.Format(HeaderFormat, key, notification.Headers[key]));
                }
            }
            writeToLog(builder.ToString());
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

        

        private void grouperTemplateNotificationProperties_CustomPaint(PaintEventArgs e)
        {
            templateNotificationDataGridView.Size = new Size(grouperTemplateNotificationProperties.Size.Width - 32,
                                                             templateNotificationDataGridView.Size.Height);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    templateNotificationDataGridView.Location.X - 1,
                                    templateNotificationDataGridView.Location.Y - 1,
                                    templateNotificationDataGridView.Size.Width + 1,
                                    templateNotificationDataGridView.Size.Height + 1);
        }

        private void grouperTemplateTags_CustomPaint(PaintEventArgs e)
        {
            templateTagsDataGridView.Size = new Size(grouperTemplateTags.Width - 32,
                                                     grouperTemplateTags.Height - 48);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     templateTagsDataGridView.Location.X - 1,
                                     templateTagsDataGridView.Location.Y - 1,
                                     templateTagsDataGridView.Size.Width + 1,
                                     templateTagsDataGridView.Size.Height + 1);
        }

        private void grouperTemplateAdditionalHeaders_CustomPaint(PaintEventArgs e)
        {
            templateHeadersDataGridView.Size = new Size(grouperTemplateAdditionalHeaders.Width - 32,
                                                        grouperTemplateAdditionalHeaders.Height - 48);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     templateHeadersDataGridView.Location.X - 1,
                                     templateHeadersDataGridView.Location.Y - 1,
                                     templateHeadersDataGridView.Size.Width + 1,
                                     templateHeadersDataGridView.Size.Height + 1);
        }

        private void grouperWnsNotificationTemplate_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    cboWnsNotificationTemplate.Location.X - 1,
                                    cboWnsNotificationTemplate.Location.Y - 1,
                                    cboWnsNotificationTemplate.Size.Width + 1,
                                    cboWnsNotificationTemplate.Size.Height + 1);
        }

        private void grouperWnsTags_CustomPaint(PaintEventArgs e)
        {
            wnsTagsDataGridView.Size = new Size(grouperWnsTags.Size.Width - 32,
                                                grouperWnsTags.Size.Height - 48);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     wnsTagsDataGridView.Location.X - 1,
                                     wnsTagsDataGridView.Location.Y - 1,
                                     wnsTagsDataGridView.Size.Width + 1,
                                     wnsTagsDataGridView.Size.Height + 1);
            
        }

        private void grouperWnsAdditionalHeaders_CustomPaint(PaintEventArgs e)
        {
            wnsHeadersDataGridView.Size = new Size(grouperWnsAdditionalHeaders.Size.Width - 32,
                                                   grouperWnsAdditionalHeaders.Size.Height - 48);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     wnsHeadersDataGridView.Location.X - 1,
                                     wnsHeadersDataGridView.Location.Y - 1,
                                     wnsHeadersDataGridView.Size.Width + 1,
                                     wnsHeadersDataGridView.Size.Height + 1);
        }

        private void grouperMpnsNotificationTemplate_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    cboMpnsNotificationTemplate.Location.X - 1,
                                    cboMpnsNotificationTemplate.Location.Y - 1,
                                    cboMpnsNotificationTemplate.Size.Width + 1,
                                    cboMpnsNotificationTemplate.Size.Height + 1);

        }

        private void grouperMpnsTags_CustomPaint(PaintEventArgs e)
        {
            mpnsTagsDataGridView.Size = new Size(grouperMpnsTags.Size.Width - 32,
                                                 grouperMpnsTags.Size.Height - 48);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     mpnsTagsDataGridView.Location.X - 1,
                                     mpnsTagsDataGridView.Location.Y - 1,
                                     mpnsTagsDataGridView.Size.Width + 1,
                                     mpnsTagsDataGridView.Size.Height + 1);
        }

        private void grouperMpnsAdditionalHeaders_CustomPaint(PaintEventArgs e)
        {
            mpnsHeadersDataGridView.Size = new Size(grouperMpnsAdditionalHeaders.Size.Width - 32,
                                                    grouperMpnsAdditionalHeaders.Size.Height - 48);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     mpnsHeadersDataGridView.Location.X - 1,
                                     mpnsHeadersDataGridView.Location.Y - 1,
                                     mpnsHeadersDataGridView.Size.Width + 1,
                                     mpnsHeadersDataGridView.Size.Height + 1);
        }

        private void grouperRegistrations_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     registrationsDataGridView.Location.X - 1,
                                     registrationsDataGridView.Location.Y - 1,
                                     registrationsDataGridView.Size.Width + 1,
                                     registrationsDataGridView.Size.Height + 1);
        }

        private void grouperRegistrationProperties_CustomPaint(PaintEventArgs obj)
        {
            registrationPropertyGrid.Size = new Size(grouperRegistrationProperties.Size.Width - 32, registrationPropertyGrid.Size.Height);
            btnUpdateRegistration.Location = new Point(grouperRegistrationProperties.Size.Width - 
                                                       btnUpdateRegistration.Size.Width - 16, 
                                                       btnUpdateRegistration.Location.Y);
            btnDeleteRegistration.Location = new Point(grouperRegistrationProperties.Size.Width - 
                                                       btnUpdateRegistration.Size.Width - 
                                                       btnDeleteRegistration.Size.Width - 24,
                                                       btnDeleteRegistration.Location.Y);
            btnCreateRegistration.Location = new Point(grouperRegistrationProperties.Size.Width - 
                                                       btnUpdateRegistration.Size.Width - 
                                                       btnDeleteRegistration.Size.Width -
                                                       btnCreateRegistration.Size.Width - 32,
                                                       btnDeleteRegistration.Location.Y);
            btnRefreshRegistrations.Location = new Point(grouperRegistrationProperties.Size.Width -
                                                       btnUpdateRegistration.Size.Width -
                                                       btnDeleteRegistration.Size.Width -
                                                       btnCreateRegistration.Size.Width - 
                                                       btnRefreshRegistrations.Size.Width - 40,
                                                       btnDeleteRegistration.Location.Y);
        }

        private void grouperAppleTags_CustomPaint(PaintEventArgs e)
        {
            appleTagsDataGridView.Size = new Size(grouperAppleTags.Size.Width - 32,
                                                  grouperAppleTags.Size.Height - 48);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     appleTagsDataGridView.Location.X - 1,
                                     appleTagsDataGridView.Location.Y - 1,
                                     appleTagsDataGridView.Size.Width + 1,
                                     appleTagsDataGridView.Size.Height + 1);
        }

        private void grouperAppleAdditionalHeaders_CustomPaint(PaintEventArgs e)
        {
            appleHeadersDataGridView.Size = new Size(grouperAppleAdditionalHeaders.Size.Width - 32,
                                                     grouperAppleAdditionalHeaders.Size.Height - 48);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     appleHeadersDataGridView.Location.X - 1,
                                     appleHeadersDataGridView.Location.Y - 1,
                                     appleHeadersDataGridView.Size.Width + 1,
                                     appleHeadersDataGridView.Size.Height + 1);
        }

        private void grouperGcmTags_CustomPaint(PaintEventArgs e)
        {
            gcmTagsDataGridView.Size = new Size(grouperGcmTags.Size.Width - 32,
                                                grouperGcmTags.Size.Height - 48);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     gcmTagsDataGridView.Location.X - 1,
                                     gcmTagsDataGridView.Location.Y - 1,
                                     gcmTagsDataGridView.Size.Width + 1,
                                     gcmTagsDataGridView.Size.Height + 1);
        }

        private void grouperGcmAdditionalHeaders_CustomPaint(PaintEventArgs e)
        {
            gcmHeadersDataGridView.Size = new Size(grouperGcmAdditionalHeaders.Size.Width - 32,
                                                   grouperGcmAdditionalHeaders.Size.Height - 48);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     gcmHeadersDataGridView.Location.X - 1,
                                     gcmHeadersDataGridView.Location.Y - 1,
                                     gcmHeadersDataGridView.Size.Width + 1,
                                     gcmHeadersDataGridView.Size.Height + 1);
        }

        private void grouperJsonPayload_CustomPaint(PaintEventArgs obj)
        {
            grouperJsonPayload.Size = new Size(grouperJsonPayload.Size.Width, appleSplitContainer.Panel1.Size.Height);
        }

        private void grouperAuthorizationRuleList_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    authorizationRulesDataGridView.Location.X - 1,
                                    authorizationRulesDataGridView.Location.Y - 1,
                                    authorizationRulesDataGridView.Size.Width + 1,
                                    authorizationRulesDataGridView.Size.Height + 1);
        }

        private void templateNotificationDataGridView_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            var properties = NotificationInfo.TemplateProperties.ToDictionary(p => p.Name, p => p.Value);
            txtTemplatePayload.Text = properties.Count > 0 ? new JavaScriptSerializer().Serialize(properties) : string.Empty;
        }

        private void EnablePage(string pageName)
        {
            var page = hiddenPages.FirstOrDefault(p => string.Compare(p.Name, pageName, StringComparison.InvariantCultureIgnoreCase) == 0);
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

        private void btnCreateDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (serviceBusHelper == null)
                {
                    return;
                }
                if (btnCreateDelete.Text == DeleteText)
                {
                    using (var deleteForm = new DeleteForm(notificationHubDescription.Path, NotificationHubEntity.ToLower()))
                    {
                        if (deleteForm.ShowDialog() == DialogResult.OK)
                        {
                            serviceBusHelper.DeleteNotificationHub(notificationHubDescription);
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

                    var description = new NotificationHubDescription(txtPath.Text)
                        {
                            UserMetadata = txtUserMetadata.Text
                        };

                    if (!string.IsNullOrWhiteSpace(txtPackageSid.Text) &&
                        !string.IsNullOrWhiteSpace(txtClientSecret.Text))
                    {
                        description.WnsCredential = new WnsCredential(txtPackageSid.Text, txtClientSecret.Text);
                    }

                    if (!string.IsNullOrWhiteSpace(txtGcmApiKey.Text))
                    {
                        description.GcmCredential = new GcmCredential(txtGcmApiKey.Text);
                    }

                    if (!string.IsNullOrWhiteSpace(mpnsCredentialCertificatePath) &&
                        !string.IsNullOrWhiteSpace(mpnsCredentialCertificateKey))
                    {
                        description.MpnsCredential = new MpnsCredential(mpnsCredentialCertificatePath, 
                                                                        mpnsCredentialCertificateKey);
                    }
                    else
                    {
                        description.MpnsCredential = checkBoxEnableUnauthenticatedMpns.Checked
                                                                        ? new MpnsCredential()
                                                                        : null;
                    }

                    if (!string.IsNullOrWhiteSpace(apnsCredentialCertificatePath) &&
                        !string.IsNullOrWhiteSpace(apnsCredentialCertificateKey))
                    {
                        description.ApnsCredential = new ApnsCredential(mpnsCredentialCertificatePath,
                                                                        mpnsCredentialCertificateKey);
                    }

                    var days = 0;
                    var hours = 0;
                    var minutes = 0;
                    var seconds = 0;
                    var milliseconds = 0;

                    if (!string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowDays.Text) ||
                        !string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowHours.Text) ||
                        !string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowMinutes.Text) ||
                        !string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowSeconds.Text) ||
                        !string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowMilliseconds.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowDays.Text))
                        {
                            if (!int.TryParse(txtRegistrationTimeToLiveWindowDays.Text, out days))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowDaysMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowHours.Text))
                        {
                            if (!int.TryParse(txtRegistrationTimeToLiveWindowHours.Text, out hours))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowHoursMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowMinutes.Text))
                        {
                            if (!int.TryParse(txtRegistrationTimeToLiveWindowMinutes.Text, out minutes))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowMinutesMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowSeconds.Text))
                        {
                            if (!int.TryParse(txtRegistrationTimeToLiveWindowSeconds.Text, out seconds))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowSecondsMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowMilliseconds.Text))
                        {
                            if (!int.TryParse(txtRegistrationTimeToLiveWindowMilliseconds.Text, out milliseconds))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowMillisecondsMustBeANumber);
                                return;
                            }
                        }
                        description.RegistrationTtl = new TimeSpan(days, hours, minutes, seconds, milliseconds);
                    }

                    var bindingList = authorizationRulesBindingSource.DataSource as BindingList<NotificationHubAuthorizationRuleWrapper>;
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

                    notificationHubDescription = serviceBusHelper.CreateNotificationHub(description);
                    InitializeData();
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
                    var days = 0;
                    var hours = 0;
                    var minutes = 0;
                    var seconds = 0;
                    var milliseconds = 0;

                    if (!string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowDays.Text) ||
                         !string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowHours.Text) ||
                         !string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowMinutes.Text) ||
                         !string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowSeconds.Text) ||
                         !string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowMilliseconds.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowDays.Text))
                        {
                            if (!int.TryParse(txtRegistrationTimeToLiveWindowDays.Text, out days))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowDaysMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowHours.Text))
                        {
                            if (!int.TryParse(txtRegistrationTimeToLiveWindowHours.Text, out hours))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowHoursMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowMinutes.Text))
                        {
                            if (!int.TryParse(txtRegistrationTimeToLiveWindowMinutes.Text, out minutes))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowMinutesMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowSeconds.Text))
                        {
                            if (!int.TryParse(txtRegistrationTimeToLiveWindowSeconds.Text, out seconds))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowSecondsMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtRegistrationTimeToLiveWindowMilliseconds.Text))
                        {
                            if (!int.TryParse(txtRegistrationTimeToLiveWindowMilliseconds.Text, out milliseconds))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowMillisecondsMustBeANumber);
                                return;
                            }
                        }
                        notificationHubDescription.RegistrationTtl = new TimeSpan(days, hours, minutes, seconds, milliseconds);
                    }

                    notificationHubDescription.WnsCredential = !string.IsNullOrWhiteSpace(txtPackageSid.Text) &&
                                                               !string.IsNullOrWhiteSpace(txtClientSecret.Text)
                                                               ? new WnsCredential(txtPackageSid.Text, txtClientSecret.Text)
                                                               : null;

                    notificationHubDescription.GcmCredential = string.IsNullOrWhiteSpace(txtGcmApiKey.Text)
                                                                   ? null
                                                                   : new GcmCredential(txtGcmApiKey.Text);

                    if (!string.IsNullOrWhiteSpace(mpnsCredentialCertificatePath) && !string.IsNullOrWhiteSpace(mpnsCredentialCertificateKey))
                    {
                        notificationHubDescription.MpnsCredential = new MpnsCredential(mpnsCredentialCertificatePath, mpnsCredentialCertificateKey);
                    }
                    else if (checkBoxEnableUnauthenticatedMpns.Checked)
                    {
                        notificationHubDescription.MpnsCredential = new MpnsCredential();
                    }

                    if (!string.IsNullOrWhiteSpace(apnsCredentialCertificatePath) && !string.IsNullOrWhiteSpace(apnsCredentialCertificateKey))
                    {
                        notificationHubDescription.ApnsCredential = new ApnsCredential(apnsCredentialCertificatePath, apnsCredentialCertificateKey);
                    }
                    if (!string.IsNullOrWhiteSpace(txtApnsEndpoint.Text) && notificationHubDescription.ApnsCredential != null)
                    {
                        notificationHubDescription.ApnsCredential.Endpoint = txtApnsEndpoint.Text.Trim();
                    }

                    notificationHubDescription.UserMetadata = txtUserMetadata.Text;

                    var bindingList = authorizationRulesBindingSource.DataSource as BindingList<NotificationHubAuthorizationRuleWrapper>;
                    if (bindingList != null)
                    {
                        for (var i = 0; i < bindingList.Count; i++)
                        {
                            var rule = bindingList[i];
                            if (rule.AuthorizationRule != null)
                            {
                                continue;
                            }
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
                                    notificationHubDescription.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                                                                                         rule.PrimaryKey ?? SharedAccessAuthorizationRule.GenerateRandomKey(),
                                                                                                         rightList));
                                }
                                else
                                {
                                    notificationHubDescription.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                                                                                         rule.PrimaryKey ?? SharedAccessAuthorizationRule.GenerateRandomKey(),
                                                                                                         rule.SecondaryKey ?? SharedAccessAuthorizationRule.GenerateRandomKey(),
                                                                                                         rightList));
                                }
                            }
                            else
                            {
                                notificationHubDescription.Authorization.Add(new AllowRule(rule.IssuerName,
                                                                                 rule.ClaimType,
                                                                                 rule.ClaimValue,
                                                                                 rightList));
                            }
                        }
                    }
                    serviceBusHelper.UpdateNotificationHub(notificationHubDescription);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                    notificationHubDescription = serviceBusHelper.GetNotificationHub(notificationHubDescription.Path);
                }
                finally
                {
                    InitializeData();
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

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);

            var numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
            var decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            var groupSeparator = numberFormatInfo.NumberGroupSeparator;
            var negativeSign = numberFormatInfo.NegativeSign;

            var keyInput = e.KeyChar.ToString(CultureInfo.InvariantCulture);

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) ||
                     keyInput.Equals(negativeSign))
            {
                // Decimal separator is OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else if (e.KeyChar == ' ')
            {

            }
            else
            {
                // Swallow this invalid key and beep
                e.Handled = true;
            }
        }

        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRegistrations.Enabled = mainTabControl.SelectedTab.Name == DescriptionPage || mainTabControl.SelectedTab.Name == AuthorizationPage || mainTabControl.SelectedTab.Name == RegistrationsPage;
            btnSend.Enabled = mainTabControl.SelectedTab.Name != DescriptionPage && mainTabControl.SelectedTab.Name != AuthorizationPage && mainTabControl.SelectedTab.Name != RegistrationsPage;
            btnRefresh.Enabled = mainTabControl.SelectedTab.Name == DescriptionPage || mainTabControl.SelectedTab.Name == AuthorizationPage || mainTabControl.SelectedTab.Name == RegistrationsPage;
            btnCreateDelete.Enabled = mainTabControl.SelectedTab.Name == DescriptionPage || mainTabControl.SelectedTab.Name == AuthorizationPage || mainTabControl.SelectedTab.Name == RegistrationsPage;
            btnCancelUpdate.Enabled = mainTabControl.SelectedTab.Name == DescriptionPage || mainTabControl.SelectedTab.Name == AuthorizationPage || mainTabControl.SelectedTab.Name == RegistrationsPage;
            if (mainTabControl.SelectedTab == tabPageDescription)
            {
                HandleNotificationHubControl_Resize(null, null);
            }
        }

        private void tabPageAppleNativeNotification_Resize(object sender, EventArgs e)
        {
            grouperExpiry.Size = new Size(grouperExpiry.Size.Width,
                                          appleSplitContainer.Panel2.Size.Height);
        }

        private void btnRegistrations_Click(object sender, EventArgs e)
        {
            GetRegistrations(true, null);
        }

        private void registrationsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var bindingList = registrationsBindingSource.DataSource as SortableBindingList<RegistrationInfo>;
            if (bindingList == null)
            {
                return;
            }
            currentRegistrationRowIndex = e.RowIndex;
            var type = bindingList[e.RowIndex].Registration.GetType();
            var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var registration = new CustomObject
            {
                Properties = propertyInfos.Select(p => new CustomProperty { Name = p.Name,
                                                                            Type = p.Name == ChannelUri ?
                                                                                   typeof(string) :
                                                                                   p.Name == BodyTemplate ?
                                                                                   typeof(string) :
                                                                                   p.Name == Tags ? 
                                                                                   typeof(List<string>) :
                                                                                   p.Name == MpnsHeaders || 
                                                                                   p.Name == WnsHeaders ?
                                                                                   typeof(List<NotificationInfo>) :
                                                                                   p.Name == Expressions ? 
                                                                                   typeof(ReadOnlyList<string>) :
                                                                                   p.Name == ExpressionLengths ||
                                                                                   p.Name == ExpressionStartIndices ?
                                                                                   typeof(ReadOnlyList<int>) :
                                                                                   p.PropertyType,
                                                                            Editor = p.Name == BodyTemplate ||
                                                                                    p.Name == ChannelUri ||
                                                                                    p.Name == DeviceToken ||
                                                                                    p.Name == GcmRegistrationId ?
                                                                                    new CustomTextEditor() as UITypeEditor : 
                                                                                    p.Name == Expressions || 
                                                                                    p.Name == ExpressionLengths ||
                                                                                    p.Name == ExpressionStartIndices ?
                                                                                    new ReadOnlyEditor() as UITypeEditor:
                                                                                    p.Name == Tags ||
                                                                                    p.Name == MpnsHeaders ||
                                                                                    p.Name == WnsHeaders ?
                                                                                    new CustomCollectionEditor() as UITypeEditor :
                                                                                    null,
                                                                            IsReadOnly = !p.CanWrite || 
                                                                                         p.SetMethod == null ||
                                                                                        (p.SetMethod != null && !p.SetMethod.IsPublic)
                }).ToList()
            };
            foreach (var propertyInfo in propertyInfos)
            {
                object value;
                if (propertyInfo.PropertyType == typeof(Uri))
                {
                    var member = propertyInfo.GetValue(bindingList[e.RowIndex].Registration) as Uri;
                    value = member != null ? member.AbsoluteUri : null;
                }
                else if (propertyInfo.PropertyType == typeof(CDataMember))
                {
                    var member = propertyInfo.GetValue(bindingList[e.RowIndex].Registration) as CDataMember;
                    value = member != null ? member.Value : null;
                }
                else if (propertyInfo.PropertyType == typeof(ISet<string>))
                {
                    var collection = propertyInfo.GetValue(bindingList[e.RowIndex].Registration) as ISet<string>;
                    value = collection == null ?
                            new List<TagInfo>() :
                            collection.Select(i => new TagInfo(i)).ToList();
                }
                else if (propertyInfo.PropertyType == typeof(MpnsHeaderCollection))
                {
                    var collection = propertyInfo.GetValue(bindingList[e.RowIndex].Registration) as MpnsHeaderCollection;
                    value = collection == null ? 
                            new List<NotificationInfo>() : 
                            collection.Select(i => new NotificationInfo(i.Key, i.Value)).ToList();
                }
                else if (propertyInfo.PropertyType == typeof(WnsHeaderCollection))
                {
                    var collection = propertyInfo.GetValue(bindingList[e.RowIndex].Registration) as WnsHeaderCollection;
                    value = collection == null ?
                            new List<NotificationInfo>() :
                            collection.Select(i => new NotificationInfo(i.Key, i.Value)).ToList();
                }
                else if ((propertyInfo.GetMethod == null ||
                         (propertyInfo.GetMethod != null && !propertyInfo.GetMethod.IsPublic)) &&
                         (propertyInfo.SetMethod == null ||
                         (propertyInfo.SetMethod != null && !propertyInfo.SetMethod.IsPublic)) &&
                          propertyInfo.PropertyType == typeof(List<int>))
                {
                    var collection = propertyInfo.GetValue(bindingList[e.RowIndex].Registration) as List<int>;
                    value = collection == null ? new ReadOnlyList<int>() : new ReadOnlyList<int>(collection);
                }
                else
                {
                    value = propertyInfo.GetValue(bindingList[e.RowIndex].Registration);
                }

                registration[propertyInfo.Name] = value;
            }
            registrationPropertyGrid.SelectedObject = registration;  
        }

        private void btnMpnsCredentialUploadCertificate_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new UploadCertificateForm();
                if (form.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                mpnsCredentialCertificatePath = form.CertificatePath;
                mpnsCredentialCertificateKey = form.CertificateKey;
                var certificate = new X509Certificate2(mpnsCredentialCertificatePath, mpnsCredentialCertificateKey);
                txtMpnsCredentialCertificateThumbprint.Text = certificate.Thumbprint;
            }
            catch (Exception ex)
            {
               HandleException(ex);
            }
        }

        private void btnApnsCredentialUploadCertificate_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new UploadCertificateForm();
                if (form.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                apnsCredentialCertificatePath = form.CertificatePath;
                apnsCredentialCertificateKey = form.CertificateKey;
                var certificate = new X509Certificate2(apnsCredentialCertificatePath, apnsCredentialCertificateKey);
                txtApnsCredentialCertificateThumbprint.Text = certificate.Thumbprint;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void HandleNotificationHubControl_Resize(object sender, EventArgs e)
        {
            var width = (tabPageDescription.Size.Width - 64 - grouperDuplicateDetectionHistoryTimeWindow.Size.Width) / 2;
            var height = (tabPageDescription.Size.Height - grouperPath.Size.Height - 48) / 2;

            grouperPath.Size = new Size(width, grouperPath.Size.Height);
            grouperUserMetadata.Size = new Size(width, grouperUserMetadata.Size.Height);
            grouperUserMetadata.Location = new Point(grouperPath.Location.X + width + 16,
                                                     grouperPath.Location.Y);
            grouperWindowsNotificationSettings.Size = new Size(width, height);
            grouperGoogleCloudMessaggingSettings.Size = new Size(width, height);
            grouperWindowsPhoneNotificationSettings.Size = new Size(width, height);
            grouperAppleNotificationSettings.Size = new Size(width, height);

            grouperGoogleCloudMessaggingSettings.Location = new Point(grouperWindowsNotificationSettings.Location.X + width + 16,
                                                                      grouperWindowsNotificationSettings.Location.Y);
            grouperWindowsPhoneNotificationSettings.Location = new Point(grouperWindowsPhoneNotificationSettings.Location.X,
                                                                         grouperWindowsNotificationSettings.Location.Y + height + 8);

            grouperAppleNotificationSettings.Location = new Point(grouperWindowsNotificationSettings.Location.X + width + 16,
                                                                  grouperWindowsNotificationSettings.Location.Y + height + 8);
        }

        private void cboMpnsNotificationTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMpnsNotificationTemplate.SelectedIndex == 0)
            {
                mpnsTemplatePropertyGrid.SelectedObject = null;
                SetGotFocusHandler(txtMpnsPayload, textBox_GotFocus, true);
                return;
            }
            if (cboMpnsNotificationTemplate.SelectedIndex == 1)
            {
                mpnsTemplatePropertyGrid.SelectedObject = null;
                SetGotFocusHandler(txtMpnsPayload, textBox_GotFocus, false);
            }
            else
            {
                var methodInfo = mpnsMethodInfoList[cboMpnsNotificationTemplate.SelectedIndex - 2];
                var parameterInfos = methodInfo.GetParameters();

                var notification = new CustomObject
                {
                    Properties = parameterInfos.Select(p => new CustomProperty { Name = p.Name, Type = p.ParameterType}).ToList()
                };

                foreach (var property in notification.Properties.Where(property => property.Type == typeof (bool)))
                {
                    property.DefaultValue = property.Name.Contains("IsRelative") || property.Name.Contains("IsResource");
                    notification[property.Name] = property.DefaultValue;
                }

                mpnsTemplatePropertyGrid.SelectedObject = notification;
                var values = notification.Properties.Select(p => notification[p.Name]).ToArray();
                // ReSharper disable CoVariantArrayConversion
                mpnsPayload = methodInfo.Invoke(null, values) as string;
                // ReSharper restore CoVariantArrayConversion
                txtMpnsPayload.Text = XmlHelper.Indent(mpnsPayload);

                // Make txtMpnsTemplate editable if method == CreateRaw
                var isNotCreateRaw = string.Compare(methodInfo.Name, "CreateRaw", StringComparison.CurrentCultureIgnoreCase) != 0;
                SetGotFocusHandler(txtMpnsPayload, textBox_GotFocus, isNotCreateRaw);
            }
        }

        private void SetGotFocusHandler(TextBox control, EventHandler handler, bool set)
        {
            if (control == null || handler == null)
            {
                return;
            }
            if (set)
            {
                if (!control.ReadOnly)
                {
                    control.GotFocus += handler;
                }
            }
            else
            {
                control.GotFocus -= handler;
            }
            control.ReadOnly = set;
        }

        private void mpnsTemplatePropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            var methodInfo = mpnsMethodInfoList[cboMpnsNotificationTemplate.SelectedIndex - 2];
            var notification = mpnsTemplatePropertyGrid.SelectedObject as CustomObject;
            if (notification == null)
            {
                return;
            }
            var values = notification.Properties.Select(p => notification[p.Name]).ToArray();
            // ReSharper disable CoVariantArrayConversion
            mpnsPayload = methodInfo.Invoke(null, values) as string;
            // ReSharper restore CoVariantArrayConversion
            txtMpnsPayload.Text = XmlHelper.Indent(mpnsPayload);
        }

        private void txtMpnsPayload_TextChanged(object sender, EventArgs e)
        {
            if (!txtMpnsPayload.ReadOnly &&
                string.Compare(cboMpnsNotificationTemplate.Text, "Raw", StringComparison.CurrentCultureIgnoreCase) == 0 &&
                mpnsTemplatePropertyGrid.SelectedObject is CustomObject)
            {
                var customObject = (CustomObject)mpnsTemplatePropertyGrid.SelectedObject;
                var customProperty = customObject.Properties.FirstOrDefault(p => p.Name == "payload");
                if (customProperty != null)
                {
                    customObject["payload"] = txtMpnsPayload.Text;
                    mpnsTemplatePropertyGrid.SelectedObject = customObject;
                }
            }
            if (string.Compare(cboMpnsNotificationTemplate.Text, ManualTemplate,
                               StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                mpnsPayload = txtMpnsPayload.Text;
            }
        }

        private async void updateRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (registrationsDataGridView.SelectedRows.Count <= 0)
                {
                    return;
                }

                if (registrationsDataGridView.SelectedRows.Count > 1)
                {
                    using (var deleteForm = new DeleteForm(UpdateRegistrationsWarningMessage))
                    {
                        if (deleteForm.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                    }
                }
                
                var updated = 0;
                for (var i = 0; i < registrationsDataGridView.SelectedRows.Count; i++)
                {
                    var registrationInfo =
                        registrationsDataGridView.SelectedRows[i].DataBoundItem as RegistrationInfo;
                    if (registrationInfo == null || registrationInfo.Registration == null)
                    {
                        continue;
                    }
                    try
                    {
                        var mpnsTemplateRegistrationDescription = registrationInfo.Registration as MpnsTemplateRegistrationDescription;
                        if (mpnsTemplateRegistrationDescription != null)
                        {
                            registrationInfo.Registration = await notificationHubClient.UpdateRegistrationAsync(mpnsTemplateRegistrationDescription);
                        }
                        else
                        {
                            var mpnsRegistrationDescription = registrationInfo.Registration as MpnsRegistrationDescription;
                            if (mpnsRegistrationDescription != null)
                            {
                                registrationInfo.Registration = await notificationHubClient.UpdateRegistrationAsync(mpnsRegistrationDescription);
                            }
                            else
                            {
                                var wnsTemplateRegistrationDescription = registrationInfo.Registration as WindowsTemplateRegistrationDescription;
                                if (wnsTemplateRegistrationDescription != null)
                                {
                                    registrationInfo.Registration = await notificationHubClient.UpdateRegistrationAsync(wnsTemplateRegistrationDescription);
                                }
                                else
                                {
                                    var wnsRegistrationDescription = registrationInfo.Registration as WindowsRegistrationDescription;
                                    if (wnsRegistrationDescription != null)
                                    {
                                        registrationInfo.Registration = await notificationHubClient.UpdateRegistrationAsync(wnsRegistrationDescription);
                                    }
                                    else
                                    {
                                        var appleRegistrationDescription = registrationInfo.Registration as AppleRegistrationDescription;
                                        if (appleRegistrationDescription != null)
                                        {
                                            registrationInfo.Registration = await notificationHubClient.UpdateRegistrationAsync(appleRegistrationDescription);
                                        }
                                        else
                                        {
                                            var gcmRegistrationDescription = registrationInfo.Registration as GcmRegistrationDescription;
                                            if (gcmRegistrationDescription != null)
                                            {
                                                registrationInfo.Registration = await notificationHubClient.UpdateRegistrationAsync(gcmRegistrationDescription);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        writeToLog(string.Format(RegistrationsUpdatedFormat,
                                                 registrationInfo.Registration.RegistrationId,
                                                 notificationHubDescription.Path));
                        updated++;
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                    }
                }
                writeToLog(string.Format(RegistrationUpdatedMessage, updated, notificationHubDescription.Path));
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void deleteRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (registrationsDataGridView.SelectedRows.Count <= 0)
                {
                    return;
                }
                using (var deleteForm = new DeleteForm(registrationsDataGridView.SelectedRows.Count == 1 ?
                                                       DeleteRegistrationWarningMessage :
                                                       DeleteRegistrationsWarningMessage))
                {
                    if (deleteForm.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                }
                var deleted = 0;
                for (var i = 0; i < registrationsDataGridView.SelectedRows.Count; i++)
                {
                    var registrationInfo = registrationsDataGridView.SelectedRows[i].DataBoundItem as RegistrationInfo;
                    if (registrationInfo == null || registrationInfo.Registration == null)
                    {
                        continue;
                    }
                    try
                    {
                        await notificationHubClient.DeleteRegistrationAsync(registrationInfo.Registration);
                        writeToLog(string.Format(RegistrationsDeletedFormat,
                                                 registrationInfo.Registration.RegistrationId,
                                                 notificationHubDescription.Path));
                        deleted++;
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                    }
                }
                writeToLog(string.Format(RegistrationDeletedMessage, deleted, notificationHubDescription.Path));
                GetRegistrations(false, null);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void registrationsDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex == -1)
            {
                return;
            }
            registrationsDataGridView.Rows[e.RowIndex].Selected = true;
            deleteRegistrationToolStripMenuItem.Text = registrationsDataGridView.SelectedRows.Count > 1 ?
                                                           DeleteRegistrations :
                                                           DeleteRegistration;
            updateRegistrationToolStripMenuItem.Text = registrationsDataGridView.SelectedRows.Count > 1 ?
                                                           UpdateRegistrations :
                                                           UpdateRegistration;
            registrationContextMenuStrip.Show(Cursor.Position);
        }

        private void txtWnsPayload_TextChanged(object sender, EventArgs e)
        {
            if (string.Compare(cboWnsNotificationTemplate.Text, ManualTemplate,
                               StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                wnsPayload = txtWnsPayload.Text;
            }
        }

        private void SetRegistrationPage()
        {
            if (currentRegistrationPage < 0 || currentRegistrationPage >= registrationPageList.Count)
            {
                return;
            }
            if (mainTabControl.TabPages[RegistrationsPage] == null)
            {
                EnablePage(RegistrationsPage);
            }
            RegistrationInfo.SetRegistrations(registrationPageList[currentRegistrationPage]);
            registrationsBindingSource.DataSource = null;
            registrationsBindingSource.DataSource = new SortableBindingList<RegistrationInfo>(RegistrationInfo.Registrations);
            registrationsDataGridView.DataSource = registrationsBindingSource;
            if (mainTabControl.TabPages[RegistrationsPage] != null)
            {
                mainTabControl.SelectTab(RegistrationsPage);
            }
            txtCurrentRegistrationPage.Text = string.Format(RegistrationPageFormat, currentRegistrationPage + 1, registrationPageList.Count);
        }

        private void btnFirstRegistrationPage_Click(object sender, EventArgs e)
        {
            if (registrationPageList.Count == 0)
            {
                return;
            }
            currentRegistrationPage = 0;
            SetRegistrationPage();
        }

        private void btnPreviousRegistrationPage_Click(object sender, EventArgs e)
        {
            if (registrationPageList.Count == 0 || currentRegistrationPage == 0)
            {
                return;
            }
            currentRegistrationPage--;
            SetRegistrationPage();
        }

        private void btnNextRegistrationPage_Click(object sender, EventArgs e)
        {
            if (registrationPageList.Count == 0)
            {
                GetRegistrations(false, null);
                return;
            }
            if (currentRegistrationPage == registrationPageList.Count - 1)
            {
                if (registrationPageList[currentRegistrationPage].Count() == currentTopCount &&
                    !string.IsNullOrWhiteSpace(registrationPageList[currentRegistrationPage].ContinuationToken))
                {
                    GetRegistrations(false, registrationPageList[currentRegistrationPage].ContinuationToken);
                }
            }
            else
            {
                currentRegistrationPage++;
                SetRegistrationPage();
            }
        }

        private void btnLastRegistrationPage_Click(object sender, EventArgs e)
        {
            if (registrationPageList.Count == 0)
            {
                GetRegistrations(false, null);
                return;
            }
            var lastPageIndex = registrationPageList.Count - 1;
            if (registrationPageList[lastPageIndex].Count() == currentTopCount &&
                !string.IsNullOrWhiteSpace(registrationPageList[lastPageIndex].ContinuationToken))
            {
                GetRegistrations(false, registrationPageList[lastPageIndex].ContinuationToken);
            }
        }

        private void txtApnsJsonPayload_TextChanged(object sender, EventArgs e)
        {
            apnsPayload = txtApnsJsonPayload.Text;
        }

        private void txtGcmJsonPayload_TextChanged(object sender, EventArgs e)
        {
            gcmPayload = txtGcmJsonPayload.Text;
        }

        private void btnOpenDescriptionForm_Click(object sender, EventArgs e)
        {
            using (var form = new TextForm(UserMetadata, txtUserMetadata.Text))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtUserMetadata.Text = form.Content;
                }
            }
        }

        private void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
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

        private void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
        }

        private void listView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
        }

        private void listView_Resize(object sender, EventArgs e)
        {
            var listView = sender as ListView;
            if (listView == null)
            {
                return;
            }
            try
            {
                listView.SuspendDrawing();
                listView.SuspendLayout();
                var width = listView.Width - listView.Columns[0].Width - 5;
                var scrollbars = ScrollBarHelper.GetVisibleScrollbars(listView);
                if (scrollbars == ScrollBars.Vertical || scrollbars == ScrollBars.Both)
                {
                    width -= 17;
                }
                listView.Columns[1].Width = width;
            }
            finally
            {
                listView.ResumeLayout();
                listView.ResumeDrawing();
            }
        }

        private void sessionPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            var bindingList = registrationsBindingSource.DataSource as SortableBindingList<RegistrationInfo>;
            
            if (bindingList == null ||
                string.IsNullOrWhiteSpace(e.ChangedItem.Label) ||
                registrationsDataGridView.SelectedRows.Count == 0)
            {
                return;
            }
            var registrationInfo = bindingList[currentRegistrationRowIndex];
            var registration = registrationInfo.Registration;
            
            switch (e.ChangedItem.Label)
            {
                case ChannelUri:
                    var channelUri = e.ChangedItem.Value as string;
                    if (registration is MpnsRegistrationDescription && !string.IsNullOrWhiteSpace(channelUri))
                    {
                        ((MpnsRegistrationDescription)registration).ChannelUri = new Uri(channelUri);
                        registrationInfo.ChannelUri = channelUri;
                        registrationsDataGridView.Tag = true;
                        return;
                    }
                    if (registration is WindowsRegistrationDescription && !string.IsNullOrWhiteSpace(channelUri))
                    {
                        ((WindowsRegistrationDescription)registration).ChannelUri = new Uri(channelUri);
                        registrationInfo.ChannelUri = channelUri;
                        registrationsDataGridView.Tag = true;
                    }
                    break;
                case DeviceToken:
                    var deviceToken = e.ChangedItem.Value as string;
                    if (registration is AppleRegistrationDescription && !string.IsNullOrWhiteSpace(deviceToken))
                    {
                        ((AppleRegistrationDescription)registration).DeviceToken = deviceToken;
                        registrationInfo.ChannelUri = deviceToken;
                        registrationsDataGridView.Tag = true;
                    }
                    break;
                case GcmRegistrationId:
                    var gcmRegistrationId = e.ChangedItem.Value as string;
                    if (registration is GcmRegistrationDescription && !string.IsNullOrWhiteSpace(gcmRegistrationId))
                    {
                        ((GcmRegistrationDescription)registration).GcmRegistrationId = gcmRegistrationId;
                        registrationInfo.ChannelUri = gcmRegistrationId;
                        registrationsDataGridView.Tag = true;
                    }
                    break;
                case BodyTemplate:
                    if (registration is MpnsTemplateRegistrationDescription && e.ChangedItem.Value is string)
                    {
                        ((MpnsTemplateRegistrationDescription)registration).BodyTemplate = new CDataMember(e.ChangedItem.Value as string);
                        registrationsDataGridView.Tag = true;
                        return;
                    }
                    if (registration is WindowsTemplateRegistrationDescription && e.ChangedItem.Value is string)
                    {
                        ((WindowsTemplateRegistrationDescription)registration).BodyTemplate = new CDataMember(e.ChangedItem.Value as string);
                        registrationsDataGridView.Tag = true;
                    }
                    break;
                case Tags:
                    if (e.ChangedItem.Value is List<TagInfo>)
                    {
                        registration.Tags = new HashSet<string>((e.ChangedItem.Value as List<TagInfo>).Select(t => t.Tag));
                        registrationInfo.Tags = registration.Tags != null && registration.Tags.Any() ?
                                                registration.Tags.Aggregate((a, t) => a + "," + t) :
                                                null;
                        registrationsDataGridView.Tag = true;
                    }
                    break;
                case MpnsHeaders:
                    if (registration is MpnsTemplateRegistrationDescription && e.ChangedItem.Value is List<NotificationInfo>)
                    {
                        var list = e.ChangedItem.Value as List<NotificationInfo>;
                        var mpnsRegistration = registration as MpnsTemplateRegistrationDescription;
                        mpnsRegistration.MpnsHeaders = new MpnsHeaderCollection();
                        foreach (var item in list)
                        {
                            mpnsRegistration.MpnsHeaders.Add(item.Name, item.Value);
                        }
                        registrationsDataGridView.Tag = true;
                    }
                    break;
                case WnsHeaders:
                    if (registration is WindowsTemplateRegistrationDescription && e.ChangedItem.Value is List<NotificationInfo>)
                    {
                        var list = e.ChangedItem.Value as List<NotificationInfo>;
                        var mpnsRegistration = registration as WindowsTemplateRegistrationDescription;
                        mpnsRegistration.WnsHeaders = new WnsHeaderCollection();
                        foreach (var item in list)
                        {
                            mpnsRegistration.WnsHeaders.Add(item.Name, item.Value);
                        }
                        registrationsDataGridView.Tag = true;
                    }
                    break;
            }
        }

        private async void btnCreateRegistration_Click(object sender, EventArgs e)
        {
            using (var form = new RegistrationForm())
            {
                if (form.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                if (form.RegistrationObject == null || form.RegistrationType == null)
                {
                    return;
                }
                var bindingList = registrationsBindingSource.DataSource as SortableBindingList<RegistrationInfo>;
                if (bindingList == null)
                {
                    return;
                }
                var tagsAsList = form.RegistrationObject[Tags] as List<TagInfo>;
                HashSet<string> tags = null;
                if (tagsAsList != null)
                {
                    tags = new HashSet<string>(tagsAsList.Select(t => t.Tag));
                }
                RegistrationInfo registrationInfo = null;
                if (form.RegistrationType == typeof (MpnsRegistrationDescription))
                {
                    var registration = tags == null || tags.Count == 0?
                                       new MpnsRegistrationDescription(form.RegistrationObject[ChannelUri] as string) :
                                       new MpnsRegistrationDescription(form.RegistrationObject[ChannelUri] as string, tags);
                    registration = await notificationHubClient.CreateRegistrationAsync(registration);
                    registrationInfo = new RegistrationInfo
                        {
                            ChannelUri = registration.ChannelUri.AbsoluteUri,
                            ETag = registration.ETag,
                            ExpirationTime = registration.ExpirationTime,
                            RegistrationId = registration.RegistrationId,
                            Registration = registration,
                            Tags = registration.Tags != null && registration.Tags.Any() ?
                                   registration.Tags.Aggregate((a, t) => a + "," + t) :
                                   null
                        };
                }
                else if (form.RegistrationType == typeof(MpnsTemplateRegistrationDescription))
                {
                    var mpnsHeadersAsList = form.RegistrationObject[MpnsHeaders] as List<NotificationInfo>;
                    var mpnsHeaders = new MpnsHeaderCollection();
                    if (mpnsHeadersAsList != null)
                    {
                        foreach (var item in mpnsHeadersAsList)
                        {
                            mpnsHeaders.Add(item.Name, item.Value);
                        }
                    }
                    var registration = tags == null || tags.Count == 0 ?
                                       new MpnsTemplateRegistrationDescription(form.RegistrationObject[ChannelUri] as string,
                                                                               form.RegistrationObject[BodyTemplate] as string) :
                                       mpnsHeaders.Count == 0 ?
                                       new MpnsTemplateRegistrationDescription(form.RegistrationObject[ChannelUri] as string,
                                                                               form.RegistrationObject[BodyTemplate] as string,
                                                                               tags) :
                                       new MpnsTemplateRegistrationDescription(form.RegistrationObject[ChannelUri] as string,
                                                                               form.RegistrationObject[BodyTemplate] as string,
                                                                               mpnsHeaders,
                                                                               tags);
                    registration = await notificationHubClient.CreateRegistrationAsync(registration);
                    registrationInfo = new RegistrationInfo
                        {
                        ChannelUri = registration.ChannelUri.AbsoluteUri,
                        ETag = registration.ETag,
                        ExpirationTime = registration.ExpirationTime,
                        RegistrationId = registration.RegistrationId,
                        Registration = registration,
                        Tags = registration.Tags != null && registration.Tags.Any() ?
                               registration.Tags.Aggregate((a, t) => a + "," + t) :
                               null
                    };
                }
                else if (form.RegistrationType == typeof(WindowsRegistrationDescription))
                {
                    var registration = tags == null || tags.Count == 0 ?
                                       new WindowsRegistrationDescription(form.RegistrationObject[ChannelUri] as string) :
                                       new WindowsRegistrationDescription(form.RegistrationObject[ChannelUri] as string, tags);
                    registration = await notificationHubClient.CreateRegistrationAsync(registration);
                    registrationInfo = new RegistrationInfo
                        {
                        ChannelUri = registration.ChannelUri.AbsoluteUri,
                        ETag = registration.ETag,
                        ExpirationTime = registration.ExpirationTime,
                        RegistrationId = registration.RegistrationId,
                        Registration = registration,
                        Tags = registration.Tags != null && registration.Tags.Any() ?
                               registration.Tags.Aggregate((a, t) => a + "," + t) :
                               null
                    };
                }
                else if (form.RegistrationType == typeof(WindowsTemplateRegistrationDescription))
                {
                    var wnsHeadersAsList = form.RegistrationObject[WnsHeaders] as List<NotificationInfo>;
                    var wnsHeaders = new WnsHeaderCollection();
                    if (wnsHeadersAsList != null)
                    {
                        foreach (var item in wnsHeadersAsList)
                        {
                            wnsHeaders.Add(item.Name, item.Value);
                        }
                    }
                    var registration = tags == null || tags.Count == 0 ?
                                       new WindowsTemplateRegistrationDescription(form.RegistrationObject[ChannelUri] as string,
                                                                                 form.RegistrationObject[BodyTemplate] as string) :
                                       wnsHeaders.Count == 0 ?
                                       new WindowsTemplateRegistrationDescription(form.RegistrationObject[ChannelUri] as string,
                                                                                  form.RegistrationObject[BodyTemplate] as string,
                                                                                  tags) :
                                       new WindowsTemplateRegistrationDescription(form.RegistrationObject[ChannelUri] as string,
                                                                                  form.RegistrationObject[BodyTemplate] as string,
                                                                                  wnsHeaders,
                                                                                  tags);
                    registration = await notificationHubClient.CreateRegistrationAsync(registration);
                    registrationInfo = new RegistrationInfo
                        {
                        ChannelUri = registration.ChannelUri.AbsoluteUri,
                        ETag = registration.ETag,
                        ExpirationTime = registration.ExpirationTime,
                        RegistrationId = registration.RegistrationId,
                        Registration = registration,
                        Tags = registration.Tags != null && registration.Tags.Any() ?
                               registration.Tags.Aggregate((a, t) => a + "," + t) :
                               null
                    };
                }
                else if (form.RegistrationType == typeof(AppleRegistrationDescription))
                {
                    var registration = tags == null || tags.Count == 0 ?
                                       new AppleRegistrationDescription(form.RegistrationObject[DeviceToken] as string) :
                                       new AppleRegistrationDescription(form.RegistrationObject[DeviceToken] as string, tags);
                    registration = await notificationHubClient.CreateRegistrationAsync(registration);
                    registrationInfo = new RegistrationInfo
                    {
                        ChannelUri = registration.DeviceToken,
                        ETag = registration.ETag,
                        ExpirationTime = registration.ExpirationTime,
                        RegistrationId = registration.RegistrationId,
                        Registration = registration,
                        Tags = registration.Tags != null && registration.Tags.Any() ?
                               registration.Tags.Aggregate((a, t) => a + "," + t) :
                               null
                    };
                }
                else if (form.RegistrationType == typeof(AppleTemplateRegistrationDescription))
                {
                    var bodyTemplate = form.RegistrationObject.Properties.Any(p => string.Compare(p.Name, BodyTemplate, StringComparison.InvariantCultureIgnoreCase) == 0)
                                           ? form.RegistrationObject[BodyTemplate] as string
                                           : null;
                    var registration = tags != null && tags.Count > 0 ?
                                       new AppleTemplateRegistrationDescription(form.RegistrationObject[DeviceToken] as string, bodyTemplate, tags) :
                                       string.IsNullOrWhiteSpace(bodyTemplate) ?
                                       new AppleTemplateRegistrationDescription(form.RegistrationObject[DeviceToken] as string) :
                                       new AppleTemplateRegistrationDescription(form.RegistrationObject[DeviceToken] as string, bodyTemplate);
                    registration = await notificationHubClient.CreateRegistrationAsync(registration);
                    registrationInfo = new RegistrationInfo
                    {
                        ChannelUri = registration.DeviceToken,
                        ETag = registration.ETag,
                        ExpirationTime = registration.ExpirationTime,
                        RegistrationId = registration.RegistrationId,
                        Registration = registration,
                        Tags = registration.Tags != null && registration.Tags.Any() ?
                               registration.Tags.Aggregate((a, t) => a + "," + t) :
                               null
                    };
                }
                else if (form.RegistrationType == typeof(GcmRegistrationDescription))
                {
                    var registration = tags == null || tags.Count == 0 ?
                                       new GcmRegistrationDescription(form.RegistrationObject[GcmRegistrationId] as string) :
                                       new GcmRegistrationDescription(form.RegistrationObject[GcmRegistrationId] as string, tags);
                    registration = await notificationHubClient.CreateRegistrationAsync(registration);
                    registrationInfo = new RegistrationInfo
                    {
                        ChannelUri = registration.GcmRegistrationId,
                        ETag = registration.ETag,
                        ExpirationTime = registration.ExpirationTime,
                        RegistrationId = registration.RegistrationId,
                        Registration = registration,
                        Tags = registration.Tags != null && registration.Tags.Any() ?
                               registration.Tags.Aggregate((a, t) => a + "," + t) :
                               null
                    };
                }
                else if (form.RegistrationType == typeof(GcmTemplateRegistrationDescription))
                {
                    var bodyTemplate = form.RegistrationObject.Properties.Any(p => string.Compare(p.Name, BodyTemplate, StringComparison.InvariantCultureIgnoreCase) == 0)
                                           ? form.RegistrationObject[BodyTemplate] as string
                                           : null;
                    var registration = tags != null && tags.Count > 0 ?
                                       new GcmTemplateRegistrationDescription(form.RegistrationObject[GcmRegistrationId] as string, bodyTemplate, tags) :
                                       string.IsNullOrWhiteSpace(bodyTemplate) ?
                                       new GcmTemplateRegistrationDescription(form.RegistrationObject[GcmRegistrationId] as string) :
                                       new GcmTemplateRegistrationDescription(form.RegistrationObject[GcmRegistrationId] as string, bodyTemplate);
                    registration = await notificationHubClient.CreateRegistrationAsync(registration);
                    registrationInfo = new RegistrationInfo
                    {
                        ChannelUri = registration.GcmRegistrationId,
                        ETag = registration.ETag,
                        ExpirationTime = registration.ExpirationTime,
                        RegistrationId = registration.RegistrationId,
                        Registration = registration,
                        Tags = registration.Tags != null && registration.Tags.Any() ?
                               registration.Tags.Aggregate((a, t) => a + "," + t) :
                               null
                    };
                }
                if (registrationInfo == null)
                {
                    return;
                }
                writeToLog(string.Format(RegistrationCreatedMessage, 
                                         registrationInfo.PlatformType,
                                         notificationHubDescription.Path));
                bindingList.Add(registrationInfo);
                registrationsBindingSource.DataSource = null;
                registrationsBindingSource.DataSource = new SortableBindingList<RegistrationInfo>(RegistrationInfo.Registrations);
                registrationsDataGridView.DataSource = registrationsBindingSource;
            }
        }

        private void btnRefreshRegistrations_Click(object sender, EventArgs e)
        {
            GetRegistrations(false, null);
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

        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var gridView = sender as DataGridView;
            if (null == gridView)
            {
                return;
            }
            using (var solidBrush = new SolidBrush(gridView.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(CultureInfo.InvariantCulture), 
                                       e.InheritedRowStyle.Font, 
                                       solidBrush, 
                                       e.RowBounds.Location.X + 14, 
                                       e.RowBounds.Location.Y + 4);
            }
        }

        private void checkBoxEnableUnauthenticatedMpns_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEnableUnauthenticatedMpns.Checked)
            {
                mpnsCredentialCertificatePath = null;
                mpnsCredentialCertificateKey = null;
                txtMpnsCredentialCertificateThumbprint.Text = null;
            }
        }

        private void btnClearApnsNotification_Click(object sender, EventArgs e)
        {
            apnsCredentialCertificatePath = null;
            apnsCredentialCertificateKey = null;
            txtApnsCredentialCertificateThumbprint.Text = null;
        }

        private void btnClearMpnsNotification_Click(object sender, EventArgs e)
        {
            mpnsCredentialCertificatePath = null;
            mpnsCredentialCertificateKey = null;
            txtMpnsCredentialCertificateThumbprint.Text = null;
            checkBoxEnableUnauthenticatedMpns.Checked = false;
        }

        private void btnClearGcmNotification_Click(object sender, EventArgs e)
        {
            txtGcmApiKey.Text = null;
        }

        private void btnClearWnsNotification_Click(object sender, EventArgs e)
        {
            txtPackageSid.Text = null;
            txtClientSecret.Text = null;
        }

        private void templateTagsTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(templateTagsTabControl, e, null);
        }

        private void mpnsTagsTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(mpnsTagsTabControl, e, null);
        }

        private void wnsTagsTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(wnsTagsTabControl, e, null);
        }

        private void appleTagsTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(appleTagsTabControl, e, null);
        }

        private void gcmTagsTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(gcmTagsTabControl, e, null);
        }

        private void appleAdditionalHeadersTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(appleAdditionalHeadersTabControl, e, null);
        }

        private void wnsTemplateTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(wnsTemplateTabControl, e, null);
        }

        private void mpnsTemplateTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(mpnsTemplateTabControl, e, null);
        }

        private void templatePropertiesTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(templatePropertiesTabControl, e, null);
        }

        private void gcmAdditionalHeadersTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(gcmAdditionalHeadersTabControl, e, null);
        }

        private void pictFindRegistrations_Click(object sender, EventArgs e)
        {
            try
            {
                registrationsDataGridView.SuspendDrawing();
                registrationsDataGridView.SuspendLayout();
                if (registrationPageList == null || registrationPageList.Count == 0)
                {
                    return;
                }
                using (var form = new TextForm(FilterExpressionTitle, FilterExpressionLabel, registrationsFilterExpression))
                {
                    form.Size = new Size(600, 200);
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    registrationsFilterExpression = form.Content;
                    if (string.IsNullOrWhiteSpace(registrationsFilterExpression))
                    {
                        currentRegistrationPage = 0;
                        SetRegistrationPage();
                        writeToLog(FilterExpressionRemovedMessage);
                    }
                    else
                    {
                        IEnumerable<RegistrationDescription> filteredCollection;
                        try
                        {
                            var expressionBuilder = new ExpressionBuilder<RegistrationDescription>();
                            var predicate = expressionBuilder.GetExpression(registrationsFilterExpression).Compile();
                            filteredCollection = registrationPageList.SelectMany(rd => rd).Where(predicate).ToList();
                        }
                        catch (Exception ex)
                        {
                            writeToLog(string.Format(FilterExpressionNotValidMessage, registrationsFilterExpression));
                            HandleException(ex);
                            return;
                        }
                        RegistrationInfo.SetRegistrations(filteredCollection);
                        var bindingList = new SortableBindingList<RegistrationInfo>(RegistrationInfo.Registrations)
                        {
                            AllowEdit = false,
                            AllowNew = false,
                            AllowRemove = false
                        };
                        registrationsBindingSource.DataSource = bindingList;
                        registrationsDataGridView.DataSource = registrationsBindingSource;
                        writeToLog(string.Format(FilterExpressionAppliedMessage, registrationsFilterExpression, bindingList.Count));
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                registrationsDataGridView.ResumeLayout();
                registrationsDataGridView.ResumeDrawing();
            }
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            var pictureBox = sender as PictureBox;
            if (pictureBox != null)
            {
                pictureBox.Image = Resources.FindExtensionRaised;
            }
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            var pictureBox = sender as PictureBox;
            if (pictureBox != null)
            {
                pictureBox.Image = Resources.FindExtension;
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                sorting = true;
            }
        }

        private void dataGridView_Sorted(object sender, EventArgs e)
        {
            sorting = false;
        }

        private void authorizationRulesDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void registrationsDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void templateNotificationDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void appleHeadersDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void gcmHeadersDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        /*
        private EventHandlerList GetEventHandlerList(IDisposable control, string eventName)
        {
            if (control == null || string.IsNullOrWhiteSpace(eventName))
            {
                return null;
            }
            var fieldInfo = typeof(Control).GetField(eventName, BindingFlags.Static | BindingFlags.NonPublic);
            if (fieldInfo == null)
            {
                return null;
            }
            var obj = fieldInfo.GetValue(control);
            var propertyInfo = control.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
            var list = (EventHandlerList)propertyInfo.GetValue(control, null);
            return list;
        }

        private void RemoveEvent(IDisposable control, string eventName)
        {
            if (control == null || string.IsNullOrWhiteSpace(eventName))
            {
                return;
            }
            var fieldInfo = typeof(Control).GetField(eventName, BindingFlags.Static | BindingFlags.NonPublic);
            if (fieldInfo == null)
            {
                return;
            }
            var obj = fieldInfo.GetValue(control);
            var propertyInfo = control.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
            var list = (EventHandlerList)propertyInfo.GetValue(control, null);
            list.RemoveHandler(obj, list[obj]);
        } 
         */

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
                if (!MetricInfo.EntityMetricDictionary.ContainsKey(NotificationHubEntity))
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
                    item.Entity = notificationHubDescription.Path;
                    item.Type = NotificationHubEntity;
                }
                BindingList<MetricDataPoint> pointBindingList;
                var allDataPoint = dataPointBindingList.FirstOrDefault(m => string.Compare(m.Metric, "all", StringComparison.OrdinalIgnoreCase) == 0);
                if (allDataPoint != null)
                {
                    pointBindingList = new BindingList<MetricDataPoint>();
                    foreach (var item in MetricInfo.EntityMetricDictionary[NotificationHubEntity])
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
                    var metricInfo = MetricInfo.EntityMetricDictionary[NotificationHubEntity].FirstOrDefault(m => m.Name == pointBindingList[i].Metric);
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
            if (metricTabPageIndexList.Count <= 0)
            {
                return;
            }
            for (var i = 0; i < metricTabPageIndexList.Count; i++)
            {
                mainTabControl.TabPages.RemoveByKey(metricTabPageIndexList[i]);
            }
            metricTabPageIndexList.Clear();
            btnCloseTabs.Enabled = false;
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
                var dataGridViewComboBoxColumn = (DataGridViewComboBoxColumn)dataPointDataGridView.Columns[MetricProperty];
                if (dataGridViewComboBoxColumn != null)
                {
                    dataGridViewComboBoxColumn.DataSource = MetricInfo.EntityMetricDictionary.ContainsKey(NotificationHubEntity)
                        ? MetricInfo.EntityMetricDictionary[NotificationHubEntity]
                        : null;
                }
            });
        }
        #endregion
    }
}
