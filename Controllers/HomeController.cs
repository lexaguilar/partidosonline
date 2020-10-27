using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineFutbol.Models;

namespace OnlineFutbol.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FutbolContext db = null;

        public HomeController(ILogger<HomeController> logger, FutbolContext _db)
        {
            this.db = _db;
            _logger = logger;
        }

        public IActionResult Index()
        {           
            return View();
        }
        public IActionResult Stream()
        {
            return View();
        }

        public IActionResult Coming()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Live(int id)
        {
            var match = db.Matches
            .Include(x => x.TeamHome)
            .Include(x => x.TeamAway)
            .FirstOrDefault(x => x.Id == id);

            if(match.EventDate >= DateTime.Now.AddHours(2) )
                return View("Preview", match);

            if(match.EventDate >= DateTime.Now && match.EventDate <= DateTime.Now.AddHours(2))
                match.Priority = 1;
            else
                match.Url = "";

            

            return View(match);
        }

    }
}
