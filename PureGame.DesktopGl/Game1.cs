using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Client;
using PureGame.Client.Controllers;
using PureGame.Engine.Controllers;
using PureGame.Engine.EntityData;
using PureGame.Render.Renderable;
using PureGame.Universe;

namespace PureGame.DesktopGl
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private IPureGameRenderer GameRenderer;
        private PureGameClient GameClient;
        public int Width = 800, Height = 480;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            BoxingViewportAdapter viewport_adapter = new BoxingViewportAdapter(Window, GraphicsDevice, Width, Height);
            var game = new PlainPureGame(Content, new FileReader("Data"), new WorldManager());
            BaseEntityObject Player = new BaseEntityObject(new Vector2(4, 4), "Test", "CharacterSheet", Direction.Down);
            GameClient = new PureGameClient(game, Player, new KeyBoardController(game));
            var game_renderer = new PlainPureGameRenderer(GameClient, viewport_adapter);
            GameRenderer = new PlainPureGameRendererDebug(game_renderer);
            LoadWorld("level01.json");
        }

        public virtual void LoadWorld(string world_name)
        {
            GameRenderer.Game.LoadWorld(world_name);
            GameRenderer.ChangeFocus(GameClient.Player);
            GameRenderer.Game.World.AddEntity(GameClient.Player);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            GameRenderer.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.Black);
            GameRenderer.Draw(spriteBatch);
        }
    }
}
