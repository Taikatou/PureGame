using Microsoft.Xna.Framework.Input;

namespace PureGame.Render.Controllers
{
    public class SmartKey
    {
        private readonly Keys _key;
        public Controls Control;
        public bool Active;
        public bool PreviouslyActive;
        public bool NewActive => Active && !PreviouslyActive;
        public bool Change => Active != PreviouslyActive;

        public SmartKey(Keys key, Controls control)
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
