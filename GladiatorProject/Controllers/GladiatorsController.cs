using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GladiatorProject.Models;
using Microsoft.AspNet.Identity;

namespace GladiatorProject.Controllers
{
    [Authorize(Roles = "Player")] // Players and admin required to access any of the actions in gladiator controller.
    public class GladiatorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Gladiators
        
        public ActionResult Index()
        {
            var PlayerId = User.Identity.GetUserId();  // picking up the users Id.
            var PlayerUser = db.Users.Include("Gladiators").SingleOrDefault(u => u.Id == PlayerId); //Finding the user and his gladiators.
            if(PlayerUser == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                return View(PlayerUser.Gladiators);  // Sending the list of gladiators to the user.
            }
        }

        public ActionResult SelectGladiator(int id)
        {
            var gladiator = db.Gladiators.SingleOrDefault(i => i.Id == id); // picking out the gladiator by the Id.
            if(gladiator == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                Session["gladiator"] = gladiator; // saving the gladiator info into a session.
                return View("GladiatorMenu", gladiator); // This view should be renamed GladiatorMenu.
            }
           
        }

        public ActionResult PartFindOpponent(Opponent opponent ,int id)
        {
            var gladiator = db.Gladiators.SingleOrDefault(i => i.Id == id);
            // Making a temporary list of opponent based on gladiator level 1 lvl lower to 1 lvl higher.
            var Enemies = opponent.Levels;
            Random rnd = new Random();
            if(gladiator == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                if (gladiator.Level >= 2) // Making sure the gladiator is lvl 2 or higher before adding lower lvl opponent.
                {
                    Enemies = (from a in db.Opponents
                               where a.Level == gladiator.Level - 1
                               select a).ToList();
                   
                    Enemies = Enemies.OrderBy(item => rnd.Next()).ToList(); //Odering the list in random order to then take out 1 opponent so it becomes random.
                    opponent.Levels.AddRange(Enemies.Take(1));  // AddRange to add multible objects to list instead of Add that just add one item.
                }
                Enemies = (from a in db.Opponents
                           where a.Level == gladiator.Level
                           select a).ToList();
                Enemies = Enemies.OrderBy(item => rnd.Next()).ToList();
                opponent.Levels.AddRange(Enemies.Take(1));

                if (gladiator.Level <= 19)  // Making sure gladiator is lvl 19 or lower before adding a higher lvl opponent.
                {
                    Enemies = (from a in db.Opponents
                               where a.Level == gladiator.Level + 1
                               select a).ToList();

                    Enemies = Enemies.OrderBy(item => rnd.Next()).ToList();
                    opponent.Levels.AddRange(Enemies.Take(1));
                }

                return PartialView("_Enemies", opponent);
            }  
        }
       
        public ActionResult SelectedOpponent(int id)
        {
            var enemy = db.Opponents.SingleOrDefault(i => i.Id == id);  // Picking out the opponent based on its Id.

            if(enemy == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                if (enemy.Health <= 0)  // If the opponent got 0 or lower health it will run metod to add stats.
                {
                    Opponent.EnemyStats(enemy);
                    db.SaveChanges();
                }
                Session["enemy"] = enemy;  // Saving opponnet to session.

                return PartialView("_Opponent", enemy);
            }
        }

        public ActionResult PreBattle()
        {
            int gId = (Session["gladiator"] as Gladiator).Id;  // taking out the gladiator id from the session.
            int oId = (Session["enemy"] as Opponent).Id;
            Gladiator gladiator = db.Gladiators.SingleOrDefault(i => i.Id == gId); // picking up the gladiator based on the Id.
            Opponent opponent = db.Opponents.SingleOrDefault(i => i.Id == oId);
            if(gladiator == null || opponent == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                BattleStart battleStart = new BattleStart();   // Setting up a new battle 
                battleStart.Gladiator = gladiator;   // Saving the gladiator into the BattleStart class.
                battleStart.Opponent = opponent;
                db.Battles.Add(battleStart);
                db.SaveChanges();

                return View("BattleView", battleStart);
            }
        }

        public ActionResult BattleStart(int id)
        {
            BattleRound round = new BattleRound(); // Starting a Battle round
            var Fighters = db.Battles.Include("Gladiator").Include("Opponent").SingleOrDefault(i => i.Id == id);
            if(Fighters == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                round.Round(Fighters); // Running the Battle metod with the gladiator and opponent info.
                db.SaveChanges();

                return PartialView("_Battle", round);
            }
        }

        public ActionResult AfterBattle(int id)
        {
            var AfterMath = db.Battles.Include("Gladiator").Include("Opponent").SingleOrDefault(i => i.Id == id);
            var PlayerId = User.Identity.GetUserId();
            var PlayerUser = db.Users.Include("Gladiators").SingleOrDefault(u => u.Id == PlayerId);
            if(AfterMath == null || PlayerUser == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                Gladiator.Leveling(AfterMath.Gladiator); // Checking the gladiators exp and level him up if he got enough.

                // If i keep the harsh List I can remove player account score from database.
                // Need to decide how harsh the player highscore gonna be. Count all gladiator highscores or just those that current got win streak.

                List<Gladiator> Winners = (from a in PlayerUser.Gladiators where a.TempLost == 0 select a).ToList(); // Take out all the players winning gladiators.
                List<int> Total = Winners.Select(i => i.GladiatorHighScore).ToList(); // adding the winning gladiators highscores to a list.

                PlayerUser.AccountHighScore = Total.Sum(); // Adding the score from the total sum of all the winning gladiators.

                //if (AfterMath.Gladiator.TempLost == 0)  // A temporary stat that will check if you won the last fight or lost.
                //{
                //    List<Gladiator> Winners = (from a in PlayerUser.Gladiators where a.TempLost == 0 select a).ToList(); // Take out all the players winning gladiators.
                //    List<int> Total = Winners.Select(i => i.GladiatorHighScore).ToList(); // adding the winning gladiators highscores to a list.
                   
                //    PlayerUser.AccountScore = Total.Sum(); // Adding the score from the total sum of all the winning gladiators.

                //    //if (PlayerUser.AccountScore > PlayerUser.AccountHighScore)   // if the player score becomes bigger then the player highscore it saves as the new highscore.
                //    //{
                //    //    PlayerUser.AccountHighScore = PlayerUser.AccountScore;
                //    //}
                //}
                //else
                //{
                //    //AfterMath.Gladiator.TempLost = 0;
                //}
                db.SaveChanges();

                return View("GladiatorMenu", AfterMath.Gladiator);
            }
        }

        public ActionResult Healing(int id)
        {
            var gladiator = db.Gladiators.SingleOrDefault(i => i.Id == id);
            if(gladiator == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                Gladiator.Healing(gladiator);  // Heal the gladiator if he miss health.
                db.SaveChanges();
                return View("GladiatorMenu", gladiator);
            }
        }

        public ActionResult AddStats(int id, string stat)
        {
            var gladiator = db.Gladiators.SingleOrDefault(i => i.Id == id);
            if(gladiator == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                if (gladiator.SkillPoints >= 1)
                {
                    Gladiator.AddingStats(gladiator, stat);   // Adding stats from the skill points you gain for leveling up.
                    db.SaveChanges();
                    return PartialView("_addStats", gladiator);
                }
                else
                {
                    return View("NotEnoughSkillPoints", gladiator);
                }
            }
        }

        // GET: Gladiators/Details/5
        public ActionResult Details(int? id)
        {
            var PlayerId = User.Identity.GetUserId();
            var PlayerUser = db.Users.Include("Gladiators").SingleOrDefault(u => u.Id == PlayerId);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gladiator gladiator = PlayerUser.Gladiators.SingleOrDefault(i => i.Id == id);
            if (gladiator == null)
            {
                return HttpNotFound();
            }
            return View(gladiator);
        }

        // GET: Gladiators/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gladiators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Gladiator gladiator)
        {
            var PlayerId = User.Identity.GetUserId();
            var PlayerUser = db.Users.Include("Gladiators").SingleOrDefault(u => u.Id == PlayerId);
            if (PlayerUser.Gladiators.Count() <= 4) // Checking if the player have less 5 or less gladiators. Setting a max of 5 gladiators per player.
            {
                if (ModelState.IsValid)
                {
                    
                    Gladiator.StartingGladiator(gladiator);  // Functions to roll starting stats for the gladiator.
                    PlayerUser.Gladiators.Add(gladiator);  // Add the gldiator to the players gladiator list.
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
               
                return View("FullList");
            }
            return View(gladiator);
        }

        // GET: Gladiators/Edit/5
        public ActionResult Edit(int? id)  //Rework planned for this functiion disabled atm.
        {
            var PlayerId = User.Identity.GetUserId();
            var PlayerUser = db.Users.Include("Gladiators").SingleOrDefault(u => u.Id == PlayerId);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gladiator gladiator = PlayerUser.Gladiators.SingleOrDefault(u => u.Id == id);
            if (gladiator == null)
            {
                return HttpNotFound();
            }
            return View(gladiator);
        }

        // POST: Gladiators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Gladiator gladiator) 
        {
            var PlayerId = User.Identity.GetUserId();
            var PlayerUser = db.Users.Include("Gladiators").SingleOrDefault(u => u.Id == PlayerId);

            Gladiator oldGladiator = PlayerUser.Gladiators.SingleOrDefault(i => i.Id == gladiator.Id);  //Saving in the old gladiator so i can save in the new name next.

            if (ModelState.IsValid)
            {
                oldGladiator.Name = gladiator.Name;  // saving the new name over the old.
                //db.Entry(gladiator.Name).State = EntityState.Modified;  // need to looking to what Entry is.
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gladiator);
        }

        // GET: Gladiators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var PlayerId = User.Identity.GetUserId();
            var PlayerUser = db.Users.Include("Gladiators").SingleOrDefault(u => u.Id == PlayerId); //Gathering the players gladiators.
            Gladiator gladiator = PlayerUser.Gladiators.SingleOrDefault(u => u.Id == id); // picking out the 1 gladiator you wanna delete.
 
            if (gladiator == null)
            {
                return HttpNotFound();
            }
            return View(gladiator);
        }

        // POST: Gladiators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var PlayerId = User.Identity.GetUserId();
            var PlayerUser = db.Users.Include("Gladiators").SingleOrDefault(u => u.Id == PlayerId);
            Gladiator gladiator = PlayerUser.Gladiators.SingleOrDefault(u => u.Id == id);
            gladiator.PreviousUser = PlayerUser.UserName;
            gladiator.DateOfDelete = DateTime.Today;
            PlayerUser.Gladiators.Remove(gladiator); // Remove the gladiator from the player gladiator list but saving it in gladiator database.
            db.SaveChanges();
            return RedirectToAction("Index");
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
