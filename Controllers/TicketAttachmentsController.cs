﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectBugTracker.Models;
using System.IO;

namespace ProjectBugTracker.Controllers
{
    public class TicketAttachmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketAttachments
        public ActionResult Index()
        {
            var ticketAttachments = db.TicketAttachments.Include(t => t.Tickets);
            return View(ticketAttachments.ToList());
        }

        // GET: TicketAttachments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAttachment ticketAttachments = db.TicketAttachments.Find(id);
            if (ticketAttachments == null)
            {
                return HttpNotFound();
            }
            return View(ticketAttachments);
        }

        // GET: TicketAttachments/Create
        public ActionResult Create()
        {
            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Title");
            return View();
        }

        // POST: TicketAttachments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TicketId,FilePath,Description,Created,UserId,FileUrl")] TicketAttachment ticketAttachments, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    //Check the file name to make sure its an image
                    var ext = Path.GetExtension(image.FileName).ToLower();
                    if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif" && ext != ".bmp")

                        ModelState.AddModelError("image", "Invalid Format.");

                }

            if (ModelState.IsValid)
            {

                if (image != null)
                {
                    //relative server path
                    var filePath = "/Images/Attachments/";
                    //path on physical drive on server
                    var absPath = Server.MapPath("~" + filePath);
                    //media url for relative path
                    ticketAttachments.FileUrl = filePath + image.FileName;
                    //save image
                    image.SaveAs(Path.Combine(absPath, image.FileName));
                }
            }
                db.TicketAttachments.Add(ticketAttachments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Title", ticketAttachments.TicketId);
            return View(ticketAttachments);
        }

        // GET: TicketAttachments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAttachment ticketAttachments = db.TicketAttachments.Find(id);
            if (ticketAttachments == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Title", ticketAttachments.TicketId);
            return View(ticketAttachments);
        }

        // POST: TicketAttachments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TicketId,FilePath,Description,Created,UserId,FileUrl")] TicketAttachment ticketAttachments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketAttachments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TicketId = new SelectList(db.Ticket, "Id", "Title", ticketAttachments.TicketId);
            return View(ticketAttachments);
        }

        // GET: TicketAttachments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAttachment ticketAttachments = db.TicketAttachments.Find(id);
            if (ticketAttachments == null)
            {
                return HttpNotFound();
            }
            return View(ticketAttachments);
        }

        // POST: TicketAttachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketAttachment ticketAttachments = db.TicketAttachments.Find(id);
            db.TicketAttachments.Remove(ticketAttachments);
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
