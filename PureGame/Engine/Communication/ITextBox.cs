using Microsoft.Xna.Framework;

namespace PureGame.Engine.Communication
{
    public interface ITextBox
    {
        void Update(GameTime time);
        bool Complete { get; }
        void Interact();
    }
}
