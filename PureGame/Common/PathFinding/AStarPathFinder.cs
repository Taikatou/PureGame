using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PureGame.Common.PathFinding
{
    public class AStarPathFinder : IPathfinder
    {
        public Dictionary<Point, Node> MapNodes;
        private readonly Node _endNode;
        public readonly SearchParameters SearchParameters;
        public int Max;
        //node has to be walkable
        private Node GetNode(Point p)
        {
            if(!MapNodes.ContainsKey(p))
            {
                MapNodes[p] = new Node(p, SearchParameters.EndLocation, Max);
            }
            return MapNodes[p];
        }

        public AStarPathFinder(SearchParameters searchParameters)
        {
            SearchParameters = searchParameters;
            MapNodes = new Dictionary<Point, Node>();
            _endNode = GetNode(searchParameters.EndLocation);
            var width = SearchParameters.Map.MapWidth;
            var height = SearchParameters.Map.MapHeight;
            Max = width*height;
        }

        public List<Point> FindPath()
        {
            var path = new List<Point>();
            var startNode = GetNode(SearchParameters.StartLocation);
            if (Search(startNode))
            {
                // If a path was found, follow the parents from the end node to build a list of locations
                var node = _endNode;
                while (node.ParentNode != null)
                {
                    path.Add(node.Location);
                    node = node.ParentNode;
                }
                // Reverse the list so it's in the correct order when returned
                path.Reverse();
            }
            return path;
        }

        private bool Search(Node startNode)
        {
            var queue = new List<Node> {startNode};
            var found = false;
            while (queue.Count > 0 && !found)
            {
                // Sort by F-value so that the shortest possible routes are considered first
                queue.Sort((node1, node2) => node1.F.CompareTo(node2.F));
                var currentNode = queue[0];
                if (currentNode == _endNode)
                {
                    found = true;
                }
                else
                {
                    currentNode.State = NodeState.Closed;
                    GetNodesToAnalysis(currentNode, queue);
                    queue.Remove(currentNode);
                }
            }
            return found;
        }

        private void GetNodesToAnalysis(Node fromNode, ICollection<Node> queue)
        {
            var nextLocations = GetAdjacentLocations(fromNode.Location);

            foreach (var location in nextLocations)
            {
                if (SearchParameters.Map.ValidPosition(location))
                {
                    var node = GetNode(location);

                    // Already-open nodes are only added to the list if their G-value is lower going via this route.
                    if (node.State == NodeState.Open)
                    {
                        var traversalCost = Node.GetTraversalCost(node.Location, fromNode.Location);
                        var gTemp = fromNode.G + traversalCost;
                        if (gTemp < node.G)
                        {
                            node.ParentNode = fromNode;
                            queue.Add(node);
                        }
                    }
                    else if (node.State == NodeState.Untested)
                    {
                        // If it's untested, set the parent and flag it as 'Open' for consideration
                        node.ParentNode = fromNode;
                        node.State = NodeState.Open;
                        queue.Add(node);
                    }
                }
            }
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
