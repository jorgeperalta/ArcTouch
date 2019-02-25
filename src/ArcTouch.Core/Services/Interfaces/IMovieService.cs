using System.Collections.Generic;
using System.Threading.Tasks;
using ArcTouch.Core.Models;

namespace ArcTouch.Core.Interfaces
{
    public interface IMovieService
	{
        Task<Token> Authenticate(string username, string password);
        Task<Session> GetSession(Token token);
        Task<List<DetailedMovie>> SearchMovie(string movieTitle);
		Task<DetailedMovie> DetailedMovieById(int id);
        Task<List<DetailedMovie>> DiscoverMovie();
	}
}
