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