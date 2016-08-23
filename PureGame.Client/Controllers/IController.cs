using Microsoft.Xna.Framework;

namespace PureGame.Client.Controllers
{
    enum Controls { Left, Right, Up, Down, A, B }
    public interface IController
    {
        void Update(GameTime time);
        SmartControl[] Buttons { get; }
    }
}
