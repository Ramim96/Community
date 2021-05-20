using Community.Interfaces;
using Community.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Repositories
{
    public class MovieWatchlistRepository : IMovieWatchlist
    {
        // Db context service 
        private readonly ApplicationDbContext _db;

        // Constructor
        public MovieWatchlistRepository(ApplicationDbContext db) => _db = db;

        // Get a movie by id
        public Movie GetMovieById(int Id)
        {
            return _db.Movies
                      .Include(m => m.Watchlists)
                      .ThenInclude(mwl => mwl.Watchlist)
                      .FirstOrDefault(m => m.MovieId == Id);
        }

        // Get all movies
        public ICollection<Movie> GetAllMovies
        {
            get
            {
                return _db.Movies
                          .Include(m => m.Watchlists)
                          .ThenInclude(mwl => mwl.Watchlist)
                          .OrderBy(m => m.Title)
                          .ToList();
            }
        }

        // Get a watch list by id
        public Watchlist GetWatchlistById(int Id)
        {
            return _db.Watchlists
                      .Include(wl => wl.Movies)
                      .ThenInclude(mwl => mwl.Movie)
                      .Include(wl => wl.Series)
                      .ThenInclude(swl => swl.Series)
                      .FirstOrDefault(wl => wl.WatchlistId == Id);
        }

        // Get all watch lists
        public ICollection<Watchlist> GetAllWatchlists
        {
            get
            {
                return _db.Watchlists
                     .Include(wl => wl.Movies)
                     .ThenInclude(mwl => mwl.Movie)
                     .Include(wl => wl.Series)
                     .ThenInclude(swl => swl.Series)
                     .OrderBy(wl => wl.Title)
                     .ToList();
            }
        }

        // Add a movie in the watch list
        public bool AddMovie(int MovieId, int WatchlistId)
        {
            var existingWatchList = GetWatchlistById(WatchlistId);
            var existingMovie = GetMovieById(MovieId);

            // Find and assert if movie already inside watch list
            var alreadyIn = existingWatchList.Movies.Any(m => m.MovieId == MovieId);

            if (!alreadyIn)
            {
                // Insert movie to watch list
                existingWatchList.Movies.Add(new MovieWatchlist
                {
                    Movie = existingMovie,
                    Watchlist = existingWatchList
                });


                _db.SaveChanges();

                return true;
            }

            return false;
        }

        // Remove a movie
        public void RemoveMovie(int MovieId, int WatchlistId)
        {
            var existingWatchList = GetWatchlistById(WatchlistId);
            var existingMovie = GetMovieById(MovieId);

            // Get movie to remove from watch list navigational 
            var movieToRemove = existingWatchList.Movies.FirstOrDefault(m => m.MovieId == MovieId);

            // Remove movie from watch list
            _db.Remove(movieToRemove);

            _db.SaveChanges();

            return;
        }
    }
}
