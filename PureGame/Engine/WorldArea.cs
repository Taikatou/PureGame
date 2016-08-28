using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine.EntityData;
using System.Diagnostics;
using PureGame.Engine.Controllers;
using PureGame.SmallGame;
using PureGame.MessageBus;

namespace PureGame.Engine
{
    public class WorldArea : GameLevel, ISubscriber
    {
        public enum MessageCode { Refresh }
        public ContentManager Content;
        public WorldEntityManager EntityManager;
        private string _subscriptionIn => Name + "In";
        public string SubscriptionOut;
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
                var facingPosition = DirectionMapper.GetMovementFromDirection(e.FacingDirection);
                Vector2 newPosition = e.Position + facingPosition;
                if (EntityManager.SpatialHash.ContainsKey(newPosition))
                {
                    EntityObject interactEntity = EntityManager.SpatialHash[newPosition];
                    e.Interact(interactEntity);
                }
            }
        }

        public void ProccessMovement(EntityObject e)
        {
            if (!EntityManager.EntityCurrentlyMoving(e))
            {
                var movementPosition = DirectionMapper.GetMovementFromDirection(e.MovementDirection);
                Vector2 newPosition = e.Position + movementPosition;
                if (ValidPosition(newPosition))
                {
                    var movementKey = new ExpiringKey<Vector2>(e.Position, e.GetSpeed());
                    EntityManager.AddEntityKey(e, movementKey);
                    e.Position = newPosition;
                }
                e.FacingDirection = e.MovementDirection;
            }
        }
        public void AddEntity(EntityObject e)
        {
            EntityManager.AddEntity(e);
            e.SubscriptionName = _subscriptionIn;
            SendRefresh();
        }

        private bool ValidPosition(Vector2 position)
        {
            bool withinLimits = position.X >= 0 && position.Y >= 0 &&
                                 position.X < Map.Map.Width &&
                                 position.Y < Map.Map.Height;
            bool entityCollision = !EntityManager.SpatialHash.ContainsKey(position);
            bool mapCollision = !Map.CheckCollision(position);
            Debug.WriteLine("Map collision: " + mapCollision);
            return withinLimits && entityCollision && mapCollision;
        }

        public void UnLoad()
        {
            Content?.Unload();
            Map?.UnLoad();
        }

        public void OnInit(string subscriptionOut)
        {
            SubscriptionOut = subscriptionOut;
            MessageManager.Instance.Subscribe(_subscriptionIn, this);
            Map.OnInit();
            foreach(EntityObject e in Objects.GetObjects<EntityObject>())
            {
                AddEntity(e);
            }
        }

        public void RecieveMessage(Message m)
        {
            var code = (EntityObject.MessageCode)m.Code;
            switch (code)
            {
                case EntityObject.MessageCode.RequestInteraction:
                    ProccessInteraction(EntityManager.IdHash[m.Value]);
                    break;

                case EntityObject.MessageCode.RequestMovement:
                    ProccessMovement(EntityManager.IdHash[m.Value]);
                    break;
            }
            SendRefresh();
        }

        public void SendRefresh()
        {
            const int code = (int)(MessageCode.Refresh);
            Message messageOut = new Message(code, "");
            MessageManager.Instance.SendMessage(SubscriptionOut, messageOut);
        }

        public void Dispose()
        {
            MessageManager.Instance.UnSubscribe(Name, this);
        }
    }
}
