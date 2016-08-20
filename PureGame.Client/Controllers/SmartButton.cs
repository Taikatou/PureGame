namespace PureGame.Client.Controllers
{
    public class SmartButton
    {
        public bool Active;
        public bool PreviouslyActive = false;
        public bool NewActive => Active && !PreviouslyActive;

        public SmartButton(bool Active = false)
        {
            this.Active = Active;
        }

        public void ChangeValue(bool NewValue)
        {
            PreviouslyActive = Active;
            Active = NewValue;
        }
    }
}
