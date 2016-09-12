﻿using Microsoft.Xna.Framework;
using PureGame.Engine;
using PureGame.Engine.EntityData;
using PureGame.Engine.Events.WorldTriggers;
using PureGame.Engine.World;

namespace PureGame.WorldAreas
{
    public class BasicWorld : WorldArea
    {
        public BasicWorld()
        {
            Map = new WorldMap("level01", Content);
            AddEntity(EntityFactory.MakeEntityObject(new Vector2(1, 1), "CharacterSheet"));
        }

        public override void OnInit(IWorldLoader worldLoader)
        {
            base.OnInit(worldLoader);
            TriggerManager.AddTrigger(new TeleportTrigger<DifferetWorld>(new Vector2(0, 0), new Vector2(5, 5), TriggerManager.WorldLoader));
        }
    }
}
