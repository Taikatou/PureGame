using Microsoft.Xna.Framework.Input;

namespace PureGame.Render.Controllers.GamePad
{
    class SmartJoyStickControl : SmartButtonControl
    {
        private readonly JoyStickButton _joyStickButton;

        public SmartJoyStickControl(Buttons button, JoyStickButton joyStickButton, Controls control) : base(button, control)
        {
            _joyStickButton = joyStickButton;
        }

        public override void Update(GamePadState state)
        {
            PreviouslyActive = Active;
            Active = _joyStickButton.Button == Button;
        }
    }
}
