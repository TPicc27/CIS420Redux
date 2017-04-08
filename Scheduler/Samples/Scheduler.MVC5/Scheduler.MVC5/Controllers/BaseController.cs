using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Scheduler.MVC5.Global.Auth;
using Scheduler.MVC5.Model;

namespace Scheduler.MVC5.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        [Inject]
        public IRepository Repository { get; set; }
       
    }
}