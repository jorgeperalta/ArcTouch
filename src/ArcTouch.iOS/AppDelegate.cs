using FFImageLoading.Forms.Platform;
using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using UIKit;

namespace ArcTouch.iOS
{
    [Register(nameof(AppDelegate))]
    public partial class AppDelegate : MvxFormsApplicationDelegate<Setup, Core.App, UI.App>
    {

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            CachedImageRenderer.Init();

            return base.FinishedLaunching(app, options);
        }

    }
}
