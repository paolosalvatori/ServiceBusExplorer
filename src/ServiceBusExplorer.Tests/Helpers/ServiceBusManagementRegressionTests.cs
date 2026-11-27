using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Azure.Core.Pipeline;
using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using Xunit;
using Legacy = Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.Tests.Helpers
{
    public partial class ServiceBusManagementClientTests
    {
        private const string CloudHost = "offline.servicebus.windows.net";
        private const string TestCredentials = ";SharedAccessKeyName=offline;SharedAccessKey=synthetic";
        private static readonly XNamespace BackupNamespace = "http://schemas.microsoft.com/servicebusexplorer";

        private sealed class OfflineHandler : HttpMessageHandler
        {
            private readonly string entityXml;
            public List<Uri> Addresses { get; } = new List<Uri>();
            public int MatchingForwardAuthorizationHeaders { get; private set; }
            public List<Dictionary<string, string>> SasFields { get; } = new List<Dictionary<string, string>>();

            public OfflineHandler(string entityXml = null) => this.entityXml = entityXml;

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                Addresses.Add(request.RequestUri);
                var header = request.Headers.GetValues("Authorization").Single();
                MatchingForwardAuthorizationHeaders = request.Headers.Count(h =>
                    h.Key.EndsWith("SupplementaryAuthorization", StringComparison.OrdinalIgnoreCase) && h.Value.Single() == header);
                var fields = header.Substring("SharedAccessSignature ".Length).Split('&')
                    .Select(field => field.Split(new[] { '=' }, 2))
                    .Where(parts => parts[0] == "sr" || parts[0] == "se")
                    .ToDictionary(parts => parts[0], parts => Uri.UnescapeDataString(parts[1]));
                SasFields.Add(fields);
                return Task.FromResult(new HttpResponseMessage(entityXml == null ? HttpStatusCode.NotFound : HttpStatusCode.OK)
                {
                    Content = new StringContent(entityXml ?? "<Error/>", Encoding.UTF8, "application/atom+xml")
                });
            }
        }

        private static ServiceBusAdministrationClient OfflineAdministrationClient(string connection, HttpClient http)
        {
            var profile = ServiceBusNamespace.GetServiceBusNamespace("test", connection, (_, __) => { });
            var options = new ServiceBusAdministrationClientOptions { Transport = new HttpClientTransport(http) };
            options.Retry.MaxRetries = 0;
            return ServiceBusHelper.CreateAdministrationClient(profile, options);
        }

        [Theory]
        [InlineData(";EntityPath=sales/orders", "https://offline.servicebus.windows.net/sales/orders")]
        [InlineData("", "https://offline.servicebus.windows.net/")]
        public async Task AdministrationSignsTheConfiguredAudience(string scope, string expected)
        {
            var handler = new OfflineHandler();
            using var http = new HttpClient(handler);
            var client = OfflineAdministrationClient("Endpoint=sb://" + CloudHost + ":5671" + TestCredentials + scope, http);
            Assert.False((await client.QueueExistsAsync("sales/orders")).Value);
            Assert.Equal(expected, Assert.Single(handler.SasFields)["sr"]);
            Assert.Equal(443, Assert.Single(handler.Addresses).Port);
        }

        [Fact]
        public async Task EntitySasRenewsOnEachRequestWithoutChangingItsAudience()
        {
            const string audience = "https://offline.servicebus.windows.net/sales/orders";
            var handler = new OfflineHandler();
            using var http = new HttpClient(handler);
            var options = new ServiceBusAdministrationClientOptions { Transport = new HttpClientTransport(http) };
            var issued = 0;
            var client = EntityScopedSasPolicy.CreateClient(new Uri("sb://" + CloudHost), options, () =>
                Microsoft.ServiceBus.SharedAccessSignatureTokenProvider.GetSharedAccessSignature("offline", "synthetic",
                    audience, TimeSpan.FromMinutes(10 * ++issued)));
            await client.QueueExistsAsync("sales/orders");
            await client.QueueExistsAsync("sales/orders");
            Assert.Equal(2, handler.SasFields.Count);
            Assert.All(handler.SasFields, fields => Assert.Equal(audience, fields["sr"]));
            Assert.True(long.Parse(handler.SasFields[1]["se"], CultureInfo.InvariantCulture) >
                        long.Parse(handler.SasFields[0]["se"], CultureInfo.InvariantCulture));
        }

        [Fact]
        public async Task ScopedForwardingHeadersRenewWithThePrimaryAuthorization()
        {
            const string audience = "https://offline.servicebus.windows.net/source";
            var handler = new OfflineHandler();
            using var http = new HttpClient(handler);
            var options = new ServiceBusAdministrationClientOptions { Transport = new HttpClientTransport(http) };
            var issued = 0;
            var client = EntityScopedSasPolicy.CreateClient(new Uri("sb://" + CloudHost), options, () =>
                Microsoft.ServiceBus.SharedAccessSignatureTokenProvider.GetSharedAccessSignature("offline", "synthetic",
                    audience, TimeSpan.FromMinutes(10 * ++issued)));
            var create = new CreateQueueOptions("source") { ForwardTo = "target", ForwardDeadLetteredMessagesTo = "archive" };
            await Assert.ThrowsAsync<ServiceBusException>(() => client.CreateQueueAsync(create));
            Assert.Equal(audience, Assert.Single(handler.SasFields)["sr"]);
            Assert.Equal(2, handler.MatchingForwardAuthorizationHeaders);
        }

        [Theory]
        [InlineData("orders/..")]
        [InlineData("/")]
        public void EntityAudienceCannotNormalizeToTheNamespace(string path)
        {
            using var http = new HttpClient(new OfflineHandler());
            Assert.Throws<ArgumentException>(() => OfflineAdministrationClient("Endpoint=sb://" + CloudHost +
                TestCredentials + ";EntityPath=" + path, http));
        }

        [Theory]
        [InlineData("sb://offline.servicebus.windows.net:5671", "", "https", 443)]
        [InlineData("sb://offline.servicebus.windows.net:5672", ";ManagementPort=4443", "https", 4443)]
        [InlineData("sb://localhost:5672", ";UseDevelopmentEmulator=true", "http", 5300)]
        [InlineData("sb://localhost:6312", ";UseDevelopmentEmulator=true", "http", 6312)]
        [InlineData("sb://localhost:5672", ";UseDevelopmentEmulator=true;ManagementPort=6400", "http", 6400)]
        public async Task AdministrationUsesTheCorrectProtocolAndListener(string endpoint, string settings, string scheme, int port)
        {
            var handler = new OfflineHandler();
            using var http = new HttpClient(handler);
            var client = OfflineAdministrationClient("Endpoint=" + endpoint + TestCredentials + settings, http);
            await client.QueueExistsAsync("orders");
            var address = Assert.Single(handler.Addresses);
            Assert.Equal(scheme, address.Scheme);
            Assert.Equal(new Uri(endpoint).Host, address.Host);
            Assert.Equal(port, address.Port);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("65536")]
        [InlineData("amqp")]
        public void InvalidManagementPortFailsBeforeAnyRequest(string port)
        {
            var handler = new OfflineHandler();
            using var http = new HttpClient(handler);
            Assert.Throws<FormatException>(() => OfflineAdministrationClient("Endpoint=sb://" + CloudHost +
                TestCredentials + ";ManagementPort=" + port, http));
            Assert.Empty(handler.Addresses);
        }

        [Fact]
        public void SecondaryManagementOperationsReuseTheSessionClient()
        {
            var sdk = new FakeAdministrationClient();
            Assert.Same(sdk, NewHelper(sdk).GetServiceBusHelper2().CreateAdministrationClient());
        }

        [Theory]
        [InlineData(true, false, 16384, 1024, 1)]
        [InlineData(true, false, 81920, 5120, 1)]
        [InlineData(true, true, 81920, 81920, 1)]
        [InlineData(true, false, 2048, 2048, 0)]
        [InlineData(false, true, 81920, 81920, 0)]
        public async Task PartitionedBackupRoundTripsTheCreationSize(bool partitioned, bool premium, long reported, long expected, int tierReads)
        {
            var sdk = new FakeAdministrationClient { Sku = premium ? MessagingSku.Premium : MessagingSku.Standard, BrokerSize = reported, Partitioned = partitioned };
            var helper = NewHelper(sdk);
            var client = NewClient(sdk);
            var logs = new List<string>();
            var exporter = new ImportExportHelper((message, _) => logs.Add(message));
            var entities = new List<IExtensibleDataObject> { client.GetQueue("orders"), client.GetTopic("events") };
            var xml = await exporter.ReadAndSerialize(helper, entities);
            var root = XElement.Parse(xml);
            Assert.Equal(new[] { expected, expected }, root.Descendants(BackupNamespace + "MaxSizeInMegabytes").Select(e => (long)e));
            Assert.Equal(tierReads, sdk.NamespaceReads);

            // A new-format file already contains creation sizes. The restore cannot reinterpret a Premium size.
            sdk.NamespaceFailure = new InvalidOperationException("Unexpected SKU read during restore");
            exporter.DeserializeAndCreate(helper, xml);
            Assert.Empty(logs);
            Assert.Equal(expected, sdk.CreatedQueue.MaxSizeInMegabytes);
            Assert.Equal(expected, sdk.CreatedTopic.MaxSizeInMegabytes);
            Assert.Equal(partitioned, sdk.CreatedQueue.EnablePartitioning);
            Assert.Equal(partitioned, sdk.CreatedTopic.EnablePartitioning);
        }

        private static string LegacyPartitionedBackup(long size)
        {
            var ns = BackupNamespace;
            return new XElement(ns + "Entities",
                new XElement(ns + "Queues", new XElement(ns + "Queue",
                    new XElement(ns + "Path", "orders"), new XElement(ns + "EnablePartitioning", true), new XElement(ns + "MaxSizeInMegabytes", size))),
                new XElement(ns + "Topics", new XElement(ns + "Topic",
                    new XElement(ns + "Path", "events"), new XElement(ns + "EnablePartitioning", true), new XElement(ns + "MaxSizeInMegabytes", size))))
                .ToString();
        }

        [Theory]
        [InlineData(false, 32768, 2048, 1)]
        [InlineData(true, 32768, 32768, 1)]
        [InlineData(false, 5120, 5120, 0)]
        public void UnmarkedPartitionedBackupsUseTheDestinationTierOnlyWhenAmbiguous(bool premium, long size, long expected, int tierReads)
        {
            var sdk = new FakeAdministrationClient { Sku = premium ? MessagingSku.Premium : MessagingSku.Standard };
            NewHelper(sdk).ImportEntities(LegacyPartitionedBackup(size));
            Assert.Equal(expected, sdk.CreatedQueue.MaxSizeInMegabytes);
            Assert.Equal(expected, sdk.CreatedTopic.MaxSizeInMegabytes);
            Assert.Equal(tierReads, sdk.NamespaceReads);
        }

        [Fact]
        public void AmbiguousBackupWithUnavailableTierStopsBeforeAnyCreation()
        {
            var sdk = new FakeAdministrationClient { NamespaceFailure = new UnauthorizedAccessException("offline") };
            var doc = XElement.Parse(LegacyPartitionedBackup(16384));
            doc.Element(BackupNamespace + "Queues").AddFirst(new XElement(BackupNamespace + "Queue", new XElement(BackupNamespace + "Path", "first")));
            Assert.Throws<UnauthorizedAccessException>(() => NewHelper(sdk).ImportEntities(doc.ToString()));
            Assert.Null(sdk.CreatedQueue);
            Assert.Null(sdk.CreatedTopic);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task BrokerExpressFlagSurvivesExportAndRejectsRestoreBeforeMutation(bool queue)
        {
            var kind = queue ? "QueueDescription" : "TopicDescription";
            var wire = "<entry xmlns='http://www.w3.org/2005/Atom'><title>express</title><content type='application/xml'><" + kind +
                " xmlns='http://schemas.microsoft.com/netservices/2010/10/servicebus/connect'><EnableExpress>true</EnableExpress></" + kind + "></content></entry>";
            using var http = new HttpClient(new OfflineHandler(wire));
            var reader = OfflineAdministrationClient("Endpoint=sb://" + CloudHost + TestCredentials, http);
            IExtensibleDataObject entity = queue ? new QueueInfo((await reader.GetQueueAsync("express")).Value, null) :
                new TopicInfo((await reader.GetTopicAsync("express")).Value, null, null);
            var sdk = new FakeAdministrationClient();
            var helper = NewHelper(sdk);
            var xml = await new ImportExportHelper((_, __) => { }).ReadAndSerialize(helper,
                new List<IExtensibleDataObject> { new QueueInfo("first"), entity });
            var doc = XElement.Parse(xml);
            var saved = doc.Descendants(BackupNamespace + (queue ? "Queue" : "Topic"))
                .Single(e => (string)e.Element(BackupNamespace + "Path") == "express");
            Assert.True(bool.Parse(saved.Element(BackupNamespace + "EnableExpress").Value));
            var error = Assert.Throws<NotSupportedException>(() => helper.ImportEntities(xml));
            Assert.Contains("EnableExpress", error.Message);
            Assert.Null(sdk.CreatedQueue);
            Assert.Null(sdk.CreatedTopic);
        }

        [Theory]
        [InlineData("Creating", Legacy.EntityStatus.Creating)]
        [InlineData("Restoring", Legacy.EntityStatus.Restoring)]
        [InlineData("Deleting", Legacy.EntityStatus.Deleting)]
        [InlineData("Renaming", Legacy.EntityStatus.Renaming)]
        [InlineData("FutureBrokerState", Legacy.EntityStatus.Unknown)]
        public void TransitionalReadStatusKeepsPeersVisibleAndWriteValidationStrict(string state, Legacy.EntityStatus expected)
        {
            var sdk = new FakeAdministrationClient { QueueStatus = new EntityStatus(state) };
            var queues = NewClient(sdk).GetQueues(null, 10).ToList();
            Assert.Equal(Legacy.EntityStatus.Active, queues.Single(q => q.Path == "invoices").Status);
            var changing = queues.Single(q => q.Path == "orders");
            Assert.Equal(expected, changing.Status);
            Assert.Throws<NotSupportedException>(() => ServiceBusManagementMapper.ToCreateOptions(changing.Description));
        }

        [Fact]
        public void ModernCategoryFailuresAreIsolatedAndSessionCancellationPropagates()
        {
            var busy = new ServiceBusException("offline", ServiceBusFailureReason.ServiceBusy);
            Assert.True(MainForm.IsEntityCategoryFailure(busy));
            Assert.True(MainForm.IsEntityCategoryFailure(new AggregateException(busy)));
            Assert.True(MainForm.IsEntityCategoryFailure(new InvalidOperationException("SDK response", new Azure.RequestFailedException(403, "offline"))));
            Assert.False(MainForm.IsEntityCategoryFailure(new InvalidOperationException("unrelated application error")));
            Assert.True(MainForm.IsEntityCategoryFailure(new TimeoutException("enumeration budget", new OperationCanceledException())));
            var canceled = new OperationCanceledException();
            Assert.False(MainForm.IsEntityCategoryFailure(canceled));
            Assert.False(MainForm.IsEntityCategoryFailure(new ServiceBusException("offline", ServiceBusFailureReason.GeneralError, innerException: canceled)));
            var signIn = new AuthenticationFailedException("interactive sign-in dismissed", canceled);
            Assert.False(MainForm.IsEntityCategoryFailure(new AggregateException(busy, new UnauthorizedAccessException("SDK wrapper", signIn))));
        }

        [Theory]
        [InlineData("sb://localhost:5672", "localhost", 5672)]
        [InlineData("sb://my.emulator.invalid:6312", "my.emulator.invalid", 6312)]
        public void CopyEntityAddressesPreservesEmulatorAuthorityForAllEntityKinds(string endpoint, string host, int port)
        {
            var helper = NewHelper(new FakeAdministrationClient(), "Endpoint=" + endpoint + TestCredentials + ";UseDevelopmentEmulator=true");
            var urls = new[] { helper.GetQueueUri("group/orders"), helper.GetQueueDeadLetterQueueUri("group/orders"),
                helper.GetTopicUri("events"), helper.GetSubscriptionUri("events", "sub"), helper.GetSubscriptionDeadLetterQueueUri("events", "sub") };
            Assert.All(urls, url => { Assert.Equal("sb", url.Scheme); Assert.Equal(host, url.Host); Assert.Equal(port, url.Port); });
            Assert.Equal(new[] { "/group/orders", "/group/orders/$deadletterqueue", "/events", "/events/subscriptions/sub", "/events/subscriptions/sub/$deadletterqueue" },
                urls.Select(uri => uri.AbsolutePath.ToLowerInvariant()));
        }
    }
}
