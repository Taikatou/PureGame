using Microsoft.Xna.Framework;
using PureGame.Engine;
using PureGame.Client.Controllers;
using PureGame.Engine.EntityData;

namespace PureGame.Client
{
    public class PureGameClient : IPureGame
    {
        public BaseEntityObject Player;
        private IController controller;
        private IPureGame parent;
        private IPureGame game;

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
            controller?.Update(Player, time);
            game.Update(time);
        }

        public PureGameClient(IPureGame game, BaseEntityObject p, IController c)
        {
            this.game = game;
            game.Parent = this;
            Player = p;
            controller = c;
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
