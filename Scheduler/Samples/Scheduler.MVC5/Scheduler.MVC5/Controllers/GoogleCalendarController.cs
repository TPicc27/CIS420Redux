using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using DHTMLX.Scheduler.GoogleCalendar;
using Scheduler.MVC5.Model;
using Scheduler.MVC5.Model.Models;

namespace Scheduler.MVC5.Controllers
{
    public class GoogleCalendarController : BaseController
    {
        // GET: GoogleCalendar
        public ActionResult Index()
        {
            var sched = new DHXScheduler {InitialDate = new DateTime(2011, 7, 19), LoadData = true};

            sched.InitialView = sched.Views[0].Name;
            sched.Config.isReadonly = true;
            sched.DataFormat = SchedulerDataLoader.DataFormats.iCal;
            sched.DataAction = Url.Action("Data", "GoogleCalendar");
            return View(sched);
        }

        public ActionResult MixedContent()
        {
            var sched = new DHXScheduler(this) {InitialDate = new DateTime(2011, 7, 19), LoadData = true};

            sched.InitialView = sched.Views[0].Name;
            sched.DataAction = "Mixed";
            return View(sched);
        }
        public ContentResult Data()
        {
            var data = new SchedulerAjaxData();

            return Content(data.FromUrl("https://www.google.com/calendar/ical/b0prga519c0g0t3crcnc0g9in0@group.calendar.google.com/public/basic.ics"));

        }
        public ContentResult Mixed()
        {
            //   var helper = new 
               var helper = new ICalHelper();
                var events = helper.GetFromFeed("https://www.google.com/calendar/ical/b0prga519c0g0t3crcnc0g9in0@group.calendar.google.com/public/basic.ics");

            var data = new SchedulerAjaxData(Repository.Events);
            //data.FromUrl("https://www.google.com/calendar/ical/b0prga519c0g0t3crcnc0g9in0@group.calendar.google.com/public/basic.ics");
            data.Add(events.Select(o=>new Event()
            {
                end_date = o.end_date,
                id=Convert.ToInt32(o.id),
                start_date = o.start_date,
                room_id = null,
                text = o.text,
                user_id = null
            }));
            return data;
        }
        public ActionResult ExportToGoogleCalendar()
        {
            var help = new GoogleCalendarHelper("login", "password");
            help.ExportToGoogleCalendar("https://www.google.com/calendar/feeds/default/private/full", Repository.Events);


            return Content("");
        }
    }
}