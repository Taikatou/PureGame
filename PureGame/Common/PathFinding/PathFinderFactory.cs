using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PureGame.Common.PathFinding.BasicAStar;
using PureGame.Common.PathFinding.Dijkstra;

namespace PureGame.Common.PathFinding
{
    public enum Severity { Entity, Npc, Player }
    public class PathFinderFactory
    {
        public static IPathfinder MakePathFinder(SearchParameters searchParameters, Severity severity)
        {
            IPathfinder toReturn;
            switch(severity)
            {
                case Severity.Entity:
                    toReturn = new AStarPathFinder(searchParameters);
                    break;
                case Severity.Npc:
                    toReturn = new AStarPathFinder(searchParameters);
                    break;
                case Severity.Player:
                    toReturn = new DijkstraPathFinder(searchParameters);
                    break;
                default:
                    toReturn = new AStarPathFinder(searchParameters);
                    break;
            }
            return toReturn;
        }

        public static List<Point> FindPath(SearchParameters searchParameters, Severity severity)
        {
            var finder = MakePathFinder(searchParameters, severity);
            return finder.FindPath();
        }
    }
}
