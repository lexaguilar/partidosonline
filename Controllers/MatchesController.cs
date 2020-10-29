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

        public IActionResult Preview(string id)
        {
            var matchId = StringCipher.Decrypt(id);

            var match = db.Matches
            .Include(x => x.TeamHome)
            .Include(x => x.TeamAway)
            .FirstOrDefault(x => x.Id == matchId);

            if(match == null)
                match = new Matches{
                    Id=matchId,
                    TeamAway = new Teams{ Name = "" },
                    TeamHome = new Teams{ Name = "" }
                };

            return View(match);
        }

        public IActionResult Stream(string id)
        {
            var matchId = StringCipher.Decrypt(id);

            var match = db.Matches
            .Include(x => x.TeamHome)
            .Include(x => x.TeamAway)            
            .FirstOrDefault(x => x.Id == matchId);         

            return View(match);
        }

        public IActionResult Option2(string id)
        {
            var matchId = StringCipher.Decrypt(id);

            var match = db.Matches
            .Include(x => x.TeamHome)
            .Include(x => x.TeamAway)
            .FirstOrDefault(x => x.Id == matchId);         

            return View(match);
        }

     

        [HttpGet("admin/match/{id}/viewer/{uid}")]
        public IActionResult Viewers(string id, string uid)
        {
            var matchId = StringCipher.Decrypt(id);
           //if(!db.Viewers.Any(x => x.MatchId == id && x.Uid == uid)){
            var viewer = new Viewers{
                Uid = uid,
                MatchId = matchId,
                Added = DateTime.Now
            };
            db.Viewers.Add(viewer);
            db.SaveChanges();
           //}

           var total = db.Viewers.Where(x => x.MatchId == matchId).Select(x => x.Uid).Distinct();
           return Json(total.Count());
        }
    }
}
