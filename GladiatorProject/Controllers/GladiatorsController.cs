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
    public class GladiatorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Gladiators
        [Authorize]
        public ActionResult Index()
        {
            var PlayerId = User.Identity.GetUserId();
            var PlayerUser = db.Users.Include("Gladiators").SingleOrDefault(u => u.Id == PlayerId);
            return View(PlayerUser.Gladiators);
        }

        public ActionResult SelectGladiator(int id)
        {
            var gladiator = db.Gladiators.SingleOrDefault(i => i.Id == id);
            if(gladiator.Health <= 0)  // temporary healing during testing.
            {
                gladiator.Health = gladiator.FullHealth;
                db.SaveChanges();
            }
            Session["gladiator"] = gladiator; 
            return View("FindOpponent", gladiator);
        }

        public ActionResult PartFindOpponent(int id)
        {
            var gladiator = db.Gladiators.SingleOrDefault(i => i.Id == id);

            Opponent opponent = new Opponent();

            opponent.Levels.Add(gladiator.Level);

            if(gladiator.Level <= 19)
            {
                opponent.Levels.Add(gladiator.Level + 1);
            }
           
            if(gladiator.Level >= 2)
            {
                opponent.Levels.Add(gladiator.Level - 1);
            }
           


            return PartialView("_Enemies", opponent /*db.Opponents.ToList()*/);
        }
        //[Authorize(Roles = "Player Overlord")]
        public ActionResult SelectedOpponent(Opponent opponent,int level)
        {
            //var gladiator = db.Gladiators.SingleOrDefault(i => i.Id == id);
            //if (gladiator.Opponent.Id <= 0)
            //{

            //    gladiator.Opponent.Level = level;
            //    db.Opponents.Add(gladiator.Opponent);
            //}
            //Opponent opponent = new Opponent();
            opponent.Level = level;
            db.Opponents.Add(opponent);
            //var enemy = db.Opponents.SingleOrDefault(i => i.Id == id);
            if (opponent.Health <= 0)
            {
               
                Opponent.EnemyStats(opponent);
                db.SaveChanges();
            }
            Session["enemy"] = opponent;

            return PartialView("_Opponent", opponent);
        }

        public ActionResult PreBattle()
        {
            int gId = (Session["gladiator"] as Gladiator).Id;
            int oId = (Session["enemy"] as Opponent).Id;
            Gladiator gladiator = db.Gladiators.SingleOrDefault(i => i.Id == gId);
            Opponent opponent = db.Opponents.SingleOrDefault(i => i.Id == oId);
            BattleStart battleStart = new BattleStart();
            battleStart.Gladiator = gladiator;
            battleStart.Opponent = opponent;
            db.Battles.Add(battleStart);
            db.SaveChanges();
            
            return View("BattleView", battleStart);
        }

        public ActionResult BattleStart(int id)
        {
            BattleRound round = new BattleRound();
            var Fighters = db.Battles.Include("Gladiator").Include("Opponent").SingleOrDefault(i => i.Id == id);
            round.Round(Fighters);
            db.SaveChanges();
            return PartialView("_Battle", round);

        }

        public ActionResult AfterBattle(int id)
        {
            var AfterMath = db.Battles.Include("Gladiator").Include("Opponent").SingleOrDefault(i => i.Id == id);
           switch(AfterMath.Gladiator.Level)
            {
                case 1:
                    if(AfterMath.Gladiator.Experiance >= 100)
                    {
                        AfterMath.Gladiator.Level += 1;
                        AfterMath.Gladiator.SkillPoints += 2;
                        db.SaveChanges();
                    }
                    break;
                case 2:
                    if(AfterMath.Gladiator.Experiance >= 200)
                    {
                        AfterMath.Gladiator.Level += 1;
                        AfterMath.Gladiator.SkillPoints += 2;
                        db.SaveChanges();
                    } 
                    break;
                case 3:
                    if(AfterMath.Gladiator.Experiance >= 300)
                    {
                        AfterMath.Gladiator.Level += 1;
                        AfterMath.Gladiator.SkillPoints += 2;
                        db.SaveChanges();
                    }
                    break;
            }

            return View("BattleView" , AfterMath);
        }

        // GET: Gladiators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gladiator gladiator = db.Gladiators.Find(id);
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
        public ActionResult Create([Bind(Include = "Id,Name,Health,Armor,Damage,SkillPoints,Experiance,Level")] Gladiator gladiator)
        {
            var PlayerId = User.Identity.GetUserId();
            var PlayerUser = db.Users.Include("Gladiators").SingleOrDefault(u => u.Id == PlayerId);
            if (PlayerUser.Gladiators.Count() <= 4)
            {
                if (ModelState.IsValid)
                {
                    
                    PlayerUser.Gladiators.Add(gladiator);
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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gladiator gladiator = db.Gladiators.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,Name,Health,Armor,Damage,SkillPoints,Experiance,Level")] Gladiator gladiator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gladiator).State = EntityState.Modified;
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
            var PlayerUser = db.Users.Include("Gladiators").SingleOrDefault(u => u.Id == PlayerId);
            Gladiator gladiator = db.Gladiators.Find(id);
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
            Gladiator gladiator = db.Gladiators.Find(id);
            db.Gladiators.Remove(gladiator);
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
