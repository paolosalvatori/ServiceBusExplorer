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
