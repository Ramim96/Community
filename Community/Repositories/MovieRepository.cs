using Community.Interfaces;
using Community.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Repositories
{
    public class MovieRepository : IMovie
    {
        // Db context service 
        private readonly ApplicationDbContext _db;

        // Constructor
        public MovieRepository(ApplicationDbContext db) => _db = db;

        // Get a movie by id
        public Movie GetById(int Id)
        {
            return _db.Movies
                      .Include(m => m.Watchlists)
                      .ThenInclude(mwl => mwl.Watchlist)
                      .FirstOrDefault(m => m.MovieId == Id);
        }

        // Get all movies
        public ICollection<Movie> GetAll
        {
            get
            {
                return _db.Movies
                          .Include(m => m.Watchlists)
                          .ThenInclude(mwl => mwl.Watchlist)
                          .OrderBy(m => m.Title)
                          .ToList();
            }
        }

        // Create a new movie
        public void Create(Movie movie)
        {
            _db.Movies.Add(movie);
            _db.SaveChanges();

            return;
        }

        // Delete a movie
        public void Delete(Movie movie)
        {
            _db.Movies.Remove(movie);
            _db.SaveChanges();

            return;
        }

        // Edit a movie
        public void Edit(Movie movie)
        {
            _db.Movies.Update(movie);
            _db.SaveChanges();

            return;
        }

        // Update likes
        public void UpdateLikes(int Id)
        {
            var movie = GetById(Id);

            movie.Likes += 1;
            _db.SaveChanges();

            return;
        }

        // Update dislikes
        public void UpdateDislikes(int Id)
        {
            var movie = GetById(Id);

            movie.Dislikes += 1;
            _db.SaveChanges();

            return;
        }

        // Check if movie already exists 
        public bool CheckExists(Movie movie)
        {
            var existingMovie = _db.Movies.FirstOrDefault(
                                        m => m.Title == movie.Title && 
                                        m.Directors == movie.Directors && 
                                        m.Actors == movie.Actors);

            if(existingMovie == null)
            {
                return false;
            }

            return true;
        }
    }
}
