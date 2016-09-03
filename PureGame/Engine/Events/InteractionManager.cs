using System.Collections.Generic;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.Events
{
    public class InteractionManager
    {
        public Dictionary<EntityObject, Interaction> Interactions;

        public InteractionManager()
        {
            Interactions = new Dictionary<EntityObject, Interaction>();
        }

        public void AddInteraction(EntityObject entity, EntityObject interactWith)
        {
            var interaction = new Interaction(entity, interactWith);
            Interactions[interaction.InitialEntity] = interaction;
            Interactions[interaction.InteractingWithEntity] = interaction;
        }

        public void RemoveInteraction(EntityObject entity, EntityObject interactWith)
        {
            Interactions.Remove(entity);
            Interactions.Remove(interactWith);
        }

        public void RemoveInteraction(Interaction interaction)
        {
            RemoveInteraction(interaction.InitialEntity, interaction.InteractingWithEntity);
        }

        public void ProgressInteractions(EntityObject e)
        {
            var interaction = Interactions[e];
            var complete = interaction.Progress();
            if (complete)
            {
                Interactions.Remove(interaction.InteractingWithEntity);
                Interactions.Remove(interaction.InitialEntity);
            }
        }

        public bool Interacting(EntityObject entityObject) => Interactions.ContainsKey(entityObject);
    }
}
