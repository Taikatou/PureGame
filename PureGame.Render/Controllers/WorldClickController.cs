using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PureGame.Render.ControlLayers;
using PureGame.Render.Renderable.WorldRenderer;

namespace PureGame.Render.Controllers
{
    public class WorldClickController : CameraController
    {
        private MouseState _previousState;
        public SmartKey Button;
        private int PreviousScrollValue => _previousState.ScrollWheelValue;

        public WorldClickController()
        {
            _previousState = Mouse.GetState();
            Button = new SmartKey(Keys.C, Controls.A);
        }

        public bool PreviouslyReleased => _previousState.LeftButton == ButtonState.Released;
        public Vector2 GetClickVector2(MouseState mouseState) => new Vector2(mouseState.X, mouseState.Y);

        public override void Update(GameTime time, IControlLayer layer)
        {
            var mouseState = Mouse.GetState();
            var keyBoardState = Keyboard.GetState();
            Button.Update(keyBoardState);
            if (keyBoardState.IsKeyDown(Keys.D))
            {
                DebugUpdate(mouseState);
            }
            else if (Button.Active)
            {
                CameraUpdate(mouseState, layer);
            }
            //!Button.Active implied
            else if (Button.PreviouslyActive)
            {
                ReleaseDrag(layer);
            }
            if (mouseState.ScrollWheelValue != PreviousScrollValue)
            {
                var zoomBy = mouseState.ScrollWheelValue - PreviousScrollValue;
                Zoom((float)zoomBy / 1000, layer);
            }
            _previousState = mouseState;
        }

        public void CameraUpdate(MouseState mouseState, IControlLayer layer)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (PreviouslyReleased)
                {
                    DragPosition = GetClickVector2(mouseState);
                }
                else
                {
                    var newDragPosition = GetClickVector2(mouseState);
                    Drag(newDragPosition, layer);
                }
            }
        }

        public void DebugUpdate(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed && PreviouslyReleased)
            {
                var position = GetClickVector2(mouseState);
                Click(position.ToPoint());
            }
        }
    }
}
