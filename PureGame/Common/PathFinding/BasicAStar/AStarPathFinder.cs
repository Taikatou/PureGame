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
            var startNode = GetNode(SearchParameters.StartLocation);
            var success = Search(startNode);
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
                if (nextNode == _endNode)
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
                var walkAble = SearchParameters.Map.ValidPosition(location);
                if (walkAble)
                {
                    var node = GetNode(location);

                    // Ignore already-closed nodes
                    if (node.State == NodeState.Closed)
                        continue;

                    // Already-open nodes are only added to the list if their G-value is lower going via this route.
                    if (node.State == NodeState.Open)
                    {
                        var traversalCost = node.AddManHattanDistance(node.ParentNode.Location);
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
