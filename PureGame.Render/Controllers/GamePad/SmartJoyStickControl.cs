using System;
using Microsoft.Xna.Framework.Input;

namespace PureGame.Render.Controllers.GamePad
{
    class SmartJoyStickControl : SmartButtonControl
    {
        private readonly bool _leftStick;
        public SmartJoyStickControl(Buttons button, Controls control, bool leftStick=true) : base(button, control)
        {
            _leftStick = leftStick;
        }

        public override void Update(GamePadState state)
        {
            PreviouslyActive = Active;
            var directonButton = GetThumbstickDirection(state);
            Active = directonButton == Button;
        }

        public Buttons GetThumbstickDirection(GamePadState state)
        {
            var thumbstickTolerance = 0.35f;

            var direction = _leftStick ? state.ThumbSticks.Left : state.ThumbSticks.Right;

            var absX = Math.Abs(direction.X);
            var absY = Math.Abs(direction.Y);
            Buttons toReturn = (Buttons) 0;
            if (absX > absY && absX > thumbstickTolerance)
            {
                toReturn = direction.X > 0 ? Buttons.DPadRight : Buttons.DPadLeft;
            }
            else if (absX < absY && absY > thumbstickTolerance)
            {
                toReturn = direction.Y > 0 ? Buttons.DPadUp : Buttons.DPadDown;
            }
            return toReturn;
        }
    }
}
