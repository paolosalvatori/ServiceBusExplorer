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
    public class TimeFilterInfo
    {
        #region Private Fields
        private string property;
        private string @operator;
        private string dateTime;
        #endregion

        #region Public Events
        public static event Action OnChange;
        #endregion

        #region Public Constructors
        public TimeFilterInfo()
        {

            Property = null;
            Operator = null;
            Value = null;
        }
        #endregion

        #region Public Instance Properties
        public string Property 
        { 
            get
            {
                return property;
            }
            set
            {
                property = Adjust(value);
                if (OnChange != null)
                {
                    OnChange();
                }
            }
        }
        public string Operator
        {
            get
            {
                return @operator;
            }
            set
            {
                @operator = Adjust(value);
                if (OnChange != null)
                {
                    OnChange();
                }
            }
        }

        public string Value
        {
            get
            {
                return dateTime;
            }
            set
            {
                dateTime = value;
                if (OnChange != null)
                {
                    OnChange();
                }
            }
        }
        #endregion

        #region Private Methods
        private string Adjust(string word)
        {
            if (String.Compare(word, "AccessedAt", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return "AccessedAt";
            }
            if (String.Compare(word, "UpdatedAt", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return "UpdatedAt";
            }
            if (String.Compare(word, "CreatedAt", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return "CreatedAt";
            }
            if (String.Compare(word, "Eq", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return "Eq";
            }
            if (String.Compare(word, "Ne", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return "Ne";
            }
            if (String.Compare(word, "Ge", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return "Ge";
            }
            if (String.Compare(word, "Gt", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return "Gt";
            }
            if (String.Compare(word, "Le", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return "Le";
            }
            if (String.Compare(word, "Lt", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return "Lt";
            }
            return word;
        }
        #endregion
    }
}