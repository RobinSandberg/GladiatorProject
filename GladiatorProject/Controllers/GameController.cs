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
        [Authorize]
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult SelectGladiator(int id)
        {
            var gladiator = db.Gladiators.SingleOrDefault(i => i.Id == id);
            return PartialView("_gladiator", gladiator);
        }
       
    }
}