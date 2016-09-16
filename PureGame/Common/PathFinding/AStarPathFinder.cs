﻿using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace PureGame.Common.PathFinding
{
    public class AStarPathFinder : IPathfinder
    {
        public Dictionary<Point, Node> MapNodes;
        public HashSet<Point> ClosedNodes;
        private readonly Node _endNode;
        private readonly int _max;
        public readonly SearchParameters SearchParameters;
        //node has to be walkable
        private Node GetNode(Point p)
        {
            Node toReturn = null;
            if (MapNodes.ContainsKey(p))
            {
                toReturn = MapNodes[p];
            }
            else if (ValidPosition(p))
            {
                MapNodes[p] = new Node(p, SearchParameters.EndLocation, _max);
                toReturn = MapNodes[p];
            }
            else
            {
                ClosedNodes.Add(p);
            }
            return toReturn;
        }

        public AStarPathFinder(SearchParameters searchParameters)
        {
            var width = searchParameters.Map.MapWidth;
            var height = searchParameters.Map.MapHeight;
            _max = width * height;
            SearchParameters = searchParameters;
            MapNodes = new Dictionary<Point, Node>();
            ClosedNodes = new HashSet<Point>();
            var endPoint = searchParameters.EndLocation;
            _endNode = ForceNode(endPoint, endPoint, _max);
            MapNodes[searchParameters.EndLocation] = _endNode;
        }

        public Node ForceNode(Point start, Point end, int max)
        {
            var node = new Node(start, end, max);
            return node;
        }

        public List<Point> FindPath()
        {
            var path = new List<Point>();
            var startPos = SearchParameters.StartLocation;
            var startNode = ForceNode(startPos, _endNode.Location, 0);
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
                    ClosedNodes.Add(currentNode.Location);
                    MapNodes.Remove(currentNode.Location);
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
                var nodeClosed = ClosedNodes.Contains(location);
                Node node = null;
                if (!nodeClosed)
                {
                    node = GetNode(location);
                }
                if (node != null)
                {
                    // Already-open nodes are only added to the list if their G-value is lower going via this route.
                    if (node.ParentNode == null)
                    {
                        node.ParentNode = fromNode;
                        queue.Add(node);
                    }
                    else
                    {
                        var traversalCost = Node.GetTraversalCost(node.Location, fromNode.Location);
                        var gTemp = fromNode.G + traversalCost;
                        if (gTemp < node.G)
                        {
                            node.ParentNode = fromNode;
                            queue.Add(node);
                        }
                    }
                }
            }
        }

        public bool ValidPosition(Point point)
        {
            var foundEnd = _endNode.Location == point;
            var validPosition = foundEnd || SearchParameters.Map.ValidPosition(point);
            return validPosition;
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
