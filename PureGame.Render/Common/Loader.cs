using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PureGame.Render.Common
{
    public class AssetLoader
    {
        public static Texture2D LoadTexture(ContentManager content, string fileName)
        {
            string pathName = $"Images/{fileName}";
            Texture2D texture = content.Load<Texture2D>(pathName);
            return texture;
        }
    }
}
