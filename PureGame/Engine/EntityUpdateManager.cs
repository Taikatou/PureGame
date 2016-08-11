using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PureGame.Engine
{
    public class EntityUpdateManager
    {
        public readonly List<EntityObject> entities;
        private readonly List<ExpiringKey<Vector2>> expiring_tiles;
        public readonly Dictionary<string, EntityObject> id_hash;
        public readonly Dictionary<Vector2, EntityObject> spatial_hash;
        private List<EntityMover> movers;

        public EntityUpdateManager(List<EntityObject> entities, List<EntityMover> movers)
        {
            this.movers = movers;
            this.entities = entities;
            id_hash = new Dictionary<string, EntityObject>();
            expiring_tiles = new List<ExpiringKey<Vector2>>();
            spatial_hash = new Dictionary<Vector2, EntityObject>();
            foreach(EntityObject e in this.entities)
            {
                id_hash[e.Id] = e;
                spatial_hash[e.Position] = e;
            }
        }

        public void AddEntity(EntityObject e)
        {
            if(!(spatial_hash.ContainsKey(e.Position) || id_hash.ContainsKey(e.Id)))
            {
                entities.Add(e);
                spatial_hash[e.Position] = e;
                id_hash[e.Id] = e;
            }
        }

        public void Update(GameTime timer)
        {
            Vector2 previous_position;
            foreach(EntityMover m in movers)
            {
                previous_position = m.Position;
                m.Update(timer);
                if(previous_position != m.Position)
                {
                    if(!spatial_hash.ContainsKey(m.Position))
                    {
                        spatial_hash[m.Position] = m.Entity;
                        expiring_tiles.Add(new ExpiringKey<Vector2>(previous_position, 350));
                    }
                    else
                    {
                        m.Position = previous_position;
                    }
                }
            }
            foreach(ExpiringKey<Vector2> k in expiring_tiles)
            {
                k.Update(timer);
                if(k.TimeLeft <= 0)
                {
                    expiring_tiles.Remove(k);
                }
            }
        }
    }
}
