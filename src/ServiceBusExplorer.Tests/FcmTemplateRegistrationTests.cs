using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Azure.NotificationHubs;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.UIHelpers;
using Xunit;

namespace ServiceBusExplorer.Tests
{
    public class FcmTemplateRegistrationTests
    {
        [Theory]
        [InlineData("updated")]
        [InlineData("")]
        [InlineData(null)]
        public void TemplateNameEdit_PersistsIncludingClearing(string name)
        {
            var registration = new FcmV1TemplateRegistrationDescription("token", "{}") { TemplateName = "old" };

            HandleNotificationHubControl.UpdateTemplateProperty(registration, "TemplateName", name).Should().BeTrue();

            registration.TemplateName.Should().Be(name);
            registration.FcmV1RegistrationId.Should().Be("token");
            registration.BodyTemplate.Value.Should().Be("{}");
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void BodyEdit_PersistsForBothFcmVersions(bool useV1)
        {
            RegistrationDescription registration = useV1
                ? (RegistrationDescription)new FcmV1TemplateRegistrationDescription("token", "{}")
                : new FcmTemplateRegistrationDescription("token", "{}");
            const string body = "{\"data\":{\"message\":\"$(message)\"}}";

            HandleNotificationHubControl.UpdateTemplateProperty(registration, "BodyTemplate", body).Should().BeTrue();

            var actual = useV1 ? ((FcmV1TemplateRegistrationDescription)registration).BodyTemplate.Value
                : ((FcmTemplateRegistrationDescription)registration).BodyTemplate.Value;
            actual.Should().Be(body);
        }

        [Fact]
        public void UnsupportedEdits_DoNotMarkRegistrationChanged()
        {
            var template = new FcmV1TemplateRegistrationDescription("token", "{}");
            HandleNotificationHubControl.UpdateTemplateProperty(template, "BodyTemplate", 42).Should().BeFalse();
            HandleNotificationHubControl.UpdateTemplateProperty(template, "TemplateName", 42).Should().BeFalse();
            HandleNotificationHubControl.UpdateTemplateProperty(template, "Unknown", "value").Should().BeFalse();
            HandleNotificationHubControl.UpdateTemplateProperty(new FcmV1RegistrationDescription("token"), "TemplateName", "name")
                .Should().BeFalse();
            template.BodyTemplate.Value.Should().Be("{}");
        }

        [Theory]
        [InlineData(false, "{}")]
        [InlineData(true, "{}")]
        [InlineData(false, null)]
        public void CreateTemplate_CopiesNameBodyTokenAndTagsBeforeSending(bool withTags, string body)
        {
            var properties = new CustomObject
            {
                Properties = new List<CustomProperty>
                {
                    new CustomProperty { Name = "FcmV1RegistrationId", Type = typeof(string) },
                    new CustomProperty { Name = "BodyTemplate", Type = typeof(string) },
                    new CustomProperty { Name = "TemplateName", Type = typeof(string) }
                }
            };
            properties["FcmV1RegistrationId"] = "token";
            properties["BodyTemplate"] = body;
            properties["TemplateName"] = "orders";
            var tags = withTags ? new HashSet<string> { "customer:42" } : null;

            var registration = HandleNotificationHubControl.CreateFcmV1TemplateRegistration(properties, tags);

            registration.TemplateName.Should().Be("orders");
            registration.FcmV1RegistrationId.Should().Be("token");
            if (body != null)
            {
                registration.BodyTemplate.Value.Should().Be(body);
            }
            if (withTags)
            {
                registration.Tags.Should().BeEquivalentTo(tags);
            }
            else
            {
                registration.Tags.Should().BeNullOrEmpty();
            }
        }
    }
}
