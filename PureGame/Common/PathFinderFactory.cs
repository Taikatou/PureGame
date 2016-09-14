using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PureGame.Common
{
    public enum Severity { entity, npc, player }
    public class PathFinderFactory
    {
        public static IPathfinder MakePathFinder(SearchParameters searchParameters, Severity severity=Severity.entity)
        {
            IPathfinder toReturn;
            switch(severity)
            {
                default:
                    toReturn = new AStarPathFinder(searchParameters);
                    break;
            }
            return toReturn;
        }

        public static List<Point> FindPath(SearchParameters searchParameters, Severity severity=Severity.entity)
        {
            IPathfinder finder = MakePathFinder(searchParameters, severity);
            return finder.FindPath();
        }
    }
}
