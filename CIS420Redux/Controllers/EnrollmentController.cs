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
    public class EnrollmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Enrollment
        public ActionResult Index()
        {
            var enrollments = db.Enrollments.Include(e => e.Course).Include(e => e.Program).Include(e => e.Student);
            return View(enrollments.ToList());
        }

        // GET: Enrollment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollment/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title");
            ViewBag.ProgramId = new SelectList(db.Program, "Id", "Name");
            ViewBag.StudentId = new SelectList(db.Students, "Id", "LastName");
            return View();
        }

        // POST: Enrollment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CourseId,StudentId,ProgramId,Semester,Grade")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                var enrollments = db.Enrollments.Where(e => e.StudentId == enrollment.StudentId);
                var actualGradeList = new List<decimal>();
                foreach (var enrollmentGrade in enrollments)
                {
                    var actualGrade = GetCourseGrade(enrollmentGrade.Grade);
                    actualGradeList.Add(actualGrade);
                }


                var testGpaSum = actualGradeList.Sum();
                var testGpaGradeCount = actualGradeList.Count() * 4;

                var testGpa = (testGpaSum / testGpaGradeCount) * 4.0M;


                //GradePointAverage = db.Students.FirstOrDefault(s => s.ID == id).GPA.ToString();
                //GradePointAverage = actualgrade;            


                Student student = db.Students.FirstOrDefault(s => s.Id == enrollment.StudentId);
                student.GPA = testGpa;



                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", enrollment.CourseId);
            ViewBag.ProgramId = new SelectList(db.Program, "Id", "Name", enrollment.ProgramId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "LastName", enrollment.StudentId);
            return View(enrollment);

        }

        // GET: Enrollment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", enrollment.CourseId);
            ViewBag.ProgramId = new SelectList(db.Program, "Id", "Name", enrollment.ProgramId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "LastName", enrollment.StudentId);
            return View(enrollment);
        }

        // POST: Enrollment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourseId,StudentId,ProgramId,Semester,Grade")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Title", enrollment.CourseId);
            ViewBag.ProgramId = new SelectList(db.Program, "Id", "Name", enrollment.ProgramId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "LastName", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public decimal GetCourseGrade(string courseLetterGrade)
        {
            var actualgrade = 0.0M;

            if (courseLetterGrade == "A+")
            {
                actualgrade = 4.0M;
            }
            else if (courseLetterGrade == "A")
            {
                actualgrade = 4.0M;
            }
            else if (courseLetterGrade == "A-")
            {
                actualgrade = 3.7M;
            }
            else if (courseLetterGrade == "B+")
            {
                actualgrade = 3.3M;
            }
            else if (courseLetterGrade == "B")
            {
                actualgrade = 3.0M;
            }
            else if (courseLetterGrade == "B-")
            {
                actualgrade = 2.7M;
            }
            else if (courseLetterGrade == "C+")
            {
                actualgrade = 2.3M;
            }
            else if (courseLetterGrade == "C")
            {
                actualgrade = 2.0M;
            }
            else if (courseLetterGrade == "C-")
            {
                actualgrade = 1.7M;
            }
            else if (courseLetterGrade == "D+")
            {
                actualgrade = 1.3M;
            }
            else if (courseLetterGrade == "D")
            {
                actualgrade = 1.0M;
            }
            else if (courseLetterGrade == "D-")
            {
                actualgrade = 0.7M;
            }
            else if (courseLetterGrade == "F")
            {
                actualgrade = 0.0M;
            }

            return actualgrade;
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
