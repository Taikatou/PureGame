using Microsoft.Xna.Framework;

namespace PureGame.Render.Controllers
{
    public enum Controls { Left, Right, Up, Down, A, B}
    public interface IController
    {
        void Update(GameTime time);
    }
}
