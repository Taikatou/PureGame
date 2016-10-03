using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using System.Collections.Generic;
using PureGame.Engine.Communication;
using System;

namespace PureGame.Engine
{
    public class TextManager
    {
        private readonly List<IInteraction> _talkingEntities;
        private readonly Dictionary<IInteraction, IEntity> _entityDict;

        public TextManager()
        {
            _talkingEntities = new List<IInteraction>();
            _entityDict = new Dictionary<IInteraction, IEntity>();
        }

        public void Update(GameTime time)
        {
            var toRemoveList = new List<IInteraction>();
            foreach(var textBox in _talkingEntities)
            {
                textBox.Update(time);
                if (textBox.Complete)
                {
                    toRemoveList.Add(textBox);
                }
            }
            foreach (var toRemove in toRemoveList)
            {
                Remove(toRemove);
            }
        }

        public void Interact(IEntity entity)
        {
            if (!entity.Talking)
            {
                entity.Talking = true;
                var textBox = entity.Interaction;
                _entityDict[textBox] = entity;
                _talkingEntities.Add(textBox);
            }
            else
            {
                entity.Talking = false;
                var textBox = _talkingEntities.Find(x => x.Entity.Id == entity.Id);
                if (textBox != null)
                {
                    Remove(textBox);
                }
            }
        }

        public void Remove(IInteraction toRemove)
        {
            var entity = _entityDict[toRemove];
            entity.Talking = false;
            _talkingEntities.Remove(toRemove);
            _entityDict.Remove(toRemove);
        }
    }
}
