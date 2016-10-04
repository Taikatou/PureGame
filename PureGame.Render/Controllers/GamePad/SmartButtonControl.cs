using Microsoft.Xna.Framework.Input;

namespace PureGame.Client.Controllers.GamePad
{
    public class SmartButtonControl : AbstractSmartControl
    {
        public readonly Buttons Button;

        public SmartButtonControl(Buttons button, Controls control) : base(control)
        {
            Button = button;
            Control = control;
        }

        public override void Update(GamePadState state)
        {
            PreviouslyActive = Active;
            Active = state.IsButtonDown(Button);
        }
    }
}
