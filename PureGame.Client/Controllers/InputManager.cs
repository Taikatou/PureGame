using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.InputListeners;
using System.Diagnostics;

namespace PureGame.Client.Controllers
{
    public class InputManager
    {
        private KeyBoardController controller;
        private InputListenerManager input_manager;

        public void Update(GameTime time)
        {
            input_manager.Update(time);
        }

        public InputManager(KeyBoardController controller)
        {
            this.controller = controller;
            InputListenerManager input_manager = new InputListenerManager();
            var mouse_listener = input_manager.AddListener(new MouseListenerSettings());
            mouse_listener.MouseClicked += (sender, args) =>
            {
                Debug.WriteLine("Click here: " + args.Position);
            };
            var keyboard_listener = input_manager.AddListener(new KeyboardListenerSettings());
            keyboard_listener.KeyPressed += (sender, args) =>
            {
                KeyAction(controller, args.Key, true);
            };
            keyboard_listener.KeyReleased += (sender, args) =>
            {
                KeyAction(controller, args.Key, false);
            };

            this.input_manager = input_manager;
        }

        public void KeyAction(KeyBoardController controller, Keys key, bool down)
        {
            switch (key)
            {
                case Keys.Left:
                    controller.Buttons[(int)Controls.Left].ChangeValue(down);
                    break;
                case Keys.Right:
                    controller.Buttons[(int)Controls.Right].ChangeValue(down);
                    break;
                case Keys.Up:
                    controller.Buttons[(int)Controls.Up].ChangeValue(down);
                    break;
                case Keys.Down:
                    controller.Buttons[(int)Controls.Down].ChangeValue(down);
                    break;
                case Keys.A:
                    controller.Buttons[(int)Controls.A].ChangeValue(down);
                    break;
                case Keys.B:
                    controller.Buttons[(int)Controls.B].ChangeValue(down);
                    break;
            }
            Debug.WriteLine(string.Format("{0}: {1}", key.ToString(), down));
        }
    }
}
