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

        public ActionResult SearchPlayer(string searchString)
        {
            var player_search = from p in db.Users.Include("Gladiators").Include("Roles") select p;  // making a variable to pick out the names from.

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                player_search = player_search.Where(i => i.UserName.ToLower().Contains(searchString) || i.Email.ToLower().Contains(searchString)); // picking out the names based on the string.
            }

            return PartialView("_PlayerSearch", player_search.ToList());
        }

        public ActionResult OpponentList()
        {
            return PartialView("_OpponentList", db.Opponents.ToList());
        }

        public ActionResult SearchOpponentName(string searchString)
        {
            var opponent_name = from o in db.Opponents select o; 

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                opponent_name = opponent_name.Where(i => i.Name.ToLower().Contains(searchString)); 
            }

            return PartialView("_OpponentSearch", opponent_name.ToList());
        }

        public ActionResult SearchOpponentLeveL(int? searchLevel) // checking so it contains a int.
        {
            var opponent_level = from o in db.Opponents select o;

            if (searchLevel == null) // if it don't contain a int return the full list back.
            {
                return PartialView("_OpponentSearch", db.Opponents.ToList());
            }
            else if(searchLevel >= 1 && searchLevel <= 20) // return the selected level if the person search between lvl 1 and 20.
            {
                opponent_level = opponent_level.Where(i => i.Level == (searchLevel));
                return PartialView("_OpponentSearch", opponent_level.ToList());
            }
            else   // if lower then level 1 or higher then level 20 return full list.
            {
                return PartialView("_OpponentSearch", db.Opponents.ToList());
            }
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
            if(User == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                return View(User);
            }
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
                return View(user);
            }
          
        }

        public ActionResult UserDeleteConfirm(string id)
        {
            var user = db.Users.SingleOrDefault(i => i.Id == id);

            if (user == null)
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

        public ActionResult GladiatorEdit(int id , string playerId)
        {
            if(playerId != null)
            {
                var PlayerUser = db.Users.Include("Gladiators").SingleOrDefault(i => i.Id == playerId);
                Session["Player"] = PlayerUser;
                var gladiator = PlayerUser.Gladiators.SingleOrDefault(u => u.Id == id);
                if (gladiator == null)
                {
                    return new HttpStatusCodeResult(400);
                }
                else
                {
                    return View(gladiator);
                }
            }
            else
            {
                var gladiatortrash = db.Gladiators.SingleOrDefault(g => g.Id == id);
                if(gladiatortrash == null)
                {
                    return new HttpStatusCodeResult(400);
                }
                else
                {
                    return View(gladiatortrash);
                }
            }
        }

        public ActionResult GladiatorSave(Gladiator gladiator)
        {
            if(Session["Player"] != null)
            {
                string PlayerId = (Session["Player"] as ApplicationUser).Id;
               
                 var PlayerUser = db.Users.Include("Gladiators").SingleOrDefault(i => i.Id == PlayerId);
                 var oldGladiator = PlayerUser.Gladiators.SingleOrDefault(i => i.Id == gladiator.Id);
                 if (oldGladiator == null)
                 {
                     return new HttpStatusCodeResult(400);
                 }
                 else
                 {
                     if (ModelState.IsValid)
                     {
                        oldGladiator.Name = gladiator.Name;
                        oldGladiator.Level = gladiator.Level;
                        oldGladiator.Experiance = gladiator.Experiance;
                        oldGladiator.FullHealth = gladiator.FullHealth;
                        oldGladiator.Armor = gladiator.Armor;
                        oldGladiator.DamageDice = gladiator.DamageDice;
                        oldGladiator.Strenght = gladiator.Strenght;
                        oldGladiator.StrenghtModifyer = gladiator.StrenghtModifyer;
                        oldGladiator.Constitution = gladiator.Constitution;
                        oldGladiator.ConstitutionModifyer = gladiator.ConstitutionModifyer;
                        oldGladiator.Battles = gladiator.Battles;
                        oldGladiator.BattlesWon = gladiator.BattlesWon;
                        oldGladiator.BattlesDraw = gladiator.BattlesDraw;
                        oldGladiator.BattlesLost = gladiator.BattlesLost;
                        oldGladiator.Gold = gladiator.Gold;
                        oldGladiator.MaxArmor = gladiator.MaxArmor;
                        oldGladiator.SkillPoints = gladiator.SkillPoints;
                        oldGladiator.CurrentWinningStreak = gladiator.CurrentWinningStreak;
                        oldGladiator.BestWinningStreak = gladiator.BestWinningStreak;
                         db.SaveChanges();
                         return RedirectToAction("Index");
                     }
                     else
                     {
                         return View("GladiatorEdit", gladiator);
                     }
                 }
            }
            else
            {
                var oldGladiatortrash = db.Gladiators.SingleOrDefault(g => g.Id == gladiator.Id);
                if (oldGladiatortrash == null)
                {
                    return new HttpStatusCodeResult(400);
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        oldGladiatortrash.Name = gladiator.Name;
                        oldGladiatortrash.Level = gladiator.Level;
                        oldGladiatortrash.Experiance = gladiator.Experiance;
                        oldGladiatortrash.FullHealth = gladiator.FullHealth;
                        oldGladiatortrash.Armor = gladiator.Armor;
                        oldGladiatortrash.DamageDice = gladiator.DamageDice;
                        oldGladiatortrash.Strenght = gladiator.Strenght;
                        oldGladiatortrash.StrenghtModifyer = gladiator.StrenghtModifyer;
                        oldGladiatortrash.Constitution = gladiator.Constitution;
                        oldGladiatortrash.ConstitutionModifyer = gladiator.ConstitutionModifyer;
                        oldGladiatortrash.Battles = gladiator.Battles;
                        oldGladiatortrash.BattlesWon = gladiator.BattlesWon;
                        oldGladiatortrash.BattlesDraw = gladiator.BattlesDraw;
                        oldGladiatortrash.BattlesLost = gladiator.BattlesLost;
                        oldGladiatortrash.Gold = gladiator.Gold;
                        oldGladiatortrash.MaxArmor = gladiator.MaxArmor;
                        oldGladiatortrash.SkillPoints = gladiator.SkillPoints;
                        oldGladiatortrash.CurrentWinningStreak = gladiator.CurrentWinningStreak;
                        oldGladiatortrash.BestWinningStreak = gladiator.BestWinningStreak;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("GladiatorEdit", gladiator);
                    }
                }
            }
        }

        public ActionResult GladiatorRestore(int id)
        {

            var gladiator = db.Gladiators.SingleOrDefault(g => g.Id == id);
            if(gladiator == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                var PlayerUser = db.Users.Include("Gladiators").SingleOrDefault(i => i.UserName == gladiator.PreviousUser);
                if(PlayerUser == null)
                {
                    return new HttpStatusCodeResult(400);
                }
                else
                {
                    PlayerUser.Gladiators.Add(gladiator);
                    gladiator.PreviousUser = null;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            } 
        }

        public ActionResult GladiatorDetails(int id , string playerId)
        {
            if(playerId != null)
            {
                var PlayerUser = db.Users.Include("Gladiators").SingleOrDefault(i => i.Id == playerId);
                var gladiator = PlayerUser.Gladiators.SingleOrDefault(i => i.Id == id);
                if (gladiator == null)
                {
                    return new HttpStatusCodeResult(400);
                }
                else
                {
                    return PartialView("_GladiatorDetails", gladiator);
                }
            }
            else
            {
                var gladiatortrash = db.Gladiators.SingleOrDefault(g => g.Id == id);
                if (gladiatortrash == null)
                {
                    return new HttpStatusCodeResult(400);
                }
                else
                {
                    return PartialView("_GladiatorDetails", gladiatortrash);
                }
            }
         
        }

        public ActionResult GladiatorHide()
        {
            return Content("");
        }

        public ActionResult GladiatorDelete(int id , string playerId)
        {
            if(playerId != null)
            {
                var PlayerUser = db.Users.Include("Gladiators").SingleOrDefault(i => i.Id == playerId);
                Session["Player"] = PlayerUser;  // saving the user in session to move over the id to confirmation action.
                var gladiator = PlayerUser.Gladiators.SingleOrDefault(u => u.Id == id);

                if (gladiator == null)
                {
                    return new HttpStatusCodeResult(400);
                }
                else
                {
                    return View(gladiator);
                }
                
            }
            else
            {
                var gladiatortrash = db.Gladiators.SingleOrDefault(g => g.Id == id);
                if(gladiatortrash == null)
                {
                    return new HttpStatusCodeResult(400);
                }
                else
                {
                    return View(gladiatortrash);
                }
            }
        }

        public ActionResult GladiatorDeleteConfirm(int id)
        {
            if(Session["Player"] != null)
            {
                string PlayerId = (Session["Player"] as ApplicationUser).Id; // taking out the Id for the user from the session.
                var PlayerUser = db.Users.Include("Gladiators").SingleOrDefault(i => i.Id == PlayerId);
                var gladiator = PlayerUser.Gladiators.SingleOrDefault(u => u.Id == id);
                if (gladiator == null)
                {
                    return new HttpStatusCodeResult(400);
                }
                else
                {
                    gladiator.PreviousUser = PlayerUser.UserName;
                    PlayerUser.Gladiators.Remove(gladiator);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var gladiatortrash = db.Gladiators.SingleOrDefault(g => g.Id == id);
                if(gladiatortrash == null)
                {
                    return new HttpStatusCodeResult(400);
                }
                else
                {
                    db.Gladiators.Remove(gladiatortrash);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
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
            if(Opponent == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                return View(Opponent);
            }
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
            if(Opponent == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                return PartialView("_OpponentDetails", Opponent);
            }
        }

        public ActionResult HideOpponent()
        {
            return Content("");
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
  
                return View(Opponent);
            }
            
        }

        public ActionResult OpponentDeleteConfirm(int id)
        {
            var Opponent = db.Opponents.SingleOrDefault(i => i.Id == id);

            if (Opponent == null)
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
            var Players = from p in db.Users.Include("Gladiators") where p.AccountHighScore >= 0 select p;

            return PartialView("_highscorePlayers", Players.OrderByDescending(u => u.AccountHighScore).Take(10).ToList());
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
                    oldUser.AccountScore = user.AccountScore;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View("UserScoreEdit", user);
            }
        }

        public ActionResult UserScoreBan(string id)  //Calling it Ban in lack of better idea right now.
        {
            var user = db.Users.SingleOrDefault(i => i.Id == id);
            if(user == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                return View(user);
            }
        }

        public ActionResult UserScoreBanConfirm(string id)  
        {
            var user = db.Users.SingleOrDefault(i => i.Id == id);
            if (user == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                user.AccountHighScore = int.MinValue;  // putting the players highscore to a huge negative number so they will be no where near the top 10.
                user.AccountScore = int.MinValue;
                db.SaveChanges();
                return RedirectToAction("Index");
            }   
        }

        public ActionResult GladiatorHighScore()
        {
            var Gladiators = from p in db.Gladiators.Include("User") where p.GladiatorHighScore >= 0 && p.User != null select p;

            return PartialView("_highscoreGladiators", Gladiators.OrderByDescending(u => u.GladiatorHighScore).Take(10).ToList());
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
                oldGladiator.GladiatorHighScore = gladiator.GladiatorHighScore;
                oldGladiator.GladiatorScore = gladiator.GladiatorScore;
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
           
        }

        public ActionResult GladiatorScoreBan(int id)
        {
            var gladiator = db.Gladiators.SingleOrDefault(i => i.Id == id);
            if(gladiator == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                return View(gladiator);
            }
        }

        public ActionResult GladiatorScoreBanConfirm(int id)
        {
            var gladiator = db.Gladiators.SingleOrDefault(i => i.Id == id);

            if (gladiator == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                gladiator.GladiatorHighScore = int.MinValue;
                gladiator.GladiatorScore = int.MinValue;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Gladiators()
        {
            var gladiators = from p in db.Gladiators where p.User == null select p;
            return PartialView("_Gladiators", gladiators.ToList());
        }
        public ActionResult SearchGladiator(string searchString)
        {
            var gladiator_search = from p in db.Gladiators select p;  // making a variable to pick out the names from.

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                gladiator_search = gladiator_search.Where(i => i.PreviousUser.ToLower().Contains(searchString)); // picking out the names based on the string.
            }

            return PartialView("_GladiatorSearch", gladiator_search.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}