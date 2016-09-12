using System;

namespace PureGame.Droid.Screens.MenuScreens
{
    public class KeyboardOptionsScreen : MenuScreen
    {
        public KeyboardOptionsScreen(IServiceProvider serviceProvider) 
            : base(serviceProvider)
        {
        }
        
        public override void LoadContent()
        {
            base.LoadContent();

            AddMenuItem("Back", Show<OptionsScreen>);
        }
    }
}