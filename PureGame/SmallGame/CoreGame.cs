using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SmallGame
{
    public class CoreGame : Game
    {
        public DataLoader DataLoader { get; private set; }
        public GameLevel Level { get; private set; }
        public GraphicsDeviceManager Graphics { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }

        public CoreGame()
        {
            Graphics = new GraphicsDeviceManager(this) { SynchronizeWithVerticalRetrace = false };
            DataLoader = new DataLoader();
        }

        public T SetLevel<T>(T level) where T: GameLevel
        {
            Level = level;
            return level;
        }

        protected override void Initialize()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }
    }
}
