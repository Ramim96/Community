using Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Interfaces
{
    public interface ISeries
    {
        public ICollection<Series> GetAll { get; }
        public Series GetById(int Id);
        public void Create(Series series);
        public void Edit(Series series);
        public void Delete(Series series);
        public void UpdateLikes(int Id);
        public void UpdateDislikes(int Id);
        public bool CheckExists(Series series);
    }
}
