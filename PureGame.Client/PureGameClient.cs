using Microsoft.Xna.Framework;
using PureGame.Client.Controllers;
using PureGame.Engine.EntityData;
using PureGame.Client.FocusLayers;
using System.Collections.Generic;
using PureGame.Engine.Controls;

namespace PureGame.Client
{
    public class PureGameClient
    {
        public EntityObject Player;
        private readonly KeyBoardController _controller;
        public PureGame PureGame;
        public Stack<ILayer> Layers;

        public void Update(GameTime time)
        {
            _controller.Update(time);
            Layers.Peek().UpdateController(_controller, time);
            Layers.Peek().UpdateData(time);
        }

        public PureGameClient(PureGame pureGame, EntityObject p, IController c)
        {
            PureGame = pureGame;
            Player = p;
            _controller = (KeyBoardController)c;
            Layers = new Stack<ILayer>();
            Layers.Push(new PureGameLayer(p, PureGame));
        }
    }
}
