using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PureGame.Common.PathFinding.BasicAStar
{
    public class AStarPathFinder : IPathfinder
    {
        public Dictionary<Point, Node> MapNodes;
        //node has to be walkable
        private Node GetNode(Point p)
        {
            if(!MapNodes.ContainsKey(p))
            {
                MapNodes[p] = new Node(p, SearchParameters.EndLocation);
            }
            return MapNodes[p];
        }
        private readonly Node _endNode;
        public int Width => SearchParameters.Map.MapWidth;
        public int Height => SearchParameters.Map.MapHeight;
        public readonly SearchParameters SearchParameters;

        /// <summary>
        /// Create a new instance of PathFinder
        /// </summary>
        /// <param name="searchParameters"></param>
        public AStarPathFinder(SearchParameters searchParameters)
        {
            SearchParameters = searchParameters;
            MapNodes = new Dictionary<Point, Node>();
            _endNode = GetNode(searchParameters.EndLocation);
        }

        /// <summary>
        /// Attempts to find a path from the start location to the end location based on the supplied SearchParameters
        /// </summary>
        /// <returns>A List of Points representing the path. If no path was found, the returned list is empty.</returns>
        public List<Point> FindPath()
        {
            // The start node is the first entry in the 'open' list
            var path = new List<Point>();
            var success = Search(SearchParameters.StartLocation);
            if (success)
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

        private bool Search(Point location)
        {
            var found = false;
            if (!MapNodes.ContainsKey(location))
            {
                var currentNode = GetNode(location);
                var nextNodes = GetAdjacentWalkableNodes(currentNode);
                nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
                foreach (var nextNode in nextNodes)
                {
                    // Check whether the end node has been reached
                    if (nextNode == _endNode)
                    {
                        found = true;
                    }
                    else if (Search(nextNode.Location))
                    {
                        found = true;
                    }
                    if (found)
                    {
                        break;
                    }
                }
            }
            return found;
        }

        /// <summary>
        /// Returns any nodes that are adjacent to <paramref name="fromNode"/> and may be considered to form the next step in the path
        /// </summary>
        /// <param name="fromNode">The node from which to return the next possible nodes in the path</param>
        /// <returns>A list of next possible nodes in the path</returns>
        private List<Node> GetAdjacentWalkableNodes(Node fromNode)
        {
            var walkableNodes = new List<Node>();
            var nextLocations = GetAdjacentLocations(fromNode.Location);

            foreach (var location in nextLocations)
            {
                var walkAble = SearchParameters.Map.ValidPosition(location);
                var closedNode = MapNodes.ContainsKey(location);
                if (walkAble && !closedNode)
                {
                    var node = GetNode(location);
                    var traversalCost = Node.GetTraversalCost(node.Location, node.ParentNode.Location);
                    var gTemp = fromNode.G + traversalCost;
                    if (gTemp < node.G)
                    {
                        node.ParentNode = fromNode;
                        walkableNodes.Add(node);
                    }
                }
            }

            return walkableNodes;
        }

        /// <summary>
        /// Returns the eight locations immediately adjacent (orthogonally and diagonally) to <paramref name="fromLocation"/>
        /// </summary>
        /// <param name="fromLocation">The location from which to return all adjacent points</param>
        /// <returns>The locations as an IEnumerable of Points</returns>
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
