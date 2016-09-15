using Microsoft.Xna.Framework;

namespace PureGame.Common.PathFinding.Dijkstra
{
    public enum NodeState { Visited, UnVisited }
    public class Node
    {
        public int ClosestDistance;
        public Point Location;
        public Node Parent;
        public Node(int distance, Point location)
        {
            ClosestDistance = distance;
            Location = location;
            Parent = null;
        }

        public override string ToString() => $"{Location.X}, {Location.Y}: ClostestDistance: {ClosestDistance}";
    }
}
