
using Android.App;
using Android.OS;
using Android.Widget;

namespace PureGame.Droid
{
    [Activity(Label = "MainMenuActivity"
        , MainLauncher = true)]
    public class MainMenuActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.MainMenuLayout);
            var button = FindViewById<Button>(Resource.Id.PlayGameButton);
            button.Click += delegate { StartActivity(typeof(GameActivity)); };
        }
    }
}