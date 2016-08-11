using Microsoft.Xna.Framework;
using SmallGame.GameObjects;

namespace PureGame.Engine
{
    public class EntityObject : GameObject
    {
        public string FileName;
        //This is not supposed to be changed directly only through entityManager
        public Vector2 Position;
        public EntityObject()
        {

        }
    }
}
