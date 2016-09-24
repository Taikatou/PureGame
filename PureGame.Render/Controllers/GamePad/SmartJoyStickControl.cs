using System;
using Microsoft.Xna.Framework.Input;

namespace PureGame.Render.Controllers.GamePad
{
    class SmartJoyStickControl : SmartButtonControl
    {
        public static Buttons ThumbStickButton;
        public SmartJoyStickControl(Buttons button, Controls control) : base(button, control)
        {
        }

        public override void Update(GamePadState state)
        {
            PreviouslyActive = Active;
            Active = ThumbStickButton == Button;
        }

        public static void UpdateJoyStick(GamePadState state, bool leftStick=true)
        {
            ThumbStickButton = GetThumbstickDirection(state, leftStick);
        }

        public static Buttons GetThumbstickDirection(GamePadState state, bool leftStick)
        {
            var thumbstickTolerance = 0.35f;

            var direction = leftStick ? state.ThumbSticks.Left : state.ThumbSticks.Right;

            var absX = Math.Abs(direction.X);
            var absY = Math.Abs(direction.Y);
            var toReturn = (Buttons) 0;
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
