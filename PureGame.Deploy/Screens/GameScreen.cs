using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Engine;
using PureGame.Engine.EntityData;
using PureGame.Client;
using PureGame.Client.Renderable;
using PureGame.Content.WorldAreas;
using PureGame.Client.Controllers;

namespace PureGame.Deploy.Screens
{
    public class GameScreen : Screen
    {
        protected ContentManager Content { get; }
        private readonly PlainPureGameRenderer _gameRenderer;
        private readonly PureGameClient _gameClient;
        private readonly SpriteBatch _spriteBatch;
        public PureGame PureGame;
        public GameScreen(IServiceProvider serviceProvider, IControllerSettings settings)
        {
            Content = new ContentManager(serviceProvider, "Content");
            var graphicsDeviceService = (IGraphicsDeviceService)serviceProvider.GetService(typeof(IGraphicsDeviceService));
            var viewportAdapter = new DefaultViewportAdapter(graphicsDeviceService.GraphicsDevice);
            _spriteBatch = new SpriteBatch(graphicsDeviceService.GraphicsDevice);
            ContentManagerManager.Instance = new ContentManagerManager(Content);
            PureGame = new PureGame();
            var player = EntityFactory.MakeEntityObject(new Point(4, 4), "CharacterSheet");
            _gameClient = new PureGameClient(player, PureGame);
            _gameRenderer = new PlainPureGameRenderer(_gameClient, viewportAdapter, player, settings,  0.25f);
            PureGame.WorldManager.OnWorldLoad += (sender, args) => _gameRenderer.LoadWorld();
            PureGame.WorldManager.AddEntity<BasicWorld>(player);
        }

        public override void Update(GameTime gameTime)
        {
            PureGame.Update(gameTime);
            _gameClient.Update(gameTime);
            _gameRenderer.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            _gameRenderer.Draw(_spriteBatch);
        }
    }
}
