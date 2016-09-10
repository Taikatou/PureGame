using Microsoft.Xna.Framework;
using PureGame.Common;

namespace PureGame.Render.Renderable.WorldRenderer
{
    public class FocusVector : IFocusable
    {
        public Vector2 Position { get; set; }
        public Vector2 FinalPosition => Position;

        public FocusVector(Vector2 position)
        {
            Position = position;
        }
    }
}
