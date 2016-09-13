using System.Collections.Generic;

namespace PureGame.Render.Common
{
    public class ContainsList <T>
    {
        private readonly HashSet<T> _contains;
        public List<T> Elements;

        public ContainsList()
        {
            _contains = new HashSet<T>();
            Elements = new List<T>();
        }

        public bool Contains(T element) => _contains.Contains(element);

        public void Add(T element)
        {
            if (!Contains(element))
            {
                Elements.Add(element);
                _contains.Add(element);
            }
        }

        public void AddOrRemove(T element, bool which)
        {
            if (which)
            {
                Add(element);
            }
            else
            {
                Remove(element);
            }
        }

        public void Remove(T element)
        {
            if (Contains(element))
            {
                Elements.Remove(element);
                _contains.Remove(element);
            }
        }
    }
}
