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
    public class TicketHistoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketHistories
        public ActionResult Index()
        {
            var ticketHistory = db.TicketHistory.Include(t => t.Tickets);
            return View(ticketHistory.ToList());
        }

        // GET: TicketHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketHistory ticketHistories = db.TicketHistory.Find(id);
            if (ticketHistories == null)
            {
                return HttpNotFound();
            }
            return View(ticketHistories);
        }

        // GET: TicketHistories/Create
        public ActionResult Create()
        {
            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Title");
            return View();
        }

        // POST: TicketHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TicketId,OldValue,NewValue,Changed,UserId")] TicketHistory ticketHistories)
        {
            if (ModelState.IsValid)
            {
                db.TicketHistory.Add(ticketHistories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Title", ticketHistories.TicketId);
            return View(ticketHistories);
        }

        // GET: TicketHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketHistory ticketHistories = db.TicketHistory.Find(id);
            if (ticketHistories == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Title", ticketHistories.TicketId);
            return View(ticketHistories);
        }

        // POST: TicketHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TicketId,OldValue,NewValue,Changed,UserId")] TicketHistory ticketHistories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketHistories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Title", ticketHistories.TicketId);
            return View(ticketHistories);
        }

        // GET: TicketHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketHistory ticketHistories = db.TicketHistory.Find(id);
            if (ticketHistories == null)
            {
                return HttpNotFound();
            }
            return View(ticketHistories);
        }

        // POST: TicketHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketHistory ticketHistories = db.TicketHistory.Find(id);
            db.TicketHistory.Remove(ticketHistories);
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
