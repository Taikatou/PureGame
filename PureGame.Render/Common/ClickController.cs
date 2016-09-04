using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PureGame.Engine.Controls;
using PureGame.Render.Renderable;

namespace PureGame.Render.Common
{
    public class ClickController : IController
    {
        private readonly PlainPureGameRenderer _renderer;

        public ClickController(PlainPureGameRenderer renderer)
        {
            _renderer = renderer;
        }

        private MouseState _previouslyDown;

        public bool PreviouslyReleased => _previouslyDown.LeftButton == ButtonState.Released;
        public bool CameraPreviouslyMoving;
        public Vector2 PreviousCameraPosition;

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
                if (!CameraPreviouslyMoving)
                {
                    PreviousCameraPosition = GetMouseVector2(mouseState);
                    CameraPreviouslyMoving = true;
                }
                else if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    var position = GetMouseVector2(mouseState);
                    position -= PreviousCameraPosition;
                    Debug.WriteLine(position);
                    _renderer.MoveFocus(position);
                    PreviousCameraPosition = position;
                }
            }
            else if (CameraPreviouslyMoving)
            {
                Debug.WriteLine("Release");
                CameraPreviouslyMoving = false;
            }
        }

        public void DebugUpdate(ILayer layer, MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed && PreviouslyReleased)
            {
                var position = WorldPosition(mouseState);
                Click(layer, position);
            }
            _previouslyDown = mouseState;
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
