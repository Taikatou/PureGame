using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PureGame.Render.Controlables;
using PureGame.Render.Controllers.KeyBoard;

namespace PureGame.Render.Controllers
{
    public class WorldClickController : CameraController, IController
    {
        private MouseState _previousState;
        private MouseState _mouseState;
        private KeyboardState _keyboardState;
        public SmartKey Button;
        private int PreviousScrollValue => _previousState.ScrollWheelValue;
        public bool PreviouslyReleased => _previousState.LeftButton == ButtonState.Released;

        public WorldClickController()
        {
            _previousState = Mouse.GetState();
            Button = new SmartKey(Keys.C, Controls.A);
            _mouseState = Mouse.GetState();
        }

        public Vector2 GetClickVector2(MouseState mouseState)
        {
            var mousePosition = new Vector2(mouseState.X, mouseState.Y);
            return mousePosition;
        }

        public void Update(GameTime time)
        {
            base.Update();
            _previousState = _mouseState;
            _mouseState = Mouse.GetState();
            _keyboardState = Keyboard.GetState();
            Button.Update(_keyboardState);
        }

        public bool UpdateLayer(GameTime time, IControlableLayer layer)
        {
            var found = false;
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                if (Button.Active)
                {
                    if (PreviouslyReleased)
                    {
                        DragPosition = GetClickVector2(_mouseState);
                    }
                    else
                    {
                        var newDragPosition = GetClickVector2(_mouseState);
                        Drag(newDragPosition, layer);
                    }
                }
                else if (PreviouslyReleased)
                {
                    var position = GetClickVector2(_mouseState);
                    found = layer.Tap(position);
                }
            }
            if (!Button.Active && Button.PreviouslyActive)
            {
                ReleaseDrag(layer);
            }
            if (_mouseState.ScrollWheelValue != PreviousScrollValue)
            {
                var zoomBy = _mouseState.ScrollWheelValue - PreviousScrollValue;
                Zoom((float) zoomBy/1000, layer);
            }
            return found;
        }
    }
}
