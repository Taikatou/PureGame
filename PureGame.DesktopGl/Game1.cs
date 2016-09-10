using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using PureGame.DesktopGl.Screens;
using PureGame.DesktopGl.Screens.MenuScreens;

namespace PureGame.DesktopGl
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

            ScreenComponent.Register(new MainMenuScreen(Services, this));
            ScreenComponent.Register(new LoadGameScreen(Services));
            ScreenComponent.Register(new OptionsScreen(Services));
            ScreenComponent.Register(new AudioOptionsScreen(Services));
            ScreenComponent.Register(new VideoOptionsScreen(Services));
            ScreenComponent.Register(new KeyboardOptionsScreen(Services));
            ScreenComponent.Register(new MouseOptionsScreen(Services));
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