using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PureGame.Engine;

namespace PureGame.Render.Controllers
{
    public class WorldKeyBoardController : IController
    {
        private readonly SmartKey[] _buttons;
        private PureGameClient _client;
        public const int CachedMovementResetValue = -1;
        public int CachedMovement = CachedMovementResetValue;

        public Direction GetMovementDirection()
        {
            // Return cached direction
            if (CachedMovement != CachedMovementResetValue && _buttons[CachedMovement].Active)
            {
                return (Direction) CachedMovement;
            }
            CachedMovement = CachedMovementResetValue;
            // Else look for another button
            for (var i = 0; i < (int)Direction.None; i++)
            {
                if (_buttons[i].Active)
                {
                    CachedMovement = i;
                    return (Direction)i;
                }
            }
            // Else return false
            return Direction.None;
        }

        public void UpdateButtons()
        {
            var state = Keyboard.GetState();
            // C is for camera
            if (!state.IsKeyDown(Keys.C))
            {
                foreach (var button in _buttons)
                {
                    button.Update(state);
                }
            }
        }

        public void Update(GameTime time)
        {
            UpdateButtons();

            if (_buttons[(int) Controls.A].NewActive)
            {
                _client.ControllerA();
            }
            var d = GetMovementDirection();
            if (d != Direction.None)
            {
                _client.ControllerDPad(d, time);
            }
            var bActive = _buttons[(int) Controls.B].Active;
            _client.ControllerB(bActive);
        }

        public WorldKeyBoardController(PureGameClient client)
        {
            _client = client;
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
