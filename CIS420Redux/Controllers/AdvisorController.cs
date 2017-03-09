using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CIS420Redux.Models;
using CIS420Redux.Models.ViewModels.Student;
using CIS420Redux.Models.ViewModels.Advisor;

namespace CIS420Redux.Controllers
{
    public class AdvisorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Dashboard()
        {
            DateTime start = DateTime.Today,
                end = start.AddDays(7);

            var viewModel = new AdvisorIndexViewModel()
            {
                AdvisorTodosList = db.Todoes.Take(2),
                AlertList = db.Events.Where(d => d.StartDate > start && d.StartDate < end),
                NCStudentsList = db.Students.Where(d => d.Is_Compliant == false)
            };
            return View(viewModel);
        }

        public ActionResult StudentRecords()
        {
            var viewModel = new StudentRecordViewModel()
            {
                StudentRecordsList = db.Students.ToList(),
                AdvisorTodosList = db.Todoes.Take(2)

            };
            return View(viewModel);

        }
        // GET: Advisor
        public ActionResult Index()
        {
            return View(db.Advisors.ToList());
        }

        //public PartialViewResult GetAlertList()
        //{
        //    var alert = db.Events.Take(2);
        //    need to filter events somehow(upcoming events this week)
        //    return PartialView("_AlertsPartial", alert);
        //}
        //public ActionResult Alerts()
        //{
        //    //
        //    DateTime start = DateTime.Today,
        //        end = start.AddDays(7);

        //    var viewModel = new StudentIndexViewModel()
        //    {
        //        //select only events within 7 days of current date
        //        AlertList = db.Events.Where(d => d.StartDate > start && d.StartDate < end)
        //    };
        //    return View(viewModel);
        //}
        public PartialViewResult StudentRecordsList()
        {
            var students = db.Students.ToList();

            return PartialView("StudentRecordsPartial", students);
            
        }
        public PartialViewResult AdvisorTodosList()
        {
            var todos = db.Todoes.Take(2);

            return PartialView("AdvisorTodosPartial", todos);
        }

        public  ActionResult Search()
        {
            return View();
        }

        public ActionResult Advisors()
        {
            return View();
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
