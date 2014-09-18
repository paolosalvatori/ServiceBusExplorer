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
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PropertyResourceAttribute : Attribute, IResourceAttribute
    {
        #region Private Fields
        private string mBaseName = String.Empty;
        private string mKeyPrefix = String.Empty;
        private string mAssemblyFullName = String.Empty;
        #endregion

        #region Public Constructors
        public PropertyResourceAttribute()
        {

        }

        public PropertyResourceAttribute(string baseString)
        {
            mBaseName = baseString;
        }

        public PropertyResourceAttribute(string baseString, string keyPrefix)
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

            if (String.Compare(this.BaseName, other.BaseName, true) == 0 &&
                String.Compare(this.AssemblyFullName, other.AssemblyFullName, true) == 0)
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