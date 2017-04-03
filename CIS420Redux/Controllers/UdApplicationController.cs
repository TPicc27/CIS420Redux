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
    public class UdApplicationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UdApplication
        public ActionResult Index()
        {
            return View(db.UDApplications.ToList());
        }

        // GET: UdApplication/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UdApplication udApplication = db.UDApplications.Find(id);
            if (udApplication == null)
            {
                return HttpNotFound();
            }
            return View(udApplication);
        }

        // GET: UdApplication/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: UdApplication/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentNumber,Id,FirstName,MiddleName,LastName,Email,Address1,Address2,City,State,ZipCode,HomePhone,CellPhone,CampusId,SelectProgram,Semester,CurrentCourses,PersonalQualties,HealthCare,Crimes,SchoolTrouble,HonorablyDischarge,DischargedEmployment,Harassment,DrugsOrAlcohol,DrugsOrAlcoholEssay,AccurateKnowledge,Status")] UdApplication udApplication)
        {
            if (ModelState.IsValid)
            {
                db.UDApplications.Add(udApplication);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(udApplication);
        }

        // GET: UdApplication/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UdApplication udApplication = db.UDApplications.Find(id);

            var model = new UdApplication();
            var statuses = GetStatusList();

           udApplication.StatusList = GetSelectListItems(statuses);

            if (udApplication == null)
            {
                return HttpNotFound();
            }
            return View(udApplication);
        }

        // POST: UdApplication/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentNumber,FirstName,MiddleName,LastName,Email,Address1,Address2,City,State,ZipCode,HomePhone,CellPhone,CampusId,SelectProgram,Semester,CurrentCourses,PersonalQualties,HealthCare,Crimes,SchoolTrouble,HonorablyDischarge,DischargedEmployment,Harassment,DrugsOrAlcohol,DrugsOrAlcoholEssay,AccurateKnowledge,Status")] UdApplication udApplication)
        {
            var statuses = GetStatusList();

            udApplication.StatusList = GetSelectListItems(statuses);
            if (ModelState.IsValid)
            {
                db.Entry(udApplication).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(udApplication);
        }

        // GET: UdApplication/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UdApplication udApplication = db.UDApplications.Find(id);
            if (udApplication == null)
            {
                return HttpNotFound();
            }
            return View(udApplication);
        }

        // POST: UdApplication/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UdApplication udApplication = db.UDApplications.Find(id);
            db.UDApplications.Remove(udApplication);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IEnumerable<string> GetStatusList()
        {
            return new List<string>
            {
               "Accepted",
               "Decline",
               "Waiting"
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
