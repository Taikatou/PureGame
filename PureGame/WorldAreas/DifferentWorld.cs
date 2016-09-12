using Microsoft.Xna.Framework;
using PureGame.Engine;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;

namespace PureGame.WorldAreas
{
    public class DifferetWorld : WorldArea
    {
        public DifferetWorld()
        {
            AddEntity(EntityFactory.MakeEntityObject(new Vector2(1, 1), "CharacterSheet"));
            Map = new WorldMap("level01", Content);
        }
    }
}
