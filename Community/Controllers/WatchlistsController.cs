using Community.Interfaces;
using Community.Models;
using Community.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Controllers
{
    public class WatchlistsController : Controller
    {
        private readonly IMovieWatchlist _movieWatchlistRepository; 
        private readonly ISeriesWatchlist _seriesWatchlistRepository;
        private readonly IWatchlist _watchlistRepository;

        public WatchlistsController(IMovieWatchlist movieWatchlistRepository, ISeriesWatchlist seriesWatchlistRepository, IWatchlist watchlistRepository)
        {
            _movieWatchlistRepository = movieWatchlistRepository;
            _seriesWatchlistRepository = seriesWatchlistRepository;
            _watchlistRepository = watchlistRepository;
        }

        // GET: Watchlists/(Index)?
        // Get all watchlists from the database
        [HttpGet]
        public IActionResult Index()
        {
            // Get all watchlists 
            WatchlistViewModel watchlistViewModel = new WatchlistViewModel();
            watchlistViewModel.allWatchlists = _watchlistRepository.GetAll;

            return View(watchlistViewModel);
        }

        // GET: Watchlists/Create
        // Get create watchlist view
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Watchlists/Create
        // Create a new watchlist
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Watchlist watchlist)
        {
            // Assert form input validation
            if (!ModelState.IsValid)
            {
                // Return to current view with validaton errors
                return View(watchlist);
            }

            // Check if watchlist already exists
            bool existingWatchlist = _watchlistRepository.CheckExists(watchlist);

            // Assert instance result
            if(!existingWatchlist)
            {
                // Create watchlist service
                _watchlistRepository.Create(watchlist);

                // Redirect to watchlists
                return RedirectToAction("Index");
            }

            // Redirect to watchlists
            return RedirectToAction("Index");
        }

        // GET: Watchlists/Details/id:int
        // Get watchlist details view
        [HttpGet]
        public IActionResult Details(int? Id)
        {
            // Assert watchlist id
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            // Find watchlist by id and assert the result
            Watchlist watchlist = _watchlistRepository.GetById((int)Id);
            if (watchlist == null)
            {
                return NotFound();
            }

            return View(watchlist);
        }

        // GET: Watchlists/Edit/id:int
        // Get edit watchlist view
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            // Assert watchlist id
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            // Find watchlist by id and assert the result
            Watchlist watchlist = _watchlistRepository.GetById((int)Id);
            if (watchlist == null)
            {
                return NotFound();
            }

            return View(watchlist);
        }

        // POST: Watchlists/Edit/id:int
        // Post new watchlist details data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Watchlist watchlist)
        {
            // Assert form input validation
            if (!ModelState.IsValid)
            {
                // Return to current view with validaton errors
                return View(watchlist);
            }

            // Update movie
            _watchlistRepository.Edit(watchlist);

            return RedirectToAction("Index");
        }

        // GET: Watchlists/Delete/id:int
        // Get delete watchlist view
        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            // Assert watchlist id
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            // Find watchlist by id and assert the result
            Watchlist watchlist = _watchlistRepository.GetById((int)Id);
            if (watchlist == null)
            {
                return NotFound();
            }

            return View(watchlist);
        }

        // POST: Watchlists/Delete/id:int
        // Delete a watchlist
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Watchlist watchlist)
        {
            //// Assert watchlist id
            //if (Id == null || Id == 0)
            //{
            //    return NotFound();
            //}

            //// Find watchlist by id and assert the result
            //Watchlist watchlistToDelete = _watchlistRepository.GetById((int)Id);
            //if (watchlistToDelete == null)
            //{
            //    return NotFound();
            //}

            // Assert watchlist id
            if (watchlist.WatchlistId == 0)
            {
                return NotFound();
            }

            // Find watchlist by id and assert the result
            Watchlist watchlistToDelete = _watchlistRepository.GetById(watchlist.WatchlistId);
            if (watchlistToDelete == null)
            {
                return NotFound();
            }

            _watchlistRepository.Delete(watchlistToDelete);

            return RedirectToAction("Index");
        }

        // POST: Watchlist/AddMovie?MovieId+WatchlistId
        // Add a movie in the repository
        public IActionResult AddMovie(int? MovieId, int? WatchlistId)
        {
            //Assert movie and watchlist ids
            if (MovieId == null || MovieId == 0)
            {
                return NotFound();
            }

            if (WatchlistId == null || WatchlistId == 0)
            {
                return NotFound();
            }

            bool insertionRes = _movieWatchlistRepository.AddMovie((int)MovieId, (int)WatchlistId);

            // Assert movie insertion to watchlist service outcome
            // Movie insertion failed due to duplicate instance already existing
            if (!insertionRes)
            {
                return RedirectToAction("Index", "Movies");
            }

            // Movie successfully inserted in the watchlist
            // Define ViewBag data to render modal
            return RedirectToAction("Index", "Movies");
        }

        // POST: WatchList/RemoveMovie?MovieId+WatchListId
        // Remove a movie from a repository
        public IActionResult RemoveMovie(int? MovieId, int? WatchlistId)
        {
            //Assert movie and watchlist ids
            if (MovieId == null || MovieId == 0)
            {
                return NotFound();
            }

            if (WatchlistId == null || WatchlistId == 0)
            {
                return NotFound();
            }

            _movieWatchlistRepository.RemoveMovie((int)MovieId, (int)WatchlistId);

            return RedirectToAction("Details", "Watchlists", new { Id = WatchlistId });
        }

        // POST: Watchlist/AddSeries?SeriesId+WatchlistId
        // Add a series in the repository
        public IActionResult AddSeries(int? SeriesId, int? WatchlistId)
        {
            //Assert series and watchlist ids
            if (SeriesId == null || SeriesId == 0)
            {
                return NotFound();
            }

            if (WatchlistId == null || WatchlistId == 0)
            {
                return NotFound();
            }

            bool insertionRes = _seriesWatchlistRepository.AddSeries((int)SeriesId, (int)WatchlistId);

            // Assert movie insertion to watchlist service outcome
            // Movie insertion failed due to duplicate instance already existing
            if (!insertionRes)
            {
                return RedirectToAction("Index", "Series");
            }

            // Movie successfully inserted in the watchlist
            // Define ViewBag data to render modal
            return RedirectToAction("Index", "Series");
        }

        // POST: Watchlist/RemoveSeries?SeriesId+WatchlistId
        // Remove a series from a repository
        public IActionResult RemoveSeries(int? SeriesId, int? WatchlistId)
        {
            //Assert movie and watchlist ids
            if (SeriesId == null || SeriesId == 0)
            {
                return NotFound();
            }

            if (WatchlistId == null || WatchlistId == 0)
            {
                return NotFound();
            }

            _seriesWatchlistRepository.RemoveSeries((int)SeriesId, (int)WatchlistId);

            return RedirectToAction("Details", "Watchlists", new { Id = WatchlistId });
        }
    }
}
