using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GladiatorProject.Models;
using Microsoft.AspNet.Identity;

namespace GladiatorProject.Controllers
{
    [Authorize(Roles = "Player")]
    public class SupportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Support
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SupportRequests support)
        {
            var PlayerId = User.Identity.GetUserId();  // picking up the users Id.
            var PlayerUser = db.Users.Include("Gladiators").SingleOrDefault(u => u.Id == PlayerId);

            if (ModelState.IsValid)
            {
                support.User = PlayerUser.UserName;
                support.Email = PlayerUser.Email;
                support.Date = DateTime.Today;
                support.Solved = "No";
                db.Support.Add(support);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(support);
        }
    }
}