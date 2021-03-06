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
    public class TicketTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketTypes
        public ActionResult Index()
        {
            return View(db.TicketType.ToList());
        }

        // GET: TicketTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketType ticketTypes = db.TicketType.Find(id);
            if (ticketTypes == null)
            {
                return HttpNotFound();
            }
            return View(ticketTypes);
        }

        // GET: TicketTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] TicketType ticketTypes)
        {
            if (ModelState.IsValid)
            {
                db.TicketType.Add(ticketTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticketTypes);
        }

        // GET: TicketTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketType ticketTypes = db.TicketType.Find(id);
            if (ticketTypes == null)
            {
                return HttpNotFound();
            }
            return View(ticketTypes);
        }

        // POST: TicketTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] TicketType ticketTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticketTypes);
        }

        // GET: TicketTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketType ticketTypes = db.TicketType.Find(id);
            if (ticketTypes == null)
            {
                return HttpNotFound();
            }
            return View(ticketTypes);
        }

        // POST: TicketTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketType ticketTypes = db.TicketType.Find(id);
            db.TicketType.Remove(ticketTypes);
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
