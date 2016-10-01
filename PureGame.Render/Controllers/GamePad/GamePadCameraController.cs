using System;
using Microsoft.Xna.Framework.Input;
using PureGame.Client.Controlables;

namespace PureGame.Client.Controllers.GamePad
{
    class GamePadCameraController : CameraController
    {
        public float ThumbStickTolerance = 0.35f;
        private readonly bool _leftStick;
        public const int DivZoomBy = 50;
        public bool PreviousLyDragged;

        public GamePadCameraController(bool leftStick=false)
        {
            _leftStick = leftStick;
        }

        public void Update(GamePadState state, IControlableLayer layer)
        {
            Update();
            UpdateJoyStick(state, layer);
            ZoomInBy(state.Triggers.Left, layer);
            ZoomOutBy(state.Triggers.Right, layer);
        }

        public void ZoomInBy(float zoomBy, IControlableLayer layer)
        {
            if (Math.Abs(zoomBy) > 0)
            {
                zoomBy /= DivZoomBy;
                Zoom(-zoomBy, layer);
            }
        }

        public void ZoomOutBy(float zoomBy, IControlableLayer layer)
        {
            ZoomInBy(-zoomBy, layer);
        }

        public void UpdateJoyStick(GamePadState state, IControlableLayer layer)
        {
            var direction = _leftStick ? state.ThumbSticks.Left : state.ThumbSticks.Right;

            var absX = Math.Abs(direction.X);
            var absY = Math.Abs(direction.Y);
            if (absX > ThumbStickTolerance || absY > ThumbStickTolerance)
            {
                direction.X = -direction.X;
                direction *= 10;
                MoveBy(direction, layer);
                PreviousLyDragged = true;
            }
            else if(PreviousLyDragged)
            {
                ReleaseDrag(layer);
                PreviousLyDragged = false;
            }
        }
    }
}
