using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Engine.EntityData;
using PureGame.Render;
using PureGame.Render.Renderable;

namespace PureGame.DesktopGl.Screens
{
    public class GameScreen : Screen
    {
        protected ContentManager Content { get; }
        private readonly PlainPureGameRenderer _gameRenderer;
        private readonly SpriteBatch _spriteBatch;
        public PureGame PureGame;
        public GameScreen(IServiceProvider serviceProvider)
        {
            Content = new ContentManager(serviceProvider, "Content");
            var graphicsDeviceService = (IGraphicsDeviceService)serviceProvider.GetService(typeof(IGraphicsDeviceService));
            var viewportAdapter = new DefaultViewportAdapter(graphicsDeviceService.GraphicsDevice);
            _spriteBatch = new SpriteBatch(graphicsDeviceService.GraphicsDevice);
            PureGame = new PureGame(Content, new FileReader("Data"));
            var player = EntityFactory.MakeEntityObject(new Vector2(4, 4), "CharacterSheet");
            var gameClient = new PureGameClient(player, PureGame);
            _gameRenderer = new PlainPureGameRenderer(gameClient, viewportAdapter, player);
            PureGame.WorldManager.OnWorldLoad += (sender, args) => _gameRenderer.LoadWorld();
            PureGame.WorldManager.AddEntity(player, "level01.json");
        }

        public override void Update(GameTime gameTime)
        {
            PureGame.Update(gameTime);
            _gameRenderer.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            _gameRenderer.Draw(_spriteBatch);
        }
    }
}
