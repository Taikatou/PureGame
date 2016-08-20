using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;
using System.Diagnostics;

namespace PureGame.Engine.EntityData
{
    public class EntityObject
    {
        public string FileName;
        //This is not supposed to be changed directly only through entityManager
        public Vector2 Position;
        public bool RequestMovement = false;
        public bool RequestInteraction = false;
        public Direction MovementDirection;
        private int WalkingSpeed = 500;
        private int RunningSpeed = 250;
        public bool Running = false;
        public bool CurrentlyInteracting = false;
        public Direction FacingDirection;
        public string Id;
        public int Speed
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

        public EntityObject(Vector2 Position, string Id, string FileName="CharacterSheet", Direction FacingDirection = Direction.Down)
        {
            this.Position = Position;
            this.FileName = FileName;
            this.FacingDirection = FacingDirection;
            this.Id = Id;
        }

        public Vector2 FacingPosition
        {
            get
            {
                return DirectionMapper.GetMovementFromDirection(FacingDirection);
            }
        }

        public Vector2 MovementPosition
        {
            get
            {
                return DirectionMapper.GetMovementFromDirection(MovementDirection);
            }
        }

        //interactions in
        public void Interact(EntityObject interact_with)
        {
            if (!CurrentlyInteracting)
            {
                Debug.WriteLine(Id + " Interact with " + interact_with.Id);
                interact_with.InteractWith(this);
            }
        }

        public virtual void InteractWith(EntityObject interact_with)
        {
        }
    }
}
