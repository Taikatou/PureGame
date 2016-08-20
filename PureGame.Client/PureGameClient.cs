using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;
using PureGame.Engine;
using PureGame.Client.Controllers;

namespace PureGame.Client
{
    public class PureGameClient : IPureGame
    {
        private IController controller;
        private PlayerEntity player_entity;
        public PlayerEntity Player
        {
            get
            {
                return player_entity;
            }
        }
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
            controller?.Update(player_entity, time);
            game.Update(time);
        }

        public PureGameClient(IPureGame game)
        {
            this.game = game;
            game.Parent = this;
        }

        public void SetPlayer(PlayerEntity p, IController c)
        {
            player_entity = p;
            controller = c;
        }

        public void LoadWorld(string world_name, IFileReader reader)
        {
            game.LoadWorld(world_name, reader);
        }

        public void OnWorldChange()
        {
            parent.OnWorldChange();
        }
    }
}
