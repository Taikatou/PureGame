using PureGame.Engine.EntityData;

namespace PureGame.Engine.Events
{
    public class Interaction
    {
        public Entity InitialEntity;
        public Entity InteractingWithEntity;

        public Interaction(Entity initalEntity, Entity interactWith)
        {
            InitialEntity = initalEntity;
            InteractingWithEntity = interactWith;
        }

        public bool Progress()
        {
            return true;
        }
    }
}
