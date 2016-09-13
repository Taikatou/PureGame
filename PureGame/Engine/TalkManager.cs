using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using System.Collections.Generic;

namespace PureGame.Engine
{
    public class TalkManager
    {
        private readonly List<EntityMessage> _talkingEntities;

        public TalkManager()
        {
            _talkingEntities = new List<EntityMessage>();
        }

        public void Update(GameTime time)
        {
            for(var i = 0; i < _talkingEntities.Count; i++)
            {
                var talkingEntity = _talkingEntities[i];
                talkingEntity.Update(time);
                if (talkingEntity.Complete)
                {
                    _talkingEntities.Remove(talkingEntity);
                }
            }
        }

        public void StartTalking(Entity entity)
        {
            var talkingEntity = new EntityMessage(entity, 500);
            _talkingEntities.Add(talkingEntity);
        }
    }
}
