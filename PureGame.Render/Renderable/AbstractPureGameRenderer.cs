﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine;
using PureGame.Engine.EntityData;
using SmallGame;

namespace PureGame.Render.Renderable
{
    public abstract class AbstractPureGameRenderer : IPureGameRenderer
    {
        public WorldArea Current
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DataLoader DataLoader
        {
            get
            {
                return Game.DataLoader;
            }

            set
            {
                Game.DataLoader = value;
            }
        }

        public IPureGame game;

        public IPureGame Game
        {
            get
            {
                return game;
            }
        }

        IPureGame parent;

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

        public abstract void ChangeFocus(EntityObject e);

        public abstract void Draw(SpriteBatch sprite_batch);

        public void LoadWorld(string world_name, IFileReader reader)
        {
            game.LoadWorld(world_name, reader);
        }

        public virtual void OnWorldChange()
        {
            parent.OnWorldChange();
        }

        public abstract void Update(GameTime time);
    }
}