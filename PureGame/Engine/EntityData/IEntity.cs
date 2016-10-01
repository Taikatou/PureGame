using System;
using Microsoft.Xna.Framework;
using PureGame.Engine.Communication;

namespace PureGame.Engine.EntityData
{
    public interface IEntity
    {
        Direction FacingDirection { get; set; }
        string FileName { get; set; }
        string Id { get; set; }
        Direction MovementDirection { get; set; }
        Point Position { get; set; }
        bool Running { get; set; }
        int Speed { get; }
        bool Talking { get; set; }
        EventHandler OnMoveEvent { get; set; }
        void MomentumChange();
        IInteraction Interaction { get; }
    }
}
