using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;
using PureGame.Engine.EntityData;

namespace PureGame.Client
{
    public class PlayerEntity : IEntity
    {
        public static Direction[] reverse_directions;
        public static Direction[] ReverseDirections
        {
            get
            {
                if(reverse_directions == null)
                {
                    reverse_directions = new Direction[5];
                    reverse_directions[(int)Direction.Left] = Direction.Right;
                    reverse_directions[(int)Direction.Right] = Direction.Left;
                    reverse_directions[(int)Direction.Up] = Direction.Down;
                    reverse_directions[(int)Direction.Down] = Direction.Up;
                    reverse_directions[(int)Direction.None] = Direction.None;
                }
                return reverse_directions;
            }
        }

        public Direction FacingDirection
        {
            get
            {
                return base_entity.FacingDirection;
            }

            set
            {
                base_entity.FacingDirection = value;
            }
        }

        public string FileName
        {
            get
            {
                return base_entity.FileName;
            }

            set
            {
                base_entity.FileName = value;
            }
        }

        public string Id
        {
            get
            {
                return base_entity.Id;
            }

            set
            {
                base_entity.Id = value;
            }
        }

        public Direction MovementDirection
        {
            get
            {
                return base_entity.MovementDirection;
            }

            set
            {
                base_entity.MovementDirection = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return base_entity.Position;
            }

            set
            {
                base_entity.Position = value;
            }
        }

        public bool RequestInteraction
        {
            get
            {
                return base_entity.RequestInteraction;
            }

            set
            {
                base_entity.RequestInteraction = value;
            }
        }

        public bool RequestMovement
        {
            get
            {
                return base_entity.RequestMovement;
            }

            set
            {
                base_entity.RequestMovement = value;
            }
        }

        public int Speed
        {
            get
            {
                return base_entity.Speed;
            }
        }

        public bool Running
        {
            get
            {
                return base_entity.Running;
            }

            set
            {
                base_entity.Running = value;
            }
        }

        IPureGame world;

        BaseEntity base_entity;
        public PlayerEntity(Vector2 Position, string Id, string FileName, Direction FacingDirection = Direction.Down, IPureGame world = null)
        {
            this.world = world;
            base_entity = new BaseEntity(Position, Id, FileName, FacingDirection);
        }

        public void InteractWith(IEntity interact_with)
        {
            FacingDirection = ReverseDirections[(int)interact_with.FacingDirection];
        }

        public void Interact(IEntity interact_entity)
        {
            interact_entity.InteractWith(this);
            world?.LoadWorld("Data/level01.json");
        }
    }
}
