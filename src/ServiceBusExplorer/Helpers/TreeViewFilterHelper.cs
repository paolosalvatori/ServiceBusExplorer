using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ServiceBusExplorer.Helpers
{
    public class TreeViewFilterHelper
    {
        private readonly Dictionary<TreeNode, List<TreeNode>> snapshot = new Dictionary<TreeNode, List<TreeNode>>();
        private readonly Dictionary<TreeNode, List<TreeNode>> childSnapshot = new Dictionary<TreeNode, List<TreeNode>>();

        public void ApplyFilter(TreeNodeCollection categoryNodes, string filterText)
        {
            foreach (TreeNode categoryNode in categoryNodes)
            {
                // Restore previous snapshot first so we always work with full node set
                if (snapshot.TryGetValue(categoryNode, out var previous))
                {
                    categoryNode.Nodes.Clear();
                    foreach (var node in previous)
                    {
                        categoryNode.Nodes.Add(node);
                    }
                }

                // Take fresh snapshot of all current children
                var children = new List<TreeNode>();
                foreach (TreeNode child in categoryNode.Nodes)
                {
                    children.Add(child);
                }
                snapshot[categoryNode] = children;

                if (!string.IsNullOrWhiteSpace(filterText))
                {
                    categoryNode.Nodes.Clear();
                    foreach (var node in children)
                    {
                        if (NodeMatchesFilter(node, filterText))
                        {
                            categoryNode.Nodes.Add(node);
                            FilterChildNodes(node, filterText);
                        }
                    }

                    if (categoryNode.Nodes.Count > 0)
                    {
                        categoryNode.Expand();
                    }
                }
                else
                {
                    // Restore child nodes when filter is cleared
                    foreach (var node in children)
                    {
                        RestoreChildNodes(node);
                    }
                }
            }
        }

        public void Clear()
        {
            snapshot.Clear();
            childSnapshot.Clear();
        }

        private void FilterChildNodes(TreeNode parentNode, string filterText)
        {
            if (parentNode.Nodes.Count == 0) return;

            // If parent itself matches by name, keep all children visible
            if (parentNode.Text.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0) return;

            // Snapshot children before filtering
            if (!childSnapshot.ContainsKey(parentNode))
            {
                var snap = new List<TreeNode>();
                foreach (TreeNode child in parentNode.Nodes)
                {
                    snap.Add(child);
                }
                childSnapshot[parentNode] = snap;
            }

            var allChildren = childSnapshot[parentNode];
            parentNode.Nodes.Clear();
            foreach (var child in allChildren)
            {
                if (NodeMatchesFilter(child, filterText))
                {
                    parentNode.Nodes.Add(child);
                    FilterChildNodes(child, filterText);
                }
            }

            if (parentNode.Nodes.Count > 0)
            {
                parentNode.Expand();
            }
        }

        private void RestoreChildNodes(TreeNode parentNode)
        {
            if (childSnapshot.TryGetValue(parentNode, out var snap))
            {
                parentNode.Nodes.Clear();
                foreach (var child in snap)
                {
                    parentNode.Nodes.Add(child);
                    RestoreChildNodes(child);
                }
                childSnapshot.Remove(parentNode);
            }
        }

        private static bool NodeMatchesFilter(TreeNode node, string filterText)
        {
            if (node.Text.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            foreach (TreeNode child in node.Nodes)
            {
                if (NodeMatchesFilter(child, filterText))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
