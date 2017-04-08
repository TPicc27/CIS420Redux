using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Controls;
using DHTMLX.Scheduler.Data;
using Scheduler.MVC5.Model.Models;


namespace Scheduler.MVC5.Controllers
{
    public class LoadMarkedTimespansController : BaseController
    {

        public ActionResult Index()
        {
            var sched = new DHXScheduler(this);
            sched.EnableDynamicLoading(SchedulerDataLoader.DynamicalLoadingMode.Day);
            sched.LoadData = true;

            sched.Extensions.Add(SchedulerExtensions.Extension.Limit);

            sched.Config.show_loading = true;

            // check LoadMarkedTimespans/Index.cshtml for a client-side settings
            sched.BeforeInit.Add("customEvents()");
            return View(sched);
        }

        public ContentResult Data(DateTime from, DateTime to)
        {
            var rnd = new Random();

            var blocked = _GetTimespansByDate(from, to, rnd);
            var events = _GetEventsByDate(from, to, rnd);

            var data = new SchedulerAjaxData(events);

            data.ServerList.Add("blocked_time", blocked);

            return (data);
        }

        private List<object> _GetTimespansByDate(DateTime from, DateTime to, Random random)
        {
            var events = new List<object>();
            

            var timeSpan = to - from;

            // get configs of blocked times, all timespans will be defined by a start and end date
            for (var i = 1; i < 7; i++)
            {
                var newSpan = new TimeSpan(0, random.Next(0, (int)timeSpan.TotalMinutes), 0);
                var evStartDate = from + newSpan;
                var evEndDate = evStartDate.AddHours(4);
                events.Add(new
                {
                    start_date = evStartDate,
                    end_date = evEndDate
                });
            }
            return events;

        }

        public List<object> _GetEventsByDate(DateTime from, DateTime to, Random random)
        {
            var events = new List<object>();
 

            var timeSpan = to - from;

            //generate couple of events within date range with random sections
            for (var i = 1; i < 15; i++)
            {
                var newSpan = new TimeSpan(0, random.Next(0, (int)timeSpan.TotalMinutes), 0);
                var evStartDate = from + newSpan;

                events.Add(new
                {
                    id = random.Next(),
                    text = string.Format("Event #{0}", i),
                    start_date = evStartDate,
                    end_date = evStartDate.AddHours(4)
                });
            }
            return events;
        }


    }
}