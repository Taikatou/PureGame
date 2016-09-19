using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using PureGame.Common;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.World
{
    public class WorldManager : IWorldLoader
    {
        public event EventHandler OnWorldLoad;
        private readonly Dictionary<string, WorldArea> _worldDict;
        private readonly UpdateList<WorldArea> _worldList;
        public readonly Dictionary<IEntity, WorldArea> EntityMapper;
        public WorldManager()
        {
            _worldList = new UpdateList<WorldArea>();
            _worldDict = new Dictionary<string, WorldArea>();
            EntityMapper = new Dictionary<IEntity, WorldArea>();
        }

        public void AddEntity<T>(IEntity entity) where T : WorldArea, new()
        {
            AddEntity<T>(entity, entity.Position);
        }

        public void AddEntity<T>(IEntity entity, Point endPoint) where T : WorldArea, new()
        {
            //if entity currently exists we remove it
            if (EntityMapper.ContainsKey(entity))
            {
                EntityMapper[entity].RemoveEntity(entity);
            }
            entity.Position = endPoint;
            var worldType = typeof(T).ToString();
            LoadWorld<T>(worldType);
            EntityMapper[entity] = _worldDict[worldType];
            Debug.WriteLine("Add entity " + entity.Id);
            EntityMapper[entity].AddEntity(entity);

            OnWorldLoad?.Invoke(this, null);
        }

        public void LoadWorld<T>(string worldType) where T : WorldArea, new()
        {
            if (!_worldDict.ContainsKey(worldType))
            {
                var world = new T();
                world.OnInit(this);
                _worldDict[worldType] = world;
                _worldList.Add(world);
                foreach (var entity in world.Entities)
                {
                    EntityMapper[entity] = _worldDict[worldType];
                }
            }
        }

        public WorldArea GetEntitysWorld(IEntity entity)
        {
            return EntityMapper.ContainsKey(entity) ? EntityMapper[entity] : null;
        }

        public void Update(GameTime time)
        {
            _worldList.Update(time);
        }
    }
}
