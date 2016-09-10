using Microsoft.Xna.Framework;

namespace PureGame.Common
{
    public interface IFocusable
    {
        Vector2 Position { get; }
        Vector2 FinalPosition { get; }
    }
}
