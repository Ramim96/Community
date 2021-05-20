using Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.ViewModels
{
    public class MovieViewModel
    {
        public IEnumerable<Movie> allMovies { get; set; }
    }
}
