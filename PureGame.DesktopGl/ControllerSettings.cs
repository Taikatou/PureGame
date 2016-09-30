using PureGame.Client.Controllers;

namespace PureGame.DesktopGl
{
    public class ControllerSettings : IControllerSettings
    {
        public bool KeyBoardMouseEnabled { get; set; }
        public bool TouchScreenEnabled { get; set; }
        public bool GamePadEnabled { get; set; }

        public ControllerSettings()
        {
            KeyBoardMouseEnabled = true;
            TouchScreenEnabled = false;
            GamePadEnabled = true;
        }
    }
}
