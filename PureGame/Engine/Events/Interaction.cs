using PureGame.Engine.EntityData;

namespace PureGame.Engine.Events
{
    public class Interaction
    {
        public EntityObject InitialEntity;
        public EntityObject InteractingWithEntity;

        public Interaction(EntityObject initalEntity, EntityObject interactWith)
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
