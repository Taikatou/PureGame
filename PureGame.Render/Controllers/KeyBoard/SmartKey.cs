﻿using Microsoft.Xna.Framework.Input;
using PureGame.Render.Controllers.Common;

namespace PureGame.Render.Controllers.KeyBoard
{
    public class SmartKey : SmartControl
    {
        public readonly Keys Key;

        public SmartKey(Keys key, Controls control) : base(control)
        {
            Key = key;
            Control = control;
        }

        public virtual void Update(KeyboardState state)
        {
            PreviouslyActive = Active;
            Active = state.IsKeyDown(Key);
        }
    }
}
