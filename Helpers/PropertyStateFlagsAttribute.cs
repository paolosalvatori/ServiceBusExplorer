#region Using Directives
using System; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class PropertyStateFlagsAttribute : Attribute
    {
        #region Private Fields
        private PropertyFlags mFlags = PropertyFlags.All & ~PropertyFlags.ExclusiveStandardValues; 
        #endregion

        #region Public Constructors
        public PropertyStateFlagsAttribute()
        {

        }
        public PropertyStateFlagsAttribute(PropertyFlags flags)
        {
            mFlags = flags;
        } 
        #endregion

        #region Public Properties
        public PropertyFlags Flags
        {
            get
            {
                return mFlags;
            }
            set
            {
                mFlags = value;
            }
        } 
        #endregion
    }
}