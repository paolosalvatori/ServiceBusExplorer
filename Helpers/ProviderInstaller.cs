#region Using Directives
using System.ComponentModel; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public static class ProviderInstaller
    {
        #region Public Static Methods
        public static DynamicCustomTypeDescriptor Install(object instance)
        {
            var parentProvider = TypeDescriptor.GetProvider(instance);
            var parentCtd = parentProvider.GetTypeDescriptor(instance);
            var ourCtd = new DynamicCustomTypeDescriptor(parentCtd, instance);
            var ourProvider = new CustomTypeDescriptionProvider(parentProvider, ourCtd);
            TypeDescriptor.AddProvider(ourProvider, instance);
            return ourCtd;
        } 
        #endregion
    }
}