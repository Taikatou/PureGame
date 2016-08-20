using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;

namespace PureGame.Engine.EntityData
{
    public class PlayerEntityObject : EntityObject
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
        public PlayerEntityObject(Vector2 Position, string Id, string FileName = "CharacterSheet", Direction FacingDirection = Direction.Down)
            :base(Position, Id, FileName, FacingDirection)
        {
            this.Position = Position;
            this.FileName = FileName;
            this.FacingDirection = FacingDirection;
            this.Id = Id;
        }

        public override void InteractWith(EntityObject interact_with)
        {
            FacingDirection = ReverseDirections[(int)interact_with.FacingDirection];
        }
    }
}
