using PureGame.Engine;

namespace PureGame.Loader
{
    public interface IWorldLoader
    {
        WorldArea Load(string world_name, IFileReader file_reader);
    }
}
