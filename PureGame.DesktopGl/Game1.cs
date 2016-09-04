using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Client;
using PureGame.Engine.EntityData;
using PureGame.Render.Renderable;

namespace PureGame.DesktopGl
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private PlainPureGameRendererDebug _gameRenderer;
        private PureGameClient _gameClient;
        public int Width = 800, Height = 480;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            BoxingViewportAdapter viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, Width, Height);
            var game = new PureGame(Content, new FileReader("Data"));
            EntityObject player = new EntityObject(new Vector2(4, 4), "CharacterSheet");
            _gameClient = new PureGameClient(game, player);
            var gameRenderer = new PlainPureGameRenderer(_gameClient, viewportAdapter, player);
            _gameRenderer = new PlainPureGameRendererDebug(gameRenderer);
            _gameRenderer.ChangeFocus(player);
            game.WorldManager.OnWorldLoad += (sender, args) => gameRenderer.LoadWorld();
            game.WorldManager.AddEntity(player, "level01.json");
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }
        protected override void Update(GameTime gameTime)
        {
            _gameRenderer.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.Black);
            _gameRenderer.Draw(_spriteBatch);
        }
    }
}
