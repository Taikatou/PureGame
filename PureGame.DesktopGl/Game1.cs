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
        private PlainPureGameRenderer _gameRenderer;
        private PureGameClient _gameClient;
        public int Width = 800, Height = 480;
        public PureGame PureGame;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            var viewportAdapter = new DefaultViewportAdapter(GraphicsDevice);
            PureGame = new PureGame(Content, new FileReader("Data"));
            var player = EntityFactory.MakeEntityObject(new Vector2(4, 4), "CharacterSheet");
            _gameClient = new PureGameClient(PureGame, player);
            _gameRenderer = new PlainPureGameRenderer(_gameClient, viewportAdapter, player);
            PureGame.WorldManager.OnWorldLoad += (sender, args) => _gameRenderer.LoadWorld();
            PureGame.WorldManager.AddEntity(player, "level01.json");
        }
        protected override void UnloadContent()
        {
            Content.Unload();
        }
        protected override void Update(GameTime gameTime)
        {
            PureGame.Update(gameTime);
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
