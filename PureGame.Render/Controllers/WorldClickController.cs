using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PureGame.Render.Controlables;
using PureGame.Render.Controllers.KeyBoard;

namespace PureGame.Render.Controllers
{
    public class WorldClickController : CameraController
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
        public Vector2 GetClickVector2(MouseState mouseState) => new Vector2(mouseState.X, mouseState.Y);

        public override void Update(GameTime time)
        {
            base.Update(time);
            _previousState = _mouseState;
            _mouseState = Mouse.GetState();
            _keyboardState = Keyboard.GetState();
            Button.Update(_keyboardState);
        }

        public override void UpdateLayer(GameTime time, IControlableLayer layer)
        {
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                if (_keyboardState.IsKeyDown(Keys.D) && PreviouslyReleased)
                {
                    var position = GetClickVector2(_mouseState);
                    Click(position.ToPoint());
                }
                else if (Button.Active)
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
        }
    }
}
