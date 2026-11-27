using System;
using System.Linq;
using System.Reflection;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    [Collection("Connection settings publication")]
    public class ConnectionPreparationTests
    {
        [Fact]
        public void FailureAfterLocalDnsResolutionPreservesPublishedProfileAndAdapters()
        {
            var retryCount = RetryHelper.RetryCount;
            try
            {
                RetryHelper.RetryCount = 0;
                var helper = new ServiceBusHelper((_, __) => { });
                var original = ServiceBusNamespace.GetServiceBusNamespace("original", "Endpoint=sb://localhost:5672;SharedAccessKeyName=name;SharedAccessKey=synthetic;UseDevelopmentEmulator=true", (_, __) => { });
                Assert.True(helper.Connect(original));
                var names = new[] { "serviceBusNamespaceInstance", "connectionString", "namespaceUri", "managementClient", "serviceBusQueue", "serviceBusTopic", "serviceBusSubscription", "serviceBusRule", "serviceBusRelay", "serviceBusNotificationHub", "serviceBusEventHub", "serviceBusConsumerGroup", "serviceBusPartition", "messagingFactory" };
                var fields = names.Select(name => typeof(ServiceBusHelper).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic)).ToArray();
                var before = fields.Select(field => field.GetValue(helper)).ToArray();
                // localhost resolves; SDK construction then rejects the unknown setting without network I/O.
                var rejected = ServiceBusNamespace.GetServiceBusNamespace("rejected", "Endpoint=sb://localhost;SharedAccessKeyName=name;SharedAccessKey=synthetic;UnsupportedSetting=reject", (_, __) => { });
                Assert.ThrowsAny<Exception>(() => helper.Connect(rejected));
                for (var i = 0; i < fields.Length; i++) Assert.Same(before[i], fields[i].GetValue(helper));
                Assert.True(helper.IsEmulator);
            }
            finally { RetryHelper.RetryCount = retryCount; }
        }
    }
}
