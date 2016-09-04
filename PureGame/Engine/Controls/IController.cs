using Microsoft.Xna.Framework;

namespace PureGame.Engine.Controls
{
    public enum Controls { Left, Right, Up, Down, A, B }
    public interface IController
    {
        void Update(ILayer layer, GameTime time);
    }
}
