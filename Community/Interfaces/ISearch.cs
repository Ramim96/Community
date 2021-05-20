using Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Interfaces
{
    public interface ISearch
    {
        public IEnumerable<Movie> getMoviesResult(string Title, string Type);
        public IEnumerable<Series> getSeriesResult(string Title, string Type);
        public IEnumerable<Watchlist> getWatchlistsResult(string Title, string Type);
    }
}
