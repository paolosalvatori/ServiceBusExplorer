using System;
using FluentAssertions;
using Microsoft.Azure.NotificationHubs;
using ServiceBusExplorer.Controls;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class RegistrationBodyTemplateTests
    {
        [Fact]
        public void FcmV1BodyEditUpdatesRegistrationAndReportsModification()
        {
            var registration = new FcmV1TemplateRegistrationDescription("token", "{\"old\":true}");
            HandleNotificationHubControl.UpdateRegistrationBodyTemplate(registration, "{\"new\":true}").Should().BeTrue();
            registration.BodyTemplate.ToString().Should().Be("{\"new\":true}");
        }

        [Fact]
        public void ExistingTemplateTypesStillSupportBodyEdits()
        {
            var mpns = new MpnsTemplateRegistrationDescription(new Uri("https://example.com"), "old");
            var windows = new WindowsTemplateRegistrationDescription("https://example.com", "old");
            HandleNotificationHubControl.UpdateRegistrationBodyTemplate(mpns, "new").Should().BeTrue();
            HandleNotificationHubControl.UpdateRegistrationBodyTemplate(windows, "new").Should().BeTrue();
            mpns.BodyTemplate.ToString().Should().Be("new");
            windows.BodyTemplate.ToString().Should().Be("new");
        }

        [Fact]
        public void NativeRegistrationDoesNotReportTemplateModification()
        {
            HandleNotificationHubControl.UpdateRegistrationBodyTemplate(
                new FcmV1RegistrationDescription("token"), "new").Should().BeFalse();
        }
    }
}
