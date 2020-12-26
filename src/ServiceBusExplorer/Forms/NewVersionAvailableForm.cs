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
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.Properties;

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
       
        private void siteLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/paolosalvatori/ServiceBusExplorer");
        }

        private void mailLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto:paolos@microsoft.com?subject=Service%20Bus%20Explorer%20Feedback");
        }

        private void blogLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://blogs.msdn.com/paolos");
        }

        private void twitterLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://twitter.com/babosbird");
        }

        private void NewVersionAvailableForm_Load(object sender, EventArgs e)
        {
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
        }
        #endregion

        private void linkLabelnewVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkLabelnewVersion.Text);
        }
    }
}
