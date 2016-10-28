#region Using Directives
using System;
using System.ComponentModel; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    internal class CustomTypeDescriptionProvider : TypeDescriptionProvider
    {
        #region Private Fields
        private TypeDescriptionProvider parent;
        private readonly ICustomTypeDescriptor customTypeDescriptor; 
        #endregion

        #region Public Constructors
        public CustomTypeDescriptionProvider(TypeDescriptionProvider parent, ICustomTypeDescriptor ctd)
            : base(parent)
        {
            this.parent = parent;
            customTypeDescriptor = ctd;
        } 
        #endregion

        #region Public Methods
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            return customTypeDescriptor;
        }
        #endregion
    }
}