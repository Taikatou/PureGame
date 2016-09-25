using System;
using Microsoft.Xna.Framework.Input;
using PureGame.Render.Controlables;

namespace PureGame.Render.Controllers.GamePad
{
    class JoyStickCameraController : CameraController
    {
        public float ThumbstickTolerance = 0.35f;
        private readonly bool _leftStick;

        public JoyStickCameraController(bool leftStick=false)
        {
            _leftStick = leftStick;
        }

        public void Update(GamePadState state, IControlableLayer layer)
        {
            base.Update();
            var snapBack = true;
            var direction = _leftStick ? state.ThumbSticks.Left : state.ThumbSticks.Right;

            var absX = Math.Abs(direction.X);
            var absY = Math.Abs(direction.Y);
            if (absX > ThumbstickTolerance || absY > ThumbstickTolerance)
            {
                direction *= 100;
                Drag(direction, layer);
                snapBack = false;
            }
            if (!snapBack)
            {
                ReleaseDrag(layer);
            }
        }
    }
}
