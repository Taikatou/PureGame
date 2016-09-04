using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using PureGame.Client.FocusLayers;
using PureGame.Engine.Controls;

namespace PureGame.Client
{
    public class PureGameClient
    {
        public EntityObject Player;
        public PureGameLayer PureGameLayer { get; }

        public void Update(GameTime time, IController controller)
        {
            controller.Update(time);
            controller.UpdateLayer(PureGameLayer, time);
            PureGameLayer.UpdateData(time);
        }

        public PureGameClient(PureGame pureGame, EntityObject player)
        {
            Player = player;
            PureGameLayer = new PureGameLayer(player, pureGame);
        }
    }
}
