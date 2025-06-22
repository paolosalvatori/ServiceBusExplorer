// // Auto-added comment

// using Azure.Messaging;
// using Azure.Messaging.EventGrid.Namespaces;
// using Azure.ResourceManager.EventGrid;
// using EventGridExplorerLibrary;
// using ServiceBusExplorer.Helpers;
// using ServiceBusExplorer.UIHelpers;
// using ServiceBusExplorer.Utilities.Helpers;
// using System;
// using System.Collections.Generic;
// using System.ComponentModel;
// using System.Drawing;
// using System.Text.Json;
// using System.Windows.Forms;

// namespace ServiceBusExplorer.Controls
// {
//     public partial class HandleEventGridSubscriptionControl : UserControl
//     {
//         #region Private Constants
//         //***************************
//         // Texts
//         //***************************
//         private const string Id = "ID";
//         private const string Source = "Source";
//         private const string Type = "Type";
//         private const string Data = "Data";
//         private const string Status = "Status";

//         //***************************
//         // Property Labels
//         //***************************
//         private const string SubName = "Subscription Name";
//         private const string SubId = "ID";
//         private const string SubType = "Type";
//         private const string ProvisioningState = "Provisioning State";
//         private const string EventDeliverySchema = "Delivery Schema";

//         //***************************
//         // Index Values
//         //***************************
//         private const int EventStatusColIndex = 5;
//         #endregion

//         #region Private Fields
//         private WriteToLogDelegate writeToLog;
//         private NamespaceTopicEventSubscriptionResource subscription;
//         private NamespaceTopicResource topic;
//         private EventGridLibrary eventGridLibrary;
//         private CloudEvent cloudEvent = default!;
//         private IReadOnlyList<ReceiveDetails> receivedEvents;
//         #endregion

//         public HandleEventGridSubscriptionControl(WriteToLogDelegate writeToLog, EventGridSubscriptionWrapper subscriptionWrapper, EventGridLibrary eventGridLibrary)
//         {
//             this.writeToLog = writeToLog;
//             this.subscription = subscriptionWrapper.SubscriptionDescription;
//             this.topic = subscriptionWrapper.TopicDescription;
//             this.eventGridLibrary = eventGridLibrary;

//             InitializeComponent();
//             ConfigureReadUserInterface();
//         }

//         private void ConfigureReadUserInterface()
//         {
//             // Set Grid style
//             eventsDataGridView.EnableHeadersVisualStyles = false;
//             eventsDataGridView.AutoGenerateColumns = false;
//             eventsDataGridView.AutoSize = true;

//             // Create columns for events in data grid view
//             var textBoxColumn = new DataGridViewTextBoxColumn
//             {
//                 DataPropertyName = Id,
//                 Name = Id,
//                 Width = 120
//             };
//             eventsDataGridView.Columns.Add(textBoxColumn);
            
//             textBoxColumn = new DataGridViewTextBoxColumn
//             {
//                 DataPropertyName = Source,
//                 Name = Source,
//                 Width = 120
//             };
//             eventsDataGridView.Columns.Add(textBoxColumn);

//             textBoxColumn = new DataGridViewTextBoxColumn
//             {
//                 DataPropertyName = Type,
//                 Name = Type,
//                 Width = 120
//             };
//             eventsDataGridView.Columns.Add(textBoxColumn);

//             textBoxColumn = new DataGridViewTextBoxColumn
//             {
//                 DataPropertyName = Data,
//                 Name = Data,
//                 Width = 120
//             };
//             eventsDataGridView.Columns.Add(textBoxColumn);

//             textBoxColumn = new DataGridViewTextBoxColumn
//             {
//                 DataPropertyName = Status,
//                 Name = Status,
//                 Width = 120
//             };
//             eventsDataGridView.Columns.Add(textBoxColumn);

//             // Set the selection background color for all the cells.
//             eventsDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
//             eventsDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

//             // Set RowHeadersDefaultCellStyle.SelectionBackColor
//             eventsDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

//             // Set the background color for all rows
//             eventsDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
//             eventsDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

//             // Set the row and column header styles.
//             eventsDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
//             eventsDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
//             eventsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
//             eventsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

//             // Set Grid style
//             eventsDataGridView.EnableHeadersVisualStyles = false;
//             eventsDataGridView.AutoGenerateColumns = false;
//             eventsDataGridView.AutoSize = true;

//             // Set up subscription property list view
//             var propertyList = new List<string[]>();

//             propertyList.AddRange(new[]
//             {
//                 new[] {SubName, subscription.Data.Name},
//                 new[] {SubId, subscription.Id.ToString()},
//                 new[] {SubType, subscription.Data.ResourceType.Type},
//                 new[] {ProvisioningState, subscription.Data.ProvisioningState.Value.ToString()},
//                 new[] {EventDeliverySchema, subscription.Data.EventDeliverySchema.ToString()}
//             });

//             subscriptionListView.Items.Clear();

//             foreach (var array in propertyList)
//             {
//                 subscriptionListView.Items.Add(new ListViewItem(array));
//             }
//         }

//         private void eventsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
//         {
//             try
//             {
//                 var bindingList = eventsBindingSource.DataSource as BindingList<CloudEvent>;
//                 if (bindingList == null)
//                 {
//                     return;
//                 }
//                 if (cloudEvent == bindingList[e.RowIndex])
//                 {
//                     return;
//                 }
//                 cloudEvent = bindingList[e.RowIndex];

//                 string eventJson = JsonSerializer.Serialize(cloudEvent, new JsonSerializerOptions { WriteIndented = true });
//                 receiveEventInfo.Text = eventJson;
//             }
//             // ReSharper disable once EmptyGeneralCatchClause
//             catch (Exception)
//             {
//             }
//         }

//         public void DisplayEventsReceived(ReceiveResult allEvents)
//         {
//             if (allEvents == null)
//             {
//                 eventsBindingSource.DataSource = null;
//                 eventsDataGridView.DataSource = null;
//                 receiveEventInfo.Text = string.Empty;
//                 return;
//             }

//             receivedEvents = allEvents.Value;
//             List<CloudEvent> cloudEvents = new List<CloudEvent>();  
            
//             foreach (var cloudEvent in receivedEvents)
//             {
//                 cloudEvents.Add(cloudEvent.Event);
//             }

//             var eventBindingList = new SortableBindingList<CloudEvent>(cloudEvents)
//             {
//                 AllowEdit = false,
//                 AllowNew = false,
//                 AllowRemove = false
//             };

//             eventsBindingSource.DataSource = eventBindingList;
//             eventsDataGridView.DataSource = eventsBindingSource;
//         }

//         private void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
//         {
//             var startX = e.ColumnIndex == 0 ? -1 : e.Bounds.X;
//             var endX = e.Bounds.X + e.Bounds.Width - 1;
//             // Background
//             e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(215, 228, 242)), startX, -1, e.Bounds.Width + 1,
//                 e.Bounds.Height + 1);
//             // Left vertical line
//             e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight), startX, -1, startX,
//                 e.Bounds.Y + e.Bounds.Height + 1);
//             // TopCount horizontal line
//             e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight), startX, -1, endX, -1);
//             // Bottom horizontal line
//             e.Graphics.DrawLine(new Pen(SystemColors.ControlDark), startX, e.Bounds.Height - 1, endX,
//                 e.Bounds.Height - 1);
//             // Right vertical line
//             e.Graphics.DrawLine(new Pen(SystemColors.ControlDark), endX, -1, endX, e.Bounds.Height + 1);
//             var roundedFontSize = (float)Math.Round(e.Font.SizeInPoints);
//             var bounds = new RectangleF(e.Bounds.X + 4, (e.Bounds.Height - 8 - roundedFontSize) / 2, e.Bounds.Width,
//                 roundedFontSize + 6);
//             e.Graphics.DrawString(e.Header.Text, e.Font, new SolidBrush(SystemColors.ControlText), bounds);
//         }

//         private void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
//         {
//             e.DrawBackground();
//             e.DrawText();
//         }

//         private void listView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
//         {
//             e.DrawBackground();
//             e.DrawText();
//         }

//         private void listView_Resize(object sender, EventArgs e)
//         {
//             var listView = sender as ListView;
//             if (listView == null)
//             {
//                 return;
//             }
//             try
//             {
//                 listView.SuspendDrawing();
//                 listView.SuspendLayout();
//                 var width = listView.Width - listView.Columns[0].Width - 5;
//                 var scrollbars = ScrollBarHelper.GetVisibleScrollbars(listView);
//                 if (scrollbars == ScrollBars.Vertical || scrollbars == ScrollBars.Both)
//                 {
//                     width -= 17;
//                 }
//                 listView.Columns[1].Width = width;
//             }
//             finally
//             {
//                 listView.ResumeLayout();
//                 listView.ResumeDrawing();
//             }
//         }

//         private async void btnEventAction_Click(object sender, EventArgs e)
//         {
//             List<string> lockTokens = new List<string>();
//             List<int> selectedRows = new List<int>();
//             var button = sender as Button;

//             foreach (DataGridViewRow row in eventsDataGridView.Rows)
//             {
//                 if (row.Cells[EventStatusColIndex].Value == null)
//                 {
//                     var checkboxCell = row.Cells[0] as DataGridViewCheckBoxCell;

//                     // Determine selected events
//                     if (Convert.ToBoolean(checkboxCell.Value))
//                     {
//                         lockTokens.Add(receivedEvents[row.Index].BrokerProperties.LockToken);
//                         selectedRows.Add(row.Index);
//                     }
//                 }
//             }

//             // Execute event action (acknowledge/release/reject)
//             var eventActionResult = await eventGridLibrary.EventActionsAsync(button.Text, lockTokens, topic.Data.Name, subscription.Data.Name, writeToLog);

//             if (eventActionResult)
//             {
//                 foreach (int index in selectedRows)
//                 {
//                     // Update status column
//                     eventsDataGridView.Rows[index].Cells[EventStatusColIndex].Value = button.Text;
//                     eventsDataGridView.Rows[index].ReadOnly = true;
//                 }
//             }
//         }
//     }
// }
