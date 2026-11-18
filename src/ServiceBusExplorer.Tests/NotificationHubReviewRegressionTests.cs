using System;
using FluentAssertions;
using Microsoft.Azure.NotificationHubs;
using ServiceBusExplorer.Controls;
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
            HandleNotificationHubControl.ApplyFcmCredentials(hub, "", null, false, false);
            hub.FcmCredential.Should().BeSameAs(legacy);
            hub.FcmV1Credential.Should().BeSameAs(v1);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Removal_AffectsOnlySelectedCredential(bool removeV1)
        {
            var hub = DualCredentials();
            HandleNotificationHubControl.ApplyFcmCredentials(hub, null, null, !removeV1, removeV1);
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
            HandleNotificationHubControl.ApplyFcmCredentials(hub, "new legacy", replacement, false, false);
            hub.FcmCredential.GoogleApiKey.Should().Be("new legacy");
            hub.FcmCredential.FcmEndpoint.Should().Be("https://example.test/legacy");
            hub.FcmV1Credential.Should().BeSameAs(v1);
            hub.FcmV1Credential.FcmV1Endpoint.Should().Be("https://fcm.googleapis.com/v1/projects/new project/messages:send");
            hub.FcmV1Credential.PrivateKey.Should().Be("new private");
            hub.FcmV1Credential.ProjectId.Should().Be("new project");
            hub.FcmV1Credential.ClientEmail.Should().Be("new email");
        }

    }
}
