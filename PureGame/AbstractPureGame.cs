﻿using PureGame.Engine;
using Microsoft.Xna.Framework;
using System;

namespace PureGame
{
    public abstract class AbstractPureGame : IPureGame
    {
        public AbstractPureGame()
        {
        }

        public WorldArea world;
        public WorldArea World
        {
            get
            {
                return world;
            }

            set
            {
                world = value;
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
