using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PureGame.Common
{
    public interface IPathfinder
    {
        List<Point> FindPath();
    }
}
