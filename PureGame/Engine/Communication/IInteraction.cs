using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.Communication
{
    public interface IInteraction
    {
        IEntity Entity { get; }
        void Update(GameTime time);
        bool Complete { get; }
        void Interact();
        string Type { get; }
    }
}
