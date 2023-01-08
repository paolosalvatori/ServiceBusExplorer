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

using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.UIHelpers;
using System;
using System.Diagnostics;
using System.Windows.Forms;

#endregion

namespace ServiceBusExplorer.Forms
{
    public partial class NewVersionAvailableForm : Form
    {
        #region Public Constructor
        public NewVersionAvailableForm()
        {
            InitializeComponent();

            //This form is double buffered
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.DoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true);
        }
        #endregion

        #region Event Handlers
        private void NewVersionAvailableForm_Load(object sender, EventArgs e)
        {
            this.SuspendDrawing();
            this.SuspendLayout();

            lblExeVersion.Text = VersionProvider.GetExeVersion();

            if (!VersionProvider.IsLatestVersion(out var releaseInfo))
            {
                labelLatestVersion.Text = $"New Release {releaseInfo.Version} Available";
                linkLabelnewVersion.Text = releaseInfo.ReleaseUri.ToString();
                linkLabelnewVersion.Visible = true;
                labelReleaseInfo.Text = releaseInfo.Body + Environment.NewLine + releaseInfo.ZipPackageUri;
                labelReleaseInfo.Visible = true;
            }
            else
            {
                labelLatestVersion.Text = "You have the latest version!";

                linkLabelnewVersion.Visible = false;
                labelReleaseInfo.Visible = false;
            }

            this.ResumeDrawing();
            this.ResumeLayout();
        }
        #endregion

        private void linkLabelnewVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkLabelnewVersion.Text);
        }
    }
}
