using System;
using PureGame.Engine;
using SmallGame;
using Microsoft.Xna.Framework;

namespace PureGame.Client
{
    public class PureGameClient : IPureGame
    {
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
            throw new NotImplementedException();
        }

        IPureGame game;

        public PureGameClient(IPureGame game)
        {
            this.game = game;
        }
    }
}
