using PureGame.Engine;
using PureGame.Loader;
using System.Collections.Generic;

namespace PureGame.Universe
{
    public class WorldManager : IWorldLoader
    {
        private Dictionary<string, WorldArea> Worlds;
        private WorldLoader world_loader;
        public WorldManager()
        {
            world_loader = new WorldLoader();
            Worlds = new Dictionary<string, WorldArea>();
        }

        public WorldArea Load(string world_name, IFileReader file_reader)
        {
            if(!Worlds.ContainsKey(world_name))
            {
                Worlds[world_name] = world_loader.Load(world_name, file_reader);
            }
            return Worlds[world_name];
        }
    }
}
