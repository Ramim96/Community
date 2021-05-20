using Community.Interfaces;
using Community.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Components
{
    public class MovieWatchlistMenu : ViewComponent
    {
        // Repository services
        private readonly IMovie _movieRepository;
        private readonly IWatchlist _watchlistRepository;

        // Constructor
        public MovieWatchlistMenu(IMovie movieRepository, IWatchlist watchlistRepository)
        {
            _movieRepository = movieRepository;
            _watchlistRepository = watchlistRepository;
        }

        // View component logic
        public IViewComponentResult Invoke(int? MovieId)
        {
            // Menu view model
            MovieMenuViewModel movieMenuViewModel = new MovieMenuViewModel();
            movieMenuViewModel.movie = _movieRepository.GetById((int)MovieId);
            movieMenuViewModel.watchlists = _watchlistRepository.GetAll.OrderBy(wl => wl.Title);

            return View(movieMenuViewModel);
        }
    }
}
