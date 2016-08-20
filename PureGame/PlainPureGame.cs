using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine;

namespace PureGame
{
    public class PlainPureGame : AbstractPureGame
    {
        public IFileReader file_reader;
        public PlainPureGame(ContentManager content, IFileReader file_reader)
        {
            this.file_reader = file_reader;
            ContentManagerManager.Instance = new ContentManagerManager(content);
        }

        public override void LoadWorld(string world_name)
        {
            World = new WorldArea(world_name, file_reader);
            Parent.OnWorldChange();
        }

        public override void Update(GameTime time)
        {
            World?.Update(time);
        }
    }
}