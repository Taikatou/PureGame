namespace PureGame.Client.Controllers
{
    public interface IControllerSettings
    {
        bool KeyBoardMouseEnabled { get; set; }
        bool TouchScreenEnabled { get; set; }
        bool GamePadEnabled { get; set; }
    }
}
