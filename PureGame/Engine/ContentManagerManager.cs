using Microsoft.Xna.Framework.Content;

namespace PureGame.Engine
{
    public class ContentManagerManager
    {
        public static ContentManagerManager Instance;

        ContentManager base_manager;

        public ContentManagerManager(ContentManager base_manager)
        {
            this.base_manager = base_manager;
        }

        public static ContentManager RequestContentManager()
        {
            ContentManager base_content = Instance.base_manager;
            ContentManager new_manager = new ContentManager(base_content.ServiceProvider, base_content.RootDirectory);
            return new_manager;
        }
    }
}
