using Microsoft.Xna.Framework;
using PureGame.Engine.Controls;

namespace PureGame.Client
{
    public interface IControllable
    {
        bool Update(GameTime time, IController controller);
        string Name { get; }
        ILayer Layer { get; }
    }
}
