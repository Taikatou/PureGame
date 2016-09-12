using Microsoft.Xna.Framework;
using PureGame.Engine;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;

namespace PureGame.Render.Renderable.WorldRenderer
{
    public class EntityPositionFinder
    {
        public Vector2 TileSize;
        public WorldArea World;
        public Vector2 Offset;
        public EntityPositionFinder(WorldArea world, Vector2 tileSize)
        {
            World = world;
            TileSize = tileSize;
            Offset = tileSize / 2;
        }

        public Vector2 GetEntityScreenPosition(Entity entity)
        {
            var position = entity.Position;
            var worldData = World.EntityManager;
            if (worldData.EntityCurrentlyMoving(entity))
            {
                var progress = worldData.EntityToKey[entity].Progress;
                var facingPosition = DirectionMapper.GetMovementFromDirection(entity.FacingDirection);
                position -= facingPosition * progress;
            }
            return GetScreenPosition(position);
        }

        public Vector2 GetScreenPosition(Vector2 pos)
        {
            var position = pos * TileSize;
            return position;
        }
    }
}
