using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PureGame.Engine;

namespace PureGame.Render.Controllers
{
    public class WorldKeyBoardController : IController
    {
        private readonly List<SmartKey> _buttons;
        private readonly List<SmartKey> _dpadButtons;
        private readonly PureGameClient _client;
        public SmartKey CachedButton;
        public SmartKey BButton;
        public SmartKey EButton;

        public Direction GetMovementDirection()
        {
            // Return cached direction
            if (CachedButton != null && CachedButton.Active)
            {
                return (Direction)CachedButton.Control;
            }
            CachedButton = null;
            // Else look for another button
            foreach(var button in _dpadButtons)
            {
                if (button.Active)
                {
                    CachedButton = button;
                    return (Direction)button.Control;
                }
            }
            // Else return false
            return Direction.None;
        }

        public void Update(GameTime time)
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

            if (EButton.NewActive)
            {
                _client.ControllerA();
            }
            var d = GetMovementDirection();
            if (d != Direction.None)
            {
                _client.ControllerDPad(d, time);
            }
            var bActive = BButton.Active;
            _client.ControllerB(bActive);
        }

        public WorldKeyBoardController(PureGameClient client)
        {
            _client = client;
            BButton = new SmartKey(Keys.B, Controls.B);
            EButton = new SmartKey(Keys.E, Controls.A);
            _dpadButtons = new List<SmartKey>
            {
                new SmartKey(Keys.Left, Controls.Left),
                new SmartKey(Keys.Up, Controls.Up),
                new SmartKey(Keys.Down, Controls.Down),
                new SmartKey(Keys.Right, Controls.Right),
                new SmartKey(Keys.A, Controls.Left),
                new SmartKey(Keys.W, Controls.Up),
                new SmartKey(Keys.S, Controls.Down),
                new SmartKey(Keys.D, Controls.Right),
            };
            _buttons = new List<SmartKey>
            {
                EButton,
                BButton
            };
            foreach (var b in _dpadButtons)
            {
                _buttons.Add(b);
            }
        }
    }
}
