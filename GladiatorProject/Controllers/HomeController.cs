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
            //List<ClassRole> classes;
            //classes = db.ClassRoles.ToList();
            //if (Session["SelectClass"] != null)
            //{
            //    classes = (List<ClassRole>)Session["SelectClass"];
            //}
            //Session["SelectClass"] = classes;
            return PartialView("_partCreateGladiator", player);
        }
        [HttpPost]
        public ActionResult PartCreateGladiator(Player gladiator)
        {
            // Make changes to the adding of stats to own view model.
            //int check = gladiator.SkillPoints;
            //gladiator.SkillPoints =  ClassRole role;
            if (ModelState.IsValid)
            {
                //switch (Classid)
                //{
                //    case 1:
                //        gladiator.Class.Health += Dice.D12();
                //        gladiator.Class.Armor += Dice.D6();
                //        gladiator.Class.Damage += Dice.D6();
                //        break;

                //    case 2:
                //        gladiator.Class.Health += Dice.D8();
                //        gladiator.Class.Armor += Dice.D4();
                //        gladiator.Class.Damage += Dice.D8();
                //        break;
                //    case 3:
                //        gladiator.Class.Health += Dice.D6();
                //        gladiator.Class.Armor += Dice.D6();
                //        gladiator.Class.Damage += Dice.D10();
                //        break;
                //    case 4:
                //        gladiator.Class.Health += Dice.D10();
                //        gladiator.Class.Armor += Dice.D6();
                //        gladiator.Class.Damage += Dice.D12();
                //        break;
                //}

                //if (Class == "Murmillo")
                //{
                //    gladiator.Class = db.Classes.First();

                //}
                //Session["Gladiator"] = gladiator.Class;
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

        //public ActionResult DisplayStats()
        //{
            
        //    return PartialView("_stats", Session["Gladiator"]);
        //}

        
    }
}