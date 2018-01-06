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
    [Authorize(Roles = Profiles.Institution)]
    public class ContractsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contracts
        public ActionResult Index()
        {
            var contract = db.Contract.Include(c => c.Child).Include(c => c.Parent).Include(c => c.Review);
            return View(contract.ToList());
        }

        // GET: Contracts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contract.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // GET: Contracts/Create
        public ActionResult Create()
        {
            ViewBag.ChildId = new SelectList(db.Child, "Id", "Name");
            ViewBag.ParentId = new SelectList(db.Parent, "Id", "Name");
            ViewBag.ReviewId = new SelectList(db.Review, "Id", "Title");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InitialDate,EndDate,IsApproved,ParentId,ChildId,ReviewId")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                db.Contract.Add(contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChildId = new SelectList(db.Child, "Id", "Name", contract.ChildId);
            ViewBag.ParentId = new SelectList(db.Users, "Id", "Name", contract.ParentId);
            ViewBag.ReviewId = new SelectList(db.Review, "Id", "Title", contract.ReviewId);
            return View(contract);
        }

        // GET: Contracts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contract.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChildId = new SelectList(db.Child, "Id", "Name", contract.ChildId);
            ViewBag.ParentId = new SelectList(db.Users, "Id", "Name", contract.ParentId);
            ViewBag.ReviewId = new SelectList(db.Review, "Id", "Title", contract.ReviewId);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InitialDate,EndDate,IsApproved,ParentId,ChildId,ReviewId")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChildId = new SelectList(db.Child, "Id", "Name", contract.ChildId);
            ViewBag.ParentId = new SelectList(db.Users, "Id", "Name", contract.ParentId);
            ViewBag.ReviewId = new SelectList(db.Review, "Id", "Title", contract.ReviewId);
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contract.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contract contract = db.Contract.Find(id);
            db.Contract.Remove(contract);
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
