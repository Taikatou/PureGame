using Microsoft.Xna.Framework;
using PureGame.Engine;
using SmallGame;

namespace PureGame
{
    public interface IPureGame
    {
        void LoadWorld(string world_name, IFileReader reader);
        void Update(GameTime time);
        WorldArea Current { get; set; }
        DataLoader DataLoader { get; set; }
    }
}
