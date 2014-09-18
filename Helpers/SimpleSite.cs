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
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    internal sealed class SimpleSite : ISite, IServiceProvider
    {
        #region Private Fields
        private readonly IContainer container = new Container();
        private Dictionary<Type, object> services;
        #endregion

        #region Public Properties
        public IComponent Component
        {
            get;
            set;
        }

        IContainer ISite.Container
        {
            get
            {
                return container;
            }
        }

        public bool DesignMode
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        } 
        #endregion

        #region Public Methods
        public void AddService<T>(T service) where T : class
        {
            if (services == null)
                services = new Dictionary<Type, object>();
            services[typeof(T)] = service;
        }
        public void RemoveService<T>() where T : class
        {
            if (services != null)
                services.Remove(typeof(T));
        }
        object IServiceProvider.GetService(Type serviceType)
        {
            object service;
            if (services != null && services.TryGetValue(serviceType, out service))
            {
                return service;
            }
            return null;
        } 
        #endregion
    }
}