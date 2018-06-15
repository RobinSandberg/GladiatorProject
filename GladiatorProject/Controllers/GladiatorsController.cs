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

        public ActionResult SelectGladiator(BattleStart battleStart, int id)
        {
            var gladiator = db.Gladiators.SingleOrDefault(i => i.Id == id);
            //Session["Gladiator"] = gladiator;
            //battleStart.Gladiator = gladiator;
            //battleStart.Gladiator.Name = gladiator.Name;
            //battleStart.Gladiator.Level = gladiator.Level;
            //battleStart.Gladiator.Experiance = gladiator.Experiance;
            //battleStart.Gladiator.Health = gladiator.Health;
            //battleStart.Gladiator.Armor = gladiator.Armor;
            //battleStart.Gladiator.Damage = gladiator.Damage;
            //battleStart.Gladiator.SkillPoints = gladiator.SkillPoints;
            return View("FindOpponent", gladiator);
        }

        public ActionResult PartFindOpponent()
        {

            return PartialView("_Enemies", db.Opponents.ToList());
        }
        //[Authorize(Roles = "Player Overlord")]
        public ActionResult SelectedOpponent(BattleStart battleStart , int id)
        {
            var enemy = db.Opponents.SingleOrDefault(i => i.Id == id);
            Opponent.EnemyStats(enemy);
            db.SaveChanges();
            //Session["Enemy"] = enemy;
            //battleStart.Opponent = enemy;
            //battleStart.Opponent.Name = enemy.Name;
            //battleStart.Opponent.Level = enemy.Level;
            //battleStart.Opponent.Health = enemy.Health;
            //battleStart.Opponent.Armor = enemy.Armor;
            //battleStart.Opponent.Damage = enemy.Damage;

            return PartialView("_Opponent", enemy);
        }

        public ActionResult PreBattle(BattleStart battleStart)
        {
            
            return PartialView("BattleView", battleStart);
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
