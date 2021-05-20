using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Models
{
    public class Watchlist
    {
        [Key]
        public int WatchlistId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Provide data")]
        [MinLength(1, ErrorMessage = "The text is too short")]
        public string Title { get; set; }

        public DateTime? CreatedAt { get; set; }

        [Required(ErrorMessage = "Provide data")]
        public string Description { get; set; }

        // M:N cardinality table
        public ICollection<MovieWatchlist> Movies { get; set; }
        public ICollection<SeriesWatchlist> Series { get; set; }
    }
}
