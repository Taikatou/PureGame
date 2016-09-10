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

        public bool Update(GameTime time, IController controller)
        {
            controller.Update(PureGameLayer, time);
            return false;
        }

        public PureGameClient(PureGame pureGame, EntityObject player)
        {
            Player = player;
            PureGameLayer = new PureGameLayer(player, pureGame);
        }
    }
}
