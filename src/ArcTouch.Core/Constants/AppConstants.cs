using System;
using System.Collections.Generic;

namespace ArcTouch.Core.Constants
{
	internal static class AppConstants
    {
        public const string TmdbApiKey = "1f54bd990f1cdfb230adb312546d765d";

        public const string TmdbBaseUrl = "https://api.themoviedb.org/3/";
        public const string TmdbAuthenticateUrl = "authentication/token/new?api_key={0}";
        public const string TmdbValidateWithLoginUrl = "authentication/token/validate_with_login?api_key={0}";
        public const string TmdbGetSessionUrl = "authentication/session/new?api_key={0}";

        public const string TmdbGenresUrl = "genre/movie/list?api_key={0}";
        public const string TmdbSearchUrl = "search/multi?api_key={0}&query={1}";
		public const string TmdbConfigurationUrl = "configuration?api_key={0}";
		public const string TmdbGetMovieUrl = "movie/{1}?api_key={0}";
        public const string TmdbMoviePopular = "discover/movie?api_key={0}&sort_by=popularity.desc";

        public const int TmdbTitleMaxLength = 20;
 	}
}