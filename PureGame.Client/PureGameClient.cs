using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using PureGame.Client.FocusLayers;
using System.Collections.Generic;
using PureGame.Client.Controllers;
using PureGame.Engine.Controls;

namespace PureGame.Client
{
    public class PureGameClient : IPureGameClient
    {
        public EntityObject Player;
        private readonly List<IController> _controllers;
        public PureGame PureGame;
        public Stack<ILayer> Layers { get; }

        public void Update(GameTime time)
        {
            var layer = Layers.Peek();
            foreach (var controller in _controllers)
            {
                controller.Update(time);
                controller.UpdateLayer(layer, time);
                layer.UpdateData(time);
            }
        }

        public PureGameClient(PureGame pureGame, EntityObject player)
        {
            PureGame = pureGame;
            Player = player;
            _controllers = new List<IController> { new KeyBoardController() };
            Layers = new Stack<ILayer>();
            Layers.Push(new PureGameLayer(player, PureGame));
        }

        public void AddController(IController controller)
        {
            _controllers.Add(controller);
        }
    }
}
