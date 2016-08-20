using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine;
using PureGame.Engine.EntityData;

namespace PureGame
{
    public class PlainPureGame : AbstractPureGame
    {
        public PlainPureGame(ContentManager content)
        {
            ContentManagerManager.Instance = new ContentManagerManager(content);
        }

        public override void LoadWorld(string world_name, IFileReader reader)
        {
            World = new WorldArea(world_name, reader);
            Parent.OnWorldChange();
        }

        public override void Update(GameTime time)
        {
            World?.Update(time);
        }
    }
}