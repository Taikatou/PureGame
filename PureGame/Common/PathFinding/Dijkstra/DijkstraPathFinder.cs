using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace PureGame.Common.PathFinding.Dijkstra
{
    public class DijkstraPathFinder : IPathfinder
    {
        public readonly Dictionary<Point, Node> Distances;

        public readonly SearchParameters SearchParameters;
        public List<Node> Queue;
        private readonly int _maximium;
        public DijkstraPathFinder(SearchParameters searchParameters)
        {
            SearchParameters = searchParameters;
            var location = searchParameters.StartLocation;
            var startNode = new Node(0, location);
            Queue = new List<Node>
            {
                startNode
            };
            Distances = new Dictionary<Point, Node>
            {
                [location] = startNode
            };
            var width = SearchParameters.Map.MapWidth;
            var height = SearchParameters.Map.MapHeight;
            _maximium = width*height;
        }

        public Node GetNode(Point p)
        {
            if (!Distances.ContainsKey(p))
            {
                var node = new Node(_maximium, p);
                Distances[p] = node;
                Queue.Add(node);
            }
            return Distances[p];
        }

        public Node FindNodePath()
        {
            while (Queue.Count > 0)
            {
                var currentNode = GetNextNode();
                var distance = currentNode.ClosestDistance + 1;
                var walkableNodes = GetAdjacentWalkableNodes(currentNode);
                foreach (var node in walkableNodes)
                {
                    if (node.ClosestDistance > distance)
                    {
                        node.ClosestDistance = distance;
                        node.Parent = currentNode;
                        if (node.Location == SearchParameters.EndLocation)
                        {
                            return node;
                        }
                    }
                }
            }
            return null;
        }

        public List<Point> FindPath()
        {
            var node = FindNodePath();
            Debug.WriteLine(node.ToString());
            var toReturn = new List<Point>();
            while (node != null)
            {
                toReturn.Add(node.Location);
                node = node.Parent;
            }
            toReturn.Reverse();
            var startLocation = SearchParameters.StartLocation;
            toReturn.Remove(startLocation);
            return toReturn;
        }

        private Node GetNextNode()
        {
            var min = _maximium;
            Node toReturn = null;
            foreach (var node in Queue)
            {
                var distance = node.ClosestDistance;
                if (distance <= min)
                {
                    min = distance;
                    toReturn = node;
                }
            }
            Queue.Remove(toReturn);
            return toReturn;
        }

        public List<Node> GetAdjacentWalkableNodes(Node fromNode)
        {
            var walkableNodes = new List<Node>();
            foreach (var point in GetAdjacentLocations(fromNode.Location))
            {
                var valid = SearchParameters.Map.ValidPosition(point);
                if (valid)
                {
                    var node = GetNode(point);
                    walkableNodes.Add(node);
                }
            }
            return walkableNodes;
        }

        private static IEnumerable<Point> GetAdjacentLocations(Point fromLocation)
        {
            return new[]
            {
                new Point(fromLocation.X-1, fromLocation.Y  ),
                new Point(fromLocation.X,   fromLocation.Y+1),
                new Point(fromLocation.X+1, fromLocation.Y  ),
                new Point(fromLocation.X,   fromLocation.Y-1)
            };
        }
    }
}
