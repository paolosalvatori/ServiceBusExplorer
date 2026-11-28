using System;
using System.Windows.Forms;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Forms;
using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    public class EventHubPartitionNodeTests
    {
        [Theory]
        [InlineData("$Default")]
        [InlineData("analytics")]
        [InlineData(null)]
        public void SelectionAndRefreshEachReadAnEmptyPartitionOnce(string group)
        {
            var parent = new TreeNode { Tag = group == null ? null : new ConsumerGroupDescription("Orders", group) };
            var node = parent.Nodes.Add("Partitions").Nodes.Add("2");
            var identity = new PartitionDescription("Orders", "2");
            node.Tag = identity;
            var calls = 0;
            PartitionDescription Read(string hub, string consumerGroup, string id)
            {
                calls++;
                Assert.Equal("Orders", hub);
                Assert.Equal(group, consumerGroup);
                Assert.Equal("2", id);
                return new PartitionDescription(hub, id);
            }

            var selected = MainForm.ReadPartitionNode(node, Read);
            Assert.Equal(1, calls);
            Assert.True(string.IsNullOrEmpty(selected.LastEnqueuedOffset));
            Assert.Same(identity, node.Tag);

            var refreshed = MainForm.ReadPartitionNode(node, Read);
            Assert.Equal(2, calls);
            Assert.NotSame(selected, refreshed);
            Assert.Same(identity, node.Tag);
        }

        [Fact]
        public void FailedPartitionReadLeavesTheCurrentNodeAvailableForRetry()
        {
            var current = new PartitionDescription("orders", "1");
            var node = new TreeNode { Tag = current };
            Assert.Throws<TimeoutException>(() => MainForm.ReadPartitionNode(node, (_, __, ___) => throw new TimeoutException()));
            Assert.Same(current, node.Tag);
            Assert.Null(MainForm.ReadPartitionNode(node, (_, __, ___) => null));
            Assert.Same(current, node.Tag);
        }
    }
}
