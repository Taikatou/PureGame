using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.Controllers
{
    public class PhysicalController : IController
    {
        public bool Right = false;
        public bool Left = false;
        public bool Up = false;
        public bool Down = false;
        public bool A = false;
        public bool B = false;
        private InputManager input_manager;

        public Direction MoveDirection
        {
            get
            {
                if (Right)
                {
                    return Direction.Right;
                }
                else if (Left)
                {
                    return Direction.Left;
                }
                else if (Up)
                {
                    return Direction.Up;
                }
                else if (Down)
                {
                    return Direction.Down;
                }
                else
                {
                    return Direction.None;
                }
            }
        }

        public void Update(EntityObject entity, GameTime time)
        {
            input_manager.Update(time);
            entity.RequestInteraction = A;
            entity.RequestMovement = MoveDirection != Direction.None;
            if (entity.RequestMovement)
            {
                //Debug.WriteLine("Moving entity : " + entity.Id);
                entity.MovementDirection = MoveDirection;
                entity.Running = B;
            }
        }

        public PhysicalController()
        {
            input_manager = new InputManager(this);
        }
    }
}
