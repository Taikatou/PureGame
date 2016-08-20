using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;

namespace PureGame.Engine.EntityData
{
    public abstract class AbstractBaseEntity : IEntity
    {
        public string file_name;
        //This is not supposed to be changed directly only through entityManager
        public Vector2 position;
        public bool request_movement = false;
        public bool request_interaction = false;
        public Direction movement_direction;
        public Direction facing_direction;
        public string id;
        public Direction FacingDirection
        {
            get
            {
                return facing_direction;
            }

            set
            {
                facing_direction = value;
            }
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public Direction MovementDirection
        {
            get
            {
                return movement_direction;
            }

            set
            {
                movement_direction = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public bool RequestInteraction
        {
            get
            {
                return request_interaction;
            }

            set
            {
                request_interaction = value;
            }
        }

        public bool RequestMovement
        {
            get
            {
                return request_movement;
            }

            set
            {
                request_movement = value;
            }
        }

        public abstract int Speed
        {
            get;
        }

        public string FileName
        {
            get
            {
                return file_name;
            }

            set
            {
                file_name = value;
            }
        }

        public abstract void Interact(IEntity interact_entity);
        public abstract void InteractWith(IEntity interact_with);
    }
}
