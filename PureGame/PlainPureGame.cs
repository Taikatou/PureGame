﻿using Microsoft.Xna.Framework;
using PureGame.Engine;
using SmallGame;

namespace PureGame
{
    public class PlainPureGame
    {
        public WorldArea Current;
        public DataLoader DataLoader { get; private set; }
        public PlainPureGame()
        {
            DataLoader = new DataLoader();
            DataLoader.RegisterParser(StandardGameObjectParser.For<EntityObject>());
        }

        public virtual void LoadWorld(string world_name, IFileReader reader)
        {
            Current = DataLoader.Load<WorldArea>(world_name, reader);
        }

        public void Update(GameTime timer)
        {
            Current?.Update(timer);
        }
    }
}