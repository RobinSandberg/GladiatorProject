using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GladiatorProject.Models;
using System.Data.Entity;

namespace GladiatorProject.Controllers
{
    [Authorize(Roles = "Overlord")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PlayerList()
        {
            return PartialView("_PlayerList", db.Users.Include("Gladiators").Include("Roles").ToList());
        }

        public ActionResult OpponentList()
        {
            return PartialView("_OpponentList", db.Opponents.ToList());
        }

        public ActionResult HighscoreList()
        {

            return PartialView("_HighScore");
        }

        public ActionResult PlayerHighScore()
        {
            
            return PartialView("_highscorePlayers", db.Users.Include("Gladiators").ToList());
        }

        public ActionResult GladiatorHighScore()
        {
            
            return PartialView("_highscoreGladiators", db.Gladiators.Include("User").ToList());
        }


    }
}