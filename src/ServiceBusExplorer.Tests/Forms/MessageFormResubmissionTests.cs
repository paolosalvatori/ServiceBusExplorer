using System;
using System.IO;
using System.Linq;
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
        public void ReceivedMessageEditorRetainsBrokerIdentityForSelectedActions()
        {
            RunSta(() =>
            {
                var received = Azure.Messaging.ServiceBus.ServiceBusModelFactory.ServiceBusReceivedMessage(
                    body: new BinaryData(new byte[] { 0, 255 }), messageId: "original", sequenceNumber: 123,
                    properties: new System.Collections.Generic.Dictionary<string, object>
                    { ["ServiceBusExplorer.SystemProperties.SequenceNumber"] = "user value" });
                using var source = ServiceBusReceivedMessageAdapter.ToBrokeredMessage(received);
                using var editor = new MessageForm(source, new ServiceBusHelper((_, __) => { }), (_, __) => { });
                ((CheckBox)editor.Controls.Find("chkNewMessageId", true).Single()).Checked = false;
                using var outbound = editor.BuildMessageForResubmission(source);
                Assert.Equal(123, source.SequenceNumber);
                Assert.Equal("user value", source.Properties["ServiceBusExplorer.SystemProperties.SequenceNumber"]);
                Assert.Equal(new byte[] { 0, 255 }, ServiceBusMessageAdapter.ToServiceBusMessage(outbound).Body.ToArray());
                Assert.Equal("original", outbound.MessageId);
            });
        }

        [Theory]
        [InlineData("Value", false)]
        [InlineData("Value", true)]
        [InlineData("Sequence", false)]
        [InlineData("Sequence", true)]
        public void StructuredAmqpEditorPreservesOriginalUntilTextEdited(string kind, bool newId)
        {
            RunSta(() =>
            {
                var received = Helpers.ServiceBusAmqpBodyTests.Message(kind);
                using var source = ServiceBusReceivedMessageAdapter.ToBrokeredMessage(received);
                using var editor = new MessageForm(source, new ServiceBusHelper((_, __) => { }), (_, __) => { });
                ((CheckBox)editor.Controls.Find("chkNewMessageId", true).Single()).Checked = newId;
                using var unchanged = editor.BuildMessageForResubmission(source);
                var outbound = ServiceBusMessageAdapter.ToServiceBusMessage(unchanged);
                Helpers.ServiceBusAmqpBodyTests.AssertBody(received.GetRawAmqpMessage().Body, outbound.GetRawAmqpMessage().Body);
                Assert.Equal(!newId, outbound.MessageId == kind);
                editor.Controls.Find("txtMessageText", true).Single().Text = "edited text";
                using var edited = editor.BuildMessageForResubmission(source);
                var data = ServiceBusMessageAdapter.ToServiceBusMessage(edited);
                Assert.Equal(Azure.Core.Amqp.AmqpMessageBodyType.Data, data.GetRawAmqpMessage().Body.BodyType);
                Assert.Equal("edited text", data.Body.ToString());
                Helpers.ServiceBusAmqpBodyTests.AssertBody(received.GetRawAmqpMessage().Body,
                    ServiceBusMessageAdapter.ToServiceBusMessage(source).GetRawAmqpMessage().Body);
            });
        }

        [Fact]
        public void QueueTestEntryRejectsTransactionalReceiveAndDeleteBeforeStarting()
        {
            RunSta(() =>
            {
                var main = (MainForm)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(MainForm));
                var logs = new System.Collections.Generic.List<string>();
                using var control = new ServiceBusExplorer.Controls.TestQueueControl(main, (message, _) => logs.Add(message),
                    () => System.Threading.Tasks.Task.CompletedTask, () => { }, new ServiceBusHelper((_, __) => { }), new QueueInfo("synthetic"));
                ((CheckBox)control.Controls.Find("receiverEnabledCheckBox", true).Single()).Checked = true;
                ((CheckBox)control.Controls.Find("checkBoxReceiverUseTransaction", true).Single()).Checked = true;
                ((ComboBox)control.Controls.Find("cboReceivedMode", true).Single()).SelectedItem = "ReceiveDelete";
                var valid = (bool)typeof(ServiceBusExplorer.Controls.TestQueueControl).GetMethod("ValidateParameters",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(control, null);
                Assert.False(valid);
                Assert.Contains(logs, message => message.Contains("cannot be rolled back"));
            });
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
