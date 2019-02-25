using System;
using ArcTouch.Core.Models;

namespace ArcTouch.Core.ViewModels.Home
{
    public class MovieDetailViewModel : BaseViewModel<DetailedMovie>
    {

        private DetailedMovie _movieItem;
        public DetailedMovie MovieItem
        {
            get => _movieItem;
            set => SetProperty(ref _movieItem, value);
        }

        public MovieDetailViewModel()
        {
            //This VM should get extended info from TMDB
        }

        public override void Prepare(DetailedMovie parameter)
        {
            if (parameter == null)
                return;

            MovieItem = parameter;
        }
    }
}
