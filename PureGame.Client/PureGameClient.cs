using PureGame.Engine;
using SmallGame;
using Microsoft.Xna.Framework;

namespace PureGame.Client
{
    public class PureGameClient : IPureGame
    {
        IPureGame game;
        NetworkManager network_manger;
        public WorldArea Current
        {
            get
            {
                return game.Current;
            }

            set
            {
                game.Current = value;
            }
        }

        public DataLoader DataLoader
        {
            get
            {
                return game.DataLoader;
            }

            set
            {
                game.DataLoader = value;
            }
        }

        public void LoadWorld(string world_name, IFileReader reader)
        {
            game.LoadWorld(world_name, reader);
        }

        public void Update(GameTime time)
        {
            network_manger.Update(time);
            game.Update(time);
        }

        public PureGameClient(IPureGame game)
        {
            this.game = game;
            network_manger = new NetworkManager();
            network_manger.Start();
        }
    }
}
