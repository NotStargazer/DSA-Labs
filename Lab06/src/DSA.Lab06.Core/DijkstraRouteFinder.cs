using System;
using System.Collections.Generic;

namespace DSA.Lab06
{
    public sealed class DijkstraRouteFinder
    {
        public RouteResult FindRoute(WeightedGraph graph, string start, string goal)
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
                return RouteResult.Success(new[] { start }, 0, 0);
            }

            var bestCost = new Dictionary<string, int>(graph.NodeCount);
            var cameFrom = new Dictionary<string, string>(graph.NodeCount);
            var frontier = new MinFrontier<string>();

            bestCost[start] = 0;
            frontier.Enqueue(start, 0);

            var expandedNodes = 0;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();
                if (current.Value == goal)
                {
                    return ReconstructPath(graph, cameFrom, start, goal, bestCost[goal], expandedNodes);
                }
                expandedNodes++;
                foreach (var edge in graph.GetOutgoing(current.Value))
                {
                    var totalCost = bestCost[edge.From] + edge.Cost;
                    if (bestCost.TryAdd(edge.To, totalCost))
                    {
                        cameFrom[edge.To] = edge.From;
                        frontier.Enqueue(edge.To, totalCost);
                    }
                    else if (totalCost < bestCost[edge.To])
                    {
                        bestCost[edge.To] = totalCost;
                        cameFrom[edge.To] = edge.From;
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
