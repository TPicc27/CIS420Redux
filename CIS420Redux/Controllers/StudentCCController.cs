using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CIS420Redux.Models;
using CIS420Redux.Models.ViewModels;
using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CIS420Redux.Controllers
{
    public class StudentCCController : Controller
    {
        readonly ApplicationDbContext _db = new ApplicationDbContext();
        // GET: StudentCC
        public ActionResult Index()
        {
            return View();
        }
    }
}