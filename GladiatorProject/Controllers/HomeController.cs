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
            //db.Gladiators.Include("Classes");
            return View();
        }

        public ActionResult PartCreateGladiator()
        {
            Player player = new Player();
            return PartialView("_partCreateGladiator", player);
        }
        [HttpPost]
        public ActionResult PartCreateGladiator(Player gladiator, string Class)
        {
            // Make changes to the adding of stats to own view model.
            //int check = gladiator.SkillPoints;
            //gladiator.SkillPoints = check - (gladiator.Armor + gladiator.Damage + gladiator.Health);
            ClassRole role = new ClassRole();
            if (ModelState.IsValid == false)
            {
                switch (Class)
                {
                    case "Murmillo":
                        gladiator.Class = db.ClassRoles.First();
                        break;

                    case "Retiarius":
                        gladiator.Class = db.ClassRoles.First();
                        break;
                    case "Dimachaerus":
                        gladiator.Class = db.ClassRoles.First();
                        break;
                    case "Cestus":
                        gladiator.Class = db.ClassRoles.First();
                        break;
                }

                //if (Class == "Murmillo")
                //{
                //    gladiator.Class = db.Classes.First();

                //}

                db.Players.Add(gladiator);
                db.SaveChanges();
                return PartialView("_gladiator", gladiator);

                //if (gladiator.SkillPoints >= 0)
                //{
                //    db.Gladiators.Add(gladiator);
                //    return PartialView("_gladiator", gladiator);
                //}
                //else
                //{
                //    gladiator.SkillPoints = 5;
                //    return PartialView("_partCreateGladiator", gladiator);
                //} 
            }
            return new HttpStatusCodeResult(404);
        }

        public ActionResult DisplayStats(int id)
        {
            ClassRole role = db.ClassRoles.SingleOrDefault(i => i.Id == id);

            return PartialView("_stats", role );
        }

        public ActionResult HideStats(int id)
        {
            ClassRole role = db.ClassRoles.SingleOrDefault(i => i.Id == id);
            return Content("");
        }
    }
}