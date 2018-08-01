using GladiatorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace GladiatorProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult HighScoreLists()
        {
             // Open the base partial for the highscore lists.
            return PartialView("_highscoreLists");
        }

        public ActionResult PlayerHighScore()
        {
            // Sending the list of users including their gladiators to the view.
            var Players = from p in db.Users.Include("Gladiators") where p.AccountHighScore >= 0 select p;

            return PartialView("_highscorePlayers", Players.OrderByDescending(i => i.AccountHighScore).Take(10).ToList());  // Order them by the highest score at top and 10 players.
        }

        public ActionResult GladiatorHighScore()
        {
            //sending the gladiators including what user they belong to the view.
            var Gladiators = from p in db.Gladiators.Include("User") where p.GladiatorHighScore >= 0 && p.User != null select p;

            return PartialView("_highscoreGladiators", Gladiators.OrderByDescending(u => u.GladiatorHighScore).Take(10).ToList()); //sort the so the highest scores comes at the top and only add 10 to the view.
        }

        public ActionResult Foxhunting()
        {
            return View();
        }

        public ActionResult Foxsurvival()
        {
            return View();
        }

    }
}