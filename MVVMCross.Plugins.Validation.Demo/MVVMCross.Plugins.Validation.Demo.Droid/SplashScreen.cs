using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;
using MvvnCross.Plugins.Validation.Demo.Droid;

namespace MvvmCross.Plugins.Validation.Demo.Droid
{
    [Activity(
        Label = "MvvmCross.Plugins.Validation.Demo.Droid"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}
