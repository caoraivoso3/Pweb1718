using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpacesForChildren.Models;

namespace SpacesForChildren.Controllers
{
    public class RequestInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RequestInfoes
        public ActionResult Index()
        {
            var requestInfo = db.RequestInfo.Include(r => r.Institution).Include(r => r.Parent);
            return View(requestInfo.ToList());
        }

        // GET: RequestInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestInfo requestInfo = db.RequestInfo.Find(id);
            if (requestInfo == null)
            {
                return HttpNotFound();
            }
            return View(requestInfo);
        }

        // GET: RequestInfoes/Create
        public ActionResult Create()
        {
            ViewBag.InstitutionId = new SelectList(db.Users, "Id", "Name");
            ViewBag.ParentId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: RequestInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Text,IsAnswered,Answer,ParentId,InstitutionId")] RequestInfo requestInfo)
        {
            if (ModelState.IsValid)
            {
                db.RequestInfo.Add(requestInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstitutionId = new SelectList(db.Users, "Id", "Name", requestInfo.InstitutionId);
            ViewBag.ParentId = new SelectList(db.Users, "Id", "Name", requestInfo.ParentId);
            return View(requestInfo);
        }

        // GET: RequestInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestInfo requestInfo = db.RequestInfo.Find(id);
            if (requestInfo == null)
            {
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
        public ActionResult Edit([Bind(Include = "Id,Title,Text,IsAnswered,Answer,ParentId,InstitutionId")] RequestInfo requestInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstitutionId = new SelectList(db.Users, "Id", "Name", requestInfo.InstitutionId);
            ViewBag.ParentId = new SelectList(db.Users, "Id", "Name", requestInfo.ParentId);
            return View(requestInfo);
        }

        // GET: RequestInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestInfo requestInfo = db.RequestInfo.Find(id);
            if (requestInfo == null)
            {
                return HttpNotFound();
            }
            return View(requestInfo);
        }

        // POST: RequestInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestInfo requestInfo = db.RequestInfo.Find(id);
            db.RequestInfo.Remove(requestInfo);
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
