using MvvmCross.IoC;
using MvvmCross.ViewModels;
using ArcTouch.Core.ViewModels.Root;
using ArcTouch.Core.ViewModels.Login;
using MvvmCross;
using Acr.UserDialogs;

namespace ArcTouch.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes()
                .EndingWith("Client")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);

            RegisterAppStart<LoginViewModel>();
        }
    }
}
