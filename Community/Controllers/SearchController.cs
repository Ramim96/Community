using Community.Interfaces;
using Community.Models;
using Community.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearch _searchRepository;

        public SearchController(ISearch searchRepository)
        {
            _searchRepository = searchRepository;
        }

        // GET: /Search/(Index)?
        // Get the search index page
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Search/(Index)?Title+Type
        // Post a search term and redirect to action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Search search)
        {
            // Assert form input validation
            if(!ModelState.IsValid)
            {
                // Return to current view with validaton errors
                return View(search);
            }

            // Assert search type and redirect with URL parameters
            if(search.Type == "Movie")
            {
                return RedirectToAction("Result", new { Title = search.Title, Type = search.Type });
            }

            if(search.Type == "Series")
            {
                return RedirectToAction("Result", new { Title = search.Title, Type = search.Type });
            }

            if(search.Type == "Watchlist")
            {
                return RedirectToAction("Result", new { Title = search.Title, Type = search.Type });
            }

            return View(search);
        }

        // POST: Search/Result?Title+Type
        // Return search result
        public IActionResult Result(string Title, string Type) 
        {
            SearchViewModel searchViewModel = new SearchViewModel();

            // Assert search types
            if(Type == "Movie")
            {
                searchViewModel.Type = Type;
                
                // Fetch movies according to search query
                searchViewModel.moviesResult = _searchRepository.getMoviesResult(Title, Type);
                searchViewModel.seriesResult = null;
                searchViewModel.watchlistsResult = null;

                return View(searchViewModel);
            }

            if (Type == "Series")
            {
                searchViewModel.Type = Type;

                // Fetch series according to search query
                searchViewModel.seriesResult = _searchRepository.getSeriesResult(Title, Type);
                searchViewModel.moviesResult = null;
                searchViewModel.watchlistsResult = null;

                return View(searchViewModel);
            }

            if (Type == "Watchlist")
            {
                searchViewModel.Type = Type;

                // Fetch watchlists according to search query
                searchViewModel.watchlistsResult = _searchRepository.getWatchlistsResult(Title, Type);
                searchViewModel.moviesResult = null;
                searchViewModel.seriesResult = null;

                return View(searchViewModel);
            }

            return View(searchViewModel);
        }
    }
}
