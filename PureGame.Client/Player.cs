using PureGame.Engine.EntityData;

namespace PureGame.Client
{
    public class Player
    {
        public IPureGame Game;

        public BaseEntityObject BaseEntity;
        public Player(IPureGame Game, BaseEntityObject BaseEntity)
        {
            this.Game = Game;
            this.BaseEntity = BaseEntity;
        }
    }
}
