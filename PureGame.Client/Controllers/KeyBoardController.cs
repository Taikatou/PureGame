using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace PureGame.Client.Controllers
{
    public class KeyBoardController : IController
    {
        private readonly SmartKey[] _buttons;

        public void Update(GameTime time)
        {
            KeyboardState state = Keyboard.GetState();
            foreach (SmartKey button in _buttons)
            {
                button.Update(state);
            }
        }

        public SmartControl[] Buttons => _buttons;

        public KeyBoardController()
        {
            var controlsCount = Enum.GetNames(typeof(Controls)).Length;
            _buttons = new SmartKey[controlsCount];
            _buttons[(int)Controls.Left] = new SmartKey(Keys.Left);
            _buttons[(int)Controls.Up] = new SmartKey(Keys.Up);
            _buttons[(int)Controls.Down] = new SmartKey(Keys.Down);
            _buttons[(int)Controls.Right] = new SmartKey(Keys.Right);
            _buttons[(int)Controls.A] = new SmartKey(Keys.A);
            _buttons[(int)Controls.B] = new SmartKey(Keys.B);
        }
    }
}
