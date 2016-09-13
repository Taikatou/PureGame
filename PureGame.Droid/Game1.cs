using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using PureGame.Droid.Screens;

namespace PureGame.Droid
{
    public class Game1 : Game
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        public ScreenComponent ScreenComponent;

        public Game1()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Components.Add(ScreenComponent = new ScreenComponent(this));
        }

        protected override void LoadContent()
        {
            ScreenComponent.Register(new GameScreen(Services));
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}