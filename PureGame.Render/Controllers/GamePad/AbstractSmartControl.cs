using Microsoft.Xna.Framework.Input;
using PureGame.Client.Controllers.Common;

namespace PureGame.Client.Controllers.GamePad
{
    public abstract class AbstractSmartControl : SmartControl
    {
        protected AbstractSmartControl(Controls control) : base(control)
        {
            
        }

        public abstract void Update(GamePadState state);
    }
}
