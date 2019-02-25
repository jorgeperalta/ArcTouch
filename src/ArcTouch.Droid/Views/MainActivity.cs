
using Android.App;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Views;
using ArcTouch.Core.ViewModels.Main;
using Acr.UserDialogs;
using Android.Content.PM;
using FFImageLoading.Forms.Platform;

namespace ArcTouch.Droid
{
    [Activity(
        Theme = "@style/AppTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        LaunchMode = LaunchMode.SingleTask)]
    public class MainActivity : MvxFormsAppCompatActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            UserDialogs.Init(this);
            CachedImageRenderer.Init(true);
            base.OnCreate(bundle);

        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
