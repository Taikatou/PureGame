using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine;
using PureGame.MessageBus;

namespace PureGame
{
    public class PureGame : ISubscriber
    {
        public enum MessageCode { WorldChanged, LayerChanged };
        public WorldManager WorldManager;
        public string Subscription = "PureGame";
        public PureGame(ContentManager content, IFileReader fileReader)
        {
            WorldManager = new WorldManager(fileReader);
            ContentManagerManager.Instance = new ContentManagerManager(content);
        }

        public void LoadWorld(string worldName)
        {
            WorldManager.LoadWorld(worldName);
            Message message = new Message((int)MessageCode.WorldChanged, "");
            MessageManager.Instance.SendMessage(Subscription, message);
        }

        public void Update(GameTime time)
        {
            WorldManager.CurrentWorld?.Update(time);
        }

        public void RecieveMessage(Message m)
        {
        }

        public void Dispose()
        {
        }
    }
}