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

            // A tiny miss with the player highscore to look into later is if you swap gladiator mid win streak the player score will reset.
            return PartialView("_highscorePlayers", db.Users.Include("Gladiators").ToList());
        }

        public ActionResult GladiatorHighScore()
        {
            //sending the gladiators including what user they belong to the view.
            return PartialView("_highscoreGladiators", db.Gladiators.Include("User").ToList());
        }

    }
}