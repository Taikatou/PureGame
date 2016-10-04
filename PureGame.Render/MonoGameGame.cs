using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Client.Controllers;
using PureGame.Client.Renderable;
using PureGame.Content.WorldAreas;
using PureGame.Engine;
using PureGame.Engine.EntityData;

namespace PureGame.Client
{
    public class MonoGameGame : Game
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private SpriteBatch _spriteBatch;
        private PlainPureGameRenderer _gameRenderer;
        private PureGameClient _gameClient;
        public PureGame PureGame;
        private IControllerSettings _controllerSettings;

        public MonoGameGame(IControllerSettings controllerSettings)
        {
            _controllerSettings = controllerSettings;
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            ContentManagerFactory.Initialise(Content);
        }

        protected override void LoadContent()
        {
            var viewportAdapter = new DefaultViewportAdapter(_graphicsDeviceManager.GraphicsDevice);
            _spriteBatch = new SpriteBatch(_graphicsDeviceManager.GraphicsDevice);
            PureGame = new PureGame();
            var player = EntityFactory.MakeEntityObject(new Point(4, 4), "CharacterSheet");
            _gameClient = new PureGameClient(player, PureGame);
            _gameRenderer = new PlainPureGameRenderer(_gameClient, viewportAdapter, player, _controllerSettings, 0.25f);
            PureGame.WorldManager.OnWorldLoad += (sender, args) => _gameRenderer.LoadWorld();
            PureGame.WorldManager.AddEntity<BasicWorld>(player);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _gameRenderer.Draw(_spriteBatch);
        }

        protected override void Update(GameTime gameTime)
        {
            PureGame.Update(gameTime);
            _gameClient.Update(gameTime);
            _gameRenderer.Update(gameTime);
        }
    }
}
