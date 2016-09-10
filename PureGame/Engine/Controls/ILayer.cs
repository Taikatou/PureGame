using Microsoft.Xna.Framework;

namespace PureGame.Engine.Controls
{
    public interface ILayer
    {
        void ControllerDPad(Direction cachedMoveDiection, GameTime time);
        void ControllerB(bool active);
        void ControllerA();
        string Name { get; }
        void Click(Vector2 position);
    }
}
