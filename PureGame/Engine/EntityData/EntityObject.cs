using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;
using PureGame.MessageBus;
using PureGame.SmallGame;
using System.Diagnostics;

namespace PureGame.Engine.EntityData
{
    public class EntityObject : BaseGameObject
    {
        public enum MessageCode { RequestInteraction, RequestMovement };
        private int WalkingSpeed = 500;
        private int RunningSpeed = 250;
        public bool CurrentlyInteracting = false;
        public string FileName;
        //This is not supposed to be changed directly only through entityManager
        public Vector2 Position;
        public Direction MovementDirection;
        public Direction FacingDirection;
        public bool Running;
        public string SubscriptionName = "Entity";
        public int GetSpeed()
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

        public EntityObject(Vector2 Position, string Id, string FileName="CharacterSheet", Direction FacingDirection = Direction.Down)
        {
            this.Position = Position;
            this.FileName = FileName;
            this.FacingDirection = FacingDirection;
            this.Id = Id;
        }

        public EntityObject()
        {

        }

        public void InteractWith(EntityObject interact_with)
        {
            FacingDirection = ReverseDirections[(int)interact_with.FacingDirection];
            // face the other entity
        }

        public void Interact(EntityObject interact_with)
        {
            if (!CurrentlyInteracting)
            {
                Debug.WriteLine(Id + " Interact with " + interact_with.Id);
                interact_with.InteractWith(this);
                CurrentlyInteracting = false;
            }
        }

        public void RequestInteraction()
        {
            int Code = (int)(MessageCode.RequestInteraction);
            Message m = new Message(Code, Id);
            MessageManager.Instance.SendMessage(SubscriptionName, m);
        }

        public void RequestMovement()
        {
            int Code = (int)(MessageCode.RequestMovement);
            Message m = new Message(Code, Id);
            MessageManager.Instance.SendMessage(SubscriptionName, m);
        }
    }
}
