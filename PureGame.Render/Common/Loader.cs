﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PureGame.Render.Common
{
    public class Loader
    {
        public static Texture2D LoadTexture(ContentManager content, string file_name)
        {
            string path_name = string.Format("Images/{0}", file_name);
            Texture2D texture = content.Load<Texture2D>(path_name);
            return texture;
        }
    }
}
