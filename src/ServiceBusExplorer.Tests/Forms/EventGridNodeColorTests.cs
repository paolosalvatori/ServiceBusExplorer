using System;
using Azure.ResourceManager.EventGrid;
using EventGridExplorerLibrary;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    public class EventGridNodeColorTests
    {
        public enum TagShape
        {
            UntaggedGroup,
            EventGridNamespace,
            EventGridTopicsFolder,
            EventGridTopic,
            EventGridSubscriptionsFolder,
            EventGridSubscription,
            ServiceBusSubscriptionsFolder,
        }

        [Theory]
        [InlineData(TagShape.UntaggedGroup, false)]
        [InlineData(TagShape.EventGridNamespace, false)]
        [InlineData(TagShape.EventGridTopicsFolder, false)]
        [InlineData(TagShape.EventGridTopic, true)]
        [InlineData(TagShape.EventGridSubscriptionsFolder, true)]
        [InlineData(TagShape.EventGridSubscription, true)]
        [InlineData(TagShape.ServiceBusSubscriptionsFolder, true)]
        public void KnownTagsKeepTheirColorRuleCategory(TagShape shape, bool isLeaf)
        {
            object tag = shape switch
            {
                TagShape.UntaggedGroup => null,
                TagShape.EventGridNamespace => new TestNamespace(),
                TagShape.EventGridTopicsFolder => new NamespaceTopic(),
                TagShape.EventGridTopic => new TestTopic(),
                TagShape.EventGridSubscriptionsFolder => new EventGridSubscriptionWrapper(null, new TestTopic(), null),
                TagShape.EventGridSubscription => new EventGridSubscriptionWrapper(new TestSubscription(), new TestTopic()),
                TagShape.ServiceBusSubscriptionsFolder => new SubscriptionWrapper(null, null, null),
                _ => throw new ArgumentOutOfRangeException(nameof(shape))
            };

            var result = MainForm.GetColorDataFromNodeTag(tag);

            Assert.Equal<bool?>(isLeaf, result.IsLeaf);
            Assert.Null(result.MessageCountDetails);
        }

        [Fact]
        public void UnknownTagHasNoColorRuleCategory()
        {
            var result = MainForm.GetColorDataFromNodeTag(new object());

            Assert.Null(result.IsLeaf);
            Assert.Null(result.MessageCountDetails);
        }

        private sealed class TestNamespace : EventGridNamespaceResource { }
        private sealed class TestTopic : NamespaceTopicResource { }
        private sealed class TestSubscription : NamespaceTopicEventSubscriptionResource { }
    }
}
