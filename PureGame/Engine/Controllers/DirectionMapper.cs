using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PureGame.Engine.Controllers
{
    public enum Direction { Right, Left, Up, Down, None };
    public class DirectionMapper
    {
        protected Dictionary<Direction, Vector2> MappedDirections;
        public DirectionMapper()
        {
            MappedDirections = new Dictionary<Direction, Vector2>();
            MappedDirections[Direction.Up] = new Vector2(0, -1);
            MappedDirections[Direction.Down] = new Vector2(0, 1);
            MappedDirections[Direction.Left] = new Vector2(-1, 0);
            MappedDirections[Direction.Right] = new Vector2(1, 0);
        }

        public static Vector2 GetDirection(Direction Facing)
        {
            return Instance.MappedDirections[Facing];
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
