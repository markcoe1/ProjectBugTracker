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
    public class TicketPrioritiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketPriorities
        public ActionResult Index()
        {
            return View(db.TicketPriority.ToList());
        }

        // GET: TicketPriorities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketPriority ticketPriorities = db.TicketPriority.Find(id);
            if (ticketPriorities == null)
            {
                return HttpNotFound();
            }
            return View(ticketPriorities);
        }

        // GET: TicketPriorities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketPriorities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] TicketPriority ticketPriorities)
        {
            if (ModelState.IsValid)
            {
                db.TicketPriority.Add(ticketPriorities);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticketPriorities);
        }

        // GET: TicketPriorities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketPriority ticketPriorities = db.TicketPriority.Find(id);
            if (ticketPriorities == null)
            {
                return HttpNotFound();
            }
            return View(ticketPriorities);
        }

        // POST: TicketPriorities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] TicketPriority ticketPriorities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketPriorities).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticketPriorities);
        }

        // GET: TicketPriorities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketPriority ticketPriorities = db.TicketPriority.Find(id);
            if (ticketPriorities == null)
            {
                return HttpNotFound();
            }
            return View(ticketPriorities);
        }

        // POST: TicketPriorities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketPriority ticketPriorities = db.TicketPriority.Find(id);
            db.TicketPriority.Remove(ticketPriorities);
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
