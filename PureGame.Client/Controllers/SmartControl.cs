using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace PureGame.Client.Controllers
{
    public class SmartControl
    {
        public bool Active = false;
        public bool PreviouslyActive = false;
        public bool NewActive => Active && !PreviouslyActive;
        private Keys key;

        public SmartControl(Keys key)
        {
            this.key = key;
        }

        public void Update(KeyboardState state)
        {
            ChangeValue(state.IsKeyDown(key));
        }

        public void ChangeValue(bool down)
        {
            PreviouslyActive = Active;
            Active = down;
        }
    }
}
