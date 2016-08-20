using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PureGame.Engine.Controllers
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

        public static Vector2 GetMovementFromDirection(Direction Facing)
        {
            return Instance.MappedDirections[Facing];
        }

        public static Direction GetDirectionFromMovment(Vector2 Movement)
        {
            if(!Instance.ReverseMappedDirections.ContainsKey(Movement))
            {
                return Direction.None;
            }
            else
            {
                return Instance.ReverseMappedDirections[Movement];
            }
        }

        protected static DirectionMapper instance;
        public static DirectionMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DirectionMapper();
                }
                return instance;
            }
        }
    }
}
