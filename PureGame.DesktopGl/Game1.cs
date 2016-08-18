using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Client;
using PureGame.Engine.Controllers;
using PureGame.Engine.EntityData;
using PureGame.Render.Renderable;

namespace PureGame.DesktopGl
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private IPureGameRenderer GameRenderer;
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
            var game = new PlainPureGame(Content);
            var game_client = new PureGameClient(game);
            var game_renderer = new PlainPureGameRenderer(game_client, viewport_adapter);
            GameRenderer = new PlainPureGameRendererDebug(game_renderer);
            game.LoadWorld("level01.json", new FileReader());
            var player_entity = new EntityObject(new Vector2(4, 4), "Test");
            game_client.SetPlayer(player_entity, new PhysicalController());
            GameRenderer.ChangeFocus(player_entity);
        }

        protected override void UnloadContent()
        {
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
