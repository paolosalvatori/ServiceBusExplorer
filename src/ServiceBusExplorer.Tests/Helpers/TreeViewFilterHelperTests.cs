using System.Windows.Forms;
using FluentAssertions;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class TreeViewFilterHelperTests
    {
        private readonly TreeViewFilterHelper helper = new TreeViewFilterHelper();

        private static TreeView BuildTree()
        {
            // Simulates: Root → Queues (category) → queue-orders, queue-payments
            //                  → Topics (category) → topic-events → Subscriptions → sub-a, sub-b
            var tree = new TreeView();
            var root = new TreeNode("Root");

            var queues = new TreeNode("Queues");
            queues.Nodes.Add(new TreeNode("queue-orders"));
            queues.Nodes.Add(new TreeNode("queue-payments"));

            var topics = new TreeNode("Topics");
            var topic = new TreeNode("topic-events");
            var subsContainer = new TreeNode("Subscriptions");
            subsContainer.Nodes.Add(new TreeNode("sub-a"));
            subsContainer.Nodes.Add(new TreeNode("sub-b"));
            topic.Nodes.Add(subsContainer);
            topics.Nodes.Add(topic);

            root.Nodes.Add(queues);
            root.Nodes.Add(topics);
            tree.Nodes.Add(root);
            return tree;
        }

        [Fact]
        public void EmptyFilter_AllNodesVisible()
        {
            var tree = BuildTree();
            var root = tree.Nodes[0];

            helper.ApplyFilter(root.Nodes, "");

            // All category children should remain
            root.Nodes[0].Nodes.Count.Should().Be(2); // Queues: 2
            root.Nodes[1].Nodes.Count.Should().Be(1); // Topics: 1
        }

        [Fact]
        public void FilterMatchesTopLevel_MatchingVisible_NonMatchingHidden()
        {
            var tree = BuildTree();
            var root = tree.Nodes[0];

            helper.ApplyFilter(root.Nodes, "orders");

            root.Nodes[0].Nodes.Count.Should().Be(1);
            root.Nodes[0].Nodes[0].Text.Should().Be("queue-orders");
            root.Nodes[1].Nodes.Count.Should().Be(0); // no topic matches
        }

        [Fact]
        public void FilterMatchesChild_ParentVisible_MatchingChildVisible()
        {
            var tree = BuildTree();
            var root = tree.Nodes[0];

            helper.ApplyFilter(root.Nodes, "sub-a");

            // Topics category should have topic-events (because sub-a is inside)
            root.Nodes[1].Nodes.Count.Should().Be(1);
            var topic = root.Nodes[1].Nodes[0];
            topic.Text.Should().Be("topic-events");

            // Inside topic, Subscriptions container kept, and only sub-a visible
            var subsContainer = topic.Nodes[0];
            subsContainer.Nodes.Count.Should().Be(1);
            subsContainer.Nodes[0].Text.Should().Be("sub-a");
        }

        [Fact]
        public void FilterMatchesParent_AllChildrenVisible()
        {
            var tree = BuildTree();
            var root = tree.Nodes[0];

            helper.ApplyFilter(root.Nodes, "topic-events");

            var topic = root.Nodes[1].Nodes[0];
            topic.Text.Should().Be("topic-events");
            // Parent matched → all children kept
            var subsContainer = topic.Nodes[0];
            subsContainer.Nodes.Count.Should().Be(2);
        }

        [Fact]
        public void FilterCleared_FullTreeRestored()
        {
            var tree = BuildTree();
            var root = tree.Nodes[0];

            // Apply filter then clear
            helper.ApplyFilter(root.Nodes, "orders");
            helper.ApplyFilter(root.Nodes, "");

            root.Nodes[0].Nodes.Count.Should().Be(2); // both queues back
            root.Nodes[1].Nodes.Count.Should().Be(1); // topic back
            var topic = root.Nodes[1].Nodes[0];
            var subsContainer = topic.Nodes[0];
            subsContainer.Nodes.Count.Should().Be(2); // both subs back
        }

        [Fact]
        public void FilterIsCaseInsensitive()
        {
            var tree = BuildTree();
            var root = tree.Nodes[0];

            helper.ApplyFilter(root.Nodes, "ORDERS");

            root.Nodes[0].Nodes.Count.Should().Be(1);
            root.Nodes[0].Nodes[0].Text.Should().Be("queue-orders");
        }

        [Fact]
        public void FilterChildThenClear_NestedChildrenRestored()
        {
            var tree = BuildTree();
            var root = tree.Nodes[0];

            // Filter to sub-b, then clear
            helper.ApplyFilter(root.Nodes, "sub-b");
            helper.ApplyFilter(root.Nodes, "");

            var subsContainer = root.Nodes[1].Nodes[0].Nodes[0];
            subsContainer.Nodes.Count.Should().Be(2);
            subsContainer.Nodes[0].Text.Should().Be("sub-a");
            subsContainer.Nodes[1].Text.Should().Be("sub-b");
        }
    }
}
