namespace ServiceBusExplorer.Controls
{
    partial class HandleEventGridNamespaceControl
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
            this.namespacePropertyListView = new ServiceBusExplorer.Controls.Grouper();
            this.namespaceListView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.propertyGrid = new ServiceBusExplorer.Controls.ReadOnlyPropertyGrid();
            this.namespacePropertyListView.SuspendLayout();
            this.SuspendLayout();
            // 
            // namespacePropertyListView
            // 
            this.namespacePropertyListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.namespacePropertyListView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.namespacePropertyListView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.namespacePropertyListView.BackgroundGradientColor = System.Drawing.Color.White;
            this.namespacePropertyListView.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.namespacePropertyListView.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.namespacePropertyListView.BorderThickness = 1F;
            this.namespacePropertyListView.Controls.Add(this.namespaceListView);
            this.namespacePropertyListView.Controls.Add(this.propertyGrid);
            this.namespacePropertyListView.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.namespacePropertyListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.namespacePropertyListView.ForeColor = System.Drawing.Color.White;
            this.namespacePropertyListView.GroupImage = null;
            this.namespacePropertyListView.GroupTitle = "Namespace Properties";
            this.namespacePropertyListView.Location = new System.Drawing.Point(68, 48);
            this.namespacePropertyListView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.namespacePropertyListView.Name = "namespacePropertyListView";
            this.namespacePropertyListView.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.namespacePropertyListView.PaintGroupBox = true;
            this.namespacePropertyListView.RoundCorners = 4;
            this.namespacePropertyListView.ShadowColor = System.Drawing.Color.DarkGray;
            this.namespacePropertyListView.ShadowControl = false;
            this.namespacePropertyListView.ShadowThickness = 1;
            this.namespacePropertyListView.Size = new System.Drawing.Size(1308, 683);
            this.namespacePropertyListView.TabIndex = 42;
            // 
            // namespaceListView
            // 
            this.namespaceListView.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.namespaceListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.namespaceListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
             this.nameColumnHeader,
             this.valueColumnHeader});
            this.namespaceListView.HideSelection = false;
            this.namespaceListView.Location = new System.Drawing.Point(33, 51);
            this.namespaceListView.Name = "namespaceListView";
            this.namespaceListView.OwnerDraw = true;
            this.namespaceListView.Size = new System.Drawing.Size(1230, 598);
            this.namespaceListView.TabIndex = 0;
            this.namespaceListView.UseCompatibleStateImageBehavior = false;
            this.namespaceListView.View = System.Windows.Forms.View.Details;
            this.namespaceListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView_DrawColumnHeader);
            this.namespaceListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView_DrawItem);
            this.namespaceListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listView_DrawSubItem);
            this.namespaceListView.Resize += new System.EventHandler(this.listView_Resize);
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
            // propertyGrid
            // 
            this.propertyGrid.Location = new System.Drawing.Point(172, 193);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.ReadOnly = false;
            this.propertyGrid.Size = new System.Drawing.Size(8, 8);
            this.propertyGrid.TabIndex = 0;
            // 
            // HandleEventGridNamespaceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.namespacePropertyListView);
            this.Name = "HandleEventGridNamespaceControl";
            this.Size = new System.Drawing.Size(1472, 811);
            this.namespacePropertyListView.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListView namespaceListView;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader valueColumnHeader;
        private Grouper namespacePropertyListView;
        private ReadOnlyPropertyGrid propertyGrid;
    }
}
