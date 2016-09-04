using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PureGame.Engine
{
    public enum Direction { Left, Right, Up, Down, None };
    public class DirectionMapper
    {
        protected Dictionary<Direction, Vector2> MappedDirections;
        protected Dictionary<Vector2, Direction> ReverseMappedDirections;
        public DirectionMapper()
        {
            MappedDirections = new Dictionary<Direction, Vector2>();
            ReverseMappedDirections = new Dictionary<Vector2, Direction>();
            AddDirection(Direction.Up, new Vector2(0, -1));
            AddDirection(Direction.Down, new Vector2(0, 1));
            AddDirection(Direction.Left, new Vector2(-1, 0));
            AddDirection(Direction.Right, new Vector2(1, 0));
        }

        public void AddDirection(Direction d, Vector2 m)
        {
            MappedDirections[d] = m;
            ReverseMappedDirections[m] = d;
        }

        public static Vector2 GetMovementFromDirection(Direction facing)
        {
            return Instance.MappedDirections[facing];
        }

        public static Direction GetDirectionFromMovment(Vector2 movement)
        {
            if(!Instance.ReverseMappedDirections.ContainsKey(movement))
            {
                return Direction.None;
            }
            else
            {
                return Instance.ReverseMappedDirections[movement];
            }
        }

        protected static DirectionMapper _instance;
        public static DirectionMapper Instance => _instance ?? (_instance = new DirectionMapper());
    }
}
