using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PureGame.Render.Renderable.WorldRenderer;

namespace PureGame.Render.Controllers
{
    public class WorldClickController : CameraController
    {
        private MouseState _previousState;
        private int PreviousScrollValue => _previousState.ScrollWheelValue;

        public WorldClickController(RenderWorldLayer renderer, PureGameClient client) : base(renderer, client)
        {
            _previousState = Mouse.GetState();
        }

        public bool PreviouslyReleased => _previousState.LeftButton == ButtonState.Released;
        public Vector2 GetClickVector2(MouseState mouseState) => new Vector2(mouseState.X, mouseState.Y);

        public override void Update(GameTime time)
        {
            var mouseState = Mouse.GetState();
            var keyBoardState = Keyboard.GetState();
            if (keyBoardState.IsKeyDown(Keys.D))
            {
                DebugUpdate(mouseState);
            }
            else if (keyBoardState.IsKeyDown(Keys.C))
            {
                CameraUpdate(mouseState);
            }
            else if (PreviouslyMovingCamera)
            {
                Renderer.EndMove();
                PreviouslyMovingCamera = false;
            }
            if (mouseState.ScrollWheelValue != PreviousScrollValue)
            {
                var zoomBy = mouseState.ScrollWheelValue - PreviousScrollValue;
                ZoomCamera((float)zoomBy / 1000);
            }
            _previousState = mouseState;
        }

        public void CameraUpdate(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (PreviouslyReleased)
                {
                    if (!PreviouslyMovingCamera)
                    {
                        Renderer.BeginMove();
                    }
                    PreviouslyMovingCamera = true;
                    DragPosition = GetClickVector2(mouseState);
                }
                else
                {
                    var newDragPosition = GetClickVector2(mouseState);
                    MoveCameraBy(newDragPosition);
                }
            }
        }

        public void DebugUpdate(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed && PreviouslyReleased)
            {
                var position = WorldPosition(mouseState);
                Click(position);
            }
        }

        public Vector2 WorldPosition(MouseState mouseState)
        {
            var position = GetClickVector2(mouseState);
            return Renderer.WorldPosition(position);
        }

        public void Click(Vector2 position)
        {
            Client.Click(position);
        }
    }
}
