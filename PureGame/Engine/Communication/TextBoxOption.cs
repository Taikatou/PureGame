using System;

namespace PureGame.Engine.Communication
{
    public class TextBoxOption
    {
        public EventHandler OnSelected;

        public TextBoxOption(EventHandler onSelected)
        {
            OnSelected = onSelected;
        }

        public void Click()
        {
            OnSelected?.Invoke(this, null);
        }
    }
}
