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
            return View();
        }

        // POST: ClincalCompliance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Type,ExpirationDate,Status")] ClincalCompliance clincalCompliance)
        {
            if (ModelState.IsValid)
            {
                db.ClincalCompliances.Add(clincalCompliance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clincalCompliance);
        }

        // GET: ClincalCompliance/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: ClincalCompliance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Type,ExpirationDate,Status")] ClincalCompliance clincalCompliance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clincalCompliance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clincalCompliance);
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
    }
}
