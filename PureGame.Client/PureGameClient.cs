using Microsoft.Xna.Framework;
using PureGame.Client.Controllers;
using PureGame.Engine.EntityData;
using PureGame.Client.FocusLayers;
using System.Collections.Generic;

namespace PureGame.Client
{
    public class PureGameClient
    {
        public EntityObject Player;
        private KeyBoardController controller;
        public PureGame PureGame;
        public Stack<ILayer> Layers;

        public void Update(GameTime time)
        {
            controller.Update(time);
            Layers.Peek().UpdateController(controller, time);
            Layers.Peek().UpdateData(time);
        }

        public PureGameClient(PureGame PureGame, EntityObject p, IController c)
        {
            this.PureGame = PureGame;
            Player = p;
            controller = (KeyBoardController)c;
            Layers = new Stack<ILayer>();
            Layers.Push(new PureGameLayer(p, PureGame));
        }
    }
}
