using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PureGame.Engine.Controls;
using PureGame.Render.Renderable;
using PureGame.Render.Renderable.WorldRenderer;

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
        private bool _previousCameraMovement;
        private Vector2 _previousOffset;

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
                var positionFinder = _renderer.Render.PositionFinder;
                if (!_previousCameraMovement)
                {
                    _previousCameraMovement = true;
                    _previousOffset = positionFinder.Offset;
                }
                CameraUpdate(layer, mouseState, positionFinder);
            }
            else if (_previousCameraMovement)
            {
                _previousCameraMovement = false;
                var positionFinder = _renderer.Render.PositionFinder;
                positionFinder.Offset = _previousOffset;
            }
        }

        public void CameraUpdate(ILayer layer, MouseState mouseState, EntityPositionFinder positionFinder)
        {
            positionFinder.Offset += GetMovement() * positionFinder.TileSize;
        }

        public Vector2 GetMovement()
        {
            var keyBoardState = Keyboard.GetState();
            if (keyBoardState.IsKeyDown(Keys.Up))
            {
                return new Vector2(0, -1);
            }
            if (keyBoardState.IsKeyDown(Keys.Down))
            {
                return new Vector2(0, 1);
            }
            if (keyBoardState.IsKeyDown(Keys.Left))
            {
                return new Vector2(-1, 0);
            }
            if (keyBoardState.IsKeyDown(Keys.Right))
            {
                return new Vector2(1, 0);
            }
            return new Vector2(0, 0);
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
