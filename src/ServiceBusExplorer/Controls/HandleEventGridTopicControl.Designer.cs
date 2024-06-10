namespace ServiceBusExplorer.Controls
{
    partial class HandleEventGridTopicControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.topicListView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.topicPropertyListView = new ServiceBusExplorer.Controls.Grouper();
            this.propertyGrid = new ServiceBusExplorer.Controls.ReadOnlyPropertyGrid();
            this.topicPropertyListView.SuspendLayout();
            this.SuspendLayout();
            // 
            // topicListView
            // 
            this.topicListView.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.topicListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topicListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.valueColumnHeader});
            this.topicListView.HideSelection = false;
            this.topicListView.Location = new System.Drawing.Point(33, 51);
            this.topicListView.Name = "topicListView";
            this.topicListView.OwnerDraw = true;
            this.topicListView.Size = new System.Drawing.Size(1230, 598);
            this.topicListView.TabIndex = 0;
            this.topicListView.UseCompatibleStateImageBehavior = false;
            this.topicListView.View = System.Windows.Forms.View.Details;
            this.topicListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView_DrawColumnHeader);
            this.topicListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView_DrawItem);
            this.topicListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listView_DrawSubItem);
            this.topicListView.Resize += new System.EventHandler(this.listView_Resize);
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Property";
            this.nameColumnHeader.Width = 115;
            // 
            // valueColumnHeader
            // 
            this.valueColumnHeader.Text = "Value";
            this.valueColumnHeader.Width = 200;
            // 
            // topicPropertyListView
            // 
            this.topicPropertyListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topicPropertyListView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.topicPropertyListView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.topicPropertyListView.BackgroundGradientColor = System.Drawing.Color.White;
            this.topicPropertyListView.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.topicPropertyListView.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.topicPropertyListView.BorderThickness = 1F;
            this.topicPropertyListView.Controls.Add(this.topicListView);
            this.topicPropertyListView.Controls.Add(this.propertyGrid);
            this.topicPropertyListView.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.topicPropertyListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.topicPropertyListView.ForeColor = System.Drawing.Color.White;
            this.topicPropertyListView.GroupImage = null;
            this.topicPropertyListView.GroupTitle = "Topic Properties";
            this.topicPropertyListView.Location = new System.Drawing.Point(68, 48);
            this.topicPropertyListView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.topicPropertyListView.Name = "topicPropertyListView";
            this.topicPropertyListView.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.topicPropertyListView.PaintGroupBox = true;
            this.topicPropertyListView.RoundCorners = 4;
            this.topicPropertyListView.ShadowColor = System.Drawing.Color.DarkGray;
            this.topicPropertyListView.ShadowControl = false;
            this.topicPropertyListView.ShadowThickness = 1;
            this.topicPropertyListView.Size = new System.Drawing.Size(1308, 683);
            this.topicPropertyListView.TabIndex = 42;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Location = new System.Drawing.Point(172, 193);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.ReadOnly = false;
            this.propertyGrid.Size = new System.Drawing.Size(8, 8);
            this.propertyGrid.TabIndex = 0;
            // 
            // HandleEventGridTopicControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.topicPropertyListView);
            this.Name = "HandleEventGridTopicControl";
            this.Size = new System.Drawing.Size(1472, 811);
            this.topicPropertyListView.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListView topicListView;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader valueColumnHeader;
        private Grouper topicPropertyListView;
        private ReadOnlyPropertyGrid propertyGrid;
    }
}
