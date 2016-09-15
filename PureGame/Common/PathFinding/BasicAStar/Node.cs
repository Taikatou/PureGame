using System;
using Microsoft.Xna.Framework;

namespace PureGame.Common.PathFinding.BasicAStar
{
    public class Node
    {
        private Node _parentNode;

        /// <summary>
        /// The node's location in the grid
        /// </summary>
        public Point Location { get; }

        /// <summary>
        /// Cost from start to here
        /// </summary>
        public int G { get; private set; }

        /// <summary>
        /// Estimated cost from here to end
        /// </summary>
        public int H { get; }

        /// <summary>
        /// Estimated total cost (F = G + H)
        /// </summary>
        public int F => G + H;

        /// <summary>
        /// Gets or sets the parent node. The start node's parent is always null.
        /// </summary>
        public Node ParentNode
        {
            get { return _parentNode; }
            set
            {
                // When setting the parent, also calculate the traversal cost from the start node to here (the 'G' value)
                _parentNode = value;
                G = _parentNode.G + GetTraversalCost(Location, _parentNode.Location);
            }
        }

        /// <summary>
        /// Creates a new instance of Node.
        /// </summary>
        /// <param name="x">The node's location along the X axis</param>
        /// <param name="y">The node's location along the Y axis</param>
        /// <param name="endLocation">The location of the destination node</param>
        public Node(Point p, Point endLocation)
        {
            Location = p;
            H = GetTraversalCost(Location, endLocation);
            G = 0;
        }

        public override string ToString()
        {
            return $"{Location.X}, {Location.Y}";
        }

        /// <summary>
        /// Gets the distance between two points
        /// </summary>
        internal static int GetTraversalCost(Point location, Point otherLocation)
        {
            var deltaX = otherLocation.X - location.X;
            var deltaY = otherLocation.Y - location.Y;
            return deltaX * deltaX + deltaY * deltaY;
        }
    }

    public enum NodeState { Untested, Open, Closed }
}
