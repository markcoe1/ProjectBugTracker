﻿using System;
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
    public class TicketCommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketComments
        public ActionResult Index()
        {
            var ticketComment = db.TicketComment.Include(t => t.Tickets);
            return View(ticketComment.ToList());
        }

        // GET: TicketComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketComment ticketComments = db.TicketComment.Find(id);
            if (ticketComments == null)
            {
                return HttpNotFound();
            }
            return View(ticketComments);
        }

        // GET: TicketComments/Create
        public ActionResult Create()
        {
            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Title");
            return View();
        }

        // POST: TicketComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Comment,Created,TicketId,UserId")] TicketComment ticketComments)
        {
            if (ModelState.IsValid)
            {
                db.TicketComment.Add(ticketComments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Title", ticketComments.TicketId);
            return View(ticketComments);
        }

        // GET: TicketComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketComment ticketComments = db.TicketComment.Find(id);
            if (ticketComments == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Title", ticketComments.TicketId);
            return View(ticketComments);
        }

        // POST: TicketComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Comment,Created,TicketId,UserId")] TicketComment ticketComments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketComments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Title", ticketComments.TicketId);
            return View(ticketComments);
        }

        // GET: TicketComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketComment ticketComments = db.TicketComment.Find(id);
            if (ticketComments == null)
            {
                return HttpNotFound();
            }
            return View(ticketComments);
        }

        // POST: TicketComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketComment ticketComments = db.TicketComment.Find(id);
            db.TicketComment.Remove(ticketComments);
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
