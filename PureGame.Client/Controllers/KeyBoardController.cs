using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace PureGame.Client.Controllers
{
    public class KeyBoardController : IController
    {
        public SmartKey [] buttons;

        public void Update(GameTime time)
        {
            KeyboardState state = Keyboard.GetState();
            foreach (SmartKey b in buttons)
            {
                b.Update(state);
            }
        }

        public SmartControl[] Buttons
        {
            get
            {
                return buttons;
            }
        }

        public KeyBoardController()
        {
            var ControlsCount = Enum.GetNames(typeof(Controls)).Length;
            buttons = new SmartKey[ControlsCount];
            buttons[(int)Controls.Left] = new SmartKey(Keys.Left);
            buttons[(int)Controls.Up] = new SmartKey(Keys.Up);
            buttons[(int)Controls.Down] = new SmartKey(Keys.Down);
            buttons[(int)Controls.Right] = new SmartKey(Keys.Right);
            buttons[(int)Controls.A] = new SmartKey(Keys.A);
            buttons[(int)Controls.B] = new SmartKey(Keys.B);
        }
    }
}
