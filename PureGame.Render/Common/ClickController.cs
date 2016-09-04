using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PureGame.Engine.Controls;

namespace PureGame.Render.Common
{
    public class ClickController : IController
    {
        private readonly IPureGameRenderer _renderer;

        public ClickController(IPureGameRenderer renderer)
        {
            _renderer = renderer;
        }

        private MouseState _previouslyDown;

        public bool PreviouslyReleased => _previouslyDown.LeftButton == ButtonState.Released;

        public void Update(ILayer layer, GameTime time)
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed && PreviouslyReleased)
            {
                // Do whatever you want here
                var x = mouseState.X;
                var y = mouseState.Y;
                Click(layer, x, y);
            }
            _previouslyDown = mouseState;
        }

        public void Click(ILayer layer, int x, int y)
        {
            var position = _renderer.Render.WorldPosition(new Vector2(x, y));
            layer.Click(position);
        }
    }
}
