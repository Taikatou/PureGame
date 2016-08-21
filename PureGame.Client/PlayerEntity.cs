using System;
using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;
using PureGame.Engine.EntityData;

namespace PureGame.Client
{
    public class PlayerEntity : IEntity
    {

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

        public string Type
        {
            get
            {
                return base_entity.Type;
            }

            set
            {
                base_entity.Type = value;
            }
        }

        IPureGame world;

        BaseEntityObject base_entity;
        public PlayerEntity(Vector2 Position, string Id, string FileName, Direction FacingDirection = Direction.Down, IPureGame world = null)
        {
            this.world = world;
            base_entity = new BaseEntityObject(Position, Id, FileName, FacingDirection);
            base_entity.Id = "Player";
        }

        public void InteractWith(IEntity interact_with)
        {
            base_entity.InteractWith(interact_with);
        }

        public void Interact(IEntity interact_entity)
        {
            base_entity.Interact(interact_entity);
            interact_entity.InteractWith(this);
            world?.LoadWorld("level01.json");
        }

        public void OnInit()
        {
            
        }
    }
}
