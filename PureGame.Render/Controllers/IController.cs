using Microsoft.Xna.Framework;
using PureGame.Engine;
using PureGame.Render.Controlables;

namespace PureGame.Render.Controllers
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
