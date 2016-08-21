using PureGame.Engine;
using PureGame.Loader;

namespace PureGame.DesktopGl
{
    public class WorldLoader : IWorldLoader
    {
        public WorldArea Load(string world_name, IFileReader file_reader)
        {
            WorldArea World = new WorldArea(world_name, file_reader);
            return World;
        }
    }
}
