using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using PureGame.Client.FocusLayers;
using PureGame.Engine.Controls;

namespace PureGame.Client
{
    public class PureGameClient : IControllable
    {
        public EntityObject Player;
        public PureGameLayer PureGameLayer { get; }
        public string Name => PureGameLayer.Name;
        public ILayer Layer => PureGameLayer;

        public void Update(GameTime time, IController controller)
        {
            controller.Update(PureGameLayer, time);
            PureGameLayer.Update(time);
        }

        public PureGameClient(PureGame pureGame, EntityObject player)
        {
            Player = player;
            PureGameLayer = new PureGameLayer(player, pureGame);
        }
    }
}
