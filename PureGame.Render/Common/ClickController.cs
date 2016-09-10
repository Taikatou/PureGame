using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PureGame.Engine.Controls;
using PureGame.Render.Renderable;

namespace PureGame.Render.Common
{
    public class ClickController : IController
    {
        private readonly PlainPureGameRenderer _renderer;
        private MouseState _previousState;
        private Vector2 _dragPosition;
        private int PreviousScrollValue => _previousState.ScrollWheelValue;
        private bool _previouslyMovingcamera;

        public ClickController(PlainPureGameRenderer renderer)
        {
            _renderer = renderer;
        }

        public bool PreviouslyReleased => _previousState.LeftButton == ButtonState.Released;

        public void Update(ILayer layer, GameTime time)
        {
            var mouseState = Mouse.GetState();
            var keyBoardState = Keyboard.GetState();
            if (keyBoardState.IsKeyDown(Keys.D))
            {
                DebugUpdate(layer, mouseState);
            }
            else if (keyBoardState.IsKeyDown(Keys.C))
            {
                CameraUpdate(mouseState);
            }
            else if (_previouslyMovingcamera)
            {
                _renderer.EndMove();
                _previouslyMovingcamera = false;
            }
            if (mouseState.ScrollWheelValue != PreviousScrollValue)
            {
                var zoomBy = mouseState.ScrollWheelValue - PreviousScrollValue;
                ZoomCamera(zoomBy);
            }
            _previousState = mouseState;
        }

        private void ZoomCamera(int zoomBy)
        {
            var camera = _renderer.Render.Camera;
            var zoom = camera.Zoom;
            zoom += (float)zoomBy/1000;
            if (zoom >= camera.MinimumZoom && zoom <= camera.MaximumZoom)
            {
                camera.Zoom = zoom;
                _renderer.RenderWorld.RefreshToDraw();
            }
        }

        public void CameraUpdate(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (PreviouslyReleased)
                {
                    if (!_previouslyMovingcamera)
                    {
                        _renderer.BeginMove();
                    }
                    _previouslyMovingcamera = true;
                    _dragPosition = GetMouseVector2(mouseState);
                }
                else
                {
                    var newDragPosition = GetMouseVector2(mouseState);
                    var moveBy = newDragPosition - _dragPosition;
                    _renderer.MoveFocusBy(moveBy);
                    _dragPosition = newDragPosition;
                }
            }
        }

        public void DebugUpdate(ILayer layer, MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed && PreviouslyReleased)
            {
                var position = WorldPosition(mouseState);
                Click(layer, position);
            }
        }

        public Vector2 GetMouseVector2(MouseState mouseState)
        {
            var x = mouseState.X;
            var y = mouseState.Y;
            return new Vector2(x, y);
        }

        public Vector2 WorldPosition(MouseState mouseState)
        {
            var position = GetMouseVector2(mouseState);
            return _renderer.Render.WorldPosition(position);
        }

        public void Click(ILayer layer, Vector2 position)
        {
            layer.Click(position);
        }
    }
}
