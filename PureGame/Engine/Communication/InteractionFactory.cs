using System;
using System.Diagnostics;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.Communication
{
    public class InteractionFactory
    {
        public static IInteraction MakeInteraction(IEntity entity)
        {
            var interaction = new OptionsInteraction(entity, 5000);
            EventHandler interactionEvent = (sender, args) =>
            {
                Debug.WriteLine("Event happened");
            };
            interaction.AddOption(new InteractionOption(interactionEvent, "Interacting"));
            interaction.AddOption(new InteractionOption(interactionEvent, "Interacting"));
            return interaction;
        }
    }
}
