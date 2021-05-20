using Community.Interfaces;
using Community.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Repositories
{
    public class WatchlistRepository : IWatchlist
    {
        // Db context service 
        private readonly ApplicationDbContext _db;

        // Constructor
        public WatchlistRepository(ApplicationDbContext db) => _db = db;

        // Get a watchlist by id
        public Watchlist GetById(int Id)
        {
            return _db.Watchlists
                      .Include(wl => wl.Movies)
                      .ThenInclude(mwl => mwl.Movie)
                      .Include(wl => wl.Series)
                      .ThenInclude(swl => swl.Series)
                      .FirstOrDefault(wl => wl.WatchlistId == Id);
        }

        // Get all watchlists
        public ICollection<Watchlist> GetAll
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

        // Create a watchlist
        public void Create(Watchlist watchlist)
        {
            // Update date object property
            watchlist.CreatedAt = DateTime.Today;

            _db.Watchlists.Add(watchlist);
            _db.SaveChanges();

            return;
        }

        // Delete a watchlist
        public void Delete(Watchlist watchlist)
        {
            _db.Watchlists.Remove(watchlist);
            _db.SaveChanges();

            return;
        }

        // Edit a watchlist
        public void Edit(Watchlist watchlist)
        {
            _db.Watchlists.Update(watchlist);
            _db.SaveChanges();

            return;
        }

        // Check if watchlist already exists
        public bool CheckExists(Watchlist watchlist)
        {
            var existingWatchlist = _db.Watchlists.FirstOrDefault(
                                        w => w.Title == watchlist.Title);

            if (existingWatchlist == null)
            {
                return false;
            }

            return true;
        }
    }
}
