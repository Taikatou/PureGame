namespace PureGame.Engine.Controls
{
    public abstract class SmartControl
    {
        public bool Active;
        public bool PreviouslyActive;
        public bool NewActive => Active && !PreviouslyActive;

        public void ChangeValue(bool down)
        {
            PreviouslyActive = Active;
            Active = down;
        }
    }
}
