using Microsoft.Xna.Framework;
using PureGame.Engine.Controls;

namespace PureGame.Client
{
    public interface IControllable
    {
        void UpdateController(GameTime time, IController controller);
        void Update(GameTime time);
        string Name { get; }
        ILayer Layer { get; }
    }
}
