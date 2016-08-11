using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PureGame.Render
{
    public class PlainPureGameRenderer : IPureGameRenderer
    {
        private PlainPureGame game;
        private RenderWorld renderer;
        private ContentManager content;
        public PlainPureGameRenderer(PlainPureGame game, ContentManager content)
        {
            this.game = game;
            this.content = content;
            renderer = new RenderWorld(game.Current, content);
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            sprite_batch.Begin();
            renderer.Draw(sprite_batch);
            sprite_batch.End();
        }

        public void Update(GameTime timer)
        {
            game.Update(timer);
            if(renderer.WorldName != game.Current.Name)
            {
                renderer.UnLoad();
                renderer = new RenderWorld(game.Current, content);
            }
        }
    }
}
