using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineFutbol.Models;

namespace OnlineFutbol.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private IHostingEnvironment _env;

        private readonly FutbolContext db = null;

        public AdminController(ILogger<AdminController> logger, FutbolContext _db,IHostingEnvironment env)
        {
            this.db = _db;
            _logger = logger;
            _env = env;
        }

        public IActionResult Matchs()
        {
            return View();
        }

       

        [HttpGet("admin/matchs-all")]
        public IActionResult MatchsAll()
        {
            return Json(db.Matches);
        }

        [HttpPost("admin/matchs-insert")]
        public IActionResult MatchsInsert([FromBody] Matches match)
        {
            db.Matches.Add(match);
            db.SaveChanges();
            return Json(match);
        }

        [HttpPost("admin/matchs-update")]
        public IActionResult MatchsUpdate([FromBody] Matches match)
        {

            var oldMatch = db.Matches.FirstOrDefault(x => x.Id == match.Id);
            oldMatch.TeamAwayId = match.TeamAwayId;
            oldMatch.TeamHomeId = match.TeamHomeId;
            oldMatch.EventDate = match.EventDate;
            oldMatch.Priority = match.Priority;
            oldMatch.IsLive = match.IsLive;
            oldMatch.Url = match.Url;
            db.SaveChanges();

            return Json(match);
        }

        [HttpGet("admin/matchs-remove/{id}")]
        public IActionResult MatchsRemove(int id)
        {
            var match = db.Matches.FirstOrDefault(x => x.Id == id);
            db.Matches.Remove(match);
            db.SaveChanges();
            return Json(match);
        }


        // Teams        
        public IActionResult Teams()
        {
            return View();
        }          

        [HttpGet("admin/teams-all")]
        public IActionResult TeamsAll()
        {
            return Json(db.Teams);
        }

        [HttpPost("admin/teams-insert")]
        public IActionResult TeamsInsert([FromBody] Teams team)
        {
            db.Teams.Add(team);
            db.SaveChanges();
            return Json(team);
        }

        [HttpPost("admin/teams-update")]
        public IActionResult TeamsUpdate([FromBody] Teams team)
        {

            var oldTeam = db.Teams.FirstOrDefault(x => x.Id == team.Id);
            oldTeam.Name = team.Name;
            oldTeam.ImageName = team.ImageName;
            db.SaveChanges();

            return Json(team);
        }

        [HttpGet("admin/teams-remove/{id}")]
        public IActionResult TeamsRemove(int id)
        {
            var teams = db.Teams.FirstOrDefault(x => x.Id == id);
            db.Teams.Remove(teams);
            db.SaveChanges();
            return Json(teams);
        }

        public IActionResult Info()
        {
            return View();
        }   


        public IActionResult Images()
        {
            var root = _env.WebRootPath;
            var images =  Directory.GetFiles(System.IO.Path.Combine(root ,"images"));
            var result = images.Select(x => new System.IO.FileInfo(x).Name).OrderBy(x => x).ToArray();
           
            return View(result);
        }    

        public async Task<IActionResult> Upload(IFormFile file){
            var root = _env.WebRootPath;
            var path =  System.IO.Path.Combine(root ,"images");
            var filePath =  System.IO.Path.Combine(path ,file.FileName);
            //file.CopyTo(new FileStream(filePath, FileMode.Create));

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return RedirectToAction("Images");
        }
    }
}
