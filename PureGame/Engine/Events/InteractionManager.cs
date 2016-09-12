using System.Collections.Generic;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.Events
{
    public class InteractionManager
    {
        public Dictionary<Entity, Interaction> Interactions;

        public InteractionManager()
        {
            Interactions = new Dictionary<Entity, Interaction>();
        }

        public void AddInteraction(Entity entity, Entity interactWith)
        {
            var interaction = new Interaction(entity, interactWith);
            Interactions[interaction.InitialEntity] = interaction;
            Interactions[interaction.InteractingWithEntity] = interaction;
        }

        public void RemoveInteraction(Entity entity, Entity interactWith)
        {
            Interactions.Remove(entity);
            Interactions.Remove(interactWith);
        }

        public void RemoveInteraction(Interaction interaction)
        {
            RemoveInteraction(interaction.InitialEntity, interaction.InteractingWithEntity);
        }

        public void ProgressInteractions(Entity e)
        {
            var interaction = Interactions[e];
            var complete = interaction.Progress();
            if (complete)
            {
                RemoveInteraction(interaction);
            }
        }

        public bool Interacting(Entity entityObject) => Interactions.ContainsKey(entityObject);
    }
}
