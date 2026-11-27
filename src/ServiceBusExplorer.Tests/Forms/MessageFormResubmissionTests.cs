using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Core.Amqp;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using ServiceBusExplorer.Tests.Helpers;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    [CollectionDefinition("Service Bus connection settings", DisableParallelization = true)]
    public class ServiceBusConnectionSettingsCollection { }

    [Collection("Service Bus connection settings")]
    public class MessageFormResubmissionTests
    {
        [Theory]
        [InlineData(false, "ByteArray")]
        [InlineData(true, "ByteArray")]
        [InlineData(false, "String")]
        [InlineData(true, "Stream")]
        public void EditorPreparationPreservesBinaryAndRoutingUnlessNewIdRequested(bool newId, string bodyType)
        {
            RunSta(() =>
            {
                var bytes = new byte[] { 0, 255, 128, 65, 13, 10 };
                using var source = new BrokeredMessage(new MemoryStream(bytes))
                {
                    MessageId = "original-id", SessionId = "partition", PartitionKey = "partition",
                    ViaPartitionKey = "transaction", To = "destination", ReplyTo = "reply",
                    ReplyToSessionId = "reply-session", CorrelationId = "correlation", Label = "subject",
                    ContentType = "application/octet-stream", TimeToLive = TimeSpan.FromMinutes(10)
                };
                using var editor = new MessageForm(source, new ServiceBusHelper((_, __) => { }), (_, __) => { });
                ((CheckBox)editor.Controls.Find("chkNewMessageId", true).Single()).Checked = newId;
                ((ComboBox)editor.Controls.Find("cboBodyType", true).Single()).SelectedItem = bodyType;
                using var outbound = editor.BuildMessageForResubmission(source);
                var modern = ServiceBusMessageAdapter.ToServiceBusMessage(outbound);
                Assert.Equal(bytes, modern.Body.ToArray());
                if (newId) Assert.NotEqual("original-id", modern.MessageId);
                else Assert.Equal("original-id", modern.MessageId);
                Assert.Equal("partition", modern.SessionId);
                Assert.Equal("partition", modern.PartitionKey);
                Assert.Equal("transaction", modern.TransactionPartitionKey);
                Assert.Equal("destination", modern.To);
                Assert.Equal("reply", modern.ReplyTo);
                Assert.Equal("reply-session", modern.ReplyToSessionId);
                Assert.Equal("correlation", modern.CorrelationId);
                Assert.Equal("subject", modern.Subject);
                Assert.Equal("application/octet-stream", modern.ContentType);
                Assert.Equal(TimeSpan.FromMinutes(10), modern.TimeToLive);
                Assert.Equal(bytes, ServiceBusMessageAdapter.ToServiceBusMessage(source).Body.ToArray());
            });
        }

        [Theory]
        [InlineData(false, false, false)]
        [InlineData(false, true, false)]
        [InlineData(true, false, false)]
        [InlineData(true, true, false)]
        [InlineData(false, false, true)]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, true)]
        public void LegacySerializedBodiesRemainReadableAfterEditorPreparation(bool bytes, bool edited, bool newId)
        {
            RunSta(() =>
            {
                const string original = "original å";
                const string correction = "corrected ö";
                using var source = bytes ? new BrokeredMessage(System.Text.Encoding.UTF8.GetBytes(original)) : new BrokeredMessage(original);
                source.MessageId = "source-id";
                source.SessionId = "route";
                source.PartitionKey = "route";
                using var editor = new MessageForm(source, new ServiceBusHelper((_, __) => { }), (_, __) => { });
                ((CheckBox)editor.Controls.Find("chkNewMessageId", true).Single()).Checked = newId;
                if (edited) editor.Controls.Find("txtMessageText", true).Single().Text = correction;
                using var outbound = editor.BuildMessageForResubmission(source);
                if (bytes) Assert.Equal(System.Text.Encoding.UTF8.GetBytes(edited ? correction : original), outbound.GetBody<byte[]>());
                else Assert.Equal(edited ? correction : original, outbound.GetBody<string>());
                Assert.Equal("route", outbound.SessionId);
                Assert.Equal("route", outbound.PartitionKey);
                if (newId) Assert.NotEqual("source-id", outbound.MessageId);
                else Assert.Equal("source-id", outbound.MessageId);
                using var originalCopy = source.Clone();
                if (bytes) Assert.Equal(System.Text.Encoding.UTF8.GetBytes(original), originalCopy.GetBody<byte[]>());
                else Assert.Equal(original, originalCopy.GetBody<string>());
            });
        }

        [Fact]
        public void EditedTextUsesUtf8AndRetainsOriginalMessageId()
        {
            RunSta(() =>
            {
                using var source = new BrokeredMessage(new MemoryStream(new byte[] { 255, 0 })) { MessageId = "original" };
                using var editor = new MessageForm(source, new ServiceBusHelper((_, __) => { }), (_, __) => { });
                ((CheckBox)editor.Controls.Find("chkNewMessageId", true).Single()).Checked = false;
                editor.Controls.Find("txtMessageText", true).Single().Text = "edited å";
                using var outbound = editor.BuildMessageForResubmission(source);
                var modern = ServiceBusMessageAdapter.ToServiceBusMessage(outbound);
                Assert.Equal(System.Text.Encoding.UTF8.GetBytes("edited å"), modern.Body.ToArray());
                Assert.Equal("original", modern.MessageId);
            });
        }

        [Fact]
        public void EditorShowsNativeTextAndPreparesTheCorrectedValueForDelivery()
        {
            RunSta(() =>
            {
                using var source = ReceivedMessageReplayTests.Receive(new AmqpAnnotatedMessage(AmqpMessageBody.FromValue("original")));
                using var editor = new MessageForm(source, new ServiceBusHelper((_, __) => { }), (_, __) => { });
                var text = editor.Controls.Find("txtMessageText", true).Single();
                Assert.Equal("original", text.Text);
                text.Text = "corrected";
                using var prepared = editor.BuildMessageForResubmission(source);
                var wire = AmqpAnnotatedMessage.FromBytes(ServiceBusMessageAdapter.ToServiceBusMessage(prepared).GetRawAmqpMessage().ToBytes());
                Assert.True(wire.Body.TryGetValue(out var value));
                Assert.Equal("corrected", value);
            });
        }

        [Fact]
        public void BulkReplayRemovesReservedPropertiesAndKeepsCustomProperties()
        {
            RunSta(() =>
            {
                var wire = new AmqpAnnotatedMessage(AmqpMessageBody.FromValue(new byte[] { 1, 2 }));
                wire.ApplicationProperties["DeadLetterReason"] = "old reason";
                wire.ApplicationProperties["deadlettererrordescription"] = "old detail";
                wire.ApplicationProperties["NServiceBus.Transport.Recovery"] = "old recovery";
                wire.ApplicationProperties["keep"] = 42;
                using var source = ReceivedMessageReplayTests.Receive(wire);
                using var editor = new MessageForm(new[] { source }, new ServiceBusHelper((_, __) => { }), (_, __) => { });
                using var prepared = editor.BuildMessageForResubmission(source);
                var properties = ServiceBusMessageAdapter.ToServiceBusMessage(prepared).ApplicationProperties;
                Assert.Single(properties);
                Assert.Equal(42, properties["keep"]);
                Assert.Equal(4, source.Properties.Count);
            });
        }

        [Theory]
        [InlineData("Queue", false)]
        [InlineData("Topic", false)]
        [InlineData("Queue", true)]
        [InlineData("Topic", true)]
        public async Task DestructiveReplayChecksTheActualDestinationUnlessGeneratingANewId(string kind, bool duplicateDetection)
        {
            var probe = new DuplicateDetectionProbe(duplicateDetection);
            if (duplicateDetection)
                await Assert.ThrowsAsync<InvalidOperationException>(() => MessageForm.ValidateMoveDestinationAsync(false, "orders", kind, () => probe));
            else
                await MessageForm.ValidateMoveDestinationAsync(false, "orders", kind, () => probe);
            Assert.Equal("orders", probe.Queried);
            await MessageForm.ValidateMoveDestinationAsync(true, "orders", kind, () => throw new Exception("unexpected lookup"));
            await Assert.ThrowsAsync<InvalidOperationException>(() => MessageForm.ValidateMoveDestinationAsync(false, "orders", null, () => probe));
            await Assert.ThrowsAsync<InvalidOperationException>(() => MessageForm.ValidateMoveDestinationAsync(false, "orders", kind, () => throw new UnauthorizedAccessException()));
        }

        private sealed class DuplicateDetectionProbe : ServiceBusAdministrationClient
        {
            private readonly bool duplicate;
            public string Queried { get; private set; }

            internal DuplicateDetectionProbe(bool duplicate) { this.duplicate = duplicate; }

            public override Task<Response<QueueProperties>> GetQueueAsync(string name, CancellationToken token = default)
            {
                Queried = name;
                var properties = ServiceBusModelFactory.QueueProperties(name, requiresDuplicateDetection: duplicate,
                    lockDuration: TimeSpan.FromMinutes(1), maxSizeInMegabytes: 1024, maxDeliveryCount: 10, userMetadata: "",
                    defaultMessageTimeToLive: TimeSpan.MaxValue, autoDeleteOnIdle: TimeSpan.MaxValue,
                    duplicateDetectionHistoryTimeWindow: TimeSpan.FromMinutes(10));
                return Task.FromResult(Response.FromValue(properties, null));
            }

            public override Task<Response<TopicProperties>> GetTopicAsync(string name, CancellationToken token = default)
            {
                Queried = name;
                var properties = ServiceBusModelFactory.TopicProperties(name, requiresDuplicateDetection: duplicate,
                    maxSizeInMegabytes: 1024, defaultMessageTimeToLive: TimeSpan.MaxValue,
                    autoDeleteOnIdle: TimeSpan.MaxValue, duplicateDetectionHistoryTimeWindow: TimeSpan.FromMinutes(10));
                return Task.FromResult(Response.FromValue(properties, null));
            }
        }

        [Theory]
        [InlineData("accepted", "send,complete")]
        [InlineData("rejected", "send,abandon")]
        [InlineData("cancelled", "send,abandon")]
        [InlineData("locklost", "send,complete,abandon")]
        public async Task SettlementFollowsConfirmedDelivery(string result, string expected)
        {
            var calls = new System.Collections.Generic.List<string>();
            Func<Task> send = () =>
            {
                calls.Add("send");
                if (result == "cancelled") return Task.FromCanceled(new CancellationToken(true));
                if (result == "rejected") return Task.FromException(new InvalidOperationException("rejected"));
                return Task.CompletedTask;
            };
            Func<Task> complete = () =>
            {
                calls.Add("complete");
                return result == "locklost" ? Task.FromException(new InvalidOperationException("expired")) : Task.CompletedTask;
            };
            Func<Task> abandon = () => { calls.Add("abandon"); return Task.CompletedTask; };
            var delivery = DeadLetterMessageHandler.SendAndSettleAsync(send, complete, abandon);
            if (result == "accepted") await delivery;
            else await Assert.ThrowsAnyAsync<Exception>(() => delivery);
            Assert.Equal(expected, string.Join(",", calls));
        }

        private static void RunSta(Action action)
        {
            Exception failure = null;
            var thread = new Thread(() => { try { action(); } catch (Exception ex) { failure = ex; } }) { IsBackground = true };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            Assert.True(thread.Join(TimeSpan.FromSeconds(30)), "Message editor did not finish.");
            if (failure != null) ExceptionDispatchInfo.Capture(failure).Throw();
        }
    }
}
