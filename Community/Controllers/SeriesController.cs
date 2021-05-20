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
    public class SeriesController : Controller
    {
        private readonly ISeries _seriesRepository;
        private readonly ISeriesWatchlist _seriesWatchlistRepository;

        public SeriesController(ISeries seriesRepository, ISeriesWatchlist seriesWatchlistRepository)
        {
            _seriesRepository = seriesRepository;
            _seriesWatchlistRepository = seriesWatchlistRepository;
        }

        // GET: Series/(Index)?
        // Get all series 
        [HttpGet]
        public IActionResult Index()
        {
            // Get all series from the database
            SeriesViewModel movieViewModel = new SeriesViewModel();
            movieViewModel.allSeries = _seriesRepository.GetAll;

            return View(movieViewModel);
        }

        // GET: Series/Create
        // Get create series view
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Series/Create
        // Create a new series
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Series series)
        {
            // Assert form input validation
            if (!ModelState.IsValid)
            {
                // Return to current view with validaton errors
                return View(series);
            }

            // Check if series already exists
            bool existingSeries = _seriesRepository.CheckExists(series);

            // Assert instance result
            if(!existingSeries)
            {
                // Create series service
                _seriesRepository.Create(series);

                // Redirect to series list
                return RedirectToAction("Index");
            }

            // Redirect to series list
            return RedirectToAction("Index");
        }

        // GET: Series/Details/id:int
        // Get series details view
        [HttpGet]
        public IActionResult Details(int? Id)
        {
            // Assert series id
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            // Find series by id and assert the result
            Series series = _seriesRepository.GetById((int)Id);
            if (series == null)
            {
                return NotFound();
            }

            return View(series);
        }

        // GET: Series/Edit/id:int
        // Get edit series view
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            // Assert series id
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            // Find series by id and assert the result
            Series series = _seriesRepository.GetById((int)Id);
            if (series == null)
            {
                return NotFound();
            }

            return View(series);
        }

        // POST: Series/Edit/id:int
        // Post new series details data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Series series)
        {
            // Assert form input validation
            if (!ModelState.IsValid)
            {
                // Return to current view with validaton errors
                return View(series);
            }

            // Update series
            _seriesRepository.Edit(series);

            return RedirectToAction("Index");
        }

        // GET: Series/Delete/id:int
        // Get delete series view
        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            // Assert series id
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            // Find series by id and assert the result
            Series series = _seriesRepository.GetById((int)Id);
            if (series == null)
            {
                return NotFound();
            }

            return View(series);
        }

        // POST: Series/Delete/id:int
        // Delete a series
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Series series)
        {
            //// Assert series id
            //    if (Id == null || Id == 0)
            //{
            //    return NotFound();
            //}

            //// Find series by id and assert the result
            //Series seriesToDelete = _seriesRepository.GetById((int)Id);
            //if (seriesToDelete == null)
            //{
            //    return NotFound();
            //}

            // Assert series id
            if (series.SeriesId == 0)
            {
                return NotFound();
            }

            // Find series by id and assert the result
            Series seriesToDelete = _seriesRepository.GetById(series.SeriesId);
            if (seriesToDelete == null)
            {
                return NotFound();
            }

            _seriesRepository.Delete(seriesToDelete);

            return RedirectToAction("Index");
        }

        // POST: Series/Likes/int:id
        // Update likes
        public IActionResult Likes(int? SeriesId, int? WatchlistId)
        {
            // Assert series id
            if (SeriesId == null || SeriesId == 0)
            {
                return NotFound();
            }

            // Assert watchlist id
            if (WatchlistId == null || WatchlistId == 0)
            {
                return NotFound();
            }

            _seriesRepository.UpdateLikes((int)SeriesId);

            return RedirectToAction("Details", "Watchlists", new { Id = (int)WatchlistId });
        }

        // POST: Series/Dislikes/int:id
        // Update dislikes
        public IActionResult Dislikes(int? SeriesId, int? WatchlistId)
        {
            // Assert series id
            if (SeriesId == null || SeriesId == 0)
            {
                return NotFound();
            }

            // Assert watchlist id
            if (WatchlistId == null || WatchlistId == 0)
            {
                return NotFound();
            }

            _seriesRepository.UpdateDislikes((int)SeriesId);

            return RedirectToAction("Details", "Watchlists", new { Id = (int)WatchlistId });
        }
    }
}
