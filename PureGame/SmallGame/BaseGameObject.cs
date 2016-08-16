using System.Diagnostics;

namespace SmallGame
{
    public class BaseIGameObject : IGameObject
    {
        private string id;
        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        private string type;
        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public virtual void OnInit()
        {
            
        }
    }
}
