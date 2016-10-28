#region Using Directives
using System; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false, Inherited = true)]
    public class EnumResourceAttribute : Attribute, IResourceAttribute
    {
        #region Private Fields
        private string mBaseName = String.Empty;
        private string mKeyPrefix = String.Empty;
        private string mAssemblyFullName = String.Empty;
        #endregion

        #region Public Constructors
        public EnumResourceAttribute()
        {

        }

        public EnumResourceAttribute(string baseString)
        {
            mBaseName = baseString;
        }

        public EnumResourceAttribute(string baseString, string keyPrefix)
        {
            mBaseName = baseString;
            mKeyPrefix = keyPrefix;
        } 
        #endregion

        #region Public Properties
        public string BaseName
        {
            get
            {
                return mBaseName;
            }
            set
            {
                mBaseName = value;
            }
        }


        public string KeyPrefix
        {
            get
            {
                return mKeyPrefix;
            }
            set
            {
                mKeyPrefix = value;
            }
        }

        public string AssemblyFullName
        {
            get
            {
                return mAssemblyFullName;
            }
            set
            {
                mAssemblyFullName = value;
            }
        } 
        #endregion

        #region Public Methods
        // Use the hash code of the string objects and xor them together.
        public override int GetHashCode()
        {

            return (BaseName.GetHashCode() ^ KeyPrefix.GetHashCode()) ^ AssemblyFullName.GetHashCode();
        }


        public override bool Equals(object obj)
        {
            if (!(obj is ClassResourceAttribute))
            {
                return false;
            }
            var other = obj as ClassResourceAttribute;

            if (String.Compare(BaseName, other.BaseName, true) == 0 &&
                String.Compare(AssemblyFullName, other.AssemblyFullName, true) == 0)
            {
                return true;
            }

            return false;
        }

        public override bool Match(object obj)
        {
            // Obviously a match.
            if (obj == this)
                return true;

            // Obviously we're not null, so no.
            if (obj == null)
                return false;

            if (obj is ClassResourceAttribute)
                // Combine the hash codes and see if they're unchanged.
                return (((ClassResourceAttribute)obj).GetHashCode() & GetHashCode()) == GetHashCode();
            return false;
        } 
        #endregion
    }
}