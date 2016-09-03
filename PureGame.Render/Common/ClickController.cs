using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PureGame.Engine.Controls;
using PureGame.Render.Renderable.RenderLayers;

namespace PureGame.Render.Common
{
    public class ClickController : IController
    {
        private readonly IPureGameRenderer _renderer;

        public ClickController(IPureGameRenderer renderer)
        {
            _renderer = renderer;
        }
        public void Update(GameTime time)
        {
            
        }

        private MouseState _previouslyDown;

        public bool PreviouslyReleased => _previouslyDown.LeftButton == ButtonState.Released;

        public void UpdateLayer(ILayer layer, GameTime time)
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed && PreviouslyReleased)
            {
                // Do whatever you want here
                int x = mouseState.X;
                int y = mouseState.Y;
                Click(layer, x, y);
            }
            _previouslyDown = mouseState;
        }

        public void Click(ILayer layer, int x, int y)
        {
            Vector2 position = _renderer.Render.WorldPosition(new Vector2(x, y));
            layer.Click(position);
        }
    }
}
