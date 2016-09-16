﻿using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;

namespace PureGame.Engine
{
    public class EntityMessage
    {
        public IEntity Entity;
        public int Timer;
        public bool Complete => Timer <= 0;

        public EntityMessage(IEntity entity, int timer)
        {
            entity.Talking = true;
            Entity = entity;
            Timer = timer;
        }

        public void Update(GameTime time)
        {
            Timer -= time.ElapsedGameTime.Milliseconds;
            if(Timer <= 0)
            {
                Entity.Talking = false;
            }
        }
    }
}