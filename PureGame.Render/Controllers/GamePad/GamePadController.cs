using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PureGame.Client.Controlables;
using PureGame.Client.Controllers.Common;

namespace PureGame.Client.Controllers.GamePad
{
    public class GamePadController : AbstractSmartController<SmartButtonControl>
    {
        private readonly JoyStickButton _joyStickButton;
        private readonly GamePadCameraController _joyStickCameraController;
        public GamePadState State;
        public bool Connected;
        public GamePadController()
        {
            _joyStickButton = new JoyStickButton();
            _joyStickCameraController = new GamePadCameraController();
            BButton = new SmartButtonControl(Buttons.B, Controls.B);
            EButton = new SmartButtonControl(Buttons.A, Controls.A);
            AddDirectionalControl(new SmartButtonControl(Buttons.DPadLeft, Controls.Left));
            AddDirectionalControl(new SmartButtonControl(Buttons.DPadUp, Controls.Up));
            AddDirectionalControl(new SmartButtonControl(Buttons.DPadDown, Controls.Down));
            AddDirectionalControl(new SmartButtonControl(Buttons.DPadRight, Controls.Right));
            AddDirectionalControl(new SmartJoyStickControl(Buttons.DPadDown, _joyStickButton, Controls.Down));
            AddDirectionalControl(new SmartJoyStickControl(Buttons.DPadRight, _joyStickButton, Controls.Right));
            AddDirectionalControl(new SmartJoyStickControl(Buttons.DPadUp, _joyStickButton, Controls.Up));
            AddDirectionalControl(new SmartJoyStickControl(Buttons.DPadLeft, _joyStickButton, Controls.Left));
            SmartControls.Add(EButton);
            SmartControls.Add(BButton);
        }
        public override void Update(GameTime time)
        {
            var capabilities = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(PlayerIndex.One);
            Connected = capabilities.IsConnected;
            if (Connected)
            {
                State = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.One);
                _joyStickButton.Update(State);
                foreach (var button in SmartControls)
                {
                    button.Update(State);
                }
            }
        }

        public override bool UpdateLayer(GameTime time, IControlableLayer layer)
        {
            var found = base.UpdateLayer(time, layer);
            if (Connected)
            {
                _joyStickCameraController.Update(State, layer);
            }
            return found;
        }
    }
}
