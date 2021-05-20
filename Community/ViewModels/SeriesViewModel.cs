using Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.ViewModels
{
    public class SeriesViewModel
    {
        public IEnumerable<Series> allSeries { get; set; }
    }
}
