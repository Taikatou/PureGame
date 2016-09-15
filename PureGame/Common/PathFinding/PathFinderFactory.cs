using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PureGame.Common.PathFinding.BasicAStar;
using PureGame.Common.PathFinding.EpPathFinding;

namespace PureGame.Common.PathFinding
{
    public enum Severity { Entity, Npc, Player }
    public class PathFinderFactory
    {
        public static IPathfinder MakePathFinder(SearchParameters searchParameters, Severity severity=Severity.Entity)
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

        public static List<Point> FindPath(SearchParameters searchParameters, Severity severity=Severity.Entity)
        {
            var finder = MakePathFinder(searchParameters, severity);
            return finder.FindPath();
        }
    }
}
