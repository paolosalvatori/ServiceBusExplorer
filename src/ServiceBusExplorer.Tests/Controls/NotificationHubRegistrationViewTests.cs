using System;
using System.Collections.Generic;
using Microsoft.Azure.NotificationHubs;
using ServiceBusExplorer.Controls;
using Xunit;

namespace ServiceBusExplorer.Tests.Controls
{
    public class NotificationHubRegistrationViewTests
    {
        [Fact]
        public void EmptyServerPageProjectsNoRowsWithOrWithoutFilter()
        {
            var pages = Array.Empty<IEnumerable<RegistrationDescription>>();

            Assert.Empty(HandleNotificationHubControl.SelectRegistrations(pages, -1, null));
            Assert.Empty(HandleNotificationHubControl.SelectRegistrations(pages, -1, _ => true));
        }

        [Fact]
        public void RefreshedPagesFollowServerOrderAndClearingFilterShowsCurrentPage()
        {
            var existing = new GcmRegistrationDescription("existing");
            var created = new GcmRegistrationDescription("created");
            var pages = new IEnumerable<RegistrationDescription>[]
            {
                new[] { existing },
                new[] { created },
            };

            Assert.Same(existing, Assert.Single(HandleNotificationHubControl.SelectRegistrations(pages, 0, null)));
            Assert.Same(created, Assert.Single(HandleNotificationHubControl.SelectRegistrations(pages, 1, null)));
        }

        [Fact]
        public void FilterUsesAllLoadedServerPagesWithoutShowingNonmatchingRows()
        {
            var nonmatching = new GcmRegistrationDescription("nonmatching");
            var matching = new GcmRegistrationDescription("matching", new[] { "selected-tag" });
            var pages = new IEnumerable<RegistrationDescription>[]
            {
                new[] { nonmatching },
                new[] { matching },
            };

            Assert.Same(matching, Assert.Single(HandleNotificationHubControl.SelectRegistrations(
                pages, 0, registration => registration.Tags?.Contains("selected-tag") == true)));
        }
    }
}
