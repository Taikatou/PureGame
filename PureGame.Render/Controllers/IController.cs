using Microsoft.Xna.Framework;
using PureGame.Engine;
using PureGame.Client.Controlables;

namespace PureGame.Client.Controllers
{
    public enum Controls
    {
        Left = Direction.Left,
        Right = Direction.Right,
        Up = Direction.Up,
        Down = Direction.Down,
        A,
        B
    }

    public interface IController
    {
        void Update(GameTime time);
        bool UpdateLayer(GameTime time, IControlableLayer layer);
    }
}
