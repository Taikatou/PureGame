using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine.Common;
using MonoGame.Extended.Maps.Tiled;
using PureGame.Engine.EntityData;
using Newtonsoft.Json;
using System.Diagnostics;
using PureGame.Engine.Controllers;

namespace PureGame.Engine
{
    public class WorldArea
    {
        public ContentManager Content;
        public CollisionTiledMap CollisionMap;
        public WorldEntityManager Data;
        public string Name;
        public List<IEntity> Entities => Data.Entities;
        public MapObject Map;
        public WorldArea(string world_name, IFileReader reader)
        {
            Name = world_name;
            Content = ContentManagerManager.RequestContentManager();
            string json_string = reader.ReadAllText(world_name);
            Map = JsonConvert.DeserializeObject<MapObject>(json_string);
            Data = new WorldEntityManager();
            TiledMap map = Map.GetTiledMap(Content);
            CollisionMap = new CollisionTiledMap(map);
        }

        public void AddEntity(IEntity e)
        {
            Data.AddEntity(e);
        }

        public void Update(GameTime time)
        {
            foreach (var e in Data.Entities)
            {
                if (e.RequestMovement && !Data.EntityCurrentlyMoving(e))
                {
                    ProccessMovement(e);
                }
                if (e.RequestInteraction)
                {
                    ProccessInteraction(e);
                }
            }
            Data.Update(time);
        }

        public void ProccessInteraction(IEntity e)
        {
            var FacingPosition = DirectionMapper.GetMovementFromDirection(e.FacingDirection);
            Vector2 new_position = e.Position + FacingPosition;
            if (Data.SpatialHash.ContainsKey(new_position))
            {
                IEntity interact_entity = Data.SpatialHash[new_position];
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
                Data.AddEntityKey(e, movement_key);
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
            bool entity_collision = !Data.SpatialHash.ContainsKey(position);
            bool map_collision = !CollisionMap.CheckCollision(position);
            Debug.WriteLine("Map collision: " + map_collision);
            return within_limits && entity_collision && map_collision;
        }

        public void UnLoad()
        {
            Content?.Unload();
        }
    }
}
