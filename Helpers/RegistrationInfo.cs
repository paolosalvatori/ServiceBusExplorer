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
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ServiceBus.Notifications;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class RegistrationInfo : INotifyPropertyChanged
    {
        #region Private Fields
        private RegistrationDescription registration;
        private string eTag;
        private DateTime? expirationTime;
        private string registrationId;
        private string tags;
        private string channelUri;
        #endregion

        #region Static Constructor
        static RegistrationInfo()
        {
            Registrations = new List<RegistrationInfo>();
        }
        #endregion

        #region Public Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Public Instance Properties
        public RegistrationDescription Registration
        {
            get
            {
                return registration;
            }
            set
            {
                if (value == registration)
                {
                    return;
                }
                registration = value;
                NotifyPropertyChanged();
            }
        }

        public string ETag
        {
            get
            {
                return eTag;
            }
            set
            {
                if (value == eTag)
                {
                    return;
                }
                eTag = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime? ExpirationTime
        {
            get
            {
                return expirationTime;
            }
            set
            {
                if (value == expirationTime)
                {
                    return;
                }
                expirationTime = value;
                NotifyPropertyChanged();
            }
        }

        public string RegistrationId
        {
            get
            {
                return registrationId;
            }
            set
            {
                if (value == registrationId)
                {
                    return;
                }
                registrationId = value;
                NotifyPropertyChanged();
            }
        }

        public string Tags
        {
            get
            {
                return tags;
            }
            set
            {
                if (value == tags)
                {
                    return;
                }
                tags = value;
                NotifyPropertyChanged();
            }
        }

        public string ChannelUri
        {
            get
            {
                return channelUri;
            }
            set
            {
                if (value == channelUri)
                {
                    return;
                }
                channelUri = value;
                NotifyPropertyChanged();
            }
        }

        public string PlatformType
        {
            get
            {
                if (registration == null)
                {
                    return "unknown";
                }
                if (registration is WindowsTemplateRegistrationDescription)
                {
                    return "windowstemplate";
                }
                if (registration is WindowsRegistrationDescription)
                {
                    return "windows";
                }
                if (registration is MpnsTemplateRegistrationDescription)
                {
                    return "windowsphonetemplate";
                }
                if (registration is MpnsRegistrationDescription)
                {
                    return "windowsphone";
                }
                if (registration is AppleTemplateRegistrationDescription)
                {
                    return "appletemplate";
                }
                if (registration is AppleRegistrationDescription)
                {
                    return "apple";
                }
                if (registration is GcmTemplateRegistrationDescription)
                {
                    return "gcmtemplate";
                }
                if (registration is GcmRegistrationDescription)
                {
                    return "gcm";
                }
                return "unknown";
            }
        }
        #endregion

        #region Public Static Properties
        public static List<RegistrationInfo> Registrations { get; private set; }
        #endregion

        #region Public Static Methods
        public static void SetRegistrations(IEnumerable<RegistrationDescription> collection)
        {
            Registrations.Clear();
            if (collection != null)
            {
                foreach (var registration in collection)
                {
                    var registrationInfo = new RegistrationInfo
                        {
                            Registration = registration,
                            ETag = registration.ETag,
                            ExpirationTime = registration.ExpirationTime,
                            RegistrationId = registration.RegistrationId,
                            Tags = registration.Tags != null && registration.Tags.Any() ? 
                                   registration.Tags.Aggregate((a, t) => a + "," + t) : 
                                   null
                        };
                    if (registration is WindowsRegistrationDescription)
                    {
                        registrationInfo.ChannelUri = ((WindowsRegistrationDescription)registration).ChannelUri.ToString();
                    }
                    if (registration is MpnsRegistrationDescription)
                    {
                        registrationInfo.ChannelUri = ((MpnsRegistrationDescription)registration).ChannelUri.ToString();
                    }
                    if (registration is AppleRegistrationDescription)
                    {
                        registrationInfo.ChannelUri = ((AppleRegistrationDescription)registration).DeviceToken;
                    }
                    if (registration is GcmRegistrationDescription)
                    {
                        registrationInfo.ChannelUri = ((GcmRegistrationDescription)registration).GcmRegistrationId;
                    }
                    Registrations.Add(registrationInfo);
                }
            }
        }
        #endregion

        #region Private Methods
        // This method is called by the Set accessor of each property. 
        // The CallerMemberName attribute that is applied to the optional propertyName 
        // parameter causes the property name of the caller to be substituted as an argument. 
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
