﻿#region Copyright
//=======================================================================================
// Windows Azure Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on the community
// blog at http://www.appfabriccat.com/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright © 2011 Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF 
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. YOU BEAR THE RISK OF USING IT.
//=======================================================================================
#endregion

#region Using Directives
using System;
using Microsoft.ServiceBus.Messaging;
using System.Linq;
#endregion


namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class AuthorizationRuleWrapper
    {
        #region Private Fields
        private bool manage;
        private bool send;
        private bool listen;
        private string keyName;
        private string primaryKey;
        private string secondaryKey;
        private string claimType;
        private string claimValue;
        private string issuerName;
        #endregion


        #region Public Constructor
		public AuthorizationRuleWrapper()
        {
        }

        public AuthorizationRuleWrapper(AuthorizationRule rule)
        {
            KeyName = rule.KeyName;
            if (rule is SharedAccessAuthorizationRule)
            {
                var sharedAccessAuthorizationRule = rule as SharedAccessAuthorizationRule;
                PrimaryKey = sharedAccessAuthorizationRule.PrimaryKey;
                SecondaryKey = sharedAccessAuthorizationRule.SecondaryKey;
            }
            Manage = rule.Rights.Contains(AccessRights.Manage);
            Send = rule.Rights.Contains(AccessRights.Send);
            Listen = rule.Rights.Contains(AccessRights.Listen);
            ClaimType = rule.ClaimType;
            ClaimValue = rule.ClaimValue;
            IssuerName = rule.IssuerName;
            Revision = rule.Revision;
            CreatedTime = rule.CreatedTime;
            ModifiedTime = rule.ModifiedTime;
            AuthorizationRule = rule;
        }
	    #endregion

        #region Public Properties
        public string KeyName
        {
            get
            {
                return keyName;
            }
            set
            {
                keyName = value; 
                if (AuthorizationRule != null)
                {
                    AuthorizationRule.KeyName = value;
                }
            }
        }

        public string PrimaryKey
        {
            get
            {
                return primaryKey;
            }
            set
            {
                primaryKey = value;
                var rule = AuthorizationRule as SharedAccessAuthorizationRule;
                if (rule != null)
                {
                    rule.PrimaryKey = value;
                }
            }
        }

        public string SecondaryKey
        {
            get
            {
                return secondaryKey;
            }
            set
            {
                secondaryKey = value;
                var rule = AuthorizationRule as SharedAccessAuthorizationRule;
                if (rule != null)
                {
                    rule.SecondaryKey = value;
                }
            }
        }

        public string ClaimType
        {
            get
            {
                return claimType;
            }
            set
            {
                claimType = value;
                if (AuthorizationRule != null)
                {
                    AuthorizationRule.ClaimType = value;
                }
            }
        }

        public string ClaimValue
        {
            get
            {
                return claimValue;
            }
            set
            {
                claimValue = value;
                if (AuthorizationRule != null)
                {
                    AuthorizationRule.ClaimValue = value;
                }
            }
        }

        public string IssuerName
        {
            get
            {
                return issuerName;
            }
            set
            {
                issuerName = value;
                if (AuthorizationRule != null)
                {
                    AuthorizationRule.IssuerName = value;
                }
            }
        }

        public DateTime CreatedTime { get; private set; }
        public DateTime ModifiedTime { get; private set; }
        public long Revision { get; private set; }
        public AuthorizationRule AuthorizationRule { get; private set; }

        public bool Manage
        {
            get
            {
                return manage;
            }
            set
            {
                manage = value;
                if (!value)
                {
                    return;
                }
                Send = true;
                Listen = true;
                if (AuthorizationRule != null &&
                    !AuthorizationRule.Rights.Contains(AccessRights.Manage))
                {
                    AuthorizationRule.Rights = new[] {AccessRights.Manage, AccessRights.Send, AccessRights.Listen};
                }
            }
        }

        public bool Send
        {
            get
            {
                return send;
            }
            set
            {
                send = value;
                if (!value && manage)
                {
                    Manage = false;
                }
                if (AuthorizationRule == null || AuthorizationRule.Rights.Contains(AccessRights.Send))
                {
                    return;
                }
                var list = AuthorizationRule.Rights.ToList();
                list.Add(AccessRights.Send);
                AuthorizationRule.Rights = list;
            }
        }

        public bool Listen
        {
            get
            {
                return listen;
            }
            set
            {
                listen = value;
                if (!value && manage)
                {
                    Manage = false;
                }
                if (AuthorizationRule == null || AuthorizationRule.Rights.Contains(AccessRights.Listen))
                {
                    return;
                }
                var list = AuthorizationRule.Rights.ToList();
                list.Add(AccessRights.Listen);
                AuthorizationRule.Rights = list;
            }
        } 
        #endregion
    }
}
