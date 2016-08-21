using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine.EntityData;
using PureGame.Loader;

namespace PureGame
{
    public class PlainPureGame : AbstractPureGame
    {
        public IFileReader file_reader;
        private IWorldLoader world_loader;
        public PlainPureGame(ContentManager content, IFileReader file_reader, IWorldLoader world_loader)
        {
            this.world_loader = world_loader;
            this.file_reader = file_reader;
            ContentLoader.LoadContentManager(content);
        }

        public override void LoadWorld(string world_name)
        {
            World = world_loader.Load(world_name, file_reader);
            //add world call back to entities
            foreach(var e in World.Entities)
            {
                e.SetPureGame(this);
            }
            Parent?.OnWorldChange();
        }

        public override void Update(GameTime time)
        {
            World?.Update(time);
        }
    }
}