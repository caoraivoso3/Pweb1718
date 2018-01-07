using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using SpacesForChildren.Models;

namespace SpacesForChildren.Controllers
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Activities
        public ActionResult Index()
        {
            var activity = db.Activity.Include(a => a.Institution);
            return View(activity.ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activity.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        [Authorize(Roles = Profiles.Institution)]
        public ActionResult Create()
        {
            ViewBag.InstitutionId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Profiles.Institution)]
        public ActionResult Create([Bind(Include = "Id,Name,Date,Description,InstitutionId")] Activity activity)
        {
            activity.InstitutionId = db.Institution.Find(User.Identity.GetUserId()).Id;
            activity.Institution = db.Institution.Find(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                db.Activity.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstitutionId = new SelectList(db.Users, "Id", "Name", activity.InstitutionId);
            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activity.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstitutionId = new SelectList(db.Users, "Id", "Name", activity.InstitutionId);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Profiles.Admin + "," + Profiles.Institution)]
        public ActionResult Edit([Bind(Include = "Id,Name,Date,Description,InstitutionId")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstitutionId = new SelectList(db.Users, "Id", "Name", activity.InstitutionId);
            return View(activity);
        }

        // GET: Activities/Delete/5
        [Authorize(Roles = Profiles.Admin)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activity.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Profiles.Admin)]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activity.Find(id);
            db.Activity.Remove(activity);
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
