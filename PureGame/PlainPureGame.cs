using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine;
using PureGame.Engine.EntityData;
using SmallGame;

namespace PureGame
{
    public class PlainPureGame : AbstractPureGame
    {
        public PlainPureGame(ContentManager content)
        {
            ContentManagerManager.Instance = new ContentManagerManager(content);
            DataLoader = new DataLoader();
            DataLoader.RegisterParser(StandardGameObjectParser.For<EntityObject>(),
                                      StandardGameObjectParser.For<MapObject>());
        }

        public override void LoadWorld(string world_name, IFileReader reader)
        {
            Current = DataLoader.Load<WorldArea>(world_name, reader);
            Parent.OnWorldChange();
        }

        public override void Update(GameTime time)
        {
            Current?.Update(time);
        }
    }
}