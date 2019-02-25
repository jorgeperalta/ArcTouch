using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using ArcTouch.Core.Helpers;
using ArcTouch.Core.Interfaces;
using ArcTouch.Core.Models;
using ArcTouch.Core.Resources;
using ArcTouch.Core.ViewModels.Root;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace ArcTouch.Core.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IMvxNavigationService _navigationService;
        private readonly IMovieService _movieService;

        private string _userName;

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public IMvxCommand LoginCommand { get; private set; }

        public LoginViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, IMovieService movieService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _movieService = movieService;
            LoginCommand = new MvxAsyncCommand(Login);
        }

        private async Task Login()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserName)
                   && string.IsNullOrWhiteSpace(Password))
                {
                    await _userDialogs.AlertAsync(new AlertConfig
                    {
                        Title = Strings.RequiredFieldsTitle,
                        Message = Strings.RequiredFieldsMesssage,
                        OkText = Strings.OkText
                    });

                    return;
                }

                Token token = await _movieService.Authenticate(UserName, Password);

                if (token != null)
                {
                    var session = await _movieService.GetSession(token);

                    if (session.Success)
                    {
                        await _navigationService.Navigate<RootViewModel>();
                        return;
                    }
                }

                await _userDialogs.AlertAsync(new AlertConfig
                {
                    Title = Strings.InvalidInfoTitle,
                    Message = Strings.InvalidUserOrPasswordMesssage,
                    OkText = Strings.OkText
                });

            }
            catch (Exception ex)
            {
                await _userDialogs.AlertAsync(new AlertConfig
                {
                    Title = Strings.InvalidInfoTitle,
                    Message = Strings.InvalidUserOrPasswordMesssage,
                    OkText = Strings.OkText
                });
                ErrorLog.LogError("Save User", ex);
            }
        }
    }
}
