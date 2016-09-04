using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using PureGame.Client.FocusLayers;
using PureGame.Engine.Controls;
using System;

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
