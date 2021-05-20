using Community.Interfaces;
using Community.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Components
{
    public class SeriesWatchlistMenu : ViewComponent
    {
        // Repository services
        private readonly ISeries _seriesRepository;
        private readonly IWatchlist _watchlistRepository;

        // Constructor
        public SeriesWatchlistMenu(ISeries seriesRepository, IWatchlist watchlistRepository)
        {
            _seriesRepository = seriesRepository;
            _watchlistRepository = watchlistRepository;
        }

        // View component logic
        public IViewComponentResult Invoke(int? SeriesId)
        {
            // Menu view model
            SeriesMenuViewModel seriesMenuViewModel = new SeriesMenuViewModel();
            seriesMenuViewModel.series = _seriesRepository.GetById((int)SeriesId);
            seriesMenuViewModel.watchlists = _watchlistRepository.GetAll.OrderBy(wl => wl.Title);

            return View(seriesMenuViewModel);
        }
    }
}
