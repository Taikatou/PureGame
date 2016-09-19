using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace PureGame.Common
{
    public class UpdateList <T> where T : IUpdate
    {
        private readonly List<T> _currentElements;
        private readonly List<T> _newElements;

        public UpdateList()
        {
            _currentElements = new List<T>();
            _newElements = new List<T>();
        }

        public void Update(GameTime time)
        {
            foreach (var element in _currentElements)
            {
                element.Update(time);
            }
            foreach (var newElement in _newElements)
            {
                _currentElements.Add(newElement);
            }
            _newElements.Clear();
        }

        public void Add(T newElement)
        {
            _newElements.Add(newElement);
        }
    }
}
