using Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.ViewModels
{
    public class WatchlistViewModel
    {
        public IEnumerable<Watchlist> allWatchlists { get; set; }
    }
}
