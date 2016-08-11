using SmallGame;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PureGame.Engine
{
    public class WorldArea : GameLevel
    {

        public WorldArea()
        {
        }

        public List<EntityObject> Entities
        {
            get
            {
                List<EntityObject> to_return = Objects.GetObjects<EntityObject>();
                return to_return;
            }
        }

        public List<EntityMover> Movers
        {
            get
            {
                List<EntityMover> to_return = Objects.GetObjects<EntityMover>();
                return to_return;
            }
        }

        public EntityUpdateManager world_entities;

        public EntityUpdateManager WorldEntities
        {
            get
            {
                if(world_entities == null)
                {
                    world_entities = new EntityUpdateManager(Entities, Movers);
                }
                return world_entities;
            }
        }

        public void Update(GameTime timer)
        {
            WorldEntities.Update(timer);
        }
    }
}
