using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SpacesForChildren.Models;

namespace SpacesForChildren.Controllers {
    public class RequestInfoesController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RequestInfoes
        public ActionResult Index() {

            if (User.IsInRole(Profiles.Institution)) {
                var requestInfo = db.RequestInfo.Include(r => r.Institution);
                return View(requestInfo);
            }
            if (User.IsInRole(Profiles.Parent)) {
                var requestInfo = db.RequestInfo.Include(r => r.Parent);
                return View(requestInfo);
            }
            if (User.IsInRole(Profiles.Admin))
                return View(db.RequestInfo);

            return Content("It should not happen");
        }

        // GET: RequestInfoes/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestInfo requestInfo = db.RequestInfo.Find(id);
            if (requestInfo == null) {
                return HttpNotFound();
            }
            return View(requestInfo);
        }

        // GET: RequestInfoes/Create
        public ActionResult Create() {
            ViewBag.InstitutionId = new SelectList(db.Institution, "Id", "Name");
            ViewBag.ParentId = new SelectList(db.Parent, "Id", "Name");
            return View();
        }

        // POST: RequestInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Text,InstitutionId")]
            RequestInfo requestInfo) {
            ModelState.Remove("Parent");
            ModelState.Remove("IsAnswered");
            ModelState.Remove("Answer");

            requestInfo.IsAnswered = false;
            requestInfo.ParentId = db.Parent.Find(User.Identity.GetUserId()).Id;

            //if (ModelState.IsValid)
            //{
            db.RequestInfo.Add(requestInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
            //}

            ViewBag.InstitutionId = new SelectList(db.Institution, "Id", "Name", requestInfo.InstitutionId);
            ViewBag.ParentId = new SelectList(db.Parent, "Id", "Name", requestInfo.ParentId);
            return View(requestInfo);
        }

        // GET: RequestInfoes/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestInfo requestInfo = db.RequestInfo.Find(id);
            if (requestInfo == null) {
                return HttpNotFound();
            }

            ViewBag.InstitutionId = new SelectList(db.Users, "Id", "Name", requestInfo.InstitutionId);
            ViewBag.ParentId = new SelectList(db.Users, "Id", "Name", requestInfo.ParentId);
            return View(requestInfo);
        }

        // POST: RequestInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Text,IsAnswered,Answer,ParentId,InstitutionId")] RequestInfo requestInfo) {

            if (User.IsInRole("Instituição")) {
                if (ModelState.IsValid) {
                    var toUpdate = db.RequestInfo.Find(requestInfo.Id);
                    toUpdate.Answer = requestInfo.Answer;
                    toUpdate.IsAnswered = true;
                    db.RequestInfo.AddOrUpdate(toUpdate);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else {
                if (ModelState.IsValid) {
                    db.Entry(requestInfo).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.InstitutionId = new SelectList(db.Users, "Id", "Name", requestInfo.InstitutionId);
            ViewBag.ParentId = new SelectList(db.Users, "Id", "Name", requestInfo.ParentId);
            return View(requestInfo);
        }

        // GET: RequestInfoes/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestInfo requestInfo = db.RequestInfo.Find(id);
            if (requestInfo == null) {
                return HttpNotFound();
            }
            return View(requestInfo);
        }

        // POST: RequestInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            RequestInfo requestInfo = db.RequestInfo.Find(id);
            db.RequestInfo.Remove(requestInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
