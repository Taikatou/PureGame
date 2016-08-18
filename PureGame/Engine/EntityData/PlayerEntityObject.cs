using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;

namespace PureGame.Engine.EntityData
{
    public class PlayerEntityObject : EntityObject
    {
        public PlayerEntityObject(Vector2 Position, string Id, string FileName = "CharacterSheet", Direction FacingDirection = Direction.Down)
        {
            this.Position = Position;
            this.FileName = FileName;
            this.FacingDirection = FacingDirection;
            this.Id = Id;
            Type = "PlayerEntityObject";
        }

        public override void InteractWith(EntityObject interact_with)
        {
            CurrentlyInteracting = true;
        }
    }
}
