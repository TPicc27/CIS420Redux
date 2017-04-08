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
using CIS420Redux.Models.ViewModels.Student;

namespace CIS420Redux.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Dashboard()
        {
            var name = HttpContext.User.Identity.Name;
            var student = db.Students.FirstOrDefault(s => s.Email == name);
            var viewModel = new HomeIndexViewModel()
            {
                StudentsList = db.Students.Where(s => s.Email.ToLower().Contains(name)).FirstOrDefault(),
                TodosList = db.Events.Take(5)
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
            var todos = db.Events.Take(5);

            return PartialView("_TodosPartial", todos);
        }

        // GET: Student
        public ActionResult Index(string searchString, string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name1_desc" : "name";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var students = from s in db.Students
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString) || s.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                case "name1_desc":
                    students = students.OrderByDescending(s => s.FirstName);
                    break;
                case "name":
                    students = students.OrderBy(s => s.FirstName);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
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
            //actualgrade = db.Enrollments.FirstOrDefault(e => e.StudentID == id).Grade;

            var enrollments = db.Enrollments.Where(e => e.StudentId == id);
            var actualGradeList = new List<decimal>();
            foreach (var enrollment in enrollments)
            {
                var actualGrade = GetCourseGrade(enrollment.Grade);
                actualGradeList.Add(actualGrade);
            }


            var testGpaSum = actualGradeList.Sum();
            var testGpaGradeCount = actualGradeList.Count() * 4;

            var testGpa = (testGpaSum / testGpaGradeCount) * 4.0M;


            //GradePointAverage = db.Students.FirstOrDefault(s => s.ID == id).GPA.ToString();
            //GradePointAverage = actualgrade;            


            Student student = db.Students.FirstOrDefault(s => s.Id == id);
            student.GPA = testGpa;

            db.SaveChanges();

            return View(student);
        }


        // GET: Student/Create
        public ActionResult Create()
        {
            var states = GetAllStates();
            var model = new Student();
            model.States = GetSelectListItems(states);

            ViewBag.CampusId = new SelectList(db.Campus, "Id", "Name");
            ViewBag.ProgramId = new SelectList(db.Program, "Id", "Name");
            return View(model);
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StudentNumber,LastName,FirstName,MiddleName,DateofBirth,Address,City,State,ZipCode,Email,PhoneNumber,EnrollmentDate,GPA,Standing,HasGraduated,CampusId,ProgramId")] Student student)
        {
            var states = GetAllStates();

            student.States = GetSelectListItems(states);
                if (ModelState.IsValid)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            ViewBag.CampusId = new SelectList(db.Campus, "Id", "Name", student.CampusId);
            ViewBag.ProgramId = new SelectList(db.Program, "Id", "Name", student.ProgramId);
            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            var states = GetAllStates();

            student.States = GetSelectListItems(states);

            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusId = new SelectList(db.Campus, "Id", "Name", student.CampusId);
            ViewBag.ProgramId = new SelectList(db.Program, "Id", "Name", student.ProgramId);
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StudentNumber,LastName,FirstName,MiddleName,DateofBirth,Address,City,State,ZipCode,Email,PhoneNumber,EnrollmentDate,GPA,Standing,HasGraduated,CampusId,ProgramId")] Student student)
        {
            var states = GetAllStates();

            student.States = GetSelectListItems(states);

            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }  
            ViewBag.CampusId = new SelectList(db.Campus, "Id", "Name", student.CampusId);
            ViewBag.ProgramId = new SelectList(db.Program, "Id", "Name", student.ProgramId);
            return View(student);
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





        public ActionResult GetStudentClinicalCompliances()
        {
            var userIdentity = HttpContext.User.Identity.Name;

            var student = db.Students.FirstOrDefault(s => s.Email == userIdentity);

            //var clinicalCompliances = db.ClincalCompliances.ToList();
            var clinicalCompliances = db.ClincalCompliances.Where(c => c.Student.Id == student.Id).ToList();

            //var isStudentCompliant = db.ClincalCompliances.Any(cc => cc.Student.Id == student.Id && cc.IsCompliant == false);
            var ccdocidList = clinicalCompliances.Select(d => d.DocumentId);
            var documents = db.Documents.Where(d => ccdocidList.Contains(d.Id));

            var viewModel = clinicalCompliances.Select(c => new StudentCCIndexViewModel
            {
                ExpirationDate = c.ExpirationDate,
                DocumentId = c.DocumentId,
                IsComplaint = c.IsCompliant,
                ID = c.ID,                
                Type = c.Type,
                StudentNumber = student.StudentNumber
            });        
                        
            foreach(var doc in documents)
            {
                viewModel.FirstOrDefault(v => v.DocumentId == doc.Id).Document = doc;                
            }


            return View(viewModel);
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
        public IEnumerable<string> GetAllStates()
        {
            return new List<string>
            {
               "Alabama",
               "Alaska",
               "Arizona",
              "Arkansas",
              "California",
              "Colorado",
               "Connecticut",
               "District of Columbia",
               "Delaware",
               "Florida",
               "Georgia", 
                "Hawaii",
                "Idaho",
                "Illinois",
                "Indiana",
                "Iowa",
                "Kansas",
                "Kentucky",
                "Louisiana",
                "Maine",
                "Maryland",
                "Massachusetts",
                "Michigan",
                "Minnesota",
                "Mississippi",
                "Missouri",
                "Montana",
                "Nebraska",
                "Nevada",
                "New Hampshire",
                "New Jersey",
                "New Mexico",
                "New York",
                "North Carolina",
                "North Dakota",
                "Ohio",
                "Oklahoma",
                "Oregon",
                "Pennsylvania",
                "Rhode Island",
                "South Carolina",
                "South Dakota",
                "Tennessee",
                "Texas",
                "Utah",
                "Vermont",
                "Virginia",
                "Washington",
                "West Virginia",
                "Wisconsin",
                "Wyoming", 
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
