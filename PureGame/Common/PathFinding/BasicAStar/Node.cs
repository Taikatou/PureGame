using System;
using Microsoft.Xna.Framework;

namespace PureGame.Common.PathFinding.BasicAStar
{
    public class Node
    {
        private Node _parentNode;

        public Point Location { get; }

        public int G { get; private set; }

        public int H { get; }

        public NodeState State { get; set; }

        public int F => G + H;

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

        public Node(Point point, Point endLocation, int max)
        {
            Location = point;
            State = NodeState.Untested;
            H = GetTraversalCost(Location, endLocation);
            G = max;
        }

        public override string ToString()
        {
            return $"{Location.X}, {Location.Y}: {State}";
        }

        internal static int GetTraversalCost(Point location, Point otherLocation)
        {
            var deltaX = otherLocation.X - location.X;
            var deltaY = otherLocation.Y - location.Y;
            return Math.Abs(deltaX) + Math.Abs(deltaY);
        }
    }

    public enum NodeState { Untested, Open, Closed }
}
