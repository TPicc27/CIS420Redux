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
    public class AdvisorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Dashboard()
        {
            return View();
        }
        // GET: Advisor
        public ActionResult Index()
        {
            return View(db.Advisors.ToList());
        }

        // GET: Advisor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advisor advisor = db.Advisors.Find(id);
            if (advisor == null)
            {
                return HttpNotFound();
            }
            return View(advisor);
        }

        // GET: Advisor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Advisor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id")] Advisor advisor)
        {
            if (ModelState.IsValid)
            {
                db.Advisors.Add(advisor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(advisor);
        }

        // GET: Advisor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advisor advisor = db.Advisors.Find(id);
            if (advisor == null)
            {
                return HttpNotFound();
            }
            return View(advisor);
        }

        // POST: Advisor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] Advisor advisor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(advisor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(advisor);
        }

        // GET: Advisor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advisor advisor = db.Advisors.Find(id);
            if (advisor == null)
            {
                return HttpNotFound();
            }
            return View(advisor);
        }

        // POST: Advisor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advisor advisor = db.Advisors.Find(id);
            db.Advisors.Remove(advisor);
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
