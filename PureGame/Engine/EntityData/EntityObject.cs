using Microsoft.Xna.Framework;
using PureGame.SmallGame;

namespace PureGame.Engine.EntityData
{
    public class EntityObject : GameObject
    {
        private readonly int _walkingSpeed;
        private readonly int _runningSpeed;
        public string FileName;
        //This is not supposed to be changed directly only through entityManager
        public Vector2 Position;
        public Direction MovementDirection;
        public Direction FacingDirection;
        public bool Running;
        public int Speed => Running ? _runningSpeed : _walkingSpeed;

        private static Direction[] _reverseDirections;
        public static Direction[] ReverseDirections
        {
            get
            {
                if (_reverseDirections == null)
                {
                    _reverseDirections = new Direction[5];
                    _reverseDirections[(int)Direction.Left] = Direction.Right;
                    _reverseDirections[(int)Direction.Right] = Direction.Left;
                    _reverseDirections[(int)Direction.Up] = Direction.Down;
                    _reverseDirections[(int)Direction.Down] = Direction.Up;
                    _reverseDirections[(int)Direction.None] = Direction.None;
                }
                return _reverseDirections;
            }
        }

        public EntityObject(Vector2 position, string id, string fileName, Direction facingDirection = Direction.Down)
        {
            Position = position;
            FileName = fileName;
            FacingDirection = facingDirection;
            Id = id;
            _walkingSpeed = 500;
            _runningSpeed = 250;
        }

        public EntityObject()
        {
            _walkingSpeed = 500;
            _runningSpeed = 250;
        }
    }
}
