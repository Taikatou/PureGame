using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PureGame.Engine
{
    public enum Direction { Left, Right, Up, Down, None };
    public class DirectionMapper
    {
        protected Dictionary<Direction, Point> MappedDirections;
        protected Dictionary<Point, Direction> ReverseMappedDirections;
        public DirectionMapper()
        {
            MappedDirections = new Dictionary<Direction, Point>();
            ReverseMappedDirections = new Dictionary<Point, Direction>();
            AddDirection(Direction.Up, new Point(0, -1));
            AddDirection(Direction.Down, new Point(0, 1));
            AddDirection(Direction.Left, new Point(-1, 0));
            AddDirection(Direction.Right, new Point(1, 0));
            AddDirection(Direction.None, new Point(0, 0));
        }

        public void AddDirection(Direction d, Point m)
        {
            MappedDirections[d] = m;
            ReverseMappedDirections[m] = d;
        }

        public static Point GetMovementFromDirection(Direction facing)
        {
            return Instance.MappedDirections[facing];
        }

        public static Direction GetDirectionFromMovment(Point movement)
        {
            var movementDirection = Direction.None;
            if(Instance.ReverseMappedDirections.ContainsKey(movement))
            {
                movementDirection = Instance.ReverseMappedDirections[movement];
            }
            return movementDirection;
        }

        private static DirectionMapper _instance;
        public static DirectionMapper Instance => _instance ?? (_instance = new DirectionMapper());
    }
}
