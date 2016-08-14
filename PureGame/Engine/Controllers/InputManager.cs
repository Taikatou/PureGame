using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.InputListeners;
using System.Diagnostics;

namespace PureGame.Engine.Controllers
{
    public class InputManager
    {
        private PhysicalController controller;
        private InputListenerManager input_manager;

        public void Update(GameTime time)
        {
            input_manager.Update(time);
        }

        public InputManager(PhysicalController controller)
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

        public void KeyAction(PhysicalController controller, Keys key, bool down)
        {
            switch (key)
            {
                case Keys.Left:
                    controller.Left = down;
                    break;
                case Keys.Right:
                    controller.Right = down;
                    break;
                case Keys.Up:
                    controller.Up = down;
                    break;
                case Keys.Down:
                    controller.Down = down;
                    break;
                case Keys.A:
                    controller.A = down;
                    break;
                case Keys.B:
                    controller.B = down;
                    break;
            }
            Debug.WriteLine(string.Format("{0}: {1}", key.ToString(), down));
        }
    }
}
