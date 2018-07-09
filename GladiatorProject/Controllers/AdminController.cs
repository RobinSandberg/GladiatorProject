using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GladiatorProject.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(RegisterViewModel register) // pulling in the info from the register form
        {
            
             if (ModelState.IsValid)
             {
                var user = new ApplicationUser(); //Making a new user
                user.Email = register.Email;   // plant the registration info into the user
                user.UserName = register.UserName;

                //some code that no longer needed but saved for exemple.
                //var hasher = new PasswordHasher();  // making a variable to hash the password
                //user.PasswordHash = hasher.HashPassword(register.Password.ToString());  // convert the password into passwordhash
                //user.SecurityStamp = Guid.NewGuid().ToString();  // making a simple security stamp 

                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new UserManager<ApplicationUser>(userStore);  // Usermanager do the password hash and stamp for me.
                var result = userManager.Create(user, register.Password); // taking the user and hashing his password and adding security stamp and adding him to db users.
                if (result.Succeeded)   // if everything works it will add a role to the user.
                {
                    userManager.AddToRole(user.Id, "Player");  //adding a role to the user
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return new HttpStatusCodeResult(400);
                }
             }
            
            return View(register);

        }

        public ActionResult UserEdit(string id)
        {
            var User = db.Users.SingleOrDefault(i => i.Id == id);

            return View(User);
        }

        public ActionResult UserSave(ApplicationUser user)
        {
            ApplicationUser oldUser = db.Users.SingleOrDefault(i => i.Id == user.Id);
            if (oldUser == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    oldUser.UserName = user.UserName;  // Updating the user info to the new info.
                    oldUser.Email = user.Email;
                    oldUser.AccountHighScore = user.AccountHighScore;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                    
                }
            }
            return View("UserEdit", user);
           
        }

        public ActionResult UserDelete(string id)
        {
            var user = db.Users.SingleOrDefault(i => i.Id == id);

            if(user == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          
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
            Opponent oldopponent = db.Opponents.SingleOrDefault(i => i.Id == opponent.Id);  //saving the opponet as a old version.

            if (oldopponent == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    oldopponent.Name = opponent.Name;  // Updating the info from old to the new.
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
                return View("OpponentEdit", opponent);
            }
           
        }

        public ActionResult OpponentDetails(int id)
        {
            var Opponent = db.Opponents.SingleOrDefault(i => i.Id == id);

            return PartialView("_OpponentDetails", Opponent);
        }

        public ActionResult OpponentDelete(int id)
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

        public ActionResult UserScoreEdit(string id)
        {
            var user = db.Users.SingleOrDefault(i => i.Id == id);

            return View(user);
        }

        public ActionResult UserScoreSave(ApplicationUser user)
        {
            var oldUser = db.Users.SingleOrDefault(i => i.Id == user.Id);

            if(oldUser == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    oldUser.AccountHighScore = user.AccountHighScore;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View("UserScoreEdit", user);
            }
        }

        public ActionResult UserScoreDelete(string id)
        {
            var user = db.Users.SingleOrDefault(i => i.Id == id);

            user.AccountHighScore = int.MinValue;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GladiatorHighScore()
        {
            
            return PartialView("_highscoreGladiators", db.Gladiators.Include("User").ToList());
        }

        public ActionResult GladiatorScoreEdit(int id)
        {
            var gladiator = db.Gladiators.SingleOrDefault(i => i.Id == id);

            return View(gladiator);
        }

        public ActionResult GladiatorScoreSave(Gladiator gladiator)
        {

            var oldGladiator = db.Gladiators.SingleOrDefault(i => i.Id == gladiator.Id);

            if(oldGladiator == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    oldGladiator.GladiatorHighScore = gladiator.GladiatorHighScore;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View("GladiatorScoreEdit", gladiator);
            }
        }

        public ActionResult GladiatorScoreDelete(int id)
        {
            var gladiator = db.Gladiators.SingleOrDefault(i => i.Id == id);

            gladiator.GladiatorHighScore = int.MinValue;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}