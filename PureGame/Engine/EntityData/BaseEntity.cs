using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;
using System.Diagnostics;
using System;

namespace PureGame.Engine.EntityData
{
    public class BaseEntity : AbstractBaseEntity
    {
        private int WalkingSpeed = 500;
        private int RunningSpeed = 250;
        public bool Running = false;
        public bool CurrentlyInteracting = false;
        public override int Speed
        {
            get
            {
                if(Running)
                {
                    return RunningSpeed;
                }
                else
                {
                    return WalkingSpeed;
                }
            }
        }

        public BaseEntity(Vector2 Position, string Id, string FileName="CharacterSheet", Direction FacingDirection = Direction.Down)
        {
            this.Position = Position;
            this.FileName = FileName;
            this.FacingDirection = FacingDirection;
            this.Id = Id;
        }

        //interactions in
        public void Interact(BaseEntity interact_with)
        {
            if (!CurrentlyInteracting)
            {
                Debug.WriteLine(Id + " Interact with " + interact_with.Id);
                interact_with.InteractWith(this);
            }
        }

        public override void InteractWith(IEntity interact_with)
        {
            
        }

        public override void Interact(IEntity interact_with)
        {
            if (!CurrentlyInteracting)
            {
                Debug.WriteLine(Id + " Interact with " + interact_with.Id);
                interact_with.InteractWith(this);
            }
        }
    }
}
