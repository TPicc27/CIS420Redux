using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Controls;
using DHTMLX.Scheduler.Data;

namespace Scheduler.MVC5.Controllers
{
    public class MultisectionController : Controller
    {
        // GET: Multisection
        public ActionResult Index()
        {
            var scheduler = new DHXScheduler(this);
            scheduler.Extensions.Add(SchedulerExtensions.Extension.Multisection);

            var timeline = new TimelineView("timeline", "y_property") {RenderMode = TimelineView.RenderModes.Bar};

            var options = new object[]{
                new {key = 1, label = "Section #1" },
                new {key = 2, label = "Section #2" },
                new {key = 3, label = "Section #3" },
                new {key = 4, label = "Section #4" }
            };
            timeline.AddOptions(options);

            scheduler.Views.Add(timeline);


            var units = new UnitsView("units", "y_property");
            units.AddOptions(options);
            scheduler.Views.Add(units);

            scheduler.Lightbox.Add(new LightboxText("text", "Title"));

            var multiselect = new LightboxMultiselect("y_property", "Sections");
            multiselect.AddOptions(options);
            scheduler.Lightbox.Add(multiselect);

            scheduler.Lightbox.Add(new LightboxMiniCalendar("time", "Time"));

            scheduler.LoadData = true;
            scheduler.InitialView = timeline.Name;
            return View(scheduler);
        }
        public ContentResult Data()
        {
            var evs = new List<object>{
                new { text = "First", start_date = DateTime.Today.AddHours(8), end_date = DateTime.Today.AddHours(16), y_property = "1,3"},
                new { text = "Second", start_date = DateTime.Today.AddHours(12), end_date = DateTime.Today.AddHours(17), y_property = "2,4"},
                new { text = "Third", start_date = DateTime.Today.AddDays(1).AddHours(2), end_date = DateTime.Today.AddDays(1).AddHours(10), y_property = "1"},
                new { text = "Fourth", start_date = DateTime.Today.AddDays(-1).AddHours(6), end_date = DateTime.Today.AddDays(-1).AddHours(16), y_property = "3"}
            };

            return Content(new SchedulerAjaxData(evs).Render());
        }
    }
}