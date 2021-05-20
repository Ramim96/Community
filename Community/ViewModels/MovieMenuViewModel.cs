using Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.ViewModels
{
    public class MovieMenuViewModel
    {
        public Movie movie { get; set; }
        public IEnumerable<Watchlist> watchlists { get; set; }
    }
}
