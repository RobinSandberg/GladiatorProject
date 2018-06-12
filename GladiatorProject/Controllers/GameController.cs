using GladiatorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GladiatorProject.Controllers
{
    public class GameController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Game
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SelectGladiator(Gladiator gladiator ,int id)
        {
            gladiator = db.Gladiators.SingleOrDefault(i => i.Id == id);
            return View("_gladiator", gladiator);
        }
        //[Authorize(Roles = "Player Overlord")]
        //public ActionResult PartCreateGladiator()
        //{
        //    Gladiator gladiator = new Gladiator();

        //    return PartialView("_partCreateGladiator", gladiator);
        //}

        //[Authorize(Roles = "Player Overlord")]
        //[HttpPost]
        //public ActionResult PartCreateGladiator(Gladiator gladiator)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        db.Gladiators.Add(gladiator);
        //        db.SaveChanges();
        //        return PartialView("_gladiator", gladiator);
        //    }
        //    return new HttpStatusCodeResult(404);
        //}
    }
}