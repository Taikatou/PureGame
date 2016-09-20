using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using System.Collections.Generic;
using PureGame.Engine.Communication;

namespace PureGame.Engine
{
    public class TalkManager
    {
        private readonly List<ITextBox> _talkingEntities;
        private readonly Dictionary<ITextBox, IEntity> _entityDict;

        public TalkManager()
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
                _talkingEntities.Remove(toRemove);
                var entity = _entityDict[toRemove];
                entity.Talking = false;
                _entityDict.Remove(toRemove);
            }
        }

        public void StartTalking(IEntity entity)
        {
            var textBox = entity.Interaction;
            _entityDict[textBox] = entity;
            entity.Talking = true;
            _talkingEntities.Add(textBox);
        }
    }
}
