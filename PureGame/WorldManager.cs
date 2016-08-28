using PureGame.Engine;
using PureGame.Engine.EntityData;
using PureGame.SmallGame;
using System.Collections.Generic;

namespace PureGame
{
    public class WorldManager
    {
        private readonly Dictionary<string, WorldArea> _worlds;
        private readonly IFileReader _fileReader;
        private readonly DataLoader _dataLoader;
        public WorldArea CurrentWorld;
        public WorldManager(IFileReader fileReader)
        {
            _fileReader = fileReader;
            _worlds = new Dictionary<string, WorldArea>();
            _dataLoader = new DataLoader();
            _dataLoader.RegisterParser(StandardGameObjectParser.For<EntityObject>(),
                                       StandardGameObjectParser.For<MapObject>());
        }

        public void LoadWorld(string worldName)
        {
            CurrentWorld?.UnLoad();
            if (!_worlds.ContainsKey(worldName))
            {
                _worlds[worldName] = _dataLoader.Load<WorldArea>(worldName, _fileReader);
            }
            CurrentWorld = _worlds[worldName];
            CurrentWorld.OnInit(CurrentWorld.Name + "Out");
        }
    }
}
