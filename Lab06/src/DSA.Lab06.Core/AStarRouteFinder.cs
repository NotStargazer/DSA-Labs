using System;
using System.Collections.Generic;

namespace DSA.Lab06
{
    /// <summary>
    /// Optional Lab 6B extension.
    /// Finish Dijkstra first.
    /// </summary>
    public sealed class AStarRouteFinder
    {
        public RouteResult FindRoute(
            WeightedGraph graph,
            string start,
            string goal,
            ISet<string>? blocked = null)
        {
            if (graph == null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            if (!graph.ContainsNode(start) || !graph.ContainsNode(goal))
            {
                return RouteResult.NotFound(0);
            }
            
            if (start == goal)
            {
                RouteResult.Success(new[] { start }, 0, 0);
            }

            var expandedNodes = 0;
            var bestCost = new Dictionary<string, int>(graph.NodeCount) { [start] = 0 };
            var cameFrom = new Dictionary<string, string>(graph.NodeCount);
            var frontier = new HashedPriorityMinFrontier<string>();
            frontier.Enqueue(start, 0);
            var goalNode = graph.GetNode(goal);
            
            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue().Value;
                expandedNodes++;
                if (current == goal)
                {
                    return ReconstructPath(graph, cameFrom, start, goal, bestCost[goal], expandedNodes);
                }
                foreach (var edge in graph.GetOutgoing(current))
                {
                    if (blocked != null && blocked.Contains(edge.To))
                    {
                        continue;
                    }
                    
                    var tentativeG = bestCost[current] + edge.Cost;
                    if (bestCost.TryAdd(edge.To, tentativeG) || tentativeG < bestCost[edge.To])
                    {
                        var neighborNode = graph.GetNode(edge.To);
                        bestCost[edge.To] = tentativeG;
                        cameFrom[edge.To] = edge.From;
                        var fCost = tentativeG + Heuristics.Manhattan(neighborNode, goalNode);
                        if (frontier.ContainsKey(edge.To))
                        {
                            frontier[edge.To] = fCost;
                        }
                        else
                        {
                            frontier.Enqueue(edge.To, fCost);
                        }
                    }
                }
            }
            
            return RouteResult.NotFound(expandedNodes);
        }
        
        private RouteResult ReconstructPath(
            WeightedGraph graph,
            Dictionary<string, string> cameFrom,
            string start,
            string goal,
            int cost,
            int expandedNodes)
        {
            var route = new List<string>(graph.NodeCount) { goal };
            for (var p = cameFrom[goal]; p != start; p = cameFrom[p])
            {
                route.Add(p);
            }
            route.Add(start);
            route.Reverse();
            return RouteResult.Success(route, cost, expandedNodes);
        }
    }
}
