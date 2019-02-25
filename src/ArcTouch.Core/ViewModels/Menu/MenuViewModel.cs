using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using ArcTouch.Core.ViewModels.Home;
using Xamarin.Forms;
using ArcTouch.Core.ViewModels.Search;

namespace ArcTouch.Core.ViewModels.Menu
{
    public class MenuViewModel : BaseViewModel
    {
        protected readonly IMvxNavigationService _navigationService;

        public IMvxAsyncCommand ShowDetailPageAsyncCommand { get; private set; }

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            MenuItemList = new MvxObservableCollection<string>()
            {
                "Home",
                "Search"
            };

            ShowDetailPageAsyncCommand = new MvxAsyncCommand(ShowDetailPageAsync);
        }

        private ObservableCollection<string> _menuItemList;
        public ObservableCollection<string> MenuItemList
        {
            get => _menuItemList;
            set => SetProperty(ref _menuItemList, value);
        }

        private async Task ShowDetailPageAsync()
        {
            // Implement your logic here.
            switch (SelectedMenuItem)
            {
                case "Home":
                    await _navigationService.Navigate<HomeViewModel>();
                    break;
                case "Search":
                    await _navigationService.Navigate<SearchViewModel>();
                    break;
                default:
                    break;
            }

            if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsPresented = false;
            }
            else if (Application.Current.MainPage is NavigationPage navigationPage
                     && navigationPage.CurrentPage is MasterDetailPage nestedMasterDetail)
            {
                nestedMasterDetail.IsPresented = false;
            }
        }

        private string _selectedMenuItem;
        public string SelectedMenuItem
        {
            get => _selectedMenuItem;
            set => SetProperty(ref _selectedMenuItem, value);
        }
    }
}
