using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Microsoft.AspNet.Identity;
using SpacesForChildren.Models;

namespace SpacesForChildren.Controllers {
    [Authorize]
    public class ServicesController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Services
        public ActionResult Index()
        {
            ViewBag.institution = db.Institution.Find(User.Identity.GetUserId());
            return View(db.Service.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Service.Find(id);
            if (service == null) {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        [Authorize(Roles = Profiles.Admin)]
        public ActionResult Create() {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Profiles.Admin)]
        public ActionResult Create([Bind(Include = "Id,Name,Description,MinAgeYear,MaxAgeYear")] Service service) {
            if (ModelState.IsValid) {
                db.Service.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(service);
        }
        
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Instituição")]
        public ActionResult AddService(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var institution = db.Institution.Find(User.Identity.GetUserId());
            if (institution == null)
            {
                return View("Index",db.Service.ToList());
            }
            if (ModelState.IsValid)
            {
                institution.Services.Add(db.Service.Find(id));
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index",db.Service.ToList());
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Instituição")]
        public ActionResult RemoveService(int? id) {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var institution = db.Institution.Find(User.Identity.GetUserId());
            if (institution == null) {
                return View("Index", db.Service.ToList());
            }
            if (ModelState.IsValid) {
                institution.Services.Remove(db.Service.Find(id));
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index", db.Service.ToList());
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Service.Find(id);
            if (service == null) {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,MinAgeYear,MaxAgeYear")] Service service) {
            if (ModelState.IsValid) {
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: Services/Delete/5
        [Authorize(Roles = Profiles.Admin)]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Service.Find(id);
            if (service == null) {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Profiles.Admin)]
        public ActionResult DeleteConfirmed(int id) {
            Service service = db.Service.Find(id);
            db.Service.Remove(service);
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
