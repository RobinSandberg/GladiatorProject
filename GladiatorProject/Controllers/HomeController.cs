﻿using GladiatorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GladiatorProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       
        public ActionResult Index()
        {
            
            return View();
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

        //[Authorize(Roles = "Player Overlord")]
        //public ActionResult PartFindOpponent()
        //{
           
        //    return PartialView("_Enemies", db.Opponents.ToList());
        //}
        //[Authorize(Roles = "Player Overlord")]
        //public ActionResult SelectedOpponent(Opponent enemy , int id)
        //{
        //    enemy = db.Opponents.SingleOrDefault(i => i.Id == id);
        //    Opponent.EnemyStats(enemy);
        //    return PartialView("_Opponent", enemy);
        //}

    }
}