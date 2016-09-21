using Microsoft.Xna.Framework.Input;
using PureGame.Render.Controllers.Common;

namespace PureGame.Render.Controllers.GamePadController
{
    public class SmartButton : SmartControl
    {
        private readonly Buttons _button;

        public SmartButton(Buttons button, Controls control) : base(control)
        {
            _button = button;
            Control = control;
        }

        public void Update(GamePadState state)
        {
            PreviouslyActive = Active;
            Active = state.IsButtonDown(_button);
        }
    }
}
