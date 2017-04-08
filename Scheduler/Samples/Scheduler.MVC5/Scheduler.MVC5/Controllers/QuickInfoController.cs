using System;
using System.Web.Mvc;
using DHTMLX.Scheduler;

namespace Scheduler.MVC5.Controllers
{
    public class QuickInfoController : BaseController
    {
        // GET: QuickInfo
        public virtual ActionResult Index()
        {

            var sched = new DHXScheduler(this) {InitialDate = new DateTime(2011, 9, 19)};

            sched.Data.Parse(Repository.Events);

            sched.Extensions.Add(SchedulerExtensions.Extension.QuickInfo);
            return View(sched);
        }
    }
}