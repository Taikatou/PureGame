using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Engine.Controllers;
using PureGame.Engine.EntityData;
using PureGame.Render.Renderable;

namespace PureGame.Droid
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private IPureGameRenderer GameRenderer;
        public int Width = 800, Height = 480;
        private EntityObject player_entity;

        private IController controller;

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
            var g = new PlainPureGame(Content);
            g.LoadWorld("level01.json", new FileReader());
            var g2 = new PlainPureGameRenderer(g, viewport_adapter);
            GameRenderer = new PlainPureGameRendererDebug(g2);
            player_entity = new EntityObject(new Vector2(4, 4), "Test");
            GameRenderer.ChangeFocus(player_entity);
            GameRenderer.Game.Current.Entities.Add(player_entity);
            controller = new PhysicalController();
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
            controller.Update(player_entity, time);
            GameRenderer.Draw(spriteBatch);
        }
    }
}
