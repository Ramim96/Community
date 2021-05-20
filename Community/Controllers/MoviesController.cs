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
    public class MoviesController : Controller 
    {
        private readonly IMovie _movieRepository;
        private readonly IMovieWatchlist _movieWatchlistRepository;

        public MoviesController(IMovie movieRepository, IMovieWatchlist movieWatchlistRepository)
        {
            _movieRepository = movieRepository;
            _movieWatchlistRepository = movieWatchlistRepository;
        }

        // GET: Movies/(Index)?
        // Get all movies 
        [HttpGet]
        public IActionResult Index()
        {
            // Get all movies from the database
            MovieViewModel movieViewModel = new MovieViewModel();
            movieViewModel.allMovies = _movieRepository.GetAll;

            return View(movieViewModel);
        }

        // GET: Movies/Create
        // Get create movie view
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // Create a new movie
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie)
        {
            // Assert form input validation
            if(!ModelState.IsValid)
            {
                // Return to current view with validaton errors
                return View(movie);
            }

            // Check if movie already exists
            bool existingMovie = _movieRepository.CheckExists(movie);

            // Assert instance result
            if(!existingMovie)
            {
                // Create movie service
                _movieRepository.Create(movie);

                // Redirect to movies list
                return RedirectToAction("Index");
            }

            // Redirect to movies list
            return RedirectToAction("Index");
        }

        // GET: Movies/Details/id:int
        // Get movie details view
        [HttpGet]
        public IActionResult Details(int? Id)
        {
            // Assert movie id
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            // Find movie by id and assert the result
            Movie movie = _movieRepository.GetById((int)Id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Edit/id:int
        // Get edit movie view
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            // Assert movie id
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            // Find movie by id and assert the result
            Movie movie = _movieRepository.GetById((int)Id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Edit/id:int
        // Post new movie details data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Movie movie)
        {
            // Assert form input validation
            if (!ModelState.IsValid)
            {
                // Return to current view with validaton errors
                return View(movie);
            }

            // Update movie
            _movieRepository.Edit(movie);

            return RedirectToAction("Index");
        }

        // GET: Movies/DeleteMovie/id:int
        // Get delete movie view
        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            // Assert movie id
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            // Find movie by id and assert the result
            Movie movie = _movieRepository.GetById((int)Id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/id:int
        // Delete a movie
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Movie movie)
        {
            //// Assert movie id
            //if (Id == null || Id == 0)
            //{
            //    return NotFound();
            //}

            //// Find movie by id and assert the result
            //Movie movieToDelete = _movieRepository.GetById((int)Id);
            //if (movieToDelete == null)
            //{
            //    return NotFound();
            //}

            // Assert movie id
            if (movie.MovieId == 0)
            {
                return NotFound();
            }

            // Find movie by id and assert the result
            Movie movieToDelete = _movieRepository.GetById(movie.MovieId);
            if (movieToDelete == null)
            {
                return NotFound();
            }

            _movieRepository.Delete(movieToDelete);

            return RedirectToAction("Index");
        }

        // POST: Movies/Likes/int:id
        // Update likes
        public IActionResult Likes(int? MovieId, int? WatchlistId)
        {
            // Assert movie id
            if (MovieId == null || MovieId == 0)
            {
                return NotFound();
            }

            // Assert watchlist id
            if (WatchlistId == null || WatchlistId == 0)
            {
                return NotFound();
            }

            _movieRepository.UpdateLikes((int)MovieId);

            return RedirectToAction("Details", "Watchlists", new { Id = (int)WatchlistId });
        }

        // POST: Movies/Dislikes/int:id
        // Update dislikes
        public IActionResult Dislikes(int? MovieId, int? WatchlistId)
        {
            // Assert movie id
            if (MovieId == null || MovieId == 0)
            {
                return NotFound();
            }

            // Assert watchlist id
            if (WatchlistId == null || WatchlistId == 0)
            {
                return NotFound();
            }

            _movieRepository.UpdateDislikes((int)MovieId);

            return RedirectToAction("Details", "Watchlists", new { Id = (int)WatchlistId });
        }
    }
}
