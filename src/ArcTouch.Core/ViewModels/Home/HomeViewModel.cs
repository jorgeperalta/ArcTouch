using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ArcTouch.Core.Helpers;
using ArcTouch.Core.Interfaces;
using ArcTouch.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ArcTouch.Core.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        protected readonly IMovieService _movieService;
        private readonly IMvxNavigationService _navigationService;

        public IMvxAsyncCommand<DetailedMovie> ShowDetailPageCommand { get; private set; }

        private ObservableCollection<DetailedMovie> _movieList;
        public ObservableCollection<DetailedMovie> MovieList
        {
            get => _movieList;
            set => SetProperty(ref _movieList, value);
        }

        public HomeViewModel(IMovieService movieService, IMvxNavigationService navigationService)
        {
            _movieService = movieService;
            _navigationService = navigationService;

            ShowDetailPageCommand = new MvxAsyncCommand<DetailedMovie>(ShowDetailPageAsync);

            Task.Run(LoadListAsync).ConfigureAwait(true);
        }

        private async Task LoadListAsync()
        {
            try
            {
                MovieList = new MvxObservableCollection<DetailedMovie>(await _movieService.DiscoverMovie());
            }
            catch (Exception ex)
            {
                ErrorLog.LogError("Getting movies", ex);
            }
        }

        private async Task ShowDetailPageAsync(DetailedMovie selectedMovieItem)
        {
            await _navigationService.Navigate<MovieDetailViewModel, DetailedMovie>(selectedMovieItem);
        }
    }
}
