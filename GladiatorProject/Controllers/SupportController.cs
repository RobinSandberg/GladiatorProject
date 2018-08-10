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
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateRequest()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRequest(SupportRequests support)
        {
            var PlayerId = User.Identity.GetUserId();
            var PlayerUser = db.Users.Include("Gladiators").Include("Supports").SingleOrDefault(u => u.Id == PlayerId);

            if (ModelState.IsValid)
            {
                support.User = PlayerUser.UserName;
                support.Email = PlayerUser.Email;
                support.Date = DateTime.Now;
                support.Solved = "No";

                PlayerUser.Supports.Add(support);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(support);
        }

        public ActionResult MyRequests()
        {
            var PlayerId = User.Identity.GetUserId();
            var PlayerUser = db.Users.Include("Gladiators").Include("Supports").SingleOrDefault(u => u.Id == PlayerId);

            return View(PlayerUser.Supports);
        }

        public ActionResult SupportSolved(int id)
        {
            var Support = db.Supports.SingleOrDefault(i => i.Id == id);

            if (Support == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                if (Support.Solved == "No")
                {
                    Support.Solved = "Yes";
                    db.SaveChanges();
                    return RedirectToAction("MyRequests");
                }
                else
                {
                    return View("SupportSolved");
                }
            }
        }

        public ActionResult SupportDetails(int id)
        {
            var Details = db.Supports.Include("Messages").SingleOrDefault(i => i.Id == id);
            if (Details == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                return PartialView("_SupportDetails", Details);
            }
        }

        [HttpGet]
        public ActionResult AddMessage(int id)
        {
            var message = db.Supports.Include("Messages").SingleOrDefault(i => i.Id == id);

            if (message == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                Session["Message"] = message;
                return View();
            }

        }

        [HttpPost]
        public ActionResult AddMessage(Message Response)
        {
            if (Session["Message"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var mID = (Session["Message"] as SupportRequests).Id;
                var message = db.Supports.Include("Messages").SingleOrDefault(i => i.Id == mID);
                var PlayerId = User.Identity.GetUserId();
                var PlayerUser = db.Users.SingleOrDefault(u => u.Id == PlayerId);
                if (message == null || PlayerUser == null)
                {
                    return new HttpStatusCodeResult(400);
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        Message themessage = new Message();
                        themessage.From = PlayerUser.UserName;
                        themessage.Body = Response.Body;
                        themessage.Sent = DateTime.Now;
                        message.Messages.Add(themessage);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(Response);
                    }
                }
            }

        }

        public ActionResult HideDetails()
        {
            return Content("");
        }

        public ActionResult SupportDelete(int id)
        {
            var Support = db.Supports.Include("Messages").SingleOrDefault(i => i.Id == id);

            if (Support == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {

                return View(Support);
            }
        }

        public ActionResult SupportDeleteConfirm(int id)
        {
            var Support = db.Supports.Include("Messages").SingleOrDefault(i => i.Id == id);

            if (Support == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                db.Supports.Remove(Support);
                db.SaveChanges();
                return RedirectToAction("MyRequests");
            }

        }
    }
}