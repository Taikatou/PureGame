using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine.Common;
using MonoGame.Extended.Maps.Tiled;
using PureGame.Engine.EntityData;
using System.Diagnostics;
using PureGame.Engine.Controllers;
using PureGame.SmallGame;
using PureGame.MessageBus;
using System;

namespace PureGame.Engine
{
    public class WorldArea : GameLevel, ISubscriber
    {
        public ContentManager Content;
        public CollisionTiledMap CollisionMap;
        public WorldEntityManager EntityManager;
        public List<EntityObject> Entities => EntityManager.Entities;
        public MapObject Map => Objects.GetObjects<MapObject>()[0];
        public WorldArea()
        {
            Content = ContentManagerManager.RequestContentManager();
            EntityManager = new WorldEntityManager();
        }

        public void Update(GameTime time)
        {
            EntityManager.Update(time);
        }

        public void ProccessInteraction(EntityObject e)
        {
            if (!EntityManager.EntityCurrentlyMoving(e))
            {
                var FacingPosition = DirectionMapper.GetMovementFromDirection(e.FacingDirection);
                Vector2 new_position = e.Position + FacingPosition;
                if (EntityManager.SpatialHash.ContainsKey(new_position))
                {
                    EntityObject interact_entity = EntityManager.SpatialHash[new_position];
                    e.Interact(interact_entity);
                }
            }
        }

        public void ProccessMovement(EntityObject e)
        {
            if (!EntityManager.EntityCurrentlyMoving(e))
            {
                Debug.WriteLine("in");
                var MovementPosition = DirectionMapper.GetMovementFromDirection(e.MovementDirection);
                Vector2 new_position = e.Position + MovementPosition;
                if (ValidPosition(new_position))
                {
                    var movement_key = new ExpiringKey<Vector2>(e.Position, e.GetSpeed());
                    EntityManager.AddEntityKey(e, movement_key);
                    e.Position = new_position;
                }
                e.FacingDirection = e.MovementDirection;
            }
        }

        public void AddEntity(EntityObject e)
        {
            EntityManager.AddEntity(e);
            e.SubscriptionName = Name;
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
            MessageManager.Instance.Subscribe(Name, this);
            TiledMap map = Map.GetTiledMap(Content);
            CollisionMap = new CollisionTiledMap(map);
            foreach(EntityObject e in Objects.GetObjects<EntityObject>())
            {
                AddEntity(e);
            }
        }

        public void RecieveMessage(Message m)
        {
            EntityObject.MessageCode code = (EntityObject.MessageCode)m.Code;
            switch (code)
            {
                case EntityObject.MessageCode.RequestInteraction:
                    ProccessInteraction(EntityManager.IdHash[m.Value]);
                    break;

                case EntityObject.MessageCode.RequestMovement:
                    ProccessMovement(EntityManager.IdHash[m.Value]);
                    break;
            }
        }
    }
}
