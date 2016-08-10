using Microsoft.Xna.Framework;
using PureGame.AbstractEngine;
using System.Collections.Generic;

namespace PureGame.Engine
{
    public class EntityManager
    {
        public readonly List<AbstractEntityObject> entities;
        public readonly Dictionary<string, AbstractEntityObject> id_hash;
        public readonly Dictionary<Vector2, AbstractEntityObject> spatial_hash;

        private Dictionary<string, AbstractEntityObject> cached_positions;

        public EntityManager()
        {
            entities = new List<AbstractEntityObject>();
            id_hash = new Dictionary<string, AbstractEntityObject>();
            spatial_hash = new Dictionary<Vector2, AbstractEntityObject>();
            cached_positions = new Dictionary<string, AbstractEntityObject>();
        }

        public void AddEntity(AbstractEntityObject e)
        {
            if(!spatial_hash.ContainsKey(e.Position))
            {
                entities.Add(e);
                spatial_hash[e.Position] = e;
                id_hash[e.Id] = e;
            }
        }

        public void Update(GameTime timer)
        {
            foreach(var e in entities)
            {
                bool contains_key = cached_positions.ContainsKey(e.Id);
                if (!contains_key)
                {
                    cached_positions[e.Id] = e;
                }
                else
                {

                }
            }
        }
    }
}
