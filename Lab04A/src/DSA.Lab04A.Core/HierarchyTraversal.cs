using System;
using System.Collections.Generic;

namespace DSA.Lab04A
{
    /// <summary>
    /// Recursive algorithms over a rooted hierarchy.
    ///
    /// The key idea is structural recursion:
    /// solve the current node, then apply the same operation to each child subtree.
    /// </summary>
    public static class HierarchyTraversal
    {
        public static int CountNodes<T>(HierarchyNode<T> root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            var count = 1;
            foreach (var child in root.Children)
            {
                CountRecursive(ref count, child);
            }

            return count;
        }

        private static void CountRecursive<T>(ref int count, HierarchyNode<T> root)
        {
            count++;
            if (root.IsLeaf)
            {
                return;
            }
            foreach (var child in root.Children)
            {
                CountRecursive(ref count, child);
            }
        }

        public static bool Contains<T>(HierarchyNode<T> root, T target)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            if (root.Value!.Equals(target))
            {
                return true;
            }
            
            foreach (var child in root.Children)
            {
                if (RecursiveSearch(child, target))
                {
                    return true;
                }
            }

            return false;
        }
        
        private static bool RecursiveSearch<T>(HierarchyNode<T> root, T target)
        {
            if (root.Value!.Equals(target))
            {
                return true;
            }
            foreach (var child in root.Children)
            {
                if (RecursiveSearch(child, target))
                {
                    return true;
                }
            }

            return false;
        }

        public static IReadOnlyList<T> DepthFirstPreOrder<T>(HierarchyNode<T> root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            var outList = new List<T>(root.Children.Count + 1) { root.Value };
            foreach (var child in root.Children)
            {
                AddChildrenToListRecursive(outList, child);
            }

            return outList;
        }

        private static void AddChildrenToListRecursive<T>(ICollection<T> collection, HierarchyNode<T> root)
        {
            collection.Add(root.Value);
            foreach (var child in root.Children)
            {
                AddChildrenToListRecursive(collection, child);
            }
        }

        public static int MaxDepth<T>(HierarchyNode<T> root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            var maxDepth = 0;
            foreach (var child in root.Children)
            {
                CheckDepthRecursive(ref maxDepth, child);
            }

            return maxDepth;
        }

        private static void CheckDepthRecursive<T>(ref int maxDepth, HierarchyNode<T> root, int depth = 1)
        {
            maxDepth = Math.Max(maxDepth, depth);
            foreach (var child in root.Children)
            {
                CheckDepthRecursive(ref maxDepth, child, depth + 1);
            }
        }
    }
}
