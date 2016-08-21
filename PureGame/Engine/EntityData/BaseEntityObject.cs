using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;
using PureGame.SmallGame;
using System.Diagnostics;

namespace PureGame.Engine.EntityData
{
    public class BaseEntityObject : AbstractEntityObject
    {
        private int WalkingSpeed = 500;
        private int RunningSpeed = 250;
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

        public static Direction[] reverse_directions;
        public static Direction[] ReverseDirections
        {
            get
            {
                if (reverse_directions == null)
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

        public BaseEntityObject(Vector2 Position, string Id, string FileName="CharacterSheet", Direction FacingDirection = Direction.Down)
        {
            this.Position = Position;
            this.FileName = FileName;
            this.FacingDirection = FacingDirection;
            this.Id = Id;
        }

        public BaseEntityObject()
        {

        }

        public void Interact(BaseEntityObject interact_with)
        {
            if (!CurrentlyInteracting)
            {
                Debug.WriteLine(Id + " Interact with " + interact_with.Id);
                interact_with.InteractWith(this);
            }
        }

        public override void InteractWith(IEntity interact_with)
        {
            FacingDirection = ReverseDirections[(int)interact_with.FacingDirection];
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
