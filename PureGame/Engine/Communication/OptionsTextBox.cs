using System.Collections.Generic;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.Communication
{
    public class OptionsTextBox : PlainTextBox
    {
        public List<TextBoxOption> TextBoxes;
        public override string Type => "OptionsTextBox";
        public OptionsTextBox(IEntity entity, int timer) : base(entity, timer)
        {
            TextBoxes = new List<TextBoxOption>();
        }

        public void AddOption(TextBoxOption option)
        {
            TextBoxes.Add(option);
        }
    }
}
