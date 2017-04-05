using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CIS420Redux.Models;

namespace CIS420Redux.Controllers
{
    public class ClincalComplianceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClincalCompliance
        public ActionResult Index()
        {
            var pOS = db.POS.Include(p => p.Student);
            return View(db.ClincalCompliances.ToList());
        }

        // GET: ClincalCompliance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClincalCompliance clincalCompliance = db.ClincalCompliances.Find(id);
            if (clincalCompliance == null)
            {
                return HttpNotFound();
            }
            return View(clincalCompliance);
        }
        // GET: ClincalCompliance/Create
        public ActionResult Create()
        {
            var types = GetAllTypes();

            var model = new ClincalCompliance();

            model.Types = GetSelectListItems(types);

            ViewBag.StudentId = new SelectList(db.Students, "Id", "LastName");

            return View(model);
        }

        // POST: ClincalCompliance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Type,ExpirationDate,StudentId,DocumentId")] ClincalCompliance model)
        {
            var types = GetAllTypes();

            model.Types = GetSelectListItems(types);

            if (ModelState.IsValid)
            {
                model.ExpirationDate = DateTime.Today;
                if(model.ExpirationDate == DateTime.Today)
                {
                    model.IsExpired = true;
                }
                db.ClincalCompliances.Add(model);
                db.SaveChanges();
                return RedirectToAction("CCDocuments", "Advisor");
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "LastName", model.StudentId);
            return View(model);
        }


        // GET: ClincalCompliance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClincalCompliance clincalcompliances = db.ClincalCompliances.Find(id);
            var types = GetAllTypes();

            clincalcompliances.Types = GetSelectListItems(types);

            if (clincalcompliances == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "LastName", clincalcompliances.StudentId);
            return View(clincalcompliances);
        }

        // POST: ClincalCompliance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Type,ExpirationDate,StudentId,DocumentId")] ClincalCompliance model)
        {
            var types = GetAllTypes();

            model.Types = GetSelectListItems(types);
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "LastName", model.StudentId);
            return View(model);
        }

        // GET: ClincalCompliance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClincalCompliance clincalCompliance = db.ClincalCompliances.Find(id);
            if (clincalCompliance == null)
            {
                return HttpNotFound();
            }
            return View(clincalCompliance);
        }

        // POST: ClincalCompliance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClincalCompliance clincalCompliance = db.ClincalCompliances.Find(id);
            db.ClincalCompliances.Remove(clincalCompliance);
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

        public IEnumerable<string> GetAllTypes()
        {
            return new List<string>
            {
                "CPR",
                "HIPAA",
                "Bloobourne Path.",
                "Liability Insurance",
                "Immunizations",
                "Drug Screening",
                "CNA",
            };
        }
        public IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            var selectList = new List<SelectListItem>();

            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {

                    Value = element,
                    Text = element
                });
            }

            return selectList;
        }
    }


}
  
