﻿using Microsoft.Xna.Framework.Input;
using PureGame.Client.Controllers.Common;

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
