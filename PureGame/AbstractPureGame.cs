using PureGame.Engine;
using SmallGame;
using Microsoft.Xna.Framework;

namespace PureGame
{
    public abstract class AbstractPureGame : IPureGame
    {
        public WorldArea current;
        public DataLoader data_loader { get; private set; }

        public WorldArea Current
        {
            get
            {
                return current;
            }

            set
            {
                current = value;
            }
        }

        public DataLoader DataLoader
        {
            get
            {
                return data_loader;
            }

            set
            {
                data_loader = value;
            }
        }

        public abstract void LoadWorld(string world_name, IFileReader reader);

        public abstract void Update(GameTime time)
    }
}
