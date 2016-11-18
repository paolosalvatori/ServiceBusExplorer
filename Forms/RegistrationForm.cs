#region Copyright
//=======================================================================================
// Windows Azure Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on the community
// blog at http://www.appfabriccat.com/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright © 2011 Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF 
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. YOU BEAR THE RISK OF USING IT.
//=======================================================================================
#endregion

#region Using Directives
using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.ServiceBus.Notifications;
using Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Helpers;

#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class RegistrationForm : Form
    {
        #region Private Constants
        //***************************
        // Texts
        //***************************
        private const string SelectRegistration = "Select a registration type...";
        private const string MpnsRegistrationDescription = "MpnsRegistrationDescription";
        private const string MpnsTemplateRegistrationDescription = "MpnsTemplateRegistrationDescription";
        private const string WindowsRegistrationDescription = "WindowsRegistrationDescription";
        private const string WindowsTemplateRegistrationDescription = "WindowsTemplateRegistrationDescription";
        private const string AppleRegistrationDescription = "AppleRegistrationDescription";
        private const string AppleTemplateRegistrationDescription = "AppleTemplateRegistrationDescription";
        private const string GcmRegistrationDescription = "GcmRegistrationDescription";
        private const string GcmTemplateRegistrationDescription = "GcmTemplateRegistrationDescription";
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
        // Formats
        //***************************
        private const string RegistrationTypeFormat = "Microsoft.ServiceBus.Notifications.{0}, Microsoft.ServiceBus";
        #endregion

        #region Public Constructor
        public RegistrationForm()
        {
            InitializeComponent();
            cboRegistrationType.Items.AddRange(new object[]{SelectRegistration, 
                                                            MpnsRegistrationDescription, 
                                                            MpnsTemplateRegistrationDescription,
                                                            WindowsRegistrationDescription,
                                                            WindowsTemplateRegistrationDescription,
                                                            AppleRegistrationDescription,
                                                            AppleTemplateRegistrationDescription,
                                                            GcmRegistrationDescription,
                                                            GcmTemplateRegistrationDescription});
        }

        #endregion

        #region Event Handlers
        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            RegistrationObject = null;
            RegistrationType = null;
            Close();
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

        private void grouperCaption_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                   cboRegistrationType.Location.X - 1,
                                   cboRegistrationType.Location.Y - 1,
                                   cboRegistrationType.Size.Width + 1,
                                   cboRegistrationType.Size.Height + 1);
        }

        private void cboRegistrationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CreateRegistrationForPropertyGrid();
            }
            catch (Exception ex)
            {
                MainForm.SingletonMainForm.HandleException(ex);
            }
        }
        #endregion

        #region Public Properties
        public CustomObject RegistrationObject { get; set; }
        public Type RegistrationType { get; set; }
        #endregion

        #region Private Methods
        private void CreateRegistrationForPropertyGrid()
        {
            try
            {
                if (cboRegistrationType.SelectedIndex == 0)
                {
                    return;
                }
                RegistrationType = Type.GetType(string.Format(RegistrationTypeFormat, cboRegistrationType.Text));
                if (RegistrationType == null)
                {
                    return;
                }
                var propertyInfos = RegistrationType.
                                    GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty).
                                    Where(p=>p.GetSetMethod(false) != null && string.Compare(p.Name, "ExtensionData", StringComparison.InvariantCultureIgnoreCase) != 0);
                IEnumerable<PropertyInfo> enumerable = propertyInfos as IList<PropertyInfo> ?? propertyInfos.ToList();
                RegistrationObject = new CustomObject
                {
                    Properties = enumerable.Select(p => new CustomProperty
                    {
                        Name = p.Name,
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
                                new ReadOnlyEditor() as UITypeEditor :
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
                foreach (var propertyInfo in enumerable)
                {
                    if (propertyInfo.PropertyType == typeof(Uri))
                    {
                        RegistrationObject[propertyInfo.Name] = null;
                    }
                    else if (propertyInfo.PropertyType == typeof(CDataMember))
                    {
                        RegistrationObject[propertyInfo.Name] = null;
                    }
                    else if (propertyInfo.PropertyType == typeof(ISet<string>))
                    {
                        RegistrationObject[propertyInfo.Name] = new List<TagInfo>();
                    }
                    else if (propertyInfo.PropertyType == typeof(MpnsHeaderCollection))
                    {
                        RegistrationObject[propertyInfo.Name] = new List<NotificationInfo>();
                    }
                    else if (propertyInfo.PropertyType == typeof(WnsHeaderCollection))
                    {
                        RegistrationObject[propertyInfo.Name] = new List<NotificationInfo>();
                    }
                    else if ((propertyInfo.GetMethod == null ||
                             (propertyInfo.GetMethod != null && !propertyInfo.GetMethod.IsPublic)) &&
                             (propertyInfo.SetMethod == null ||
                             (propertyInfo.SetMethod != null && !propertyInfo.SetMethod.IsPublic)) &&
                              propertyInfo.PropertyType == typeof(List<int>))
                    {
                        RegistrationObject[propertyInfo.Name] = new ReadOnlyList<int>();
                    }
                }
                registrationPropertyGrid.SelectedObject = RegistrationObject;
            }
            catch (Exception ex)
            {
                MainForm.SingletonMainForm.HandleException(ex);
            }
        }
        #endregion
    }
}
