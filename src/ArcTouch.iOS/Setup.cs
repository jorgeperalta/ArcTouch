using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.Plugin.Json;

namespace ArcTouch.iOS
{
    public class Setup : MvxFormsIosSetup<Core.App, UI.App>
    {
        public override void InitializePrimary()
        {
            base.InitializePrimary();

            Mvx.IoCProvider.RegisterType<IMvxJsonConverter, MvxJsonConverter>();
        }
    }
}
