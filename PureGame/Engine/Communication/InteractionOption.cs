using System;

namespace PureGame.Engine.Communication
{
    public class InteractionOption
    {
        public EventHandler OnSelected;
        public string Text;

        public InteractionOption(EventHandler onSelected, string text)
        {
            OnSelected = onSelected;
            Text = text;
        }

        public void Click()
        {
            OnSelected?.Invoke(this, null);
        }
    }
}
