using Microsoft.Xna.Framework.Input;

namespace PureGame.Client.Controllers
{
    public class SmartKey : SmartControl
    {
        private readonly Keys key;

        public SmartKey(Keys key)
        {
            this.key = key;
        }

        public void Update(KeyboardState state)
        {
            ChangeValue(state.IsKeyDown(key));
        }
    }
}
