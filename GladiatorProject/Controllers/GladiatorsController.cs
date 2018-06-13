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

            var PlayerUser = db.Users.SingleOrDefault(u => u.Id == User.Identity.GetUserId()); // fix this get id above then use it.
                
            return View();
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
            if (ModelState.IsValid)
            {
                db.Gladiators.Add(gladiator);
                db.SaveChanges();
                return RedirectToAction("Index");
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
