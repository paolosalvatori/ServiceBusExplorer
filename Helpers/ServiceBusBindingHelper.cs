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
using System.Linq;
using System.Reflection;
using System.ServiceModel.Channels;
using Microsoft.ServiceBus;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public static class ServiceBusBindingHelper
    {
        #region Private Constants
        private const string ServiceBusAssemblyPrefix = "Microsoft.ServiceBus";
        private const string BasePostfix = "Base";
        private const string NetMessagingBinding = "NetMessagingBinding";
        private const string SecurityProperty = "Security";
        private const string ModeProperty = "Mode";
        private const string RelayClientAuthenticationTypeProperty = "RelayClientAuthenticationType";
        #endregion

        #region Private Static Fields
        private static readonly Dictionary<string, Type> bindingDictionary = new Dictionary<string, Type>();
        private static readonly Dictionary<string, PropertyInfo> securityDictionary = new Dictionary<string, PropertyInfo>();
        private static readonly Dictionary<string, PropertyInfo> modeDictionary = new Dictionary<string, PropertyInfo>();
        private static readonly Dictionary<string, PropertyInfo> relayClientAuthenticationDictionary = new Dictionary<string, PropertyInfo>();
        #endregion

        #region Public Static Properties
        public static Dictionary<string, Type> Bindings { get { return bindingDictionary; } }
        #endregion

        #region Static Constructor
        static ServiceBusBindingHelper()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var serviceBusAssemblies = assemblies.Where(a => a.FullName.StartsWith(ServiceBusAssemblyPrefix));
            foreach (var assembly in serviceBusAssemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.Name != NetMessagingBinding &&
                        type.Name.Length > 4 &&
                        type.Name.Substring(type.Name.Length - 4, 4) != BasePostfix &&
                        type.IsSubclassOf(typeof(Binding)))
                    {
                        bindingDictionary.Add(type.Name, type);
                        var securityProperty =
                            type.GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(
                                p => p.Name == SecurityProperty);
                        if (securityProperty == null)
                        {
                            continue;
                        }
                        securityDictionary.Add(type.Name, securityProperty);
                        var modeProperty = securityProperty.PropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .FirstOrDefault(p => p.Name == ModeProperty);
                        var relayClientAuthenticationTypeProperty = securityProperty.PropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .FirstOrDefault(p => p.Name == RelayClientAuthenticationTypeProperty);

                        if (modeProperty == null && relayClientAuthenticationTypeProperty == null)
                        {
                            continue;
                        }
                        modeDictionary.Add(type.Name, modeProperty);
                        relayClientAuthenticationDictionary.Add(type.Name, relayClientAuthenticationTypeProperty);
                    }
                }
            }
        } 
        #endregion

        #region Public Static Methods
        public static bool IsOneWay(Binding binding)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return binding is NetOnewayRelayBinding || binding is NetEventRelayBinding;
        }

        public static RelayClientAuthenticationType GetRelayClientAuthenticationType(Binding binding)
        {
            if (binding == null ||
                !securityDictionary.ContainsKey(binding.Name) ||
                securityDictionary[binding.Name] == null ||
                !relayClientAuthenticationDictionary.ContainsKey(binding.Name) ||
                relayClientAuthenticationDictionary[binding.Name] == null)
            {
                return RelayClientAuthenticationType.RelayAccessToken;
            }
            var securityProperty = securityDictionary[binding.Name].GetValue(binding, null);
            return (RelayClientAuthenticationType)relayClientAuthenticationDictionary[binding.Name].GetValue(securityProperty, null);
        }

        public static void SetSecurityProperties(bool requiresClientAuthorization, bool requiresTransportSecurity, ref Binding binding)
        {
            if (binding == null ||
                !securityDictionary.ContainsKey(binding.Name) ||
                securityDictionary[binding.Name] == null ||
                !relayClientAuthenticationDictionary.ContainsKey(binding.Name) ||
                relayClientAuthenticationDictionary[binding.Name] == null)
            {
                return;
            }
            var securityProperty = securityDictionary[binding.Name].GetValue(binding, null);
            relayClientAuthenticationDictionary[binding.Name].SetValue(securityProperty, 
                                                                       requiresClientAuthorization ? 
                                                                       RelayClientAuthenticationType.RelayAccessToken :
                                                                       RelayClientAuthenticationType.None);
            var modeProperty = securityProperty.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .FirstOrDefault(p => p.Name == ModeProperty);
            if (modeProperty == null)
            {
                return;
            }
            var array = Enum.GetValues(modeProperty.PropertyType);
            object transportValue = null;
            object noneValue = null;
            foreach (var item in array)
            {
                if (item.ToString() == "Transport")
                {
                    transportValue = item;
                }
                if (item.ToString() == "None")
                {
                    noneValue = item;
                }
            }
            modeDictionary[binding.Name].SetValue(securityProperty, requiresTransportSecurity ? transportValue : noneValue);
        }
        #endregion
    }
}
