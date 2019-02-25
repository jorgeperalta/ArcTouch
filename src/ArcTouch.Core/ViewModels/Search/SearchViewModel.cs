using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ArcTouch.Core.Helpers;
using ArcTouch.Core.Interfaces;
using ArcTouch.Core.Models;
using ArcTouch.Core.ViewModels.Home;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace ArcTouch.Core.ViewModels.Search
{
    public class SearchViewModel : HomeViewModel
    {

        public IMvxAsyncCommand<TextChangedEventArgs> SearchMovieCommand { get; private set; }

        public bool IsBusy { get; set; }

        private string _searchField;

        public SearchViewModel(IMovieService movieService, IMvxNavigationService navigationService) : base(movieService, navigationService)
        {
            SearchMovieCommand = new MvxAsyncCommand<TextChangedEventArgs>(SearchMovie);
        }

        public string SearchField
        {
            get => _searchField;
            set => SetProperty(ref _searchField, value);
        }

        private async Task SearchMovie(TextChangedEventArgs args)
        {
            try
            {
                MovieList = new MvxObservableCollection<DetailedMovie>(await _movieService.SearchMovie(SearchField));
            }
            catch (Exception ex)
            {
                ErrorLog.LogError("Error searching movies", ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
