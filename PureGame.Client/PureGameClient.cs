using Microsoft.Xna.Framework;
using PureGame.Engine;
using PureGame.Client.Controllers;
using PureGame.Engine.EntityData;
using PureGame.Client.FocusLayers;
using System.Collections.Generic;

namespace PureGame.Client
{
    public class PureGameClient : IPureGame
    {
        public BaseEntityObject Player;
        private KeyBoardController controller;
        private IPureGame parent;
        private IPureGame game;
        public Stack<ILayer> Layers;

        public WorldArea World
        {
            get
            {
                return game.World;
            }

            set
            {
                game.World = value;
            }
        }

        public IPureGame Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        public void Update(GameTime time)
        {
            controller.Update(time);
            Layers.Peek().UpdateController(controller, time);
            Layers.Peek().UpdateData(time);
        }

        public PureGameClient(IPureGame game, BaseEntityObject p, IController c)
        {
            this.game = game;
            game.Parent = this;
            Player = p;
            controller = (KeyBoardController)c;
            Layers = new Stack<ILayer>();
            Layers.Push(new PureGameLayer(p, game));
        }

        public void LoadWorld(string world_name)
        {
            game.LoadWorld(world_name);
        }

        public void OnWorldChange()
        {
            parent.OnWorldChange();
        }
    }
}
