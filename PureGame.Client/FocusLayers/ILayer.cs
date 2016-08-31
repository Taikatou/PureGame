using Microsoft.Xna.Framework;
using PureGame.Engine.Controls;

namespace PureGame.Client.FocusLayers
{
    public interface ILayer
    {
        void UpdateController(IController controller, GameTime time);
        void UpdateData(GameTime time);
        string Name { get; }
    }
}
