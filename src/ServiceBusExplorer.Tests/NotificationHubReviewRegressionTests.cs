using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.NotificationHubs.Messaging;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.NotificationHubs.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests
{
    public class NotificationHubReviewRegressionTests
    {
        [Theory]
        [InlineData(typeof(AppleTemplateRegistrationDescription))]
        [InlineData(typeof(WindowsTemplateRegistrationDescription))]
        [InlineData(typeof(MpnsTemplateRegistrationDescription))]
        [InlineData(typeof(FcmTemplateRegistrationDescription))]
        [InlineData(typeof(FcmV1TemplateRegistrationDescription))]
        public void TemplateEdits_PersistForEverySupportedPlatform(Type type)
        {
            var registration = (RegistrationDescription)Activator.CreateInstance(type,
                type == typeof(AppleTemplateRegistrationDescription) ? new string('a', 64) : "https://example.test/channel", "{}");
            foreach (var name in new[] { "orders", "", null })
            {
                HandleNotificationHubControl.UpdateTemplateProperty(registration, "TemplateName", name).Should().BeTrue();
                type.GetProperty("TemplateName").GetValue(registration).Should().Be(name);
            }
            HandleNotificationHubControl.UpdateTemplateProperty(registration, "BodyTemplate", "new body").Should().BeTrue();
            ((CDataMember)type.GetProperty("BodyTemplate").GetValue(registration)).Value.Should().Be("new body");
        }

        private static NotificationHubDescription DualCredentials()
        {
            return new NotificationHubDescription("hub")
            {
                FcmCredential = new FcmCredential("old legacy") { FcmEndpoint = "https://example.test/legacy" },
                FcmV1Credential = new FcmV1Credential("old private", "old project", "old email")
            };
        }

        [Fact]
        public void UneditedCredentials_AreRetained()
        {
            var hub = DualCredentials();
            var legacy = hub.FcmCredential;
            var v1 = hub.FcmV1Credential;
            HandleNotificationHubControl.ApplyFcmCredentials(hub, null, false, false);
            hub.FcmCredential.Should().BeSameAs(legacy);
            hub.FcmV1Credential.Should().BeSameAs(v1);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Removal_AffectsOnlySelectedCredential(bool removeV1)
        {
            var hub = DualCredentials();
            HandleNotificationHubControl.ApplyFcmCredentials(hub, null, !removeV1, removeV1);
            if (removeV1)
            {
                hub.FcmV1Credential.Should().BeNull();
                hub.FcmCredential.GoogleApiKey.Should().Be("old legacy");
            }
            else
            {
                hub.FcmCredential.Should().BeNull();
                hub.FcmV1Credential.PrivateKey.Should().Be("old private");
            }
        }

        [Fact]
        public void Replacement_PreservesLegacyEndpointAndUpdatesV1ProjectEndpoint()
        {
            var hub = DualCredentials();
            // SDK 4.2 derives the read-only v1 endpoint from ProjectId.
            var v1 = hub.FcmV1Credential;
            var replacement = new FcmV1Credential("new private", "new project", "new email");
            HandleNotificationHubControl.ApplyFcmCredentials(hub, replacement, false, false);
            hub.FcmCredential.GoogleApiKey.Should().Be("old legacy");
            hub.FcmCredential.FcmEndpoint.Should().Be("https://example.test/legacy");
            hub.FcmV1Credential.Should().BeSameAs(v1);
            hub.FcmV1Credential.FcmV1Endpoint.Should().Be("https://fcm.googleapis.com/v1/projects/new project/messages:send");
            hub.FcmV1Credential.PrivateKey.Should().Be("new private");
            hub.FcmV1Credential.ProjectId.Should().Be("new project");
            hub.FcmV1Credential.ClientEmail.Should().Be("new email");
        }

        [Fact]
        public void UpdateDraft_DoesNotMutateConfirmedCredentialsOrAuthorizationRules()
        {
            var confirmed = DualCredentials();
            confirmed.IsDisabled = true;
            var originalRule = new SharedAccessAuthorizationRule("original", TestKey(), new[] { AccessRights.Listen });
            confirmed.Authorization.Add(originalRule);
            var editable = HandleNotificationHubControl.CopyNotificationHubDescription(confirmed);
            var ruleEdit = new NotificationHubAuthorizationRuleWrapper(editable.Authorization.Single()) { KeyName = "new name" };
            var draft = HandleNotificationHubControl.CopyNotificationHubDescription(confirmed);
            HandleNotificationHubControl.ApplyFcmCredentials(draft,
                new FcmV1Credential("new test private", "new-test-project", "new@example.test"), true, false);
            HandleNotificationHubControl.ReplaceAuthorizationRules(draft, new[] { ruleEdit }, true,
                _ => throw new InvalidOperationException(), () => throw new InvalidOperationException());

            draft.Path.Should().Be(confirmed.Path);
            draft.IsDisabled.Should().BeTrue();
            draft.FcmCredential.Should().BeNull();
            draft.FcmV1Credential.ProjectId.Should().Be("new-test-project");
            draft.Authorization.Should().ContainSingle().Which.KeyName.Should().Be("new name");
            confirmed.FcmCredential.FcmEndpoint.Should().Be("https://example.test/legacy");
            confirmed.FcmV1Credential.ProjectId.Should().Be("old project");
            confirmed.Authorization.Should().ContainSingle().Which.KeyName.Should().Be("original");
        }

        [Fact]
        public void RulesOnNonCloudNamespace_SkipNewRowAndPreserveExistingRuleAndCredentialEdit()
        {
            var hub = DualCredentials();
            var existing = new SharedAccessAuthorizationRule("existing", TestKey(), new[] { AccessRights.Listen });
            var rules = new[]
            {
                new NotificationHubAuthorizationRuleWrapper(existing),
                new NotificationHubAuthorizationRuleWrapper { KeyName = "unsupported", Listen = true }
            };
            var skipped = 0;
            HandleNotificationHubControl.ApplyFcmCredentials(hub, null, false, true);
            HandleNotificationHubControl.ReplaceAuthorizationRules(hub, rules, false,
                _ => throw new InvalidOperationException(), () => skipped++);

            skipped.Should().Be(1);
            hub.Authorization.Should().ContainSingle().Which.Should().BeSameAs(existing);
            hub.FcmV1Credential.Should().BeNull();
        }

        [Fact]
        public void RulesOnCloudNamespace_ApplyNewRowsAndHonorDeletedRows()
        {
            var hub = DualCredentials();
            hub.Authorization.Add(new SharedAccessAuthorizationRule("deleted", TestKey(), new[] { AccessRights.Send }));
            var rules = new[] { new NotificationHubAuthorizationRuleWrapper { KeyName = "new", PrimaryKey = TestKey(), Listen = true } };
            HandleNotificationHubControl.ReplaceAuthorizationRules(hub, rules, true,
                _ => throw new InvalidOperationException(), () => throw new InvalidOperationException());

            var rule = hub.Authorization.Should().ContainSingle().Which;
            rule.KeyName.Should().Be("new");
            rule.Rights.Should().Equal(AccessRights.Listen);
        }

        [Fact]
        public void TemplateEdits_AcceptBaiduAndRejectOtherPropertiesAndWrongTypes()
        {
            var registration = new BaiduTemplateRegistrationDescription("user", "channel", "{}");
            HandleNotificationHubControl.UpdateTemplateProperty(registration, "TemplateName", "orders").Should().BeTrue();
            HandleNotificationHubControl.UpdateTemplateProperty(registration, "BodyTemplate", """{"valid":true}""").Should().BeTrue();
            HandleNotificationHubControl.UpdateTemplateProperty(registration, "TemplateName", 1).Should().BeFalse();
            HandleNotificationHubControl.UpdateTemplateProperty(registration, "BaiduUserId", "changed").Should().BeFalse();
            registration.TemplateName.Should().Be("orders");
            registration.BodyTemplate.Value.Should().Be("""{"valid":true}""");
        }

        [Fact]
        public void CredentialImport_IdentifiesMissingFieldWithoutEchoingInput()
        {
            var exception = Assert.Throws<HandleNotificationHubControl.FcmServiceAccountImportException>(() =>
                HandleNotificationHubControl.ParseFcmV1Credential("""{"private_key":"test-only-input","project_id":"test-project"}"""));

            exception.Message.Should().Contain("client_email").And.NotContain("test-only-input");
        }

        [Fact]
        public void CredentialImport_ReportsInvalidJsonWithoutEchoingInput()
        {
            var exception = Assert.Throws<HandleNotificationHubControl.FcmServiceAccountImportException>(() =>
                HandleNotificationHubControl.ParseFcmV1Credential("""{"private_key":test-only-input}"""));

            exception.Message.Should().Contain("valid JSON object").And.NotContain("test-only-input");
        }

        [Fact]
        public void CredentialImport_ReportsMissingFileWithoutEchoingPath()
        {
            var path = Path.Combine(Path.GetTempPath(), $"test-only-{Guid.NewGuid():N}.json");
            var exception = Assert.Throws<HandleNotificationHubControl.FcmServiceAccountImportException>(() =>
                HandleNotificationHubControl.ReadFcmV1CredentialFile(path));

            exception.Message.Should().Contain("not found").And.NotContain(path);
        }

        [Fact]
        public void CredentialImport_ParsesServiceAccountWithMultilineTestKey()
        {
            var credential = HandleNotificationHubControl.ParseFcmV1Credential(
                """{"private_key":"test line 1\nline 2","project_id":"test-project","client_email":"test@example.test"}""");

            credential.PrivateKey.Should().Be("test line 1\nline 2");
            credential.ProjectId.Should().Be("test-project");
            credential.ClientEmail.Should().Be("test@example.test");
        }

        private static string TestKey() => Convert.ToBase64String(new byte[32]);
    }
}
