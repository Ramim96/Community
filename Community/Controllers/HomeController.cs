using Community.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/(Index)?
        // Get index page 
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Home/About
        // Get about page
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
    }
}
