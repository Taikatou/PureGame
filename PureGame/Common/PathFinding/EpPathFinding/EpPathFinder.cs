using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using PureGame.Common.PathFinding.EpPathFinding.Grid;

namespace PureGame.Common.PathFinding.EpPathFinding
{
    class EpPathFinder : IPathfinder
    {
        private readonly BaseGrid _searchGrid;
        private readonly SearchParameters _searchParameters;
        public int Width => _searchParameters.Map.MapWidth;
        public int Height => _searchParameters.Map.MapHeight;
        public List<Point> FindPath()
        {
            var startPos = _searchParameters.StartLocation;
            var endPos = _searchParameters.EndLocation;
            var jpParam = new JumpPointParam(_searchGrid, startPos, endPos, true, false);
            var resultPathList = JumpPointFinder.FindPath(jpParam);
            Debug.WriteLine(resultPathList.Count);
            return resultPathList;
        }

        public EpPathFinder(SearchParameters searchParameters)
        {
            _searchParameters = searchParameters;
            _searchGrid = new StaticGrid(Width, Height);
            var world = searchParameters.Map;
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    if (world.ValidPosition(i, j))
                    {
                        _searchGrid.SetWalkableAt(i, j, true);
                    }
                }
            }
        }
    }
}
