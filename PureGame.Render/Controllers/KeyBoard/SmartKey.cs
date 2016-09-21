using Microsoft.Xna.Framework.Input;
using PureGame.Render.Controllers.Common;

namespace PureGame.Render.Controllers.KeyBoard
{
    public class SmartKey : SmartControl
    {
        private readonly Keys _key;

        public SmartKey(Keys key, Controls control) : base(control)
        {
            _key = key;
            Control = control;
        }

        public void Update(KeyboardState state)
        {
            PreviouslyActive = Active;
            Active = state.IsKeyDown(_key);
        }
    }
}
