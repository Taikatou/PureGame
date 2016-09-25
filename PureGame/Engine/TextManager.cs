using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using System.Collections.Generic;
using PureGame.Engine.Communication;

namespace PureGame.Engine
{
    public class TextManager
    {
        private readonly List<ITextBox> _talkingEntities;
        private readonly Dictionary<ITextBox, IEntity> _entityDict;

        public TextManager()
        {
            _talkingEntities = new List<ITextBox>();
            _entityDict = new Dictionary<ITextBox, IEntity>();
        }

        public void Update(GameTime time)
        {
            var toRemoveList = new List<ITextBox>();
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
                var entity = _entityDict[toRemove];
                entity.Talking = false;
                Remove(entity.Interaction);
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

        public void Remove(ITextBox toRemove)
        {
            _talkingEntities.Remove(toRemove);
            _entityDict.Remove(toRemove);
        }
    }
}
