using System;
using System.Collections.Generic;

namespace DSA.Lab05
{
    /// <summary>
    /// Iterative graph traversals.
    ///
    /// Complete these methods after the adjacency representation is working.
    /// Think in terms of:
    /// frontier + visited + neighbors.
    /// </summary>
    public static class GraphTraversal
    {
        private static Stack<string> _frontierStack = new();
        
        public static IReadOnlyList<string> BreadthFirstOrder(
            AdjacencyGraph graph,
            string start)
        {
            var returnList = new List<string>(graph.NodeCount) { start };
            var visited = new HashSet<string>(graph.NodeCount) { start };
            var frontier = new Queue<string>(graph.NodeCount);
            frontier.Enqueue(start);

            while (frontier.Count > 0)
            {
                foreach (var neighbor in graph.GetNeighbors(frontier.Dequeue()))
                {
                    if (visited.Add(neighbor))
                    {
                        returnList.Add(neighbor);
                        frontier.Enqueue(neighbor);
                    }
                }
            }

            return returnList;
        }

        public static IReadOnlyList<string> DepthFirstOrder(
            AdjacencyGraph graph,
            string start)
        {     
            var returnList = new List<string>(graph.NodeCount) { start };
            var visited = new HashSet<string>(graph.NodeCount) { start };
            
            DepthFirstRecursive(returnList, visited, graph, start);

            return returnList;
        }

        private static void DepthFirstRecursive(List<string> returnList, HashSet<string> visited, AdjacencyGraph graph, string start)
        {
            foreach (var neighbor in graph.GetNeighbors(start))
            {
                if (visited.Add(neighbor))
                {
                    returnList.Add(neighbor);
                    DepthFirstRecursive(returnList, visited, graph, neighbor);
                }
            }
        }

        public static bool IsReachableBfs(
            AdjacencyGraph graph,
            string start,
            string target)
        {
            if (start == target)
            {
                return true;
            }
            
            var visited = new HashSet<string>(graph.NodeCount) { start };
            var frontier = new Queue<string>(graph.NodeCount);
            frontier.Enqueue(start);

            while (frontier.Count > 0)
            {
                foreach (var neighbor in graph.GetNeighbors(frontier.Dequeue()))
                {
                    if (visited.Add(neighbor))
                    {
                        if (neighbor == target)
                        {
                            return true;
                        }
                        frontier.Enqueue(neighbor);
                    }
                }
            }

            return false;
        }
    }
}
