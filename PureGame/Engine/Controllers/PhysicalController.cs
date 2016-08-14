using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.InputListeners;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

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
        public bool Moveing
        {
            get
            {
                bool moving = Right || Left || Up || Down;
                return moving;
            }
        }
        private InputManager input_manager;

        public Direction Facing
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

        public int speed = 400;

        public int Speed
        {
            get
            {
                if(B)
                {
                    return speed / 2;
                }
                else
                {
                    return speed;
                }
            }
        }

        public void Update(EntityObject entity, GameTime time)
        {
            input_manager.Update(time);
            if(Moveing)
            {
                Debug.WriteLine("Moving entity : " + entity.Id);
                entity.Position += DirectionMapper.GetDirection(Facing);
                entity.Facing = Facing;
            }
        }

        public PhysicalController()
        {
            input_manager = new InputManager(this);
        }
    }
}
