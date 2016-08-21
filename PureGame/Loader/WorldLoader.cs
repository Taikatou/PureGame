using PureGame.Engine;
using PureGame.Engine.EntityData;
using PureGame.SmallGame;

namespace PureGame.Loader
{
    public class WorldLoader : IWorldLoader
    {
        private DataLoader DataLoader;
        public WorldLoader()
        {
            DataLoader = new DataLoader();
            DataLoader.RegisterParser(StandardGameObjectParser.For<BaseEntityObject>(),
                                      StandardGameObjectParser.For<MapObject>());
        }
        public WorldArea Load(string world_name, IFileReader file_reader)
        {
            WorldArea World = DataLoader.Load<WorldArea>(world_name, file_reader);
            return World;
        }
    }
}
