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

        [HttpGet]
        public ActionResult PlayerCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PlayerCreate(ApplicationUser user)
        {
            return View();
        }

        public ActionResult PlayerEdit()
        {
            return View();
        }

        public ActionResult PlayerSave()
        {
            return View();
        }

        public ActionResult OpponentList()
        {
            return PartialView("_OpponentList", db.Opponents.ToList());
        }

        [HttpGet]
        public ActionResult CreateOpponent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateOpponent(Opponent opponent)
        {
            if (opponent.Level >= 1 && opponent.Level <= 20)
            {
                if (ModelState.IsValid)
                {
                    db.Opponents.Add(opponent);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(opponent);
           
        }

        public ActionResult OpponentEdit(int id)
        {
            var Opponent = db.Opponents.SingleOrDefault(i => i.Id == id);

            return View(Opponent);
        }

       
        public ActionResult OpponentSave(Opponent opponent)
        {
            Opponent oldopponent = db.Opponents.SingleOrDefault(i => i.Id == opponent.Id);

            if (oldopponent == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                oldopponent.Name = opponent.Name;
                oldopponent.Level = opponent.Level;
                oldopponent.Health = opponent.Health;
                oldopponent.DamageDice = opponent.DamageDice;
                oldopponent.Armor = opponent.Armor;
                oldopponent.Strenght = opponent.Strenght;
                oldopponent.StrenghtModifyer = (opponent.Strenght - 10) / 2;
                oldopponent.Constitution = opponent.Constitution;
                oldopponent.ConstitutionModifyer = (opponent.Constitution - 10) / 2;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
        }

        public ActionResult OpponentDetails(int id)
        {
            var Opponent = db.Opponents.SingleOrDefault(i => i.Id == id);

            return PartialView("_OpponentDetails", Opponent);
        }

        public ActionResult OpponentDelete(int id) //might not use this one
        {
            var Opponent = db.Opponents.SingleOrDefault(i => i.Id == id);

            if(Opponent == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                db.Opponents.Remove(Opponent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
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