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
                G = _parentNode.G + AddManHattanDistance(_parentNode.Location);
            }
        }

        public Node(Point point, Point endLocation)
        {
            Location = point;
            State = NodeState.Untested;
            H = AddManHattanDistance(endLocation);
            G = 0;
        }

        public override string ToString()
        {
            return $"{Location.X}, {Location.Y}: {State}";
        }

        internal int AddManHattanDistance(Point otherLocation)
        {
            var deltaX = otherLocation.X - Location.X;
            var deltaY = otherLocation.Y - Location.Y;
            return deltaX + deltaY;
        }
    }

    public enum NodeState { Untested, Open, Closed }
}
