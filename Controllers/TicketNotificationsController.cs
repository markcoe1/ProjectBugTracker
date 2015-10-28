using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectBugTracker.Models;

namespace ProjectBugTracker.Controllers
{
    public class TicketNotificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketNotifications
        public ActionResult Index()
        {
            var ticketNotification = db.TicketNotification.Include(t => t.Tickets);
            return View(ticketNotification.ToList());
        }

        // GET: TicketNotifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNotification ticketNotifications = db.TicketNotification.Find(id);
            if (ticketNotifications == null)
            {
                return HttpNotFound();
            }
            return View(ticketNotifications);
        }

        // GET: TicketNotifications/Create
        public ActionResult Create()
        {
            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Title");
            return View();
        }

        // POST: TicketNotifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TicketId,UserId")] TicketNotification ticketNotifications)
        {
            if (ModelState.IsValid)
            {
                db.TicketNotification.Add(ticketNotifications);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Title", ticketNotifications.TicketId);
            return View(ticketNotifications);
        }

        // GET: TicketNotifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNotification ticketNotifications = db.TicketNotification.Find(id);
            if (ticketNotifications == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Title", ticketNotifications.TicketId);
            return View(ticketNotifications);
        }

        // POST: TicketNotifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TicketId,UserId")] TicketNotification ticketNotifications)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketNotifications).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Title", ticketNotifications.TicketId);
            return View(ticketNotifications);
        }

        // GET: TicketNotifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNotification ticketNotifications = db.TicketNotification.Find(id);
            if (ticketNotifications == null)
            {
                return HttpNotFound();
            }
            return View(ticketNotifications);
        }

        // POST: TicketNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketNotification ticketNotifications = db.TicketNotification.Find(id);
            db.TicketNotification.Remove(ticketNotifications);
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
    }
}
