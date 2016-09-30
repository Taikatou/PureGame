using PureGame.Client.Controllers.GamePad;
using PureGame.Client.Controllers.KeyBoard;

namespace PureGame.Client.Controllers
{
    public class ControllerManagerFactory
    {
        public static ControllerManager MakeControllerManager(IControllerSettings settings)
        {
            var controllerManager = new ControllerManager();
            if (settings.KeyBoardMouseEnabled)
            {
                controllerManager.Add(new WorldClickController());
                controllerManager.Add(new WorldKeyBoardController());
            }
            if (settings.TouchScreenEnabled)
            {
                controllerManager.Add(new TouchScreenController());
            }
            if (settings.GamePadEnabled)
            {
                controllerManager.Add(new GamePadController());
            }
            return controllerManager;
        }
    }
}
