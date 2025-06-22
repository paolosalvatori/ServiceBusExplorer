// // Auto-added comment

// using System.Windows.Forms;

// namespace ServiceBusExplorer.Controls
// {
//     partial class HandleEventGridSubscriptionControl
//     {
//         /// <summary> 
//         /// Required designer variable.
//         /// </summary>
//         private System.ComponentModel.IContainer components = null;

//         /// <summary> 
//         /// Clean up any resources being used.
//         /// </summary>
//         /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//         protected override void Dispose(bool disposing)
//         {
//             if (disposing && (this.components != null))
//             {
//                 this.components.Dispose();
//             }
//             base.Dispose(disposing);
//         }

//         #region Component Designer generated code

//         /// <summary> 
//         /// Required method for Designer support - do not modify 
//         /// the contents of this method with the code editor.
//         /// </summary>
//         private void InitializeComponent()
//         {
//             this.components = new System.ComponentModel.Container();
//             System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
//             System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HandleEventGridSubscriptionControl));
//             this.eventsBindingSource = new System.Windows.Forms.BindingSource(this.components);
//             this.subscriptionPropertyListView = new ServiceBusExplorer.Controls.Grouper();
//             this.subscriptionListView = new System.Windows.Forms.ListView();
//             this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
//             this.valueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
//             this.propertyGrid = new ServiceBusExplorer.Controls.ReadOnlyPropertyGrid();
//             this.receivedEventsGrouper = new ServiceBusExplorer.Controls.Grouper();
//             this.btnAck = new System.Windows.Forms.Button();
//             this.btnRel = new System.Windows.Forms.Button();
//             this.btnRej = new System.Windows.Forms.Button();
//             this.eventsDataGridView = new System.Windows.Forms.DataGridView();
//             this.eventsCheckboxCol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
//             this.grouperCaption = new ServiceBusExplorer.Controls.Grouper();
//             this.receiveEventInfo = new FastColoredTextBoxNS.FastColoredTextBox();
//             ((System.ComponentModel.ISupportInitialize)(this.eventsBindingSource)).BeginInit();
//             this.subscriptionPropertyListView.SuspendLayout();
//             this.receivedEventsGrouper.SuspendLayout();
//             ((System.ComponentModel.ISupportInitialize)(this.eventsDataGridView)).BeginInit();
//             this.grouperCaption.SuspendLayout();
//             ((System.ComponentModel.ISupportInitialize)(this.receiveEventInfo)).BeginInit();
//             this.SuspendLayout();
//             // 
//             // subscriptionPropertyListView
//             // 
//             this.subscriptionPropertyListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
//             | System.Windows.Forms.AnchorStyles.Left) 
//             | System.Windows.Forms.AnchorStyles.Right)));
//             this.subscriptionPropertyListView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
//             this.subscriptionPropertyListView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
//             this.subscriptionPropertyListView.BackgroundGradientColor = System.Drawing.Color.White;
//             this.subscriptionPropertyListView.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
//             this.subscriptionPropertyListView.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
//             this.subscriptionPropertyListView.BorderThickness = 1F;
//             this.subscriptionPropertyListView.Controls.Add(this.subscriptionListView);
//             this.subscriptionPropertyListView.Controls.Add(this.propertyGrid);
//             this.subscriptionPropertyListView.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
//             this.subscriptionPropertyListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
//             this.subscriptionPropertyListView.ForeColor = System.Drawing.Color.White;
//             this.subscriptionPropertyListView.GroupImage = null;
//             this.subscriptionPropertyListView.GroupTitle = "Subscription Properties";
//             this.subscriptionPropertyListView.Location = new System.Drawing.Point(1081, 44);
//             this.subscriptionPropertyListView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
//             this.subscriptionPropertyListView.Name = "subscriptionPropertyListView";
//             this.subscriptionPropertyListView.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
//             this.subscriptionPropertyListView.PaintGroupBox = true;
//             this.subscriptionPropertyListView.RoundCorners = 4;
//             this.subscriptionPropertyListView.ShadowColor = System.Drawing.Color.DarkGray;
//             this.subscriptionPropertyListView.ShadowControl = false;
//             this.subscriptionPropertyListView.ShadowThickness = 1;
//             this.subscriptionPropertyListView.Size = new System.Drawing.Size(392, 753);
//             this.subscriptionPropertyListView.TabIndex = 41;
//             // 
//             // subscriptionListView
//             // 
//             this.subscriptionListView.Alignment = System.Windows.Forms.ListViewAlignment.Default;
//             this.subscriptionListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
//             | System.Windows.Forms.AnchorStyles.Left) 
//             | System.Windows.Forms.AnchorStyles.Right)));
//             this.subscriptionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
//             this.nameColumnHeader,
//             this.valueColumnHeader});
//             this.subscriptionListView.HideSelection = false;
//             this.subscriptionListView.Location = new System.Drawing.Point(33, 51);
//             this.subscriptionListView.Name = "subscriptionListView";
//             this.subscriptionListView.OwnerDraw = true;
//             this.subscriptionListView.Size = new System.Drawing.Size(326, 666);
//             this.subscriptionListView.TabIndex = 0;
//             this.subscriptionListView.UseCompatibleStateImageBehavior = false;
//             this.subscriptionListView.View = System.Windows.Forms.View.Details;
//             this.subscriptionListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView_DrawColumnHeader);
//             this.subscriptionListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView_DrawItem);
//             this.subscriptionListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listView_DrawSubItem);
//             this.subscriptionListView.Resize += new System.EventHandler(this.listView_Resize);
//             // 
//             // nameColumnHeader
//             // 
//             this.nameColumnHeader.Text = "Property";
//             this.nameColumnHeader.Width = 115;
//             // 
//             // valueColumnHeader
//             // 
//             this.valueColumnHeader.Text = "Value";
//             this.valueColumnHeader.Width = 200;
//             // 
//             // propertyGrid
//             // 
//             this.propertyGrid.Location = new System.Drawing.Point(172, 193);
//             this.propertyGrid.Name = "propertyGrid";
//             this.propertyGrid.ReadOnly = false;
//             this.propertyGrid.Size = new System.Drawing.Size(8, 8);
//             this.propertyGrid.TabIndex = 0;
//             // 
//             // receivedEventsGrouper
//             // 
//             this.receivedEventsGrouper.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
//             | System.Windows.Forms.AnchorStyles.Left)));
//             this.receivedEventsGrouper.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
//             this.receivedEventsGrouper.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
//             this.receivedEventsGrouper.BackgroundGradientColor = System.Drawing.Color.White;
//             this.receivedEventsGrouper.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
//             this.receivedEventsGrouper.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
//             this.receivedEventsGrouper.BorderThickness = 1F;
//             this.receivedEventsGrouper.Controls.Add(this.btnAck);
//             this.receivedEventsGrouper.Controls.Add(this.btnRel);
//             this.receivedEventsGrouper.Controls.Add(this.btnRej);
//             this.receivedEventsGrouper.Controls.Add(this.eventsDataGridView);
//             this.receivedEventsGrouper.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
//             this.receivedEventsGrouper.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
//             this.receivedEventsGrouper.ForeColor = System.Drawing.Color.White;
//             this.receivedEventsGrouper.GroupImage = null;
//             this.receivedEventsGrouper.GroupTitle = "Received Events";
//             this.receivedEventsGrouper.Location = new System.Drawing.Point(41, 44);
//             this.receivedEventsGrouper.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
//             this.receivedEventsGrouper.Name = "receivedEventsGrouper";
//             this.receivedEventsGrouper.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
//             this.receivedEventsGrouper.PaintGroupBox = true;
//             this.receivedEventsGrouper.RoundCorners = 4;
//             this.receivedEventsGrouper.ShadowColor = System.Drawing.Color.DarkGray;
//             this.receivedEventsGrouper.ShadowControl = false;
//             this.receivedEventsGrouper.ShadowThickness = 1;
//             this.receivedEventsGrouper.Size = new System.Drawing.Size(1009, 420);
//             this.receivedEventsGrouper.TabIndex = 40;
//             // 
//             // btnAck
//             // 
//             this.btnAck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
//             this.btnAck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
//             this.btnAck.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
//             this.btnAck.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
//             this.btnAck.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
//             this.btnAck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//             this.btnAck.ForeColor = System.Drawing.SystemColors.ControlText;
//             this.btnAck.Location = new System.Drawing.Point(548, 365);
//             this.btnAck.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
//             this.btnAck.Name = "btnAck";
//             this.btnAck.Size = new System.Drawing.Size(133, 37);
//             this.btnAck.TabIndex = 41;
//             this.btnAck.Text = "Acknowledge";
//             this.btnAck.UseVisualStyleBackColor = false;
//             this.btnAck.Click += new System.EventHandler(this.btnEventAction_Click);
//             // 
//             // btnRel
//             // 
//             this.btnRel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
//             this.btnRel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
//             this.btnRel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
//             this.btnRel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
//             this.btnRel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
//             this.btnRel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//             this.btnRel.ForeColor = System.Drawing.SystemColors.ControlText;
//             this.btnRel.Location = new System.Drawing.Point(697, 365);
//             this.btnRel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
//             this.btnRel.Name = "btnRel";
//             this.btnRel.Size = new System.Drawing.Size(133, 37);
//             this.btnRel.TabIndex = 42;
//             this.btnRel.Text = "Release";
//             this.btnRel.UseVisualStyleBackColor = false;
//             this.btnRel.Click += new System.EventHandler(this.btnEventAction_Click);
//             // 
//             // btnRej
//             // 
//             this.btnRej.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
//             this.btnRej.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
//             this.btnRej.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
//             this.btnRej.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
//             this.btnRej.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
//             this.btnRej.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//             this.btnRej.ForeColor = System.Drawing.SystemColors.ControlText;
//             this.btnRej.Location = new System.Drawing.Point(844, 365);
//             this.btnRej.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
//             this.btnRej.Name = "btnRej";
//             this.btnRej.Size = new System.Drawing.Size(133, 37);
//             this.btnRej.TabIndex = 43;
//             this.btnRej.Text = "Reject";
//             this.btnRej.UseVisualStyleBackColor = false;
//             this.btnRej.Click += new System.EventHandler(this.btnEventAction_Click);
//             // 
//             // eventsDataGridView
//             // 
//             this.eventsDataGridView.AllowUserToAddRows = false;
//             this.eventsDataGridView.AllowUserToDeleteRows = false;
//             this.eventsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
//             | System.Windows.Forms.AnchorStyles.Left) 
//             | System.Windows.Forms.AnchorStyles.Right)));
//             this.eventsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
//             this.eventsDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
//             this.eventsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
//             this.eventsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
//             this.eventsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
//             this.eventsCheckboxCol});
//             this.eventsDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
//             this.eventsDataGridView.Location = new System.Drawing.Point(33, 51);
//             this.eventsDataGridView.Name = "eventsDataGridView";
//             this.eventsDataGridView.RowHeadersWidth = 62;
//             dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
//             this.eventsDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle1;
//             this.eventsDataGridView.RowTemplate.Height = 28;
//             this.eventsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
//             this.eventsDataGridView.Size = new System.Drawing.Size(943, 296);
//             this.eventsDataGridView.TabIndex = 40;
//             this.eventsDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.eventsDataGridView_RowEnter);
//             // 
//             // eventsCheckboxCol
//             // 
//             this.eventsCheckboxCol.HeaderText = "";
//             this.eventsCheckboxCol.MinimumWidth = 8;
//             this.eventsCheckboxCol.Name = "eventsCheckboxCol";
//             // 
//             // grouperCaption
//             // 
//             this.grouperCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//             this.grouperCaption.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
//             this.grouperCaption.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
//             this.grouperCaption.BackgroundGradientColor = System.Drawing.Color.White;
//             this.grouperCaption.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
//             this.grouperCaption.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
//             this.grouperCaption.BorderThickness = 1F;
//             this.grouperCaption.Controls.Add(this.receiveEventInfo);
//             this.grouperCaption.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
//             this.grouperCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
//             this.grouperCaption.ForeColor = System.Drawing.Color.White;
//             this.grouperCaption.GroupImage = null;
//             this.grouperCaption.GroupTitle = "Event Info";
//             this.grouperCaption.Location = new System.Drawing.Point(41, 493);
//             this.grouperCaption.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
//             this.grouperCaption.Name = "grouperCaption";
//             this.grouperCaption.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
//             this.grouperCaption.PaintGroupBox = true;
//             this.grouperCaption.RoundCorners = 4;
//             this.grouperCaption.ShadowColor = System.Drawing.Color.DarkGray;
//             this.grouperCaption.ShadowControl = false;
//             this.grouperCaption.ShadowThickness = 1;
//             this.grouperCaption.Size = new System.Drawing.Size(1009, 304);
//             this.grouperCaption.TabIndex = 38;
//             // 
//             // receiveEventInfo
//             // 
//             this.receiveEventInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
//             | System.Windows.Forms.AnchorStyles.Left) 
//             | System.Windows.Forms.AnchorStyles.Right)));
//             this.receiveEventInfo.AutoCompleteBracketsList = new char[] {
//             '(',
//             ')',
//             '{',
//             '}',
//             '[',
//             ']',
//             '\"',
//             '\"',
//             '\'',
//             '\''};
//             this.receiveEventInfo.AutoScrollMinSize = new System.Drawing.Size(35, 22);
//             this.receiveEventInfo.BackBrush = null;
//             this.receiveEventInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
//             this.receiveEventInfo.CharHeight = 22;
//             this.receiveEventInfo.CharWidth = 12;
//             this.receiveEventInfo.Cursor = System.Windows.Forms.Cursors.IBeam;
//             this.receiveEventInfo.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
//             this.receiveEventInfo.Font = new System.Drawing.Font("Courier New", 9.75F);
//             this.receiveEventInfo.ForeColor = System.Drawing.SystemColors.ControlText;
//             this.receiveEventInfo.IsReplaceMode = false;
//             this.receiveEventInfo.Location = new System.Drawing.Point(33, 49);
//             this.receiveEventInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
//             this.receiveEventInfo.Name = "receiveEventInfo";
//             this.receiveEventInfo.Paddings = new System.Windows.Forms.Padding(0);
//             this.receiveEventInfo.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
//             this.receiveEventInfo.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("receiveEventInfo.ServiceColors")));
//             this.receiveEventInfo.Size = new System.Drawing.Size(943, 219);
//             this.receiveEventInfo.TabIndex = 40;
//             this.receiveEventInfo.Zoom = 100;
//             // 
//             // HandleEventGridSubscriptionControl
//             // 
//             this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
//             this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//             this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
//             this.Controls.Add(this.subscriptionPropertyListView);
//             this.Controls.Add(this.receivedEventsGrouper);
//             this.Controls.Add(this.grouperCaption);
//             this.Name = "HandleEventGridSubscriptionControl";
//             this.Size = new System.Drawing.Size(1512, 837);
//             ((System.ComponentModel.ISupportInitialize)(this.eventsBindingSource)).EndInit();
//             this.subscriptionPropertyListView.ResumeLayout(false);
//             this.receivedEventsGrouper.ResumeLayout(false);
//             ((System.ComponentModel.ISupportInitialize)(this.eventsDataGridView)).EndInit();
//             this.grouperCaption.ResumeLayout(false);
//             ((System.ComponentModel.ISupportInitialize)(this.receiveEventInfo)).EndInit();
//             this.ResumeLayout(false);
//         }

//         #endregion
//         private Grouper grouperCaption;
//         private Grouper receivedEventsGrouper;
//         private System.Windows.Forms.DataGridView eventsDataGridView;
//         private System.Windows.Forms.BindingSource eventsBindingSource;
//         private FastColoredTextBoxNS.FastColoredTextBox receiveEventInfo;
//         private Grouper subscriptionPropertyListView;
//         private ReadOnlyPropertyGrid propertyGrid;
//         private ListView subscriptionListView;
//         private System.Windows.Forms.ColumnHeader nameColumnHeader;
//         private System.Windows.Forms.ColumnHeader valueColumnHeader;
//         private DataGridViewCheckBoxColumn eventsCheckboxCol;
//         private Button btnAck;
//         private Button btnRel;
//         private Button btnRej;
//     }
// }
