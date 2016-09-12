using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.World
{
    public class WorldManager : IWorldLoader
    {
        public event EventHandler OnWorldLoad;
        private readonly Dictionary<string, WorldArea> _worldDict;
        private readonly List<WorldArea> _worldList;
        private readonly Dictionary<Entity, WorldArea> _entityMapper;
        public WorldManager()
        {
            _worldList = new List<WorldArea>();
            _worldDict = new Dictionary<string, WorldArea>();
            _entityMapper = new Dictionary<Entity, WorldArea>();
        }

        public void AddEntity<T>(Entity entity) where T : WorldArea, new()
        {
            AddEntity<T>(entity, entity.Position);
        }

        public void AddEntity<T>(Entity entity, Vector2 endPoint) where T : WorldArea, new()
        {
            //if entity currently exists we remove it
            if (_entityMapper.ContainsKey(entity))
            {
                _entityMapper[entity].RemoveEntity(entity);
            }
            entity.Position = endPoint;
            var worldType = typeof(T).ToString();
            LoadWorld<T>(worldType);
            _entityMapper[entity] = _worldDict[worldType];
            Debug.WriteLine("Add entity " + entity.Id);
            _entityMapper[entity].AddEntity(entity);

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
                    _entityMapper[entity] = _worldDict[worldType];
                }
            }
        }

        public WorldArea GetEntitysWorld(Entity entity)
        {
            return _entityMapper.ContainsKey(entity) ? _entityMapper[entity] : null;
        }

        public void Update(GameTime time)
        {
            for (var i = 0; i < _worldList.Count; i++)
            {
                _worldList[i].Update(time);
            }
        }
    }
}
