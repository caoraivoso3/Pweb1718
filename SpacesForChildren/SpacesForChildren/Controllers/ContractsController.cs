using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SpacesForChildren.Models;

namespace SpacesForChildren.Controllers {
    public class ContractsController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contracts
        public ActionResult Index() {
            var contract = db.Contract.Include(c => c.Child).Include(c => c.Parent).Include(c => c.Review);

            if (User.IsInRole("Pai"))
            {
                var myId = User.Identity.GetUserId();
                contract = db.Contract.Include(c => c.Parent).Where(c => c.ParentId == myId);
            }

            return View(contract.ToList());
        }

        // GET: Contracts/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contract.Find(id);
            if (contract == null) {
                return HttpNotFound();
            }
            return View(contract);
        }


        public ActionResult mainCreate() {
            ViewBag.ChildId = new List<SelectListItem>()
            {
                new SelectListItem{Text = "Sem opção", Value = "empty"}
            };/*new SelectList(db.Child, "Id", "Name");*/
            ViewBag.ParentsId = new SelectList(db.Parent, "Id", "Name");
            ViewBag.ReviewId = new List<SelectListItem>()
            {
                new SelectListItem{Text = "Sem opção", Value = "empty"}
            }; ;/*new SelectList(db.Review, "Id", "Title");*/
            ViewBag.ServiceId = new SelectList(db.Service, "Id", "Name");
            return View("mainCreate");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult mainCreate(string parentsId) {
            string parentId = parentsId;
            if (parentId == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var parent = db.Parent.Find(parentId);
            var child = db.Child.Where(p => p.ParentId == parentId);

            // Partial View
            ViewBag.ChildId = new SelectList(child, "Id", "Name");
            ViewBag.ParentId = parentId;
            ViewBag.ReviewId = new SelectList(db.Review, "Id", "Title");
            ViewBag.InstitutionId = new SelectList(db.Institution, "Id", "Name");
            ViewBag.ServiceId = new SelectList(db.Service, "Id", "Name");

            // View
            ViewBag.ParentsId = new SelectList(db.Parent, "Id", "Name");

            var contract = new Contract {
                ParentId = parentId,
                InitialDate = DateTime.Now
            };

            Session["parentId"] = null;
            Session["parentId"] = parentId;

            return View("mainCreate", contract);
        }

        // GET: Contracts/Create
        /*public ActionResult Create() {
            ViewBag.ChildId = new SelectList(db.Child, "Id", "Name");
            ViewBag.ParentId = new SelectList(db.Parent, "Id", "Name");
            ViewBag.ReviewId = new SelectList(db.Review, "Id", "Title");

            return View();
        }*/

        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InitialDate,ParentId,ChildId,ServiceId")] Contract contract)
        {
            return Content("ParentId:" + contract.ParentId + "--");

            string parentId = (string) Session["parentId"];

            var institution = db.Institution.Find(User.Identity.GetUserId());
            contract.Institution = institution;
            contract.InstitutionId = User.Identity.GetUserId();
            contract.Approvation = EApprovation.Awaiting;
            contract.ParentId = parentId;
            contract.Parent = db.Parent.Find(contract.ParentId);

            if (ModelState.IsValid) {
                db.Contract.Add(contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChildId = new SelectList(db.Child, "Id", "Name", contract.ChildId);
            //ViewBag.ParentId = new SelectList(db.Parent, "Id", "Name", contract.ParentId);
            ViewBag.ReviewId = new SelectList(db.Review, "Id", "Title", contract.ReviewId);
            return View(contract);
        }


        public ActionResult CreateContract()
        {

            var institution = db.Institution.Find(User.Identity.GetUserId());

            ViewBag.ChildId = new SelectList(db.Child, "Id", "Name");
            ViewBag.ParentId = new SelectList(db.Parent, "Id", "Name");
            //ViewBag.ReviewId = new SelectList(db.Review, "Id", "Title");
            //ViewBag.InstitutionId = new SelectList(db.Institution, "Id", "Name");
            ViewBag.ServiceId = new SelectList(db.Service.Where(i => i.Institutions.Any(w => w.Id == institution.Id)).ToList(), "Id", "Name");
            return View("CreateContract");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContract([Bind(Include = "Id,InitialDate,ParentId,ChildId,ServiceId")] Contract contract) {

            //return Content("ParentId:" + contract.ParentId);

            contract.Institution = db.Institution.Find(contract.InstitutionId);
            contract.Child = db.Child.Find(contract.ChildId);
            contract.Parent = db.Parent.Find(contract.ParentId);
            contract.Approvation = EApprovation.Awaiting;
            contract.Service = db.Service.Find(contract.ServiceId);
            contract.EndDate = contract.InitialDate.Value.AddYears(1);
            //contract.Review = null;
            //contract.ReviewId = null;

            if (ModelState.IsValid) {
                db.Contract.Add(contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            /*
            else
            {
                string messages = string.Join("; ", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
                return Content(messages);
            }
            */
            var institution = db.Institution.Find(User.Identity.GetUserId());
            ViewBag.ChildId = new SelectList(db.Child, "Id", "Name", contract.ChildId);
            ViewBag.ParentId = new SelectList(db.Parent, "Id", "Name", contract.ParentId);
            ViewBag.ReviewId = new SelectList(db.Review, "Id", "Title", contract.ReviewId);
            ViewBag.InstitutionId = new SelectList(db.Institution, "Id", "Title", contract.InstitutionId);
            ViewBag.ReviewSelection = new SelectList(db.Service.Where(i => i.Institutions.Any(w => w.Id == institution.Id)).ToList(), "Id", "Name");
            return View("CreateContract", contract);
        }

        //[HttpPost]
        /*public ActionResult CreateContract(DateTime InitialDate, DateTime EndDate, int? ChildId) {
            return Content("Biew");
        }
        */
        [Authorize(Roles = "Administrador")]
        public ActionResult CreateAdmin() {

            ViewBag.ChildId = new SelectList(db.Child, "Id", "Name");
            ViewBag.ParentId = new SelectList(db.Parent, "Id", "Name");
            ViewBag.ReviewId = new SelectList(db.Review, "Id", "Title");
            ViewBag.InstitutionId = new SelectList(db.Institution, "Id", "Name");
            ViewBag.ServiceId = new SelectList(db.Service, "Id", "Name");
            return View("CreateAdmin");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdmin([Bind(Include = "Id,InitialDate,EndDate,ParentId,ChildId,InstitutionId,ServiceId")] Contract contract) {

            contract.Institution = db.Institution.Find(contract.InstitutionId);
            contract.Child = db.Child.Find(contract.ChildId);
            contract.Parent = db.Parent.Find(contract.ParentId);
            contract.Approvation = EApprovation.Awaiting;
            contract.Service = db.Service.Find(contract.ServiceId);
            //contract.Review = null;
            //contract.ReviewId = null;

            if (ModelState.IsValid) {
                db.Contract.Add(contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            /*
            else
            {
                string messages = string.Join("; ", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
                return Content(messages);
            }
            */

            ViewBag.ChildId = new SelectList(db.Child, "Id", "Name", contract.ChildId);
            ViewBag.ParentId = new SelectList(db.Parent, "Id", "Name", contract.ParentId);
            ViewBag.ReviewId = new SelectList(db.Review, "Id", "Title", contract.ReviewId);
            ViewBag.InstitutionId = new SelectList(db.Institution, "Id", "Title", contract.InstitutionId);
            ViewBag.ReviewSelection = new SelectList(db.Service, "Id", "Name");
            return View("CreateAdmin", contract);
        }


        [Authorize(Roles = Profiles.Parent)]
        public ActionResult AcceptContract(int? id) {

            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contract = db.Contract.Find(id);
            if (contract == null) {
                return Content("It Should not Happen.");
            }
            if (contract.Approvation.Equals(EApprovation.Refused) ||
                contract.Approvation.Equals(EApprovation.Accepted)) {
                return Content("Contract already resolved");
            }

            contract.Approvation = EApprovation.Accepted;
            if (ModelState.IsValid) {
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Profiles.Parent)]
        public ActionResult RefuseContract(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contract = db.Contract.Find(id);
            if (contract == null) {
                return Content("It Should not Happen.");
            }
            if (contract.Approvation.Equals(EApprovation.Refused) ||
                contract.Approvation.Equals(EApprovation.Accepted)) {
                return Content("Contract already resolved");
            }
            contract.Approvation = EApprovation.Refused;
            if (ModelState.IsValid) {
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetChilds(string idParent) {

            return Content("Ccc: ");
            if (idParent == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var childs = new SelectList(db.Child.Where(p => p.ParentId == idParent), "Id", "Name");
            return Content("Ccc: " + childs.Count());
            ViewBag.ChildId = childs;
            ViewBag.ParentId = new SelectList(db.Parent, "Id", "Name");
            ViewBag.ReviewId = new SelectList(db.Review, "Id", "Title");
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetChilds() {

            return Content("Ccc: ");
        }

        // GET: Contracts/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contract.Find(id);
            if (contract == null) {
                return HttpNotFound();
            }
            ViewBag.ChildId = new SelectList(db.Child, "Id", "Name", contract.ChildId);
            ViewBag.ParentId = new SelectList(db.Parent, "Id", "Name", contract.ParentId);
            ViewBag.ReviewId = new SelectList(db.Review, "Id", "Title", contract.ReviewId);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InitialDate,EndDate,IsApproved,ParentId,ChildId,ReviewId")] Contract contract) {
            if (ModelState.IsValid) {
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChildId = new SelectList(db.Child, "Id", "Name", contract.ChildId);
            ViewBag.ParentId = new SelectList(db.Parent, "Id", "Name", contract.ParentId);
            ViewBag.ReviewId = new SelectList(db.Review, "Id", "Title", contract.ReviewId);
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contract.Find(id);
            if (contract == null) {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Contract contract = db.Contract.Find(id);
            db.Contract.Remove(contract);
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
