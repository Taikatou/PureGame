using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PureGame.SmallGame
{

    public class GameObjectCollection
    {

        public Dictionary<string, List<IGameObject>> Objects { get; set; }

        private Dictionary<string, IGameObject> _objectMap;

        public GameObjectCollection()
        {
            Objects = new Dictionary<string, List<IGameObject>>();
            _objectMap = new Dictionary<string, IGameObject>();
        }

        public void Add(IGameObject obj)
        {
            if (obj == null) return; // do nothing with a null object.

            if (!Objects.ContainsKey(obj.Type))
            {
                Objects.Add(obj.Type, new List<IGameObject>());
            }
            Debug.WriteLine(obj.Id);
            _objectMap.Add(obj.Id, obj);
            Objects[obj.Type].Add(obj);
        }

        public List<T> GetObjects<T>() where T : IGameObject
        {
            if (Objects.ContainsKey(typeof(T).Name))
            {
                return Objects[typeof(T).Name].Cast<T>().ToList();
            }
            else
            {
                return new List<T>();
            }
        }
    }
}