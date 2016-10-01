using System.Collections.Generic;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.Communication
{
    public class OptionsInteraction : PlainTextInteraction
    {
        public List<InteractionOption> Options;
        public override string Type => "OptionsInteraction";
        public OptionsInteraction(IEntity entity, int timer) : base(entity, timer)
        {
            Options = new List<InteractionOption>();
        }

        public void AddOption(InteractionOption option)
        {
            Options.Add(option);
        }
    }
}
