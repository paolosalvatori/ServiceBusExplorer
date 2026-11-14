using System;
using System.Collections.Generic;
using System.Linq;
using Admin = Azure.Messaging.ServiceBus.Administration;
using Legacy = Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.Helpers
{
    public static class ServiceBusManagementMapper
    {
        public static Admin.CreateQueueOptions ToCreateOptions(Legacy.QueueDescription source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            EnsureQueueFeaturesSupported(source);

            var options = new Admin.CreateQueueOptions(source.Path)
            {
                LockDuration = source.LockDuration,
                MaxSizeInMegabytes = source.MaxSizeInMegabytes,
                RequiresDuplicateDetection = source.RequiresDuplicateDetection,
                RequiresSession = source.RequiresSession,
                DefaultMessageTimeToLive = source.DefaultMessageTimeToLive,
                AutoDeleteOnIdle = source.AutoDeleteOnIdle,
                DeadLetteringOnMessageExpiration = source.EnableDeadLetteringOnMessageExpiration,
                DuplicateDetectionHistoryTimeWindow = source.DuplicateDetectionHistoryTimeWindow,
                MaxDeliveryCount = source.MaxDeliveryCount,
                EnableBatchedOperations = source.EnableBatchedOperations,
                Status = ToAdminStatus(source.Status),
                ForwardTo = source.ForwardTo,
                ForwardDeadLetteredMessagesTo = source.ForwardDeadLetteredMessagesTo,
                EnablePartitioning = source.EnablePartitioning,
                UserMetadata = source.UserMetadata ?? string.Empty
            };
            options.AuthorizationRules.AddRange(ToAdminAuthorizationRules(source.Authorization));
            return options;
        }

        public static Admin.CreateTopicOptions ToCreateOptions(Legacy.TopicDescription source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            EnsureTopicFeaturesSupported(source);

            var options = new Admin.CreateTopicOptions(source.Path)
            {
                DefaultMessageTimeToLive = source.DefaultMessageTimeToLive,
                AutoDeleteOnIdle = source.AutoDeleteOnIdle,
                MaxSizeInMegabytes = source.MaxSizeInMegabytes,
                RequiresDuplicateDetection = source.RequiresDuplicateDetection,
                DuplicateDetectionHistoryTimeWindow = source.DuplicateDetectionHistoryTimeWindow,
                Status = ToAdminStatus(source.Status),
                EnablePartitioning = source.EnablePartitioning,
                SupportOrdering = source.SupportOrdering,
                EnableBatchedOperations = source.EnableBatchedOperations,
                UserMetadata = source.UserMetadata ?? string.Empty
            };
            options.AuthorizationRules.AddRange(ToAdminAuthorizationRules(source.Authorization));
            return options;
        }

        public static Admin.CreateSubscriptionOptions ToCreateOptions(Legacy.SubscriptionDescription source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            EnsureSubscriptionFeaturesSupported(source);

            return new Admin.CreateSubscriptionOptions(source.TopicPath, source.Name)
            {
                LockDuration = source.LockDuration,
                RequiresSession = source.RequiresSession,
                DefaultMessageTimeToLive = source.DefaultMessageTimeToLive,
                AutoDeleteOnIdle = source.AutoDeleteOnIdle,
                DeadLetteringOnMessageExpiration = source.EnableDeadLetteringOnMessageExpiration,
                EnableDeadLetteringOnFilterEvaluationExceptions = source.EnableDeadLetteringOnFilterEvaluationExceptions,
                MaxDeliveryCount = source.MaxDeliveryCount,
                Status = ToAdminStatus(source.Status),
                ForwardTo = source.ForwardTo,
                ForwardDeadLetteredMessagesTo = source.ForwardDeadLetteredMessagesTo,
                EnableBatchedOperations = source.EnableBatchedOperations,
                UserMetadata = source.UserMetadata ?? string.Empty
            };
        }

        public static Admin.QueueProperties Apply(Legacy.QueueDescription source, Admin.QueueProperties target)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            EnsureQueueIdentityMatches(source, target);
            EnsureQueueFeaturesSupported(source);
            EnsureQueueCreateOnlyPropertiesUnchanged(source, target);
            var status = ToAdminStatus(source.Status);
            var authorizationRules = ToAdminAuthorizationRules(source.Authorization);

            target.LockDuration = source.LockDuration;
            target.MaxSizeInMegabytes = source.MaxSizeInMegabytes;
            target.DefaultMessageTimeToLive = source.DefaultMessageTimeToLive;
            target.AutoDeleteOnIdle = source.AutoDeleteOnIdle;
            target.DeadLetteringOnMessageExpiration = source.EnableDeadLetteringOnMessageExpiration;
            target.DuplicateDetectionHistoryTimeWindow = source.DuplicateDetectionHistoryTimeWindow;
            target.MaxDeliveryCount = source.MaxDeliveryCount;
            target.EnableBatchedOperations = source.EnableBatchedOperations;
            target.AuthorizationRules.Clear();
            target.AuthorizationRules.AddRange(authorizationRules);
            target.Status = status;
            target.ForwardTo = source.ForwardTo;
            target.ForwardDeadLetteredMessagesTo = source.ForwardDeadLetteredMessagesTo;
            target.UserMetadata = source.UserMetadata ?? string.Empty;
            return target;
        }

        public static Admin.TopicProperties Apply(Legacy.TopicDescription source, Admin.TopicProperties target)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            EnsureTopicIdentityMatches(source, target);
            EnsureTopicFeaturesSupported(source);
            EnsureTopicCreateOnlyPropertiesUnchanged(source, target);
            var status = ToAdminStatus(source.Status);
            var authorizationRules = ToAdminAuthorizationRules(source.Authorization);

            target.DefaultMessageTimeToLive = source.DefaultMessageTimeToLive;
            target.AutoDeleteOnIdle = source.AutoDeleteOnIdle;
            target.MaxSizeInMegabytes = source.MaxSizeInMegabytes;
            target.DuplicateDetectionHistoryTimeWindow = source.DuplicateDetectionHistoryTimeWindow;
            target.SupportOrdering = source.SupportOrdering;
            target.EnableBatchedOperations = source.EnableBatchedOperations;
            target.AuthorizationRules.Clear();
            target.AuthorizationRules.AddRange(authorizationRules);
            target.Status = status;
            target.UserMetadata = source.UserMetadata ?? string.Empty;
            return target;
        }

        public static Admin.SubscriptionProperties Apply(
            Legacy.SubscriptionDescription source,
            Admin.SubscriptionProperties target)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            EnsureSubscriptionIdentityMatches(source, target);
            EnsureSubscriptionFeaturesSupported(source);
            if (source.RequiresSession != target.RequiresSession)
            {
                throw Unsupported("Subscription RequiresSession cannot be changed after creation");
            }
            var status = ToAdminStatus(source.Status);

            target.LockDuration = source.LockDuration;
            target.DefaultMessageTimeToLive = source.DefaultMessageTimeToLive;
            target.AutoDeleteOnIdle = source.AutoDeleteOnIdle;
            target.DeadLetteringOnMessageExpiration = source.EnableDeadLetteringOnMessageExpiration;
            target.EnableDeadLetteringOnFilterEvaluationExceptions = source.EnableDeadLetteringOnFilterEvaluationExceptions;
            target.MaxDeliveryCount = source.MaxDeliveryCount;
            target.Status = status;
            target.ForwardTo = source.ForwardTo;
            target.ForwardDeadLetteredMessagesTo = source.ForwardDeadLetteredMessagesTo;
            target.EnableBatchedOperations = source.EnableBatchedOperations;
            target.UserMetadata = source.UserMetadata ?? string.Empty;
            return target;
        }

        public static Legacy.QueueDescription From(Admin.QueueProperties source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var result = new Legacy.QueueDescription(source.Name)
            {
                LockDuration = source.LockDuration,
                MaxSizeInMegabytes = source.MaxSizeInMegabytes,
                RequiresDuplicateDetection = source.RequiresDuplicateDetection,
                RequiresSession = source.RequiresSession,
                DefaultMessageTimeToLive = source.DefaultMessageTimeToLive,
                AutoDeleteOnIdle = source.AutoDeleteOnIdle,
                EnableDeadLetteringOnMessageExpiration = source.DeadLetteringOnMessageExpiration,
                DuplicateDetectionHistoryTimeWindow = source.DuplicateDetectionHistoryTimeWindow,
                MaxDeliveryCount = source.MaxDeliveryCount,
                EnableBatchedOperations = source.EnableBatchedOperations,
                Status = ToLegacyStatus(source.Status),
                ForwardTo = source.ForwardTo,
                ForwardDeadLetteredMessagesTo = source.ForwardDeadLetteredMessagesTo,
                EnablePartitioning = source.EnablePartitioning,
                UserMetadata = source.UserMetadata
            };
            PopulateLegacyAuthorizationRules(source.AuthorizationRules, result.Authorization);
            return result;
        }

        public static Legacy.TopicDescription From(Admin.TopicProperties source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var result = new Legacy.TopicDescription(source.Name)
            {
                DefaultMessageTimeToLive = source.DefaultMessageTimeToLive,
                AutoDeleteOnIdle = source.AutoDeleteOnIdle,
                MaxSizeInMegabytes = source.MaxSizeInMegabytes,
                RequiresDuplicateDetection = source.RequiresDuplicateDetection,
                DuplicateDetectionHistoryTimeWindow = source.DuplicateDetectionHistoryTimeWindow,
                Status = ToLegacyStatus(source.Status),
                EnablePartitioning = source.EnablePartitioning,
                SupportOrdering = source.SupportOrdering,
                EnableBatchedOperations = source.EnableBatchedOperations,
                UserMetadata = source.UserMetadata
            };
            PopulateLegacyAuthorizationRules(source.AuthorizationRules, result.Authorization);
            return result;
        }

        public static Legacy.SubscriptionDescription From(Admin.SubscriptionProperties source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Legacy.SubscriptionDescription(source.TopicName, source.SubscriptionName)
            {
                LockDuration = source.LockDuration,
                RequiresSession = source.RequiresSession,
                DefaultMessageTimeToLive = source.DefaultMessageTimeToLive,
                AutoDeleteOnIdle = source.AutoDeleteOnIdle,
                EnableDeadLetteringOnMessageExpiration = source.DeadLetteringOnMessageExpiration,
                EnableDeadLetteringOnFilterEvaluationExceptions = source.EnableDeadLetteringOnFilterEvaluationExceptions,
                MaxDeliveryCount = source.MaxDeliveryCount,
                EnableBatchedOperations = source.EnableBatchedOperations,
                Status = ToLegacyStatus(source.Status),
                ForwardTo = source.ForwardTo,
                ForwardDeadLetteredMessagesTo = source.ForwardDeadLetteredMessagesTo,
                UserMetadata = source.UserMetadata
            };
        }

        public static Admin.CreateRuleOptions ToCreateOptions(Legacy.RuleDescription source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Admin.CreateRuleOptions(source.Name)
            {
                Filter = ToAdminFilter(source.Filter),
                Action = ToAdminAction(source.Action)
            };
        }

        public static Legacy.RuleDescription From(Admin.RuleProperties source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Legacy.RuleDescription(source.Name)
            {
                Filter = ToLegacyFilter(source.Filter),
                Action = ToLegacyAction(source.Action)
            };
        }

        private static void EnsureQueueFeaturesSupported(Legacy.QueueDescription source)
        {
            // Queue ordering is intrinsic in the administration SDK; the legacy flag is retained
            // for XML compatibility and does not represent a configurable queue feature.
            if (source.IsAnonymousAccessible)
            {
                throw Unsupported("Queue IsAnonymousAccessible");
            }

            if (source.EnableExpress)
            {
                throw Unsupported("Queue EnableExpress");
            }
        }

        private static void EnsureQueueIdentityMatches(
            Legacy.QueueDescription source,
            Admin.QueueProperties target)
        {
            if (!string.Equals(source.Path, target.Name, StringComparison.Ordinal))
            {
                throw new ArgumentException("The queue path must match the target queue name.", nameof(source));
            }
        }

        private static void EnsureQueueCreateOnlyPropertiesUnchanged(
            Legacy.QueueDescription source,
            Admin.QueueProperties target)
        {
            if (source.RequiresSession != target.RequiresSession)
            {
                throw Unsupported("Queue RequiresSession cannot be changed after creation");
            }

            if (source.RequiresDuplicateDetection != target.RequiresDuplicateDetection)
            {
                throw Unsupported("Queue RequiresDuplicateDetection cannot be changed after creation");
            }

            if (source.EnablePartitioning != target.EnablePartitioning)
            {
                throw Unsupported("Queue EnablePartitioning cannot be changed after creation");
            }
        }

        private static void EnsureTopicFeaturesSupported(Legacy.TopicDescription source)
        {
            if (source.EnableFilteringMessagesBeforePublishing)
            {
                throw Unsupported("Topic EnableFilteringMessagesBeforePublishing");
            }

            if (source.IsAnonymousAccessible)
            {
                throw Unsupported("Topic IsAnonymousAccessible");
            }

            if (source.EnableExpress)
            {
                throw Unsupported("Topic EnableExpress");
            }
        }

        private static void EnsureTopicIdentityMatches(
            Legacy.TopicDescription source,
            Admin.TopicProperties target)
        {
            if (!string.Equals(source.Path, target.Name, StringComparison.Ordinal))
            {
                throw new ArgumentException("The topic path must match the target topic name.", nameof(source));
            }
        }

        private static void EnsureTopicCreateOnlyPropertiesUnchanged(
            Legacy.TopicDescription source,
            Admin.TopicProperties target)
        {
            if (source.RequiresDuplicateDetection != target.RequiresDuplicateDetection)
            {
                throw Unsupported("Topic RequiresDuplicateDetection cannot be changed after creation");
            }

            if (source.EnablePartitioning != target.EnablePartitioning)
            {
                throw Unsupported("Topic EnablePartitioning cannot be changed after creation");
            }
        }

        private static void EnsureSubscriptionFeaturesSupported(Legacy.SubscriptionDescription source)
        {
            if (source.IsClientAffine ||
                (source.ClientAffineProperties != null &&
                 (!string.IsNullOrEmpty(source.ClientAffineProperties.ClientId) ||
                  source.ClientAffineProperties.IsDurable ||
                  source.ClientAffineProperties.IsShared)))
            {
                throw Unsupported("Subscription client-affinity properties");
            }
        }

        private static void EnsureSubscriptionIdentityMatches(
            Legacy.SubscriptionDescription source,
            Admin.SubscriptionProperties target)
        {
            if (!string.Equals(source.TopicPath, target.TopicName, StringComparison.Ordinal) ||
                !string.Equals(source.Name, target.SubscriptionName, StringComparison.Ordinal))
            {
                throw new ArgumentException(
                    "The subscription topic path and name must match the target subscription identity.",
                    nameof(source));
            }
        }

        private static List<Admin.SharedAccessAuthorizationRule> ToAdminAuthorizationRules(
            Legacy.AuthorizationRules source)
        {
            var result = new List<Admin.SharedAccessAuthorizationRule>();
            if (source == null)
            {
                return result;
            }

            foreach (var rule in source)
            {
                var sasRule = rule as Legacy.SharedAccessAuthorizationRule;
                if (sasRule == null)
                {
                    if (rule is Legacy.AllowRule)
                    {
                        throw Unsupported("AllowRule authorization rules");
                    }

                    throw Unsupported("Unknown authorization rule type");
                }

                result.Add(new Admin.SharedAccessAuthorizationRule(
                    sasRule.KeyName,
                    sasRule.PrimaryKey,
                    sasRule.SecondaryKey,
                    sasRule.Rights.Select(ToAdminAccessRight)));
            }

            return result;
        }

        private static void PopulateLegacyAuthorizationRules(
            Admin.AuthorizationRules source,
            Legacy.AuthorizationRules target)
        {
            if (source == null)
            {
                return;
            }

            foreach (var rule in source)
            {
                var sasRule = rule as Admin.SharedAccessAuthorizationRule;
                if (sasRule == null)
                {
                    throw Unsupported("Unknown authorization rule type");
                }

                target.Add(new Legacy.SharedAccessAuthorizationRule(
                    sasRule.KeyName,
                    sasRule.PrimaryKey,
                    sasRule.SecondaryKey,
                    sasRule.Rights.Select(ToLegacyAccessRight)));
            }
        }

        private static Admin.AccessRights ToAdminAccessRight(Legacy.AccessRights right)
        {
            switch (right)
            {
                case Legacy.AccessRights.Manage:
                    return Admin.AccessRights.Manage;
                case Legacy.AccessRights.Send:
                    return Admin.AccessRights.Send;
                case Legacy.AccessRights.Listen:
                    return Admin.AccessRights.Listen;
                default:
                    throw Unsupported("Authorization access right " + right);
            }
        }

        private static Legacy.AccessRights ToLegacyAccessRight(Admin.AccessRights right)
        {
            switch (right)
            {
                case Admin.AccessRights.Manage:
                    return Legacy.AccessRights.Manage;
                case Admin.AccessRights.Send:
                    return Legacy.AccessRights.Send;
                case Admin.AccessRights.Listen:
                    return Legacy.AccessRights.Listen;
                default:
                    throw Unsupported("Authorization access right " + right);
            }
        }

        private static Admin.EntityStatus ToAdminStatus(Legacy.EntityStatus status)
        {
            switch (status)
            {
                case Legacy.EntityStatus.Active:
                    return Admin.EntityStatus.Active;
                case Legacy.EntityStatus.Disabled:
                    return Admin.EntityStatus.Disabled;
                case Legacy.EntityStatus.SendDisabled:
                    return Admin.EntityStatus.SendDisabled;
                case Legacy.EntityStatus.ReceiveDisabled:
                    return Admin.EntityStatus.ReceiveDisabled;
                default:
                    throw Unsupported("Entity status " + status);
            }
        }

        private static Legacy.EntityStatus ToLegacyStatus(Admin.EntityStatus status)
        {
            if (status == Admin.EntityStatus.Active)
            {
                return Legacy.EntityStatus.Active;
            }

            if (status == Admin.EntityStatus.Disabled)
            {
                return Legacy.EntityStatus.Disabled;
            }

            if (status == Admin.EntityStatus.SendDisabled)
            {
                return Legacy.EntityStatus.SendDisabled;
            }

            if (status == Admin.EntityStatus.ReceiveDisabled)
            {
                return Legacy.EntityStatus.ReceiveDisabled;
            }

            throw Unsupported("Entity status " + status);
        }

        private static Admin.RuleFilter ToAdminFilter(Legacy.Filter filter)
        {
            if (filter is Legacy.TrueFilter)
            {
                return new Admin.TrueRuleFilter();
            }

            if (filter is Legacy.FalseFilter)
            {
                return new Admin.FalseRuleFilter();
            }

            var sqlFilter = filter as Legacy.SqlFilter;
            if (sqlFilter != null)
            {
                EnsureDefaultSqlCompatibility(sqlFilter.CompatibilityLevel, "SQL filter");
                var result = new Admin.SqlRuleFilter(sqlFilter.SqlExpression);
                CopyParameters(sqlFilter.Parameters, result.Parameters);
                return result;
            }

            var correlationFilter = filter as Legacy.CorrelationFilter;
            if (correlationFilter != null)
            {
                var result = new Admin.CorrelationRuleFilter
                {
                    CorrelationId = correlationFilter.CorrelationId,
                    MessageId = correlationFilter.MessageId,
                    To = correlationFilter.To,
                    ReplyTo = correlationFilter.ReplyTo,
                    Subject = correlationFilter.Label,
                    SessionId = correlationFilter.SessionId,
                    ReplyToSessionId = correlationFilter.ReplyToSessionId,
                    ContentType = correlationFilter.ContentType
                };
                CopyParameters(correlationFilter.Properties, result.ApplicationProperties);
                return result;
            }

            if (filter == null)
            {
                throw Unsupported("Null rule filter");
            }

            throw Unsupported("Unknown rule filter type");
        }

        private static Legacy.Filter ToLegacyFilter(Admin.RuleFilter filter)
        {
            if (filter is Admin.TrueRuleFilter)
            {
                return new Legacy.TrueFilter();
            }

            if (filter is Admin.FalseRuleFilter)
            {
                return new Legacy.FalseFilter();
            }

            var sqlFilter = filter as Admin.SqlRuleFilter;
            if (sqlFilter != null)
            {
                var result = new Legacy.SqlFilter(sqlFilter.SqlExpression);
                CopyParameters(sqlFilter.Parameters, result.Parameters);
                return result;
            }

            var correlationFilter = filter as Admin.CorrelationRuleFilter;
            if (correlationFilter != null)
            {
                var result = new Legacy.CorrelationFilter
                {
                    CorrelationId = correlationFilter.CorrelationId,
                    MessageId = correlationFilter.MessageId,
                    To = correlationFilter.To,
                    ReplyTo = correlationFilter.ReplyTo,
                    Label = correlationFilter.Subject,
                    SessionId = correlationFilter.SessionId,
                    ReplyToSessionId = correlationFilter.ReplyToSessionId,
                    ContentType = correlationFilter.ContentType
                };
                CopyParameters(correlationFilter.ApplicationProperties, result.Properties);
                return result;
            }

            if (filter == null)
            {
                throw Unsupported("Null rule filter");
            }

            throw Unsupported("Unknown rule filter type");
        }

        private static Admin.RuleAction ToAdminAction(Legacy.RuleAction action)
        {
            if (action == null)
            {
                return null;
            }

            var sqlAction = action as Legacy.SqlRuleAction;
            if (sqlAction != null)
            {
                EnsureDefaultSqlCompatibility(sqlAction.CompatibilityLevel, "SQL rule action");
                var result = new Admin.SqlRuleAction(sqlAction.SqlExpression);
                CopyParameters(sqlAction.Parameters, result.Parameters);
                return result;
            }

            throw Unsupported("Unknown rule action type");
        }

        private static Legacy.RuleAction ToLegacyAction(Admin.RuleAction action)
        {
            if (action == null)
            {
                return null;
            }

            var sqlAction = action as Admin.SqlRuleAction;
            if (sqlAction != null)
            {
                var result = new Legacy.SqlRuleAction(sqlAction.SqlExpression);
                CopyParameters(sqlAction.Parameters, result.Parameters);
                return result;
            }

            throw Unsupported("Unknown rule action type");
        }

        private static void EnsureDefaultSqlCompatibility(int compatibilityLevel, string kind)
        {
            if (compatibilityLevel != 20)
            {
                throw Unsupported(kind + " compatibility level " + compatibilityLevel);
            }
        }

        private static void CopyParameters(
            IDictionary<string, object> source,
            IDictionary<string, object> target)
        {
            if (source == null || target == null)
            {
                return;
            }

            foreach (var parameter in source)
            {
                target[parameter.Key] = parameter.Value;
            }
        }

        private static NotSupportedException Unsupported(string feature)
        {
            return new NotSupportedException(feature + " cannot be represented by the Service Bus administration SDK.");
        }
    }
}
