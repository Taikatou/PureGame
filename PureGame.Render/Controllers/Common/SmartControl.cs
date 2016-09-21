namespace PureGame.Render.Controllers.Common
{
    public class SmartControl
    {
        public Controls Control;
        public bool Active;
        public bool PreviouslyActive;
        public bool NewActive => Active && !PreviouslyActive;
        public bool Change => Active != PreviouslyActive;
        public SmartControl(Controls control)
        {
            Control = control;
        }
    }
}
