using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PureGame.Render.Controllers.Common;

namespace PureGame.Render.Controllers.KeyBoard
{
    public class WorldKeyBoardController : AbstractSmartController<SmartKey>
    {
        public override void Update(GameTime time)
        {
            var keyBoardState = Keyboard.GetState();
            // C is for camera
            if (!keyBoardState.IsKeyDown(Keys.C))
            {
                foreach (var button in SmartControls)
                {
                    button.Update(keyBoardState);
                }
            }
        }

        public WorldKeyBoardController()
        {
            BButton = new SmartKey(Keys.B, Controls.B);
            EButton = new SmartKey(Keys.E, Controls.A);
            DirectionalControls = new List<SmartKey>
            {
                new SmartKey(Keys.Left, Controls.Left),
                new SmartKey(Keys.Up, Controls.Up),
                new SmartKey(Keys.Down, Controls.Down),
                new SmartKey(Keys.Right, Controls.Right),
                new SmartKey(Keys.A, Controls.Left),
                new SmartKey(Keys.W, Controls.Up),
                new SmartKey(Keys.S, Controls.Down),
                new SmartKey(Keys.D, Controls.Right)
            };
            SmartControls = new List<SmartKey>
            {
                EButton,
                BButton
            };
            foreach (var b in DirectionalControls)
            {
                SmartControls.Add(b);
            }
        }
    }
}
