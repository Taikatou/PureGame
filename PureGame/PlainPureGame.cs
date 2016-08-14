using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine;
using PureGame.Engine.Controllers;
using PureGame.Engine.EntityData;
using PureGame.Engine.Scripts;
using SmallGame;

namespace PureGame
{
    public class PlainPureGame : AbstractPureGame
    {

        public IScriptService ScriptService;
        public PlainPureGame(ContentManager content, IScriptService ScriptService=null)
        {
            this.ScriptService = ScriptService;
            ContentManagerManager.Instance = new ContentManagerManager(content);
            DataLoader = new DataLoader();
            DataLoader.RegisterParser(StandardGameObjectParser.For<EntityObject>(),
                                      StandardGameObjectParser.For<MapObject>());
        }

        public override void LoadWorld(string world_name, IFileReader reader)
        {
            Current = DataLoader.Load<WorldArea>(world_name, reader);
            Current.AddMover(new EntityMover(Current.Entities[0], new PhysicalController()));
        }

        public override void Update(GameTime time)
        {
            Current?.Update(time);
            ScriptService?.Update(time);
        }
    }
}