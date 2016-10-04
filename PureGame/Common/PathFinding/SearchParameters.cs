using Microsoft.Xna.Framework;
using PureGame.Engine.World;

namespace PureGame.Common.PathFinding
{
    public class SearchParameters
    {
        public Point StartLocation { get; set; }

        public Point EndLocation { get; set; }

        public WorldArea Map { get; set; }

        public SearchParameters(Point startLocation, Point endLocation, WorldArea world)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
            Map = world;
        }

        public SearchParameters(Vector2 startLocation, Vector2 endLocation, WorldArea world) : this(startLocation.ToPoint(), endLocation.ToPoint(), world)
        {
        }
    }
}
