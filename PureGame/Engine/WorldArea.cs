using SmallGame;
using System.Collections.Generic;
using System;
using PureGame.AbstractEngine;
using Microsoft.Xna.Framework;

namespace PureGame.Engine
{
    public class WorldArea : GameLevel
    {
        EntityManager entityManager;

        GenericListAccessor<AbstractEntityObject> Entities;

        GenericListAccessor<AbstractMover> Movers;

        public EntityManager world_entities;

        public EntityManager WorldEntities
        {
            get
            {
                if(world_entities == null)
                {
                    world_entities = new EntityManager();
                }
                return world_entities;
            }
        }

        public WorldArea()
        {
            Entities = new GenericListAccessor<AbstractEntityObject>(Objects);
            Movers = new GenericListAccessor<AbstractMover>(Objects);
        }

        public void Update(GameTime timer)
        {
            foreach(var mover in Movers.List)
            {
                mover.Update(timer);
            }
            entityManager.Update(timer);
        }
    }
}
