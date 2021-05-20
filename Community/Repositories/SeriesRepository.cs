using Community.Interfaces;
using Community.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Repositories
{
    public class SeriesRepository : ISeries
    {
        // Db context service 
        private readonly ApplicationDbContext _db;

        // Constructor
        public SeriesRepository(ApplicationDbContext db) => _db = db;

        // Get a series by Id
        public Series GetById(int Id)
        {
            return _db.Series
                      .Include(s => s.Watchlists)
                      .ThenInclude(swl => swl.Watchlist)
                      .FirstOrDefault(s => s.SeriesId == Id);
        }

        // Get all series
        public ICollection<Series> GetAll
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

        // Create a new series
        public void Create(Series series)
        {
            _db.Series.Add(series);
            _db.SaveChanges();

            return;
        }

        // Delete a series
        public void Delete(Series series)
        {
            _db.Series.Remove(series);
            _db.SaveChanges();

            return;
        }

        // Edit a series
        public void Edit(Series series)
        {
            _db.Series.Update(series);
            _db.SaveChanges();

            return;
        }

        // Update likes
        public void UpdateLikes(int Id)
        {
            var series = GetById(Id);

            series.Likes += 1;
            _db.SaveChanges();

            return;
        }

        // Update dislikes
        public void UpdateDislikes(int Id)
        {
            var series = GetById(Id);

            series.Dislikes += 1;
            _db.SaveChanges();

            return;
        }

        // Check if series already exists
        public bool CheckExists(Series series)
        {
            var existingSeries = _db.Series.FirstOrDefault(
                                        s => s.Title == series.Title &&
                                        s.Creators == series.Creators &&
                                        s.Actors == series.Actors);

            if (existingSeries == null)
            {
                return false;
            }

            return true;
        }
    }
}
