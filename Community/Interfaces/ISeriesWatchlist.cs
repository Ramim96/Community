using Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Interfaces
{
    public interface ISeriesWatchlist
    {
        public Series GetSeriesById(int Id);
        public Watchlist GetWatchlistById(int Id);
        public ICollection<Series> GetAllSeries { get; }
        public ICollection<Watchlist> GetAllWatchlists { get; }
        public bool AddSeries(int SeriesId, int WatchlistId);
        public void RemoveSeries(int SeriesId, int WatchlistId);
    }
}
