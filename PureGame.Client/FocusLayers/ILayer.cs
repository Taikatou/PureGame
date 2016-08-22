using Microsoft.Xna.Framework;
using PureGame.Client.Controllers;

namespace PureGame.Client.FocusLayers
{
    public interface ILayer
    {
        void UpdateController(IController controller, GameTime time);
        void UpdateData(GameTime time);
    }
}
