using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallGame.GameObjects;

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

            _objectMap.Add(obj.Id, obj);
            Objects[obj.Type].Add(obj);
        }


        //public List<GameObject> GetAll()
        //{
        //    var gobs = new List<GameObject>();
        //    Objects.Values.ToList().ForEach(g => gobs.Add(g));
        //    return gobs;
        //} 

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
