﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PureGame.Engine;
using PureGame.Render.Controlables;

namespace PureGame.Render.Controllers
{
    public class WorldKeyBoardController : IController
    {
        private readonly List<SmartKey> _buttons;
        private readonly List<SmartKey> _dpadButtons;
        public SmartKey CachedButton;
        public SmartKey BButton;
        public SmartKey EButton;
        private KeyboardState _keyBoardState;

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
            _keyBoardState = Keyboard.GetState();
            // C is for camera
            if (!_keyBoardState.IsKeyDown(Keys.C))
            {
                foreach (var button in _buttons)
                {
                    button.Update(_keyBoardState);
                }
            }
        }

        public void UpdateLayer(GameTime time, IControlableLayer layer)
        {
            if (EButton.NewActive)
            {
                layer.Interact();
            }
            var d = GetMovementDirection();
            if (d != Direction.None)
            {
                layer.ControllerDPad(d);
            }
            if (BButton.Change)
            {
                var bActive = BButton.Active;
                layer.Cancel(bActive);
            }
        }

        public WorldKeyBoardController()
        {
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
            _keyBoardState = Keyboard.GetState();
        }
    }
}
