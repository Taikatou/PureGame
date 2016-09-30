using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PureGame.Client.Controllers.Common;

namespace PureGame.Client.Controllers.KeyBoard
{
    public class WorldKeyBoardController : AbstractSmartController<SmartKey>
    {
        public override void Update(GameTime time)
        {
            var keyBoardState = Keyboard.GetState();
            foreach (var button in SmartControls)
            {
                button.Update(keyBoardState);
            }
        }

        public WorldKeyBoardController()
        {
            BButton = new SmartKey(Keys.B, Controls.B);
            EButton = new SmartKey(Keys.E, Controls.A);
            AddDirectionalControl(new DualSmartKey(Keys.Left, Keys.A, Controls.Left));
            AddDirectionalControl(new DualSmartKey(Keys.Up, Keys.W, Controls.Up));
            AddDirectionalControl(new DualSmartKey(Keys.Down, Keys.S, Controls.Down));
            AddDirectionalControl(new DualSmartKey(Keys.Right, Keys.D, Controls.Right));
            SmartControls.Add(EButton);
            SmartControls.Add(BButton);
        }
    }
}
