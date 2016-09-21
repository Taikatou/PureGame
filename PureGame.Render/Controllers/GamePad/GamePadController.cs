using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PureGame.Render.Controllers.Common;

namespace PureGame.Render.Controllers.GamePad
{
    public class GamePadController : AbstractSmartController<SmartButton>
    {
        public GamePadController()
        {
            BButton = new SmartButton(Buttons.B, Controls.B);
            EButton = new SmartButton(Buttons.A, Controls.A);
            DirectionalControls = new List<SmartButton>
            {
                new SmartButton(Buttons.DPadLeft, Controls.Left),
                new SmartButton(Buttons.DPadUp, Controls.Up),
                new SmartButton(Buttons.DPadDown, Controls.Down),
                new SmartButton(Buttons.DPadRight, Controls.Right)
            };
            SmartControls = new List<SmartButton>
            {
                EButton,
                BButton
            };
            foreach (var b in DirectionalControls)
            {
                SmartControls.Add(b);
            }
        }
        public override void Update(GameTime time)
        {
            var capabilities = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(PlayerIndex.One);
            if (capabilities.IsConnected)
            {
                var state = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.One);
                foreach (var button in SmartControls)
                {
                    button.Update(state);
                }
            }
        }
    }
}
