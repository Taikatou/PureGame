using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PureGame.Common.PathFinding.BasicAStar
{
    public class AStarPathFinder : IPathfinder
    {
        public Node[,] MapNodes;
        private Node GetNode(int x, int y)
        {
            if(MapNodes[x, y] == null)
            {
                var walkAble = SearchParameters.Map.ValidPosition(x, y);
                MapNodes[x, y] = new Node(x, y, walkAble, SearchParameters.EndLocation);
            }
            return MapNodes[x, y];
        }
        private readonly Node _startNode;
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
            MapNodes = new Node[Width, Height];
            _startNode = GetNode(searchParameters.StartLocation.X, searchParameters.StartLocation.Y);
            _startNode.State = NodeState.Open;
            _endNode = GetNode(searchParameters.EndLocation.X, searchParameters.EndLocation.Y);
        }

        /// <summary>
        /// Attempts to find a path from the start location to the end location based on the supplied SearchParameters
        /// </summary>
        /// <returns>A List of Points representing the path. If no path was found, the returned list is empty.</returns>
        public List<Point> FindPath()
        {
            // The start node is the first entry in the 'open' list
            var path = new List<Point>();
            var success = Search(_startNode);
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

        /// <summary>
        /// Attempts to find a path to the destination node using <paramref name="currentNode"/> as the starting location
        /// </summary>
        /// <param name="currentNode">The node from which to find a path</param>
        /// <returns>True if a path to the destination has been found, otherwise false</returns>
        private bool Search(Node currentNode)
        {
            // Set the current node to Closed since it cannot be traversed more than once
            currentNode.State = NodeState.Closed;
            var nextNodes = GetAdjacentWalkableNodes(currentNode);

            // Sort by F-value so that the shortest possible routes are considered first
            nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
            var found = false;
            foreach (var nextNode in nextNodes)
            {
                // Check whether the end node has been reached
                if (nextNode.Location == _endNode.Location)
                {
                    found = true;
                }
                else if(Search(nextNode))
                {
                    found = true;
                }
                if (found)
                {
                    break;
                }
            }

            // The method returns false if this path leads to be a dead end
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
                var x = location.X;
                var y = location.Y;

                // Stay within the grid's boundaries
                if (x < 0 || x >= Width || y < 0 || y >= Height)
                    continue;

                var node = GetNode(x, y);
                // Ignore non-walkable nodes
                if (!node.IsWalkable && node != _endNode)
                    continue;

                // Ignore already-closed nodes
                if (node.State == NodeState.Closed)
                    continue;

                // Already-open nodes are only added to the list if their G-value is lower going via this route.
                if (node.State == NodeState.Open)
                {
                    var traversalCost = Node.GetTraversalCost(node.Location, node.ParentNode.Location);
                    var gTemp = fromNode.G + traversalCost;
                    if (gTemp < node.G)
                    {
                        node.ParentNode = fromNode;
                        walkableNodes.Add(node);
                    }
                }
                else
                {
                    // If it's untested, set the parent and flag it as 'Open' for consideration
                    node.ParentNode = fromNode;
                    node.State = NodeState.Open;
                    walkableNodes.Add(node);
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
