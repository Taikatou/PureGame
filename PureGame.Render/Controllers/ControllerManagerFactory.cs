using PureGame.Render.Controllers.GamePad;
using PureGame.Render.Controllers.KeyBoard;

namespace PureGame.Render.Controllers
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
