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
    public class LoadTimelineSectionsController : BaseController
    {
      
        public ActionResult Index()
        {
            var sched = new DHXScheduler(this);
            var unit = new UnitsView("unit1", "section_id");
            sched.Views.Add(unit);
            
            var timeline = new TimelineView("timeline", "section_id") { 
                    RenderMode = TimelineView.RenderModes.Bar,
                    X_Unit = TimelineView.XScaleUnits.Day,
                    X_Size = 7,
                    X_Date = "%d"
            };
            sched.Views.Add(timeline);


            var list_name = "sections";

            unit.ServerList = list_name;
            timeline.ServerList = list_name;

            sched.Config.show_loading = true;

            // check LoadTimelineSections/Index.cshtml for a client-side settings
            sched.BeforeInit.Add("customEvents()");

            sched.InitialView = timeline.Name;
            return View(sched);
        }

        public ContentResult Data(DateTime from, DateTime to)
        {

            var sections = _GetSectionsByDate(from, to);
            var events = _GetEventsByDate(from, to);
   
            var data = new SchedulerAjaxData(events);

            data.ServerList.Add("sections", sections);

            return (data);
        }

        private List<object> _GetSectionsByDate(DateTime from, DateTime to)
        {
            var items = new List<object>();
            var current = from;
            var key = 1;
            var rnd = new Random();
            while (current < to)
            {
                items.Add(new { key = key, label = string.Format("Section #{0}", rnd.Next()) });
                current = current.AddDays(1);
                key++;
            }
            return items;

        }

        public List<object> _GetEventsByDate(DateTime from, DateTime to)
        {
            var events = new List<object>();
            var rnd = new Random();

            var timeSpan = to - from;
           
            //generate couple of events within date range with random sections
            for (var i = 1; i < 15; i++)
            {
                var newSpan = new TimeSpan(0, rnd.Next(0, (int)timeSpan.TotalMinutes), 0);
                var evStartDate = from + newSpan;

                events.Add(new
                {
                    id = rnd.Next(),
                    text = string.Format("Event #{0}", i),
                    start_date = evStartDate,
                    end_date = evStartDate.AddHours(8),
                    section_id = rnd.Next(timeSpan.Days)
                });
            }
            return events;
        }
        
          
    }
}