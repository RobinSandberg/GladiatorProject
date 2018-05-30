using GladiatorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GladiatorProject.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartCreateGladiator()
        {
            Player player = new Player();
            return PartialView("_partCreateGladiator", player);
        }
        [HttpPost]
        public ActionResult PartCreateGladiator(Player gladiator)
        {
            // Make changes to the adding of stats to own view model.
            int check = gladiator.SkillPoints;
            gladiator.SkillPoints = check - (gladiator.Armor + gladiator.Damage + gladiator.Health);

            if (ModelState.IsValid)
            {
                if(gladiator.SkillPoints >= 0)
                {
                    db.Gladiators.Add(gladiator);
                    return PartialView("_gladiator", gladiator);
                }
                else
                {
                    gladiator.SkillPoints = 5;
                    return PartialView("_partCreateGladiator", gladiator);
                } 
            }
            return new HttpStatusCodeResult(400);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}