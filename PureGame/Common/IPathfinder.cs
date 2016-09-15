using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PureGame.Common
{
    public interface IPathfinder
    {
        /// <summary>
        /// Attempts to find a path from the start location to the end location based on the supplied SearchParameters
        /// </summary>
        /// <returns>A List of Points representing the path. If no path was found, the returned list is empty.</returns>
        List<Point> FindPath();
    }
}
