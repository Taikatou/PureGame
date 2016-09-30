using Microsoft.Xna.Framework;
using PureGame.Engine;
using PureGame.Engine.EntityData;
using PureGame.Engine.Events.WorldTriggers;
using PureGame.Engine.World;

namespace PureGame.Content.WorldAreas
{
    public class DifferetWorld : WorldArea
    {
        public DifferetWorld()
        {
            Map = new WorldMap("level02", Content);
            AddEntity(EntityFactory.MakeEntityObject(new Point(1, 1), "CharacterSheet"));
        }

        public override void OnInit(IWorldLoader worldLoader)
        {
            base.OnInit(worldLoader);
            TriggerManager.AddTrigger(new TeleportTrigger<BasicWorld>(new Point(0, 0), new Point(5, 5), TriggerManager.WorldLoader));
        }
    }
}
