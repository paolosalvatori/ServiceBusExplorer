using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.Utilities.Helpers;
using ServiceBusExplorer.WindowsAzure;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class EventHubDiscoveryTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        public async Task NamespaceDiscoveryReturnsTheCompleteManagementDescriptions(string path)
        {
            var first = new EventHubDescription("orders") { MessageRetentionInDays = 2 };
            var second = new EventHubDescription("audit") { Status = EntityStatus.Disabled };
            var reader = new RecordingEventHubs { List = () => Task.FromResult<IEnumerable<EventHubDescription>>(new[] { first, second }) };
            var helper = CreateHelper(path, reader);

            Assert.Equal(new[] { first, second }, await helper.GetVisibleEventHubsAsync());
            Assert.Equal(new[] { "list" }, reader.Calls);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ScopedDiscoveryReadsOnlyTheNamedHub(bool exists)
        {
            var hub = exists ? new EventHubDescription("Orders") : null;
            var reader = new RecordingEventHubs { Read = () => Task.FromResult(hub) };
            var helper = CreateHelper("Orders", reader);

            var found = await helper.GetVisibleEventHubsAsync();

            if (exists)
                Assert.Same(hub, Assert.Single(found));
            else
                Assert.Empty(found);
            Assert.Equal(new[] { "get Orders" }, reader.Calls);
        }

        [Fact]
        public async Task ScopedMessagingFailureReportsThePathWithoutListingTheNamespace()
        {
            var reader = new RecordingEventHubs { Read = () => Task.FromException<EventHubDescription>(new MessagingException("unavailable")) };
            var log = new List<string>();
            var helper = CreateHelper("Orders", reader, log);

            Assert.Empty(await helper.GetVisibleEventHubsAsync());
            Assert.Contains("EntityPath 'Orders'", Assert.Single(log));
            Assert.Equal(new[] { "get Orders" }, reader.Calls);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task CredentialAndNamespaceFailuresPropagateWithoutASecondRequest(bool scoped)
        {
            var failure = scoped
                ? new MessagingException("credential acquisition", new AuthenticationFailedException("sign-in failed"))
                : new MessagingException("namespace unavailable");
            var reader = new RecordingEventHubs
            {
                Read = () => Task.FromException<EventHubDescription>(failure),
                List = () => Task.FromException<IEnumerable<EventHubDescription>>(failure)
            };
            var log = new List<string>();
            var helper = CreateHelper(scoped ? "Orders" : null, reader, log);

            Assert.Same(failure, await Assert.ThrowsAsync<MessagingException>(() => helper.GetVisibleEventHubsAsync()));
            Assert.Empty(log);
            Assert.Equal(new[] { scoped ? "get Orders" : "list" }, reader.Calls);
        }

        private static ServiceBusHelper CreateHelper(string path, RecordingEventHubs reader, List<string> log = null)
        {
            var helper = new ServiceBusHelper((message, _) => log?.Add(message));
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            typeof(ServiceBusHelper).GetField("serviceBusNamespaceInstance", flags).SetValue(helper, new ServiceBusNamespace { EntityPath = path });
            typeof(ServiceBusHelper).GetField("serviceBusEventHub", flags).SetValue(helper, reader);
            return helper;
        }

        private sealed class RecordingEventHubs : IServiceBusEventHub
        {
            public readonly List<string> Calls = new List<string>();
            public Func<Task<EventHubDescription>> Read { get; set; }
            public Func<Task<IEnumerable<EventHubDescription>>> List { get; set; }

            public Task<EventHubDescription> GetEventHubAsync(string path)
            {
                Calls.Add("get " + path);
                return Read();
            }

            public Task<IEnumerable<EventHubDescription>> GetEventHubsAsync()
            {
                Calls.Add("list");
                return List();
            }

            public Task<EventHubDescription> UpdateEventHubAsync(EventHubDescription description) => throw new NotSupportedException();
            public EventHubDescription GetEventHub(string path) => throw new NotSupportedException();
            public EventHubDescription CreateEventHub(EventHubDescription description) => throw new NotSupportedException();
            public EventHubDescription UpdateEventHub(EventHubDescription description) => throw new NotSupportedException();
            public void DeleteEventHub(EventHubDescription description) => throw new NotSupportedException();
            public void DeleteEventHubs(IEnumerable<string> paths) => throw new NotSupportedException();
            public Uri GetEventHubUri(string path) => throw new NotSupportedException();
            public string Scheme { get; set; }
            public WriteToLogDelegate WriteToLog { get; set; }
            public event IServiceBusEntity.EventHandler OnCreate { add { } remove { } }
            public event IServiceBusEntity.EventHandler OnDelete { add { } remove { } }
        }
    }
}
