using System.Collections.Generic;
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

        public override void Update(GameTime time, List<IControlAbleLayer> layers)
        {
            var mouseState = Mouse.GetState();
            var keyBoardState = Keyboard.GetState();
            Button.Update(keyBoardState);
            foreach (var layer in layers)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (keyBoardState.IsKeyDown(Keys.D) && PreviouslyReleased)
                    {
                        var position = GetClickVector2(mouseState);
                        Click(position.ToPoint());
                    }
                    else if (Button.Active)
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
                //!Button.Active implied
                else if (Button.PreviouslyActive)
                {
                    ReleaseDrag(layer);
                }
                if (mouseState.ScrollWheelValue != PreviousScrollValue)
                {
                    var zoomBy = mouseState.ScrollWheelValue - PreviousScrollValue;
                    Zoom((float) zoomBy/1000, layer);
                }
            }
            _previousState = mouseState;
        }
    }
}
