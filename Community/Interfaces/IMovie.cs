using Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Interfaces
{
    public interface IMovie
    {
        public ICollection<Movie> GetAll { get; }
        public Movie GetById(int Id);
        public void Create(Movie movie);
        public void Edit(Movie movie);
        public void Delete(Movie movie);
        public void UpdateLikes(int Id);
        public void UpdateDislikes(int Id);
        public bool CheckExists(Movie movie);
    }
}
