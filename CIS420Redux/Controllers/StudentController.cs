﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CIS420Redux.Models;
using CIS420Redux.Models.ViewModels;
using CIS420Redux.Models.ViewModels.Student;

namespace CIS420Redux.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Dashboard()
        {
            var name = HttpContext.User.Identity.Name;

            var viewModel = new HomeIndexViewModel()
            {
                StudentsList = db.Students.Where(s => s.Email.ToLower().Contains(name)).FirstOrDefault(),
                TodosList = db.Events.Take(2)
            };
            return View(viewModel);
        }


        public PartialViewResult GetStudentsList()
        {
            var name = HttpContext.User.Identity.Name;
            var students = db.Students.Where(s => s.Email.ToLower().Contains(name)).FirstOrDefault();
            return PartialView("_StudentsPartial", students);
        }

        public PartialViewResult GetTodosList()
        {
            var todos = db.Events.Take(2);

            return PartialView("_TodosPartial", todos);
        }

        // GET: Student
        public ActionResult Index(string searchString)
        {
            var students = db.Students.Select(s => new StudentIndexViewModel()
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Address = s.Address,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                EnrollmentDate = s.EnrollmentDate,
                CampusId = s.CampusId,
                ProgramId = s.ProgramId
            });

            if(!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString));
            }

            return View(students.ToList());
        }

        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult StudentReport()
        {
            return View(db.Students.ToList());
        }

        public ActionResult GPAReport(decimal gpaThreshold)
        {
            var Student = db.Students.Where(s => s.GPA >= gpaThreshold).ToList();
            return View(Student);
        }

        public ActionResult StudentSearch(int programThreshold)
        {
            var Student = db.Students.Where(s => s.ProgramId >= programThreshold).ToList();
            return View(Student);
        }      

        public ActionResult Alerts()
        {
            DateTime start = DateTime.Today,
                 end = start.AddDays(7);

            var alertModel = db.Events.Where(d => d.StartDate > start && d.StartDate < end);
            return View("Alerts", alertModel);       
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var student = new Student()
                {
                    Address = vm.Address,
                    City = vm.City,
                    EnrollmentDate = vm.EnrollmentDate,
                    Email = vm.Email,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    ZipCode = vm.ZipCode.ToString(),
                    State = vm.State,
                    CampusId = vm.CampusId,
                    ProgramId = vm.ProgramId
                };
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index", "Student");
            }

            return View(vm);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentIndexViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var student = db.Students.FirstOrDefault(s => s.Id == vm.Id);

                if (student != null)
                {
                    student.FirstName = vm.FirstName;
                    student.LastName = vm.LastName;
                    student.Address = vm.Address;
                    student.Email = vm.Email;
                    student.EnrollmentDate = vm.EnrollmentDate;
                    student.CampusId = vm.CampusId;
                    student.ProgramId = vm.ProgramId;
                }

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
