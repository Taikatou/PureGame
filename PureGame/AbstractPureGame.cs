using PureGame.Engine;
using SmallGame;
using Microsoft.Xna.Framework;
using System;

namespace PureGame
{
    public abstract class AbstractPureGame : IPureGame
    {
        public WorldArea current;
        public DataLoader data_loader { get; private set; }
        public AbstractPureGame()
        {
        }

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

        private IPureGame parent;

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

        public abstract void LoadWorld(string world_name, IFileReader reader);

        public abstract void Update(GameTime time);

        public virtual void OnWorldChange()
        {
            parent?.OnWorldChange();
        }
    }
}
