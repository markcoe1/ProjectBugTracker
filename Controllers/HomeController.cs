using ProjectBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectBugTracker.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {

            var user = db.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            var model = new UserViewModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            DisplayName = user.DisplayName,
            Email = user.Email,
            UserProjects = user.Project.ToList(),
            TicketsAssigned = user.TicketsAssigned.ToList(),
            TicketsOwned = user.TicketsOwned.ToList(),
            TicketNotifications = db.TicketHistory.Where(h => h.NotificationSeen == false && h.UserId == user.Id).ToList()
        };
            return View(model);
        }
        public ActionResult Landing2()
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