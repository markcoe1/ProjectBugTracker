using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectBugTracker.Models;
using ProjectBugTracker.Helper;

namespace ProjectBugTracker.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RolesViewModel helper = new RolesViewModel();

        // GET: Tickets
        public ActionResult Index(int? projectId)
        {
            var tickets = db.Ticket.Include(t => t.OwnerUser).Include(t => t.AssignedUser).Include(t => t.Projects).Include(t => t.TicketPrioritys).Include(t => t.TicketStatuses).Include(t => t.TicketType);

            if (projectId != null) // view by project
            {
                return View(tickets.Where(t => t.ProjectId == projectId).ToList());
            }
            else // view by role
            {
                if (User.IsInRole("Admin"))
                {
                    return View(tickets);
                }
                else if (User.IsInRole("ProjectManager"))
                {
                    return View(tickets.Where(t => t.Projects._ApplicationUsers.Any(u => u.UserName == User.Identity.Name)));
                }
                else if (User.IsInRole("Developer"))
                {
                    return View(tickets.Where(t => t.AssignedUser.UserName == User.Identity.Name));
                }
                else
                {
                    return View(tickets.Where(t => t.OwnerUser.UserName == User.Identity.Name));
                }

            }
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket tickets = db.Ticket.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            return View(tickets);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            Ticket model = new Ticket();
            model.Created = new DateTimeOffset(DateTime.Now);
            model.OwnerUser = db.Users.Single(u => u.UserName == User.Identity.Name);
            model.OwnerUserId = model.OwnerUser.Id;
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name");
            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "UserName");
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "UserName");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriority, "Id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketType, "Id", "Name");
            return View(model);
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,ProjectId,Created,Updated,UpdateReason,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedUserId")] Ticket tickets)
        {
            if (ModelState.IsValid)
            {
                
                tickets.Created = DateTimeOffset.Now;
                tickets.OwnerUser = db.Users.Find(tickets.OwnerUserId);
                db.Ticket.Add(tickets);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "UserName", tickets.AssignedUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "UserName", tickets.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", tickets.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriority, "Id", "Name", tickets.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name", tickets.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketType, "Id", "Name", tickets.TicketTypeId);
            return View(tickets);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket tickets = db.Ticket.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }

            UserRolesHelper helper = new UserRolesHelper();

            Ticket model = new Ticket();
            model.AssignedUserId = db.Users.Single(u => u.UserName == User.Identity.Name).Id;
            ViewBag.AssignedUserId = new SelectList(helper.UsersInRole("Developer"), "Id", "UserName", tickets.AssignedUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "UserName", tickets.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", tickets.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriority, "Id", "Name", tickets.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name", tickets.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketType, "Id", "Name", tickets.TicketTypeId);

            return View(tickets);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,ProjectId,Created,Updated,UpdateReason,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedUserId")] Ticket tickets)
        {
            if (ModelState.IsValid)
            {
               
                tickets.Updated = new DateTimeOffset(DateTime.Now);
                tickets.OwnerUser = db.Users.Find(tickets.OwnerUserId);
                tickets.AssignedUser = db.Users.Find(tickets.AssignedUserId);
                db.Entry(tickets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            UserRolesHelper helper = new UserRolesHelper();
            ViewBag.AssignedToUserId = new SelectList(helper.UsersInRole("Developer"), "Id", "UserName", tickets.AssignedUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "UserName", tickets.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", tickets.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriority, "Id", "Name", tickets.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name", tickets.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketType, "Id", "Name", tickets.TicketTypeId);
            return View(tickets);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket tickets = db.Ticket.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            return View(tickets);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket tickets = db.Ticket.Find(id);
            db.Ticket.Remove(tickets);
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

        public int projectId { get; set; }
    }
}
