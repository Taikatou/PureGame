using Microsoft.Xna.Framework.Input;

namespace PureGame.Client.Controllers.GamePad
{
    public class SmartDirectionControl : AbstractSmartControl
    {
        public SmartButtonControl DirectionControl;
        public SmartJoyStickControl JoyStickControl;

        public bool JoyStickActive(GamePadState state)
        {
            JoyStickControl.Update(state);
            return JoyStickControl.Active;
        }

        public bool DirectionControlActive(GamePadState state)
        {
            DirectionControl.Update(state);
            return DirectionControl.Active;
        }

        public SmartDirectionControl(Buttons button, JoyStickButton joyStickButton, Controls control) : base(control)
        {
            DirectionControl = new SmartButtonControl(button, control);
            JoyStickControl = new SmartJoyStickControl(button, joyStickButton, control);
        }

        public override void Update(GamePadState state)
        {
            PreviouslyActive = Active;
            Active = DirectionControlActive(state) ||
                     JoyStickActive(state);
        }
    }
}
