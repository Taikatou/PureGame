using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Render;
using PureGame.Render.Renderable;
using System.Diagnostics;

namespace PureGame.DesktopGl
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        IPureGameRenderer game;
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
            var g = new PlainPureGame(Content);
            g.LoadWorld("level01.json", new FileReader());
            var g2 = new PlainPureGameRenderer(g, viewport_adapter, Content);
            game = new PlainPureGameRendererDebug(g2, Content);
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            game.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            game.Draw(spriteBatch);
        }
    }
}
