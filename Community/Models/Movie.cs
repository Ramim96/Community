using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Provide data")]
        [MinLength(1, ErrorMessage = "The text is too short")]
        public string Title { get; set; }

        [Display(Name = "Director/s")]
        [Required(ErrorMessage = "Provide data")]
        [MinLength(1, ErrorMessage = "The text is too short")]
        public string Directors { get; set; }

        [Display(Name = "Actor/s")]
        [Required(ErrorMessage = "Provide data")]
        [MinLength(1, ErrorMessage = "The text is too short")]
        public string Actors { get; set; }

        [Display(Name = "Genre/s")]
        [Required(ErrorMessage = "Provide data")]
        [MinLength(1, ErrorMessage = "The text is too short")]
        public string Genres { get; set; }

        public string PosterImg { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string TrailerLink { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        [Required(ErrorMessage = "Provide data")]
        public string Synopsis { get; set; }

        // M:N cardinality table
        public ICollection<MovieWatchlist> Watchlists { get; set; }
    }
}
