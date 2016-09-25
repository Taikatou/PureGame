using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.Communication
{
    public interface ITextBox
    {
        IEntity Entity { get; }
        void Update(GameTime time);
        bool Complete { get; }
        void Interact();
    }
}
