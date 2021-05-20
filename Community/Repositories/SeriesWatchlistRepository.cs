using Community.Interfaces;
using Community.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Repositories
{
    public class SeriesWatchlistRepository : ISeriesWatchlist
    {
        // Db context service 
        private readonly ApplicationDbContext _db;

        // Constructor
        public SeriesWatchlistRepository(ApplicationDbContext db) => _db = db;
        
        // Get series by id
        public Series GetSeriesById(int Id)
        {
            return _db.Series
                      .Include(m => m.Watchlists)
                      .ThenInclude(mwl => mwl.Watchlist)
                      .FirstOrDefault(m => m.SeriesId == Id);
        }

        // Get all series
        public ICollection<Series> GetAllSeries 
        {
            get
            {
                return _db.Series
                          .Include(s => s.Watchlists)
                          .ThenInclude(swl => swl.Watchlist)
                          .OrderBy(s => s.Title)
                          .ToList();
            }
        }

        // Get watchlist by id
        public Watchlist GetWatchlistById(int Id)
        {
            return _db.Watchlists
                      .Include(wl => wl.Movies)
                      .ThenInclude(mwl => mwl.Movie)
                      .Include(wl => wl.Series)
                      .ThenInclude(swl => swl.Series)
                      .FirstOrDefault(wl => wl.WatchlistId == Id);
        }

        // Get all watchlists
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

        public bool AddSeries(int SeriesId, int WatchlistId)
        {
            var existingWatchList = GetWatchlistById(WatchlistId);
            var existingSeries = GetSeriesById(SeriesId);

            // Find and assert if movie already inside watch list
            var alreadyIn = existingWatchList.Series.Any(m => m.SeriesId == SeriesId);

            if (!alreadyIn)
            {
                // Insert movie to watch list
                existingWatchList.Series.Add(new SeriesWatchlist
                {
                    Series = existingSeries,
                    Watchlist = existingWatchList
                });


                _db.SaveChanges();

                return true;
            }

            return false;
        }

        // Remove a series from the watchlist
        public void RemoveSeries(int SeriesId, int WatchlistId)
        {
            var existingWatchList = GetWatchlistById(WatchlistId);
            var existingSeries = GetSeriesById(SeriesId);

            // Get movie to remove from watch list navigational 
            var seriesToRemove = existingWatchList.Series.FirstOrDefault(m => m.SeriesId == SeriesId);

            // Remove movie from watch list
            _db.Remove(seriesToRemove);

            _db.SaveChanges();

            return;
        }
    }
}
