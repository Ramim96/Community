using Community.Interfaces;
using Community.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Repositories
{
    public class SearchRepository : ISearch
    {
        // Db context service 
        private readonly ApplicationDbContext _db;

        // Constructor
        public SearchRepository(ApplicationDbContext db) => _db = db;

        // Get movies based on query service
        public IEnumerable<Movie> getMoviesResult(string Title, string Type)
        {
            return _db.Movies
                      .Include(m => m.Watchlists)
                      .ThenInclude(mwl => mwl.Watchlist)
                      .OrderBy(m => m.Title)
                      .AsEnumerable()
                      .Where(m => m.Title.ToLower().Contains(Title.ToLower()))
                      .ToList();
        }

        // Get series based on query service
        public IEnumerable<Series> getSeriesResult(string Title, string Type)
        {
            return _db.Series
                      .Include(s => s.Watchlists)
                      .ThenInclude(swl => swl.Watchlist)
                      .OrderBy(s => s.Title)
                      .AsEnumerable()
                      .Where(m => m.Title.ToLower().Contains(Title.ToLower()))
                      .ToList();
        }

        // Get watchlists based on query service
        public IEnumerable<Watchlist> getWatchlistsResult(string Title, string Type)
        {
            return _db.Watchlists
                      .Include(wl => wl.Movies)
                      .ThenInclude(mwl => mwl.Movie)
                      .Include(wl => wl.Series)
                      .ThenInclude(swl => swl.Series)
                      .OrderBy(wl => wl.Title)
                      .AsEnumerable()
                      .Where(m => m.Title.ToLower().Contains(Title.ToLower()))
                      .ToList();
        }
    }
}
