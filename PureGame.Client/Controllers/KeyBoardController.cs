using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace PureGame.Client.Controllers
{
    public class KeyBoardController : IController
    {
        public SmartControl [] buttons;

        public void Update(GameTime time)
        {
            KeyboardState state = Keyboard.GetState();
            foreach (SmartControl b in Buttons)
            {
                b.Update(state);
            }
        }
        public IPureGame game;

        public SmartControl[] Buttons
        {
            get
            {
                return buttons;
            }
            set
            {
                buttons = value;
            }
        }

        public KeyBoardController(IPureGame game)
        {
            this.game = game;
            var ControlsCount = Enum.GetNames(typeof(Controls)).Length;
            Buttons = new SmartControl[ControlsCount];
            Buttons[(int)Controls.Left] = new SmartControl(Keys.Left);
            Buttons[(int)Controls.Up] = new SmartControl(Keys.Up);
            Buttons[(int)Controls.Down] = new SmartControl(Keys.Down);
            Buttons[(int)Controls.Right] = new SmartControl(Keys.Right);
            Buttons[(int)Controls.A] = new SmartControl(Keys.A);
            Buttons[(int)Controls.B] = new SmartControl(Keys.B);
        }
    }
}
