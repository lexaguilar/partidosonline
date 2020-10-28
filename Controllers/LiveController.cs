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
    public class LiveController : Controller
    {
        private readonly ILogger<LiveController> _logger;
        private readonly FutbolContext db = null;

        public LiveController(ILogger<LiveController> logger, FutbolContext _db)
        {
            this.db = _db;
            _logger = logger;
        }   

         public IActionResult Index()
        {           
             var match = db.Matches
            .Include(x => x.TeamAway)
            .Include(x =>x.TeamHome)
            .Select(x => new ViewMatch{
                Id = x.Id,
                EventDate = x.EventDate,
                Priority = x.Priority,
                TeamHome = x.TeamHome.Name,
                TeamAway = x.TeamAway.Name,
                IsLive  = x.IsLive??false,
            })
            .Where(x => x.EventDate > DateTime.Now.AddHours(-2) && x.Priority > 0)
            .OrderByDescending(x => x.Id)
            .FirstOrDefault();

            return View(match);
        }

        [HttpGet("live/main")]
        public IActionResult Main()
        {
            var match = db.Matches
            .Include(x => x.TeamAway)
            .Include(x =>x.TeamHome)
            .Select(x => new{
                x.Id,
                x.EventDate,
                x.Priority,
                TeamHome = x.TeamHome.Name,
                TeamAway = x.TeamAway.Name,
                IsLive = x.IsLive??false,
            })
            .Where(x => x.EventDate > DateTime.Now.AddHours(-2) && x.Priority > 0)
            .OrderByDescending(x => x.Id)
            .FirstOrDefault();

            return Json(match);
        }

         [HttpGet("live/main/{id}")]
        public IActionResult Main(int id)
        {
            var match = db.Matches
            .Include(x => x.TeamAway)
            .Include(x =>x.TeamHome)
            .Select(x => new{
                x.Id,
                x.EventDate,
                x.Priority,
                TeamHome = x.TeamHome.Name,
                TeamAway = x.TeamAway.Name,
                IsLive = x.IsLive??false,
            })           
            .FirstOrDefault(x => x.Id == id);

            return Json(match);
        }


         [HttpGet("live/next")]
        public IActionResult Next()
        {
           var matches = db.Matches
            .Include(x => x.TeamAway)
            .Include(x =>x.TeamHome)
            .Select(x => new{
                x.Id,
                x.EventDate,
                x.Priority,
                TeamHome = x.TeamHome.Name,
                TeamAway = x.TeamAway.Name,
                TeamHomeImageName = x.TeamHome.ImageName,
                TeamAwayImageName = x.TeamAway.ImageName,
            })
            .Where(x => x.EventDate > DateTime.Now.AddHours(-2))
            .OrderBy(x => x.Id);
            return Json(matches);
        }

        [HttpGet("live/coming/{id}")]
        public IActionResult Next(int id)
        {
           var matches = db.Matches
            .Include(x => x.TeamAway)
            .Include(x =>x.TeamHome)
            .Select(x => new{
                x.Id,
                x.EventDate,
                x.Priority,
                TeamHome = x.TeamHome.Name,
                TeamAway = x.TeamAway.Name,
                TeamHomeImageName = x.TeamHome.ImageName,
                TeamAwayImageName = x.TeamAway.ImageName,
            })
            .Where(x => x.EventDate > DateTime.Now.AddHours(-2) && x.Id != id)
            .OrderBy(x => x.Id);
            return Json(matches);
        }

    }
}
