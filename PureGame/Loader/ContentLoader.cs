using Microsoft.Xna.Framework.Content;
using PureGame.Engine;

namespace PureGame.Loader
{
    public class ContentLoader
    {
        public static void LoadContentManager(ContentManager content)
        {
            if (ContentManagerManager.Instance == null)
            {
                ContentManagerManager.Instance = new ContentManagerManager(content);
            }
        }
    }
}
