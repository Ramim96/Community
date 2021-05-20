using Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Interfaces
{
    public interface IMovieWatchlist
    {
        public Movie GetMovieById(int Id);
        public Watchlist GetWatchlistById(int Id);
        public ICollection<Movie> GetAllMovies { get; }
        public ICollection<Watchlist> GetAllWatchlists { get; }
        public bool AddMovie(int MovieId, int WatchlistId);
        public void RemoveMovie(int MovieId, int WatchlistId);
    }
}
