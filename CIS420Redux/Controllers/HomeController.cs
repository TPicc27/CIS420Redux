using CIS420Redux.Models;
using CIS420Redux.Models.ViewModels.Student;
using CIS420Redux.Models.ViewModels.Advisor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace CIS420Redux.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
       public ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DocumentManagement()
        {

            return View();
        }

        [HttpPost]
        public ActionResult UploadDocument(int studentNumber,DateTime expirationDate, string type, HttpPostedFileBase file)
        {

            byte[] uploadedFile = new byte[file.InputStream.Length];
            file.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

            var student = db.Students.FirstOrDefault(s => s.StudentNumber == studentNumber);

            if (student != null)
            {
                var documentModel = new Document
                {
                    StudentId = student.Id,
                    StudentNumber = studentNumber,
                    ExpirationDate = expirationDate,
                    Type = type,
                    UploadedBy = HttpContext.User.Identity.Name,
                    ContentLength = file.ContentLength,
                    ContentType = file.ContentType,
                    FileName = file.FileName,
                    FileBytes = uploadedFile,
                };

                db.Documents.Add(documentModel);
                db.SaveChanges();



                var documentRecord = db.Documents.FirstOrDefault(d => d.StudentId == student.Id && d.Type == type);
            

                var ccRecord = db.ClincalCompliances.FirstOrDefault(d => d.StudentId == student.Id && d.Type == type);

                ccRecord.DocumentId = documentRecord.Id;

                ccRecord.Type = documentRecord.Type;

                var complianceRecord = db.ClincalCompliances.FirstOrDefault(d => d.DocumentId == documentModel.Id);

                complianceRecord.IsCompliant = true;

                db.SaveChanges();
            }

            return View("DocumentManagement");
        }

        public ActionResult GetDocument(int documentId)
        {
            var userIdentity = HttpContext.User.Identity.Name;

            var student = db.Students.FirstOrDefault(s => s.Email == userIdentity);

            var allDocumentsForStudent = db.Documents.Where(d => d.StudentId == student.Id);

            var oneDocumentFromStudent = allDocumentsForStudent.Where(d => d.Id == documentId).FirstOrDefault();

            if (oneDocumentFromStudent != null)
            {
                return File(oneDocumentFromStudent.FileBytes, "application/octet-stream", oneDocumentFromStudent.FileName);
            }
            return RedirectToAction("GetStudentClinicalCompliances", "Student", false);
        }

        public ActionResult ViewDocument()
        {
            var userIdentity = HttpContext.User.Identity.Name;
            var student = db.Students.FirstOrDefault(s => s.Email == userIdentity);
            var allDocumentsForStudent = db.Documents.Where(d => d.StudentId == student.Id);
            return View(allDocumentsForStudent.ToList());
        }

        public ActionResult DeleteDocument(int? id)
        {
            Document documents = db.Documents.FirstOrDefault(d => d.Id == id);
            db.Documents.Remove(documents);
            db.SaveChanges();
            return RedirectToAction("ViewDocument");
        }

        

    }
}