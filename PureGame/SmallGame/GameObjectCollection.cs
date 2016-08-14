using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SmallGame
{

    public class GameObjectCollection
    {

        public Dictionary<string, List<GameObject>> Objects { get; set; }

        private Dictionary<string, GameObject> _objectMap; 

        public GameObjectCollection()
        {
            Objects = new Dictionary<string, List<GameObject>>();
            _objectMap = new Dictionary<string, GameObject>();
        }

        public void Add(GameObject obj)
        {
            if (obj == null) return; // do nothing with a null object.

            if (!Objects.ContainsKey(obj.Type))
            {
                Objects.Add(obj.Type, new List<GameObject>());
            }
            Debug.WriteLine(obj.Id);
            _objectMap.Add(obj.Id, obj);
            Objects[obj.Type].Add(obj);
        }

        public List<T> GetObjects<T>() where T : GameObject
        {
            if (Objects.ContainsKey(typeof (T).Name))
            {
                return Objects[typeof (T).Name].Cast<T>().ToList();
            }
            else
            {
                return new List<T>();
            }
        }
    }
}
