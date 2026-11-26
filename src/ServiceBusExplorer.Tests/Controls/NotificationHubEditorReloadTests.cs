using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Azure.NotificationHubs;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.UIHelpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Controls
{
    public class NotificationHubEditorReloadTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void RefreshDataDiscardsUnsavedEditsWhenConfirmedValuesAreAbsent(string metadata)
        {
            RunSta(() =>
            {
                var confirmed = new NotificationHubDescription("hub") { UserMetadata = metadata };
                var logs = new List<string>();
                using var editor = Create(confirmed, logs);
                Edit(editor);

                editor.RefreshData(confirmed);

                AssertCleared(editor, confirmed);
                Assert.Empty(logs);
                Assert.False(Field<NotificationHubSettingsRecovery>(editor, "settingsRecovery").NeedsRefresh);
            });
        }

        [Fact]
        public void RefreshDataRestoresSameConfirmedTtlAfterTextEditAndKeepsUnauthenticatedMpns()
        {
            RunSta(() =>
            {
                var confirmed = new NotificationHubDescription("hub")
                {
                    UserMetadata = "confirmed", RegistrationTtl = TimeSpan.FromDays(2), MpnsCredential = new MpnsCredential()
                };
                var logs = new List<string>();
                using var editor = Create(confirmed, logs);
                Edit(editor);
                Field<CheckBox>(editor, "checkBoxEnableUnauthenticatedMpns").Checked = false;

                editor.RefreshData(confirmed);

                Assert.Equal("confirmed", Field<TextBox>(editor, "txtUserMetadata").Text);
                Assert.Equal(TimeSpan.FromDays(2), Field<TimeSpanControl>(editor, "tsRegistrationTimeToLive").TimeSpanValue);
                Assert.True(Field<CheckBox>(editor, "checkBoxEnableUnauthenticatedMpns").Checked);
                Assert.Empty(logs);
            });
        }

        [Fact]
        public void RefreshDataRemovesPreviouslyConfirmedCredentialsAndTtl()
        {
            RunSta(() =>
            {
                var old = new NotificationHubDescription("hub")
                {
                    UserMetadata = "old", RegistrationTtl = TimeSpan.FromDays(2),
                    MpnsCredential = new MpnsCredential(),
                    ApnsCredential = new ApnsCredential { Endpoint = "https://api.push.apple.com" },
                    WnsCredential = new WnsCredential("old package", "old secret")
                };
                var logs = new List<string>();
                using var editor = Create(old, logs);
                var confirmed = new NotificationHubDescription("hub");
                editor.RefreshData(confirmed);
                AssertCleared(editor, confirmed);
                Assert.Empty(logs);
            });
        }

        [Fact]
        public void FailedUpdateRestoresConfirmedEditorAndRefreshClearsRecoveryGuard()
        {
            RunSta(() =>
            {
                var confirmed = new NotificationHubDescription("hub");
                var logs = new List<string>();
                // No connection or management adapter: Update and reload fail locally, without Azure traffic.
                using var editor = Create(confirmed, logs);
                Edit(editor);
                typeof(HandleNotificationHubControl).GetMethod("btnCancelUpdate_Click", BindingFlags.Instance | BindingFlags.NonPublic)
                    .Invoke(editor, new object[] { Field<Button>(editor, "btnCancelUpdate"), EventArgs.Empty });

                Assert.Contains(logs, message => message.Contains("Refresh before retrying"));
                AssertCleared(editor, confirmed);
                Assert.True(Field<NotificationHubSettingsRecovery>(editor, "settingsRecovery").NeedsRefresh);
                logs.Clear();
                editor.RefreshData(confirmed);
                AssertCleared(editor, confirmed);
                Assert.False(Field<NotificationHubSettingsRecovery>(editor, "settingsRecovery").NeedsRefresh);
                Assert.Empty(logs);
            });
        }

        [Fact]
        public void AssigningNullTtlAgainDiscardsTypedText()
        {
            RunSta(() =>
            {
                using var ttl = new TimeSpanControl { TimeSpanValue = null };
                ttl.Controls.Find("txtDays", true).Single().Text = "9";
                ttl.TimeSpanValue = null;
                Assert.Null(ttl.TimeSpanValue);
                Assert.False(ttl.IsFilled);
            });
        }

        private static HandleNotificationHubControl Create(NotificationHubDescription description, List<string> logs) =>
            new HandleNotificationHubControl((message, async) => logs.Add(message),
                new ServiceBusHelper((message, async) => logs.Add(message)), description);

        private static void Edit(HandleNotificationHubControl editor)
        {
            Field<TextBox>(editor, "txtUserMetadata").Text = "unsaved";
            // Simulate typing, rather than assigning TimeSpanValue and updating its former cache.
            var ttl = Field<TimeSpanControl>(editor, "tsRegistrationTimeToLive");
            ttl.Controls.Find("txtDays", true).Single().Text = "9";
            Field<CheckBox>(editor, "checkBoxEnableUnauthenticatedMpns").Checked = true;
            Field<TextBox>(editor, "txtApnsEndpoint").Text = "https://unsaved.invalid";
            Field<TextBox>(editor, "txtApnsCredentialCertificateThumbprint").Text = "unsaved";
            Field<TextBox>(editor, "txtMpnsCredentialCertificateThumbprint").Text = "unsaved";
            Field<TextBox>(editor, "txtPackageSid").Text = "unsaved";
            Field<TextBox>(editor, "txtClientSecret").Text = "unsaved";
        }

        private static void AssertCleared(HandleNotificationHubControl editor, NotificationHubDescription confirmed)
        {
            Assert.Equal(confirmed.UserMetadata ?? "", Field<TextBox>(editor, "txtUserMetadata").Text);
            var ttl = Field<TimeSpanControl>(editor, "tsRegistrationTimeToLive");
            // The SDK maps an omitted TTL to MaxValue; the editor displays millisecond precision.
            var expected = confirmed.RegistrationTtl.Value;
            Assert.Equal(new TimeSpan(expected.Days, expected.Hours, expected.Minutes, expected.Seconds, expected.Milliseconds), ttl.TimeSpanValue);
            Assert.False(Field<CheckBox>(editor, "checkBoxEnableUnauthenticatedMpns").Checked);
            foreach (var name in new[] { "txtApnsEndpoint", "txtApnsCredentialCertificateThumbprint",
                "txtMpnsCredentialCertificateThumbprint", "txtPackageSid", "txtClientSecret" })
                Assert.Equal("", Field<TextBox>(editor, name).Text);
        }

        private static T Field<T>(object instance, string name) =>
            (T)instance.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);

        private static void RunSta(Action action)
        {
            Exception failure = null;
            var thread = new Thread(() => { try { action(); } catch (Exception ex) { failure = ex; } }) { IsBackground = true };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            Assert.True(thread.Join(TimeSpan.FromSeconds(30)), "Editor test did not finish.");
            if (failure != null) ExceptionDispatchInfo.Capture(failure).Throw();
        }
    }
}
