using Microsoft.Xna.Framework;
using PureGame.Engine;

namespace PureGame
{
    public interface IPureGame
    {
        void LoadWorld(string world_name, IFileReader reader);
        void Update(GameTime time);
        WorldArea Current { get; set; }
        IPureGame Parent { get; set; }
        void OnWorldChange();
    }
}
