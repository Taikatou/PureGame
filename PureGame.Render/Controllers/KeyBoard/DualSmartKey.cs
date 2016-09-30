using Microsoft.Xna.Framework.Input;

namespace PureGame.Client.Controllers.KeyBoard
{
    class DualSmartKey : SmartKey
    {
        public Keys SecondKey;
        public DualSmartKey(Keys key, Keys secondKey, Controls control) : base(key, control)
        {
            SecondKey = secondKey;
        }

        public override void Update(KeyboardState state)
        {
            PreviouslyActive = Active;
            Active = state.IsKeyDown(Key) || state.IsKeyDown(SecondKey);
        }
    }
}
