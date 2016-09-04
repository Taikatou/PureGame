using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine;
using PureGame.Engine.World;

namespace PureGame
{
    public class PureGame
    {
        public WorldManager WorldManager;
        public PureGame(ContentManager content, IFileReader fileReader)
        {
            WorldManager = new WorldManager(fileReader);
            ContentManagerManager.Instance = new ContentManagerManager(content);
        }

        public void Update(GameTime time)
        {
            WorldManager.Update(time);
        }
    }
}