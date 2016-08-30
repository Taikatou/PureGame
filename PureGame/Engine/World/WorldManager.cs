using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using PureGame.SmallGame;

namespace PureGame.Engine.World
{
    public class WorldManager
    {
        private readonly Dictionary<string, WorldArea> _worlds;
        private readonly List<WorldArea> _worldList;
        private readonly IFileReader _fileReader;
        private readonly DataLoader _dataLoader;
        public WorldArea CurrentWorld;
        public WorldManager(IFileReader fileReader)
        {
            _fileReader = fileReader;
            _worldList = new List<WorldArea>();
            _worlds = new Dictionary<string, WorldArea>();
            _dataLoader = new DataLoader();
            _dataLoader.RegisterParser(StandardGameObjectParser.For<EntityObject>(),
                                       StandardGameObjectParser.For<MapObject>());
        }

        public void LoadWorld(string worldName)
        {
            if (!_worlds.ContainsKey(worldName))
            {
                _worlds[worldName] = _dataLoader.Load<WorldArea>(worldName, _fileReader);
                _worldList.Add(_worlds[worldName]);
                _worlds[worldName].OnInit();
            }
            CurrentWorld = _worlds[worldName];
        }

        public void Update(GameTime time)
        {
            foreach (var world in _worldList)
            {
                world.Update(time);
            }
        }
    }
}
