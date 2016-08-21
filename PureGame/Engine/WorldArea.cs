using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine.Common;
using MonoGame.Extended.Maps.Tiled;
using PureGame.Engine.EntityData;
using System.Diagnostics;
using PureGame.Engine.Controllers;
using PureGame.SmallGame;
using System;

namespace PureGame.Engine
{
    public class WorldArea : GameLevel
    {
        public ContentManager Content;
        public CollisionTiledMap CollisionMap;
        public WorldEntityManager EntityManager;
        public List<IEntity> Entities => EntityManager.Entities;
        public MapObject Map => Objects.GetObjects<MapObject>()[0];
        public WorldArea()
        {
            Content = ContentManagerManager.RequestContentManager();
            EntityManager = new WorldEntityManager();
        }

        public void AddEntity(IEntity e)
        {
            EntityManager.AddEntity(e);
        }

        public void Update(GameTime time)
        {
            foreach (var e in EntityManager.Entities)
            {
                if (e.RequestMovement && !EntityManager.EntityCurrentlyMoving(e))
                {
                    ProccessMovement(e);
                }
                if (e.RequestInteraction)
                {
                    ProccessInteraction(e);
                }
            }
            EntityManager.Update(time);
        }

        public void ProccessInteraction(IEntity e)
        {
            var FacingPosition = DirectionMapper.GetMovementFromDirection(e.FacingDirection);
            Vector2 new_position = e.Position + FacingPosition;
            if (EntityManager.SpatialHash.ContainsKey(new_position))
            {
                IEntity interact_entity = EntityManager.SpatialHash[new_position];
                e.Interact(interact_entity);
            }
        }

        public void ProccessMovement(IEntity e)
        {
            var MovementPosition = DirectionMapper.GetMovementFromDirection(e.MovementDirection);
            Vector2 new_position = e.Position + MovementPosition;
            if (ValidPosition(new_position))
            {
                var movement_key = new ExpiringKey<Vector2>(e.Position, e.Speed);
                EntityManager.AddEntityKey(e, movement_key);
                e.Position = new_position;
            }
            e.FacingDirection = e.MovementDirection;
            e.RequestMovement = false;
        }

        private bool ValidPosition(Vector2 position)
        {
            bool within_limits = position.X >= 0 && position.Y >= 0 &&
                                 position.X < CollisionMap.TiledMap.Width &&
                                 position.Y < CollisionMap.TiledMap.Height;
            bool entity_collision = !EntityManager.SpatialHash.ContainsKey(position);
            bool map_collision = !CollisionMap.CheckCollision(position);
            Debug.WriteLine("Map collision: " + map_collision);
            return within_limits && entity_collision && map_collision;
        }

        public void UnLoad()
        {
            Content?.Unload();
        }

        public override void OnInit()
        {
            TiledMap map = Map.GetTiledMap(Content);
            CollisionMap = new CollisionTiledMap(map);
            foreach(BaseEntityObject e in Objects.GetObjects<BaseEntityObject>())
            {
                EntityManager.AddEntity(e);
            }
        }
    }
}
