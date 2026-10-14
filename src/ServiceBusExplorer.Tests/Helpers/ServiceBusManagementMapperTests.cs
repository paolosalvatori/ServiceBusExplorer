using System;
using Admin = Azure.Messaging.ServiceBus.Administration;
using Legacy = Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ServiceBusManagementMapperTests
    {
        private const string PrimarySasKey = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=";
        private const string SecondarySasKey = "AQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQE=";

        [Fact]
        public void QueueCreateOptions_PreserveConfigurationAndSasAuthorization()
        {
            var source = new Legacy.QueueDescription("queue")
            {
                LockDuration = TimeSpan.FromMinutes(2),
                MaxSizeInMegabytes = 2048,
                RequiresDuplicateDetection = true,
                RequiresSession = true,
                DefaultMessageTimeToLive = TimeSpan.FromDays(2),
                AutoDeleteOnIdle = TimeSpan.FromDays(3),
                EnableDeadLetteringOnMessageExpiration = true,
                DuplicateDetectionHistoryTimeWindow = TimeSpan.FromMinutes(8),
                MaxDeliveryCount = 9,
                EnableBatchedOperations = false,
                EnablePartitioning = true,
                UserMetadata = "queue metadata"
            };
            source.Authorization.Add(new Legacy.SharedAccessAuthorizationRule(
                "queue-key",
                PrimarySasKey,
                SecondarySasKey,
                new[] { Legacy.AccessRights.Manage, Legacy.AccessRights.Send, Legacy.AccessRights.Listen }));

            var options = ServiceBusManagementMapper.ToCreateOptions(source);

            Assert.Equal(source.Path, options.Name);
            Assert.Equal(source.LockDuration, options.LockDuration);
            Assert.Equal(source.MaxSizeInMegabytes, options.MaxSizeInMegabytes);
            Assert.Equal(source.RequiresDuplicateDetection, options.RequiresDuplicateDetection);
            Assert.Equal(source.RequiresSession, options.RequiresSession);
            Assert.Equal(source.DuplicateDetectionHistoryTimeWindow, options.DuplicateDetectionHistoryTimeWindow);
            Assert.Equal(source.EnablePartitioning, options.EnablePartitioning);
            Assert.Equal(source.UserMetadata, options.UserMetadata);
            var authorization = Assert.IsType<Admin.SharedAccessAuthorizationRule>(Assert.Single(options.AuthorizationRules));
            Assert.Equal("queue-key", authorization.KeyName);
            Assert.Equal(PrimarySasKey, authorization.PrimaryKey);
            Assert.Equal(SecondarySasKey, authorization.SecondaryKey);
            Assert.Equal(
                new[] { Admin.AccessRights.Manage, Admin.AccessRights.Send, Admin.AccessRights.Listen },
                authorization.Rights);

            var queueProperties = Azure.Messaging.ServiceBus.ServiceBusModelFactory.QueueProperties(
                options.Name,
                options.LockDuration,
                options.MaxSizeInMegabytes,
                options.RequiresDuplicateDetection,
                options.RequiresSession,
                options.DefaultMessageTimeToLive,
                options.AutoDeleteOnIdle,
                options.DeadLetteringOnMessageExpiration,
                options.DuplicateDetectionHistoryTimeWindow,
                options.MaxDeliveryCount,
                options.EnableBatchedOperations,
                options.Status,
                options.ForwardTo,
                options.ForwardDeadLetteredMessagesTo,
                options.UserMetadata,
                options.EnablePartitioning);
            queueProperties.AuthorizationRules.AddRange(options.AuthorizationRules);
            var legacyProjection = ServiceBusManagementMapper.From(queueProperties);
            Assert.Equal("queue", legacyProjection.Path);
            Assert.Equal(source.LockDuration, legacyProjection.LockDuration);
            Assert.Equal(source.MaxSizeInMegabytes, legacyProjection.MaxSizeInMegabytes);
            Assert.Equal(source.RequiresDuplicateDetection, legacyProjection.RequiresDuplicateDetection);
            Assert.Equal(source.RequiresSession, legacyProjection.RequiresSession);
            Assert.Equal(source.DefaultMessageTimeToLive, legacyProjection.DefaultMessageTimeToLive);
            Assert.Equal(source.AutoDeleteOnIdle, legacyProjection.AutoDeleteOnIdle);
            Assert.Equal(source.EnableDeadLetteringOnMessageExpiration, legacyProjection.EnableDeadLetteringOnMessageExpiration);
            Assert.Equal(source.DuplicateDetectionHistoryTimeWindow, legacyProjection.DuplicateDetectionHistoryTimeWindow);
            Assert.Equal(source.MaxDeliveryCount, legacyProjection.MaxDeliveryCount);
            Assert.Equal(source.EnableBatchedOperations, legacyProjection.EnableBatchedOperations);
            Assert.Equal(source.Status, legacyProjection.Status);
            Assert.Equal(source.ForwardTo, legacyProjection.ForwardTo);
            Assert.Equal(source.ForwardDeadLetteredMessagesTo, legacyProjection.ForwardDeadLetteredMessagesTo);
            Assert.Equal(source.EnablePartitioning, legacyProjection.EnablePartitioning);
            Assert.Equal(source.UserMetadata, legacyProjection.UserMetadata);
            var legacyAuthorization = Assert.IsType<Legacy.SharedAccessAuthorizationRule>(
                Assert.Single(legacyProjection.Authorization));
            Assert.Equal("queue-key", legacyAuthorization.KeyName);
            Assert.Equal(PrimarySasKey, legacyAuthorization.PrimaryKey);
            Assert.Equal(SecondarySasKey, legacyAuthorization.SecondaryKey);
            Assert.Equal(
                new[] { Legacy.AccessRights.Manage, Legacy.AccessRights.Send, Legacy.AccessRights.Listen },
                legacyAuthorization.Rights);
        }

        [Fact]
        public void TopicCreateOptions_PreserveConfigurationAndSasAuthorization()
        {
            var source = new Legacy.TopicDescription("topic")
            {
                DefaultMessageTimeToLive = TimeSpan.FromDays(2),
                AutoDeleteOnIdle = TimeSpan.FromDays(3),
                MaxSizeInMegabytes = 2048,
                RequiresDuplicateDetection = true,
                DuplicateDetectionHistoryTimeWindow = TimeSpan.FromMinutes(8),
                SupportOrdering = true,
                EnableBatchedOperations = false,
                EnablePartitioning = true,
                UserMetadata = "topic metadata"
            };
            source.Authorization.Add(new Legacy.SharedAccessAuthorizationRule(
                "topic-key",
                PrimarySasKey,
                SecondarySasKey,
                new[] { Legacy.AccessRights.Send, Legacy.AccessRights.Listen }));

            var options = ServiceBusManagementMapper.ToCreateOptions(source);

            Assert.Equal(source.Path, options.Name);
            Assert.Equal(source.SupportOrdering, options.SupportOrdering);
            Assert.Equal(source.DuplicateDetectionHistoryTimeWindow, options.DuplicateDetectionHistoryTimeWindow);
            Assert.Equal(source.EnablePartitioning, options.EnablePartitioning);
            Assert.Equal(source.UserMetadata, options.UserMetadata);
            var authorization = Assert.IsType<Admin.SharedAccessAuthorizationRule>(Assert.Single(options.AuthorizationRules));
            Assert.Equal("topic-key", authorization.KeyName);
            Assert.Equal(PrimarySasKey, authorization.PrimaryKey);
            Assert.Equal(SecondarySasKey, authorization.SecondaryKey);
            Assert.Equal(new[] { Admin.AccessRights.Send, Admin.AccessRights.Listen }, authorization.Rights);

            var topicProperties = Azure.Messaging.ServiceBus.ServiceBusModelFactory.TopicProperties(
                options.Name,
                options.MaxSizeInMegabytes,
                options.RequiresDuplicateDetection,
                options.DefaultMessageTimeToLive,
                options.AutoDeleteOnIdle,
                options.DuplicateDetectionHistoryTimeWindow,
                options.EnableBatchedOperations,
                options.Status,
                options.EnablePartitioning);
            topicProperties.SupportOrdering = options.SupportOrdering;
            topicProperties.UserMetadata = options.UserMetadata;
            topicProperties.AuthorizationRules.AddRange(options.AuthorizationRules);
            var legacyProjection = ServiceBusManagementMapper.From(topicProperties);
            Assert.Equal("topic", legacyProjection.Path);
            Assert.Equal(source.DefaultMessageTimeToLive, legacyProjection.DefaultMessageTimeToLive);
            Assert.Equal(source.AutoDeleteOnIdle, legacyProjection.AutoDeleteOnIdle);
            Assert.Equal(source.MaxSizeInMegabytes, legacyProjection.MaxSizeInMegabytes);
            Assert.Equal(source.RequiresDuplicateDetection, legacyProjection.RequiresDuplicateDetection);
            Assert.Equal(source.DuplicateDetectionHistoryTimeWindow, legacyProjection.DuplicateDetectionHistoryTimeWindow);
            Assert.Equal(source.SupportOrdering, legacyProjection.SupportOrdering);
            Assert.Equal(source.EnableBatchedOperations, legacyProjection.EnableBatchedOperations);
            Assert.Equal(source.Status, legacyProjection.Status);
            Assert.Equal(source.EnablePartitioning, legacyProjection.EnablePartitioning);
            Assert.Equal(source.UserMetadata, legacyProjection.UserMetadata);
            var legacyAuthorization = Assert.IsType<Legacy.SharedAccessAuthorizationRule>(
                Assert.Single(legacyProjection.Authorization));
            Assert.Equal("topic-key", legacyAuthorization.KeyName);
            Assert.Equal(PrimarySasKey, legacyAuthorization.PrimaryKey);
            Assert.Equal(SecondarySasKey, legacyAuthorization.SecondaryKey);
            Assert.Equal(new[] { Legacy.AccessRights.Send, Legacy.AccessRights.Listen }, legacyAuthorization.Rights);
        }

        [Fact]
        public void SubscriptionCreateOptions_PreserveConfigurationAndIdentity()
        {
            var source = new Legacy.SubscriptionDescription("topic", "subscription")
            {
                LockDuration = TimeSpan.FromMinutes(2),
                RequiresSession = true,
                DefaultMessageTimeToLive = TimeSpan.FromDays(2),
                AutoDeleteOnIdle = TimeSpan.FromDays(3),
                EnableDeadLetteringOnMessageExpiration = true,
                EnableDeadLetteringOnFilterEvaluationExceptions = true,
                MaxDeliveryCount = 9,
                EnableBatchedOperations = false,
                Status = Legacy.EntityStatus.Disabled,
                ForwardTo = "forward-to",
                ForwardDeadLetteredMessagesTo = "forward-dead-lettered",
                UserMetadata = "subscription metadata"
            };

            var options = ServiceBusManagementMapper.ToCreateOptions(source);

            Assert.Equal(source.TopicPath, options.TopicName);
            Assert.Equal(source.Name, options.SubscriptionName);
            Assert.Equal(source.LockDuration, options.LockDuration);
            Assert.Equal(source.RequiresSession, options.RequiresSession);
            Assert.Equal(source.EnableDeadLetteringOnMessageExpiration, options.DeadLetteringOnMessageExpiration);
            Assert.Equal(source.EnableDeadLetteringOnFilterEvaluationExceptions, options.EnableDeadLetteringOnFilterEvaluationExceptions);
            Assert.Equal(Admin.EntityStatus.Disabled, options.Status);
            Assert.Equal(source.ForwardTo, options.ForwardTo);
            Assert.Equal(source.ForwardDeadLetteredMessagesTo, options.ForwardDeadLetteredMessagesTo);
            Assert.Equal(source.UserMetadata, options.UserMetadata);

            var subscriptionProperties = Azure.Messaging.ServiceBus.ServiceBusModelFactory.SubscriptionProperties(
                options.TopicName,
                options.SubscriptionName,
                options.LockDuration,
                options.RequiresSession,
                options.DefaultMessageTimeToLive,
                options.AutoDeleteOnIdle,
                options.DeadLetteringOnMessageExpiration,
                options.MaxDeliveryCount,
                options.EnableBatchedOperations,
                options.Status,
                options.ForwardTo,
                options.ForwardDeadLetteredMessagesTo,
                options.UserMetadata);
            subscriptionProperties.EnableDeadLetteringOnFilterEvaluationExceptions =
                options.EnableDeadLetteringOnFilterEvaluationExceptions;
            var legacyProjection = ServiceBusManagementMapper.From(subscriptionProperties);
            Assert.Equal(source.TopicPath, legacyProjection.TopicPath);
            Assert.Equal(source.Name, legacyProjection.Name);
            Assert.Equal(source.LockDuration, legacyProjection.LockDuration);
            Assert.Equal(source.RequiresSession, legacyProjection.RequiresSession);
            Assert.Equal(source.DefaultMessageTimeToLive, legacyProjection.DefaultMessageTimeToLive);
            Assert.Equal(source.AutoDeleteOnIdle, legacyProjection.AutoDeleteOnIdle);
            Assert.Equal(source.EnableDeadLetteringOnMessageExpiration, legacyProjection.EnableDeadLetteringOnMessageExpiration);
            Assert.Equal(source.EnableDeadLetteringOnFilterEvaluationExceptions, legacyProjection.EnableDeadLetteringOnFilterEvaluationExceptions);
            Assert.Equal(source.MaxDeliveryCount, legacyProjection.MaxDeliveryCount);
            Assert.Equal(source.EnableBatchedOperations, legacyProjection.EnableBatchedOperations);
            Assert.Equal(source.Status, legacyProjection.Status);
            Assert.Equal(source.ForwardTo, legacyProjection.ForwardTo);
            Assert.Equal(source.ForwardDeadLetteredMessagesTo, legacyProjection.ForwardDeadLetteredMessagesTo);
            Assert.Equal(source.UserMetadata, legacyProjection.UserMetadata);
        }

        [Fact]
        public void DefaultLegacyDescriptions_CreateWithEmptyMetadata()
        {
            var queue = new Legacy.QueueDescription("xml-queue");
            Assert.True(queue.SupportOrdering);

            var queueOptions = ServiceBusManagementMapper.ToCreateOptions(queue);
            Assert.Equal("xml-queue", queueOptions.Name);
            Assert.Equal(string.Empty, queueOptions.UserMetadata);

            var topic = new Legacy.TopicDescription("default-topic");
            var topicOptions = ServiceBusManagementMapper.ToCreateOptions(topic);
            Assert.Equal("default-topic", topicOptions.Name);
            Assert.Equal(string.Empty, topicOptions.UserMetadata);

            var subscription = new Legacy.SubscriptionDescription("default-topic", "default-subscription");
            var subscriptionOptions = ServiceBusManagementMapper.ToCreateOptions(subscription);
            Assert.Equal("default-topic", subscriptionOptions.TopicName);
            Assert.Equal("default-subscription", subscriptionOptions.SubscriptionName);
            Assert.Equal(string.Empty, subscriptionOptions.UserMetadata);
        }

        [Fact]
        public void RuleConversions_PreserveSqlFilterParametersAndSqlAction()
        {
            var sourceFilter = new Legacy.SqlFilter("amount > @limit");
            sourceFilter.Parameters["@limit"] = 17;
            sourceFilter.Parameters["@category"] = "premium";
            var sourceAction = new Legacy.SqlRuleAction("SET tier = @tier");
            sourceAction.Parameters["@tier"] = "gold";
            var source = new Legacy.RuleDescription("sql-rule", sourceFilter)
            {
                Action = sourceAction
            };

            var nativeOptions = ServiceBusManagementMapper.ToCreateOptions(source);
            var nativeFilter = Assert.IsType<Admin.SqlRuleFilter>(nativeOptions.Filter);
            Assert.Equal(17, nativeFilter.Parameters["@limit"]);
            Assert.Equal("premium", nativeFilter.Parameters["@category"]);
            var nativeAction = Assert.IsType<Admin.SqlRuleAction>(nativeOptions.Action);
            Assert.Equal("gold", nativeAction.Parameters["@tier"]);

            var projection = ServiceBusManagementMapper.From(
                Azure.Messaging.ServiceBus.ServiceBusModelFactory.RuleProperties("sql-rule", nativeFilter, nativeAction));
            var legacyFilter = Assert.IsType<Legacy.SqlFilter>(projection.Filter);
            Assert.Equal(17, legacyFilter.Parameters["@limit"]);
            Assert.Equal("premium", legacyFilter.Parameters["@category"]);
            var legacyAction = Assert.IsType<Legacy.SqlRuleAction>(projection.Action);
            Assert.Equal("gold", legacyAction.Parameters["@tier"]);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void RuleConversions_PreserveTrueAndFalseFilters(bool value)
        {
            Legacy.Filter legacyFilter = value
                ? (Legacy.Filter)new Legacy.TrueFilter()
                : new Legacy.FalseFilter();
            var nativeOptions = ServiceBusManagementMapper.ToCreateOptions(
                new Legacy.RuleDescription("bool-rule", legacyFilter));

            if (value)
            {
                Assert.IsType<Admin.TrueRuleFilter>(nativeOptions.Filter);
            }
            else
            {
                Assert.IsType<Admin.FalseRuleFilter>(nativeOptions.Filter);
            }

            var nativeFilter = value
                ? (Admin.RuleFilter)new Admin.TrueRuleFilter()
                : new Admin.FalseRuleFilter();
            var projection = ServiceBusManagementMapper.From(
                Azure.Messaging.ServiceBus.ServiceBusModelFactory.RuleProperties("bool-rule", nativeFilter, null));

            if (value)
            {
                Assert.IsType<Legacy.TrueFilter>(projection.Filter);
            }
            else
            {
                Assert.IsType<Legacy.FalseFilter>(projection.Filter);
            }
        }

        [Fact]
        public void RuleConversions_PreserveCorrelationSystemAndTypedApplicationProperties()
        {
            var sourceFilter = new Legacy.CorrelationFilter
            {
                CorrelationId = "correlation-id",
                MessageId = "message-id",
                To = "to",
                ReplyTo = "reply-to",
                Label = "subject",
                SessionId = "session-id",
                ReplyToSessionId = "reply-to-session-id",
                ContentType = "application/json"
            };
            sourceFilter.Properties["priority"] = 23;
            sourceFilter.Properties["is-expedited"] = true;
            sourceFilter.Properties["category"] = "premium";

            var nativeOptions = ServiceBusManagementMapper.ToCreateOptions(
                new Legacy.RuleDescription("correlation-rule", sourceFilter));
            var nativeFilter = Assert.IsType<Admin.CorrelationRuleFilter>(nativeOptions.Filter);
            Assert.Equal(sourceFilter.CorrelationId, nativeFilter.CorrelationId);
            Assert.Equal(sourceFilter.MessageId, nativeFilter.MessageId);
            Assert.Equal(sourceFilter.To, nativeFilter.To);
            Assert.Equal(sourceFilter.ReplyTo, nativeFilter.ReplyTo);
            Assert.Equal(sourceFilter.Label, nativeFilter.Subject);
            Assert.Equal(sourceFilter.SessionId, nativeFilter.SessionId);
            Assert.Equal(sourceFilter.ReplyToSessionId, nativeFilter.ReplyToSessionId);
            Assert.Equal(sourceFilter.ContentType, nativeFilter.ContentType);
            Assert.Equal(23, nativeFilter.ApplicationProperties["priority"]);
            Assert.Equal(true, nativeFilter.ApplicationProperties["is-expedited"]);
            Assert.Equal("premium", nativeFilter.ApplicationProperties["category"]);

            var projection = ServiceBusManagementMapper.From(
                Azure.Messaging.ServiceBus.ServiceBusModelFactory.RuleProperties("correlation-rule", nativeFilter, null));
            var legacyFilter = Assert.IsType<Legacy.CorrelationFilter>(projection.Filter);
            Assert.Equal(sourceFilter.CorrelationId, legacyFilter.CorrelationId);
            Assert.Equal(sourceFilter.MessageId, legacyFilter.MessageId);
            Assert.Equal(sourceFilter.To, legacyFilter.To);
            Assert.Equal(sourceFilter.ReplyTo, legacyFilter.ReplyTo);
            Assert.Equal(sourceFilter.Label, legacyFilter.Label);
            Assert.Equal(sourceFilter.SessionId, legacyFilter.SessionId);
            Assert.Equal(sourceFilter.ReplyToSessionId, legacyFilter.ReplyToSessionId);
            Assert.Equal(sourceFilter.ContentType, legacyFilter.ContentType);
            Assert.Equal(23, legacyFilter.Properties["priority"]);
            Assert.Equal(true, legacyFilter.Properties["is-expedited"]);
            Assert.Equal("premium", legacyFilter.Properties["category"]);
        }

        [Fact]
        public void RuleConversions_PreserveNoAction()
        {
            var nativeOptions = ServiceBusManagementMapper.ToCreateOptions(
                new Legacy.RuleDescription("no-action", new Legacy.TrueFilter()));
            Assert.Null(nativeOptions.Action);

            var projection = ServiceBusManagementMapper.From(
                Azure.Messaging.ServiceBus.ServiceBusModelFactory.RuleProperties("no-action", new Admin.TrueRuleFilter(), null));
            Assert.Null(projection.Action);
        }

        [Fact]
        public void Apply_UpdatesMutablePropertiesAndPreservesNativeMessageSize()
        {
            var queue = CreateQueueProperties("queue", maxMessageSizeInKilobytes: 512);
            var queueSource = new Legacy.QueueDescription("queue")
            {
                DuplicateDetectionHistoryTimeWindow = TimeSpan.FromMinutes(14),
                SupportOrdering = false
            };
            ServiceBusManagementMapper.Apply(queueSource, queue);
            Assert.Equal(queueSource.DuplicateDetectionHistoryTimeWindow, queue.DuplicateDetectionHistoryTimeWindow);
            Assert.Equal(512, queue.MaxMessageSizeInKilobytes);

            var topic = CreateTopicProperties("topic");
            topic.SupportOrdering = false;
            var topicSource = new Legacy.TopicDescription("topic")
            {
                DuplicateDetectionHistoryTimeWindow = TimeSpan.FromMinutes(15),
                SupportOrdering = true
            };
            ServiceBusManagementMapper.Apply(topicSource, topic);
            Assert.Equal(topicSource.DuplicateDetectionHistoryTimeWindow, topic.DuplicateDetectionHistoryTimeWindow);
            Assert.Equal(topicSource.SupportOrdering, topic.SupportOrdering);
        }

        [Fact]
        public void Apply_RejectsChangedCreateOnlyPropertiesAndIdentity()
        {
            Assert.Throws<NotSupportedException>(() => ServiceBusManagementMapper.Apply(
                new Legacy.QueueDescription("queue") { RequiresSession = true },
                CreateQueueProperties("queue")));
            Assert.Throws<NotSupportedException>(() => ServiceBusManagementMapper.Apply(
                new Legacy.QueueDescription("queue") { RequiresDuplicateDetection = true },
                CreateQueueProperties("queue")));
            Assert.Throws<NotSupportedException>(() => ServiceBusManagementMapper.Apply(
                new Legacy.QueueDescription("queue") { EnablePartitioning = true },
                CreateQueueProperties("queue")));
            Assert.Throws<NotSupportedException>(() => ServiceBusManagementMapper.Apply(
                new Legacy.TopicDescription("topic") { EnablePartitioning = true },
                CreateTopicProperties("topic")));
            Assert.Throws<NotSupportedException>(() => ServiceBusManagementMapper.Apply(
                new Legacy.TopicDescription("topic") { RequiresDuplicateDetection = true },
                CreateTopicProperties("topic")));
            Assert.Throws<NotSupportedException>(() => ServiceBusManagementMapper.Apply(
                new Legacy.SubscriptionDescription("topic", "subscription") { RequiresSession = true },
                CreateSubscriptionProperties("topic", "subscription")));

            Assert.Throws<ArgumentException>(() => ServiceBusManagementMapper.Apply(
                new Legacy.QueueDescription("other") { SupportOrdering = false },
                CreateQueueProperties("queue")));
            Assert.Throws<ArgumentException>(() => ServiceBusManagementMapper.Apply(
                new Legacy.TopicDescription("other"),
                CreateTopicProperties("topic")));
            Assert.Throws<ArgumentException>(() => ServiceBusManagementMapper.Apply(
                new Legacy.SubscriptionDescription("other-topic", "subscription"),
                CreateSubscriptionProperties("topic", "subscription")));
        }

        [Fact]
        public void CreateConversions_RejectUnsupportedLegacyFlagsAndAllowRules()
        {
            var orderingQueue = new Legacy.QueueDescription("queue") { SupportOrdering = true };
            Assert.Equal("queue", ServiceBusManagementMapper.ToCreateOptions(orderingQueue).Name);
            Assert.Throws<NotSupportedException>(() => ServiceBusManagementMapper.ToCreateOptions(
                new Legacy.QueueDescription("queue") { IsAnonymousAccessible = true }));
            Assert.Throws<NotSupportedException>(() => ServiceBusManagementMapper.ToCreateOptions(
                new Legacy.QueueDescription("queue") { EnableExpress = true }));
            Assert.Throws<NotSupportedException>(() => ServiceBusManagementMapper.ToCreateOptions(
                new Legacy.TopicDescription("topic") { EnableFilteringMessagesBeforePublishing = true }));
            Assert.Throws<NotSupportedException>(() => ServiceBusManagementMapper.ToCreateOptions(
                new Legacy.TopicDescription("topic") { IsAnonymousAccessible = true }));
            Assert.Throws<NotSupportedException>(() => ServiceBusManagementMapper.ToCreateOptions(
                new Legacy.TopicDescription("topic") { EnableExpress = true }));

            var defaultQueue = ServiceBusManagementMapper.ToCreateOptions(new Legacy.QueueDescription("default-queue"));
            Assert.Equal("default-queue", defaultQueue.Name);

            var queue = new Legacy.QueueDescription("queue");
            queue.Authorization.Add(new Legacy.AllowRule(
                "key",
                "claim-type",
                "claim-value",
                new[] { Legacy.AccessRights.Send }));
            Assert.Throws<NotSupportedException>(() => ServiceBusManagementMapper.ToCreateOptions(queue));
        }

        private static Admin.QueueProperties CreateQueueProperties(
            string name,
            bool requiresDuplicateDetection = false,
            bool requiresSession = false,
            bool enablePartitioning = false,
            long maxMessageSizeInKilobytes = 512)
        {
            return Azure.Messaging.ServiceBus.ServiceBusModelFactory.QueueProperties(
                name,
                TimeSpan.FromMinutes(1),
                1024,
                requiresDuplicateDetection,
                requiresSession,
                TimeSpan.FromDays(1),
                TimeSpan.FromMinutes(10),
                false,
                TimeSpan.FromMinutes(10),
                10,
                true,
                Admin.EntityStatus.Active,
                null,
                null,
                string.Empty,
                enablePartitioning,
                maxMessageSizeInKilobytes);
        }

        private static Admin.TopicProperties CreateTopicProperties(
            string name,
            bool requiresDuplicateDetection = false,
            bool enablePartitioning = false)
        {
            var topic = Azure.Messaging.ServiceBus.ServiceBusModelFactory.TopicProperties(
                name,
                1024,
                requiresDuplicateDetection,
                TimeSpan.FromDays(1),
                TimeSpan.FromMinutes(10),
                TimeSpan.FromMinutes(10),
                true,
                Admin.EntityStatus.Active,
                enablePartitioning,
                512);
            topic.AuthorizationRules.Clear();
            return topic;
        }

        private static Admin.SubscriptionProperties CreateSubscriptionProperties(
            string topicName,
            string subscriptionName,
            bool requiresSession = false)
        {
            return Azure.Messaging.ServiceBus.ServiceBusModelFactory.SubscriptionProperties(
                topicName,
                subscriptionName,
                TimeSpan.FromMinutes(1),
                requiresSession,
                TimeSpan.FromDays(1),
                TimeSpan.FromMinutes(10),
                false,
                10,
                true,
                Admin.EntityStatus.Active,
                null,
                null,
                string.Empty);
        }
    }
}
