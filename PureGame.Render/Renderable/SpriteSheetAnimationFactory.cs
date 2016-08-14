using MonoGame.Extended.TextureAtlases;

namespace PureGame.Render.Renderable
{
    internal class SpriteSheetAnimationFactory
    {
        private TextureAtlas atlas;

        public SpriteSheetAnimationFactory(TextureAtlas atlas)
        {
            this.atlas = atlas;
        }
    }
}