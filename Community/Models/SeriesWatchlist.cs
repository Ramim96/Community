using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Models
{
    public class SeriesWatchlist
    {
        /*
         * The two set of properties defined below describe the setting
         * for the join table and the M:N cardinality in a relational
         * database management system
         */

        // Implement the M:N cardinality
        public int SeriesId { get; set; }

        public Series Series { get; set; }

        // Implement the M:N cardinality
        public int WatchlistId { get; set; }

        public Watchlist Watchlist { get; set; }
    }
}
