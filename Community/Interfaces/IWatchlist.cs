using Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Interfaces
{
    public interface IWatchlist
    {
        public ICollection<Watchlist> GetAll { get; }
        public Watchlist GetById(int Id);
        public void Create(Watchlist watchlist);
        public void Edit(Watchlist watchlist);
        public void Delete(Watchlist watchlist);
        public bool CheckExists(Watchlist watchlist);
    }
}
