using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using PureGame.Engine.Events;
using PureGame.SmallGame;

namespace PureGame.Engine.World
{
    public class WorldManager
    {
        private readonly Dictionary<string, WorldArea> _worlds;
        private readonly List<WorldArea> _worldList;
        private readonly IFileReader _fileReader;
        private readonly DataLoader _dataLoader;
        private readonly Dictionary<EntityObject, WorldArea> _entityMapper;
        public WorldManager(IFileReader fileReader)
        {
            _fileReader = fileReader;
            _worldList = new List<WorldArea>();
            _worlds = new Dictionary<string, WorldArea>();
            _entityMapper = new Dictionary<EntityObject, WorldArea>();
            _dataLoader = new DataLoader();
            _dataLoader.RegisterParser(StandardGameObjectParser.For<EntityObject>(),
                                       StandardGameObjectParser.For<TriggerObject>(),
                                       StandardGameObjectParser.For<MapObject>());
        }

        public void AddEntity(EntityObject entity, string newWorldName)
        {
            //if entity currently exists we remove it
            if (_entityMapper.ContainsKey(entity))
            {
                _entityMapper[entity].EntityManager.RemoveEntity(entity);
            }
            LoadWorld(newWorldName);
            _entityMapper[entity] = _worlds[newWorldName];
            Debug.WriteLine("Add entity " + entity.Id);
            _entityMapper[entity].AddEntity(entity);
        }

        public void LoadWorld(string worldName)
        {
            if (!_worlds.ContainsKey(worldName))
            {
                _worlds[worldName] = _dataLoader.Load<WorldArea>(worldName, _fileReader);
                _worldList.Add(_worlds[worldName]);
                _worlds[worldName].OnInit(this);
                foreach (var entity in _worlds[worldName].EntityManager.Entities)
                {
                    _entityMapper[entity] = _worlds[worldName];
                }
            }
        }

        public WorldArea GetEntitysWorld(EntityObject entity)
        {
            return _entityMapper[entity];
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
