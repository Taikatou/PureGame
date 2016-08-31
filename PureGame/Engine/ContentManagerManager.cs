using Microsoft.Xna.Framework.Content;

namespace PureGame.Engine
{
    public class ContentManagerManager
    {
        public static ContentManagerManager Instance;

        private readonly ContentManager _baseManager;

        public ContentManagerManager(ContentManager baseManager)
        {
            _baseManager = baseManager;
        }

        public static ContentManager RequestContentManager()
        {
            ContentManager baseContent = Instance._baseManager;
            ContentManager newManager = new ContentManager(baseContent.ServiceProvider, baseContent.RootDirectory);
            return newManager;
        }
    }
}
