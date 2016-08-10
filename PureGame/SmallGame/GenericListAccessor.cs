using SmallGame.GameObjects;
using System.Collections.Generic;

namespace SmallGame
{
    public class GenericListAccessor<T> where T : GameObject
    {
        GameObjectCollection Objects;
        public GenericListAccessor(GameObjectCollection Objects)
        {
            this.Objects = Objects;
        }

        public List<T> List
        {
            get
            {
                List<T> to_return = Objects.GetObjects<T>();
                return to_return;
            }
        }
    }
}
