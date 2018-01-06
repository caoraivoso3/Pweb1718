using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using SpacesForChildren.Models;

namespace SpacesForChildren.Controllers
{
    [AllowAnonymous]
    public class InstitutionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void PopulateSearchViewBags()
        {
            ViewBag.RankingListItems = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Todos", Value = "0" },
                new SelectListItem { Text = "1", Value = "1" },
                new SelectListItem { Text = "2", Value = "2" },
                new SelectListItem { Text = "3", Value = "3" },
                new SelectListItem { Text = "4", Value = "4" },
                new SelectListItem { Text = "5", Value = "5" }
            };
            ViewBag.CityListItems = new SelectList(db.Institution, "Id", "City");
        }

        // GET: Institutions
        public ActionResult Index()
        {
            PopulateSearchViewBags();
            return View(db.Institution.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string CityListItems, int? RankingListItems)
        {
            //int ranking = Int32.Parse(ViewBag.RankingListItems);
            string location = CityListItems;
            int? ranking = RankingListItems;
            /*if (!location.IsNullOrWhiteSpace() && ranking != null)
            {
                PopulateSearchViewBags();
                return View(db.Institution
                    .Where(i => i.Id == location)
                        .Include(r => r.Reviews.Where(rank => rank.Ranking == ranking))
                        .ToList());
            }else*/ if (!location.IsNullOrWhiteSpace())
            {
                PopulateSearchViewBags();
                return View(db.Institution
                    .Where(i => i.Id == location).ToList());
            }
            
            return Index();
        }

        // GET: Institutions/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = db.Institution.Find(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        // GET: Institutions/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = db.Institution.Find(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        // POST: Institutions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,NIF,Address,City,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Type,Acronym,Description,IsApproved")] Institution institution)
        {
            if (ModelState.IsValid)
            {
                var toUpdate = db.Institution.Find(institution.Id);
                toUpdate.IsApproved = institution.IsApproved;
                toUpdate.Name = institution.Name;
                toUpdate.NIF = institution.NIF;
                toUpdate.Address = institution.Address;
                toUpdate.City = institution.City;
                toUpdate.Email = institution.Email;
                toUpdate.PhoneNumber = institution.PhoneNumber;
                toUpdate.Type = institution.Type;
                toUpdate.Description = institution.Description;

                db.Institution.AddOrUpdate(toUpdate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(institution);
        }

        // GET: Institutions/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = db.Institution.Find(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        // POST: Institutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Institution institution = db.Institution.Find(id);
            db.Users.Remove(institution);
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
