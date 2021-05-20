using Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<Movie> moviesResult { get; set; }
        public IEnumerable<Series> seriesResult { get; set; }
        public IEnumerable<Watchlist> watchlistsResult { get; set; }
        public string Type { get; set; }
    }
}
