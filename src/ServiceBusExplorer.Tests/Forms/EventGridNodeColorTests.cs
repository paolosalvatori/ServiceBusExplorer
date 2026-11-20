using System.Runtime.Serialization;
using Azure.ResourceManager.EventGrid;
using EventGridExplorerLibrary;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    public class EventGridNodeColorTests
    {
        [Fact]
        public void EventGridTreeTagsUseEntityOrGroupColorsWithoutMessageCounts()
        {
            var eventGridNamespace = Uninitialized<EventGridNamespaceResource>();
            var topic = Uninitialized<NamespaceTopicResource>();
            var subscription = Uninitialized<NamespaceTopicEventSubscriptionResource>();

            AssertColorData(eventGridNamespace, false);
            AssertColorData(new NamespaceTopic(), false);
            AssertColorData(topic, true);
            AssertColorData(new EventGridSubscriptionWrapper(null, topic, null), false);
            AssertColorData(new EventGridSubscriptionWrapper(subscription, topic), true);
        }

        private static T Uninitialized<T>()
        {
            return (T)FormatterServices.GetUninitializedObject(typeof(T));
        }

        private static void AssertColorData(object tag, bool isLeaf)
        {
            var result = MainForm.GetColorDataFromNodeTag(tag);
            Assert.Equal(isLeaf, result.IsLeaf);
            Assert.Null(result.MessageCountDetails);
        }
    }
}
