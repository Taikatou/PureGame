using Microsoft.Xna.Framework;
using PureGame.Engine.Controls;

namespace PureGame.Engine.World
{
    public interface IWorldController
    {
        WorldArea CurrentWorld { get; }
        void UpdateController(IController controller, GameTime time);
        void Interact(IWorldController worldController);
    }
}
