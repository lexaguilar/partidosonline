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
    public class MatchesController : Controller
    {
        private readonly ILogger<MatchesController> _logger;
        private readonly FutbolContext db = null;

        public MatchesController(ILogger<MatchesController> logger, FutbolContext _db)
        {
            this.db = _db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Stream(int id)
        {
            var match = db.Matches
            .Include(x => x.TeamHome)
            .Include(x => x.TeamAway)
            .FirstOrDefault(x => x.Id == id);         

            return View(match);
        }

     

        [HttpGet("admin/match/{id}/viewer/{uid}")]
        public IActionResult Viewers(int id, string uid)
        {
           //if(!db.Viewers.Any(x => x.MatchId == id && x.Uid == uid)){
            var viewer = new Viewers{
                Uid = uid,
                MatchId = id,
                Added = DateTime.Now
            };
            db.Viewers.Add(viewer);
            db.SaveChanges();
           //}

           var total = db.Viewers.Where(x => x.MatchId == id && x.Added >= DateTime.Now.AddSeconds(-5));
           return Json(total.Count());
        }
    }
}
