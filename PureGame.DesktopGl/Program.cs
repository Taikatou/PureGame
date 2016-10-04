using PureGame.Client;
using System;

namespace PureGame.DesktopGl
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new MonoGameGame(new ControllerSettings()))
                game.Run();
        }
    }
}
