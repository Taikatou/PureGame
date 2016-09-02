using Microsoft.Xna.Framework;
using PureGame.SmallGame;

namespace PureGame.Engine.Events
{
    public enum TriggerType { MoveEntityTo }
    public class TriggerObject : IGameObject
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public Vector2 Position;
        public Vector2 EndPosition;
        public string Value = "level02.json";
        public TriggerType EventType = TriggerType.MoveEntityTo;
    }
}
