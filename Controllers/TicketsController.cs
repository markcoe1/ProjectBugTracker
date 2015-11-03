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
using System.Threading.Tasks;

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
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            UserRolesHelper helper = new UserRolesHelper();

            
            
            ViewBag.AssignedUserId = new SelectList(helper.UsersInRole("Developer"), "Id", "DisplayName", ticket.AssignedUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "DisplayName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Project, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriority, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketType, "Id", "Name", ticket.TicketTypeId);

            TempData["OldTicket"] = ticket;

            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,ProjectId,Created,Updated,UpdateReason,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedUserId")] Ticket tickets)
        {

            if (ModelState.IsValid)
            {
                var user = await db.Users.SingleOrDefaultAsync(u => u.UserName == User.Identity.Name);
                var changedTime = new DateTimeOffset(DateTime.Now);
                Ticket oldTicket = (Ticket)TempData["OldTicket"];

                if (tickets.AssignedUserId != oldTicket.AssignedUserId)
                {
                    string temp = "";

                    if (oldTicket.AssignedUserId == null)
                    {
                        temp = "Unassigned";
                    }
                    else
                    {
                        temp = oldTicket.AssignedUser.DisplayName;
                    }

                    db.TicketHistory.Add(new TicketHistory
                        {
                            Changed = changedTime,
                            Property = "Assigned User",
                            TicketId = tickets.Id,
                            OldValue = temp,
                            NewValue = db.Users.Find(tickets.AssignedUserId).DisplayName,
                            UserId = user.Id
                        });
                }

                if (tickets.TicketTypeId != oldTicket.TicketTypeId)
                {
                    db.TicketHistory.Add(new TicketHistory
                    {
                        Changed = changedTime,
                        Property = "Ticket Type",
                        TicketId = tickets.Id,
                        OldValue = oldTicket.TicketType.Name,
                        NewValue = db.TicketType.Find(tickets.TicketTypeId).Name,
                        UserId = user.Id
                    });
                }

                if (tickets.TicketPriorityId != oldTicket.TicketPriorityId)
                {
                    db.TicketHistory.Add(new TicketHistory
                    {
                        Changed = changedTime,
                        Property = "Ticket Priority",
                        TicketId = tickets.Id,
                        OldValue = oldTicket.TicketPrioritys.Name,
                        NewValue = db.TicketPriority.Find(tickets.TicketPriorityId).Name,
                        UserId = user.Id
                    });
                }

                if (tickets.TicketStatusId != oldTicket.TicketStatusId)
                {
                    db.TicketHistory.Add(new TicketHistory
                    {
                        Changed = changedTime,
                        Property = "Ticket Status",
                        TicketId = tickets.Id,
                        OldValue = oldTicket.TicketStatuses.Name,
                        NewValue = db.TicketStatus.Find(tickets.TicketStatusId).Name,
                        UserId = user.Id
                    });
                }

                    tickets.Updated = new DateTimeOffset(DateTime.Now);
                    tickets.OwnerUser = db.Users.Find(tickets.OwnerUserId);
                    tickets.AssignedUser = db.Users.Find(tickets.AssignedUserId);
                    db.Entry(tickets).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                UserRolesHelper helper = new UserRolesHelper();
                ViewBag.AssignedUserId = new SelectList(helper.UsersInRole("Developer"), "Id", "UserName", tickets.AssignedUserId);
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

        public ActionResult MarkNotificationSeen(int id)
        {
            var history = db.TicketHistory.Find(id);
            history.NotificationSeen = true;
            db.Entry(history).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
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
