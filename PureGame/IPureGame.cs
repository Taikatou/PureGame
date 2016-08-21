using Microsoft.Xna.Framework;
using PureGame.Engine;
using PureGame.Engine.EntityData;

namespace PureGame
{
    public interface IPureGame
    {
        void LoadWorld(string world_name);
        void Update(GameTime time);
        WorldArea World { get; set; }
        IPureGame Parent { get; set; }
        void OnWorldChange();
    }
}
