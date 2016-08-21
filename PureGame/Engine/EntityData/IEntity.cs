using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;
using PureGame.SmallGame;

namespace PureGame.Engine.EntityData
{
    public interface IEntity: IGameObject
    {
        Direction FacingDirection { get; set; }
        string FileName { get; set; }
        Direction MovementDirection { get; set; }
        Vector2 Position { get; set; }
        void RequestInteraction();
        void RequestMovement();
        bool Running { get; set; }
        int GetSpeed();

        void Interact(IEntity interact_entity);
        void InteractWith(IEntity baseEntity);
    }
}
