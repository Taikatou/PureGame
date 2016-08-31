using Microsoft.Xna.Framework.Input;
using PureGame.Engine.Controls;

namespace PureGame.Client.Controllers
{
    public class SmartKey : SmartControl
    {
        private readonly Keys _key;

        public SmartKey(Keys key)
        {
            _key = key;
        }

        public void Update(KeyboardState state)
        {
            ChangeValue(state.IsKeyDown(_key));
        }
    }
}
