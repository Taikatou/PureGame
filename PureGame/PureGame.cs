using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine;
using PureGame.Loader;

namespace PureGame
{
    public class PureGame
    {
        public IFileReader file_reader;
        public WorldArea World;
        public PureGame(ContentManager content, IFileReader file_reader)
        {
            this.file_reader = file_reader;
            ContentLoader.LoadContentManager(content);
        }

        public void LoadWorld(string world_name)
        {
            World?.Dispose();
            World = WorldManager.Instance.Load(world_name, file_reader);
        }

        public void Update(GameTime time)
        {
            World?.Update(time);
        }
    }
}