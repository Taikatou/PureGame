using Microsoft.Xna.Framework.Input;

namespace PureGame.Render.Controllers
{
    public class SmartKey
    {
        private readonly Keys _key;
        public bool Active;
        public bool PreviouslyActive;
        public bool NewActive => Active && !PreviouslyActive;

        public SmartKey(Keys key)
        {
            _key = key;
        }

        public void Update(KeyboardState state)
        {
            PreviouslyActive = Active;
            Active = state.IsKeyDown(_key);
        }
    }
}
