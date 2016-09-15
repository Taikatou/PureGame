using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Engine.EntityData;
using PureGame.Render;
using PureGame.Render.Renderable;
using PureGame.WorldAreas;

namespace PureGame.Droid.Screens
{
    public class GameScreen : Screen
    {
        protected ContentManager Content { get; }
        private readonly PlainPureGameRenderer _gameRenderer;
        private readonly PureGameClient _gameClient;
        private readonly SpriteBatch _spriteBatch;
        public PureGame PureGame;
        public GameScreen(IServiceProvider serviceProvider)
        {
            Content = new ContentManager(serviceProvider, "Content");
            var graphicsDeviceService = (IGraphicsDeviceService)serviceProvider.GetService(typeof(IGraphicsDeviceService));
            var viewportAdapter = new DefaultViewportAdapter(graphicsDeviceService.GraphicsDevice);
            _spriteBatch = new SpriteBatch(graphicsDeviceService.GraphicsDevice);
            PureGame = new PureGame(Content);
            var player = EntityFactory.MakeEntityObject(new Point(4, 4), "CharacterSheet");
            _gameClient = new PureGameClient(player, PureGame);
            _gameRenderer = new PlainPureGameRenderer(_gameClient, viewportAdapter, player, 0.25f);
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
