#region Using Directives
using System; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ClassResourceAttribute : Attribute, IResourceAttribute
    {
        #region Private Fields
        private string baseName = String.Empty;
        private string keyPrefix = String.Empty;
        private string assemblyFullName = String.Empty;
        #endregion

        #region Public Constructors
        public ClassResourceAttribute()
        {

        }

        public ClassResourceAttribute(string baseString)
        {
            baseName = baseString;
        }

        public ClassResourceAttribute(string baseString, string keyPrefix)
        {
            baseName = baseString;
            this.keyPrefix = keyPrefix;
        } 
        #endregion

        #region Public Properties
        public string BaseName
        {
            get
            {
                return baseName;
            }
            set
            {
                baseName = value;
            }
        }


        public string KeyPrefix
        {
            get
            {
                return keyPrefix;
            }
            set
            {
                keyPrefix = value;
            }
        }


        public string AssemblyFullName
        {
            get
            {
                return assemblyFullName;
            }
            set
            {
                assemblyFullName = value;
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