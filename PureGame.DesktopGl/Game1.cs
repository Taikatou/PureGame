using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Client;
using PureGame.Client.Controllers;
using PureGame.Engine.Controllers;
using PureGame.Engine.EntityData;
using PureGame.Render.Renderable;

namespace PureGame.DesktopGl
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private PlainPureGameRendererDebug GameRenderer;
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
            var game = new PureGame(Content, new FileReader("Data"));
            game.LoadWorld("level01.json");
            EntityObject Player = new EntityObject(new Vector2(4, 4), "Test", "CharacterSheet", Direction.Down);
            game.World.AddEntity(Player);
            GameClient = new PureGameClient(game, Player, new KeyBoardController());
            var game_renderer = new PlainPureGameRenderer(GameClient, viewport_adapter);
            GameRenderer = new PlainPureGameRendererDebug(game_renderer);
            GameRenderer.ChangeFocus(Player);
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
