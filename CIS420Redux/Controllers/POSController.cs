using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CIS420Redux.Models;
using CIS420Redux.Models.ViewModels;
using CIS420Redux.Models.ViewModels.POS;

namespace CIS420Redux.Controllers
{
    public class POSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: POS
        public ActionResult Index()
        {
            var pOS = db.POS.Include(p => p.Student);
            return View(pOS.ToList());
        }

        // GET: POS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POS pOS = db.POS.Find(id);
            if (pOS == null)
            {
                return HttpNotFound();
            }
            return View(pOS);
        }

        // GET: POS/Create
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "Id", "LastName");
            return View();
        }

        // POST: POS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Course1,Course2,Course3,Course4,Course5,Course6,Course7,Course8,Course9,Course10,Course11,Course12,StudentId")] POS pOS)
        {
            if (ModelState.IsValid)
            {
                db.POS.Add(pOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentId = new SelectList(db.Students, "Id", "LastName", pOS.StudentId);
            return View(pOS);
        }

        // GET: POS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                var curentUserEmail = HttpContext.User.Identity.Name;
                var student = db.Students.FirstOrDefault(s => s.Email == curentUserEmail);

                var pos = db.POS.FirstOrDefault(p => p.StudentId == student.Id);

                var programOfStudy = db.POS.FirstOrDefault(p => p.Id == pos.Id);

                var programOfStudyViewModel = new PotentialClassIndexViewModel()
                {
                    CourseList = programOfStudy,
                    posImage = db.Students.FirstOrDefault(s => s.Id == programOfStudy.StudentId)

                };

                return View(programOfStudyViewModel);

            }

            POS pOS = db.POS.Find(id);

            var viewModel = new PotentialClassIndexViewModel()
            {
                CourseList = pOS,
                posImage = db.Students.FirstOrDefault(s => s.Id == pOS.StudentId)

            };
            //if (pOS == null)
            //{
            //    return HttpNotFound();
            //}
            return View(viewModel);
        }



        // POST: POS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Course1,Course2,Course3,Course4,Course5,Course6,Course7,Course8,Course9,Course10,Course11,Course12,StudentId")] POS pOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "LastName", pOS.StudentId);
            return View(pOS);
        }

        // GET: POS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POS pOS = db.POS.Find(id);
            if (pOS == null)
            {
                return HttpNotFound();
            }
            return View(pOS);
        }

        // POST: POS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            POS pOS = db.POS.Find(id);
            db.POS.Remove(pOS);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult CourseList()
        {
            var courses = db.POS.ToList();
            return PartialView("CoursePartial", courses);
        }
       
        public PartialViewResult posImage(int ?id)
        {
            POS pOS = db.POS.Find(id);
            var document = db.Students.Where(s => s.Id == id);

            return PartialView("ImagePartial", document);
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
