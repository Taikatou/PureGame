using PureGame.Engine;
using PureGame.Loader;
using System.Collections.Generic;

namespace PureGame
{
    public class WorldManager
    {
        private Dictionary<string, WorldArea> Worlds;
        private WorldLoader world_loader;
        protected WorldManager()
        {
            world_loader = new WorldLoader();
            Worlds = new Dictionary<string, WorldArea>();
        }

        protected static WorldManager instance;
        public static WorldManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new WorldManager();
                }
                return instance;
            }
        }

        public WorldArea Load(string world_name, IFileReader file_reader)
        {
            if (!Worlds.ContainsKey(world_name))
            {
                Worlds[world_name] = world_loader.Load(world_name, file_reader);
            }
            return Worlds[world_name];
        }
    }
}
