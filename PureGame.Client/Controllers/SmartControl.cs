using Microsoft.Xna.Framework.Input;

namespace PureGame.Client.Controllers
{
    public abstract class SmartControl
    {
        public bool Active = false;
        public bool PreviouslyActive = false;
        public bool NewActive => Active && !PreviouslyActive;

        public void ChangeValue(bool down)
        {
            PreviouslyActive = Active;
            Active = down;
        }
    }
}
