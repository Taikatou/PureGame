using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;

namespace PureGame.Engine.EntityData
{
    public interface IEntity
    {
        Direction FacingDirection { get; set; }
        string FileName { get; set; }
        string Id { get; set; }
        Direction MovementDirection { get; set; }
        Vector2 Position { get; set; }
        bool RequestInteraction { get; set; }
        bool RequestMovement { get; set; }
        bool Running { get; set; }
        int Speed { get; }

        void Interact(IEntity interact_entity);
        void InteractWith(IEntity baseEntity);
    }
}
