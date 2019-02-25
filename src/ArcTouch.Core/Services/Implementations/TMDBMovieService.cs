using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ArcTouch.Core.Constants;
using ArcTouch.Core.Helpers;
using ArcTouch.Core.Interfaces;
using ArcTouch.Core.Models;
using ArcTouch.Core.Rest.Interfaces;

namespace ArcTouch.Core
{
    public class TMDBMovieService : IMovieService
	{
		private Genres _genres = null;
		private Configuration _tmdbConfiguration;
        private readonly IRestClient _restClient;
        private Session _session;

        public TMDBMovieService(IRestClient restClient)
        {
            _restClient = restClient;

        }

        private async Task GetConfigurationIfNeededAsync(bool force = false)
		{
			if (_tmdbConfiguration != null && !force) return;

			try
			{
                _tmdbConfiguration = await _restClient.MakeApiCall<Configuration>(string.Format(AppConstants.TmdbConfigurationUrl, AppConstants.TmdbApiKey), HttpMethod.Get);
				
			}
			catch (Exception ex)
			{

                ErrorLog.LogError("Error getting configuration", ex);
			}
		}

        public async Task<List<DetailedMovie>> DiscoverMovie()
        {
            try
            {
                if (_genres == null)
                    await LoadGenres();

                Movies movies = await _restClient.MakeApiCall<Movies>(string.Format(AppConstants.TmdbMoviePopular, AppConstants.TmdbApiKey), HttpMethod.Get);
                await GetConfigurationIfNeededAsync();

                var movieList = movies.results.Select(movie => new DetailedMovie
                {
                    Id = movie.id,
                    OriginalTitle = movie.original_title,
                    ComposedTitle = string.Format("{0}{1}({2})", movie.original_title.Substring(0, Math.Min(movie.original_title.Length, AppConstants.TmdbTitleMaxLength)),
                                                  movie.original_title.Length > AppConstants.TmdbTitleMaxLength
                                                  ? "..." : " ", movie.release_date.Substring(0, 4)),
                    Overview = movie.overview,
                    Score = movie.vote_average,
                    VoteCount = movie.vote_count,
                    ImdbId = movie.imdb_id,
                    PosterUrl = _tmdbConfiguration.images.base_url +
                        _tmdbConfiguration.images.poster_sizes[3] +
                        movie.poster_path,
                    GenresCommaSeparated = GetGenresString(String.Join(", ", movie.genre_ids)),
                    ReleaseDate = movie.release_date,
                    Runtime = movie.runtime,
                    Tagline = movie.tagline
                }).ToList();

                return movieList;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError("Error getting Movies", ex);
                return null;
            }
        }

        public async Task<List<DetailedMovie>> SearchMovie(string movieTitle)
		{
			try
			{
                var movies = await _restClient.MakeApiCall<Movies>(string.Format(AppConstants.TmdbSearchUrl, AppConstants.TmdbApiKey, movieTitle), HttpMethod.Get);
                await GetConfigurationIfNeededAsync();

				var movieList = movies.results.Where(x => x.original_title != null).Select(movie => new DetailedMovie
				{
					Id = movie.id,
					OriginalTitle = movie.original_title,
                    ComposedTitle = string.Format("{0}{1}({2})", movie.title.Substring(0, Math.Min(movie.title.Length, AppConstants.TmdbTitleMaxLength)),
                                                  movie.title.Length > AppConstants.TmdbTitleMaxLength
                                                  ? "..." : " ", string.IsNullOrEmpty(movie.release_date) ? "N/A" : movie.release_date.Substring(0, 4)),
                    Overview = movie.overview,
					Score = movie.vote_average,
					VoteCount = movie.vote_count,
					ImdbId = movie.imdb_id,
					PosterUrl = _tmdbConfiguration.images.base_url +
						_tmdbConfiguration.images.poster_sizes[3] +
						movie.poster_path,
					GenresCommaSeparated = GetGenresString(String.Join(", ", movie.genre_ids)),
					ReleaseDate = movie.release_date,
					Runtime = movie.runtime,
					Tagline = movie.tagline
				}).ToList();

				return movieList;
			}
			catch (Exception ex)
			{
                ErrorLog.LogError("Error fetching movie title.", ex);
                return null;
			}
		}		


		public async Task<DetailedMovie> DetailedMovieById(int id)
		{
			try
			{
                TmdbMovie movie = await _restClient.MakeApiCall<TmdbMovie>(string.Format(AppConstants.TmdbGetMovieUrl, AppConstants.TmdbApiKey,id), HttpMethod.Get);
                await GetConfigurationIfNeededAsync();

				var detailed = new DetailedMovie
				{
					OriginalTitle = movie.original_title,
					ComposedTitle = string.Format("{0}{1}({2})", movie.original_title.Substring(0, Math.Min(movie.original_title.Length, AppConstants.TmdbTitleMaxLength)),
					                              movie.original_title.Length > AppConstants.TmdbTitleMaxLength 
					                              ? "..." : " ", movie.release_date.Substring(0, 4)),
					Overview = movie.overview,
					Score = movie.vote_average,
					VoteCount = movie.vote_count,
					ImdbId = movie.imdb_id,
					PosterUrl = _tmdbConfiguration.images.base_url +
						_tmdbConfiguration.images.poster_sizes[3] +
						movie.poster_path,
					GenresCommaSeparated = GetGenresString(String.Join(", ", movie.genre_ids)),
					ReleaseDate = movie.release_date,
					Runtime = movie.runtime,
					Tagline = movie.tagline
				};

				return detailed;
			}
			catch (Exception ex)
			{
                ErrorLog.LogError($"Error getting movie detail for id {id}", ex);
                return null;
			}
		}

		private string GetGenresString(string genresIDs)
		{
			var r = string.Empty;

			try
			{
				var ids = genresIDs?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

				if (ids.Any())
				{
                    var i = 0;
					foreach (var genreId in ids)
					{
						i++;

                        Genre g = _genres.genres.FirstOrDefault(x => x.id == Int32.Parse(genreId));

						var genreName = (g != null) ? g.name : string.Empty;

						if (!string.IsNullOrEmpty(genreName))
						{
							if (g != null)
							{
								r += genreName;
							}

							if (i != ids.Count())
							{
								r += ", ";
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.LogError("ERROR: Getting genres by ids", ex);
				r = string.Empty;
			}

			return r;
		}

		private async Task LoadGenres()
		{
            try
			{
                _genres = await _restClient.MakeApiCall<Genres>(string.Format(AppConstants.TmdbGenresUrl, AppConstants.TmdbApiKey), HttpMethod.Get);
            }
			catch (Exception ex)
			{
				ErrorLog.LogError("Error loading Genres", ex);
			}
		}

        public async Task<Token> Authenticate(string username, string password)
        {
            try
            {
                Token auth = await _restClient.MakeApiCall<Token>(string.Format(AppConstants.TmdbAuthenticateUrl, AppConstants.TmdbApiKey), HttpMethod.Get);

                var data = new SessionRequest
                {
                    Username = username,
                    Password = password,
                    RequestToken = auth.RequestToken
                };


                auth = await _restClient.MakeApiCall<Token>(string.Format(AppConstants.TmdbValidateWithLoginUrl, AppConstants.TmdbApiKey), HttpMethod.Post, data);

                return !auth.Success ? null : auth;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError("Error Authenticating", ex);
                return null;
            }
        }

        public async Task<Session> GetSession(Token token)
        {
            try
            {
                if (_session != null)
                    return _session;
                //Add extra checks for token before returning stored session

                Session session = await _restClient.MakeApiCall<Session>(string.Format(AppConstants.TmdbGetSessionUrl, AppConstants.TmdbApiKey), HttpMethod.Post, token);

                if (session == null || !session.Success)
                    return null;

                _session = session;

                return session;
            }
            catch (Exception ex)
            {
                ErrorLog.LogError("Error creating Session", ex);
                return null;
            }
}
    }

}
