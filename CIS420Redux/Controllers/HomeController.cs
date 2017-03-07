using CIS420Redux.Models;
using CIS420Redux.Models.ViewModels.Student;
using CIS420Redux.Models.ViewModels.Advisor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIS420Redux.Controllers
{
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


    }
}