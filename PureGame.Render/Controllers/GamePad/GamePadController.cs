using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PureGame.Render.Controllers.Common;

namespace PureGame.Render.Controllers.GamePad
{
    public class GamePadController : AbstractSmartController<SmartButtonControl>
    {
        public GamePadController()
        {
            BButton = new SmartButtonControl(Buttons.B, Controls.B);
            EButton = new SmartButtonControl(Buttons.A, Controls.A);
            AddDirectionalControl(new SmartButtonControl(Buttons.DPadLeft, Controls.Left));
            AddDirectionalControl(new SmartButtonControl(Buttons.DPadUp, Controls.Up));
            AddDirectionalControl(new SmartButtonControl(Buttons.DPadDown, Controls.Down));
            AddDirectionalControl(new SmartButtonControl(Buttons.DPadRight, Controls.Right));
            AddDirectionalControl(new SmartJoyStickControl(Buttons.DPadDown, Controls.Down));
            AddDirectionalControl(new SmartJoyStickControl(Buttons.DPadRight, Controls.Right));
            AddDirectionalControl(new SmartJoyStickControl(Buttons.DPadUp, Controls.Up));
            AddDirectionalControl(new SmartJoyStickControl(Buttons.DPadLeft, Controls.Left));
            SmartControls.Add(EButton);
            SmartControls.Add(BButton);
        }
        public override void Update(GameTime time)
        {
            var capabilities = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(PlayerIndex.One);
            if (capabilities.IsConnected)
            {
                var state = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.One);
                SmartJoyStickControl.UpdateJoyStick(state);
                foreach (var button in SmartControls)
                {
                    button.Update(state);
                }
            }
        }
    }
}
