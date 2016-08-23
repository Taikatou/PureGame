using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine;
using PureGame.Loader;
using PureGame.MessageBus;

namespace PureGame
{
    public class PureGame : ISubscriber
    {
        public enum MessageCode { WorldChanged, LayerChanged };
        public IFileReader file_reader;
        public WorldArea World;
        public string Subscription = "PureGame";
        public PureGame(ContentManager content, IFileReader file_reader)
        {
            this.file_reader = file_reader;
            ContentLoader.LoadContentManager(content);
        }

        public void LoadWorld(string world_name)
        {
            World?.Dispose();
            World = WorldManager.Instance.Load(world_name, file_reader);
            Message m = new Message((int)MessageCode.WorldChanged, "");
            MessageManager.Instance.SendMessage(Subscription, m);
        }

        public void Update(GameTime time)
        {
            World?.Update(time);
        }

        public void RecieveMessage(Message m)
        {
        }

        public void Dispose()
        {
        }
    }
}