using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine;
using PureGame.Engine.Controllers;
using SmallGame;

namespace PureGame
{
    public class PlainPureGame
    {
        public WorldArea Current;
        public DataLoader DataLoader { get; private set; }
        public PlainPureGame(ContentManager content)
        {
            ContentManagerManager.Instance = new ContentManagerManager(content);
            DataLoader = new DataLoader();
            DataLoader.RegisterParser(StandardGameObjectParser.For<EntityObject>(),
                                      StandardGameObjectParser.For<MapObject>());
        }

        public virtual void LoadWorld(string world_name, IFileReader reader)
        {
            Current = DataLoader.Load<WorldArea>(world_name, reader);
            Current.AddMover(new EntityMover(Current.Entities[0], new PhysicalController()));
        }

        public void Update(GameTime timer)
        {
            Current?.Update(timer);
        }
    }
}