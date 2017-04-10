﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Controls;

namespace SchedulerTest.Controllers
{
    /// <summary>
    /// you can create multiple scheduler on page
    /// </summary>
    public class MultiSchedulerController : Controller
    {
        //
        // GET: /MultiScheduler/
        public class mod
        {
            public DHXScheduler sh1 { get; set; }
            public DHXScheduler sh2 { get; set; }
        }
        public ActionResult Index()
        {
            //each scheduler must have unique name
            var scheduler = new DHXScheduler("sched1");
  
            scheduler.InitialView = scheduler.Views[scheduler.Views.Count - 1].Name;
            var scheduler2 = new DHXScheduler("sched2");
   
            return View(new mod() { sh1 = scheduler, sh2 = scheduler2 });
        }

        public ActionResult DragBetween()
        {
            //each scheduler must have unique name
            var scheduler = new DHXScheduler("sched1");
            scheduler.Extensions.Add(SchedulerExtensions.Extension.DragBetween);
            scheduler.InitialView = scheduler.Views[scheduler.Views.Count - 1].Name;


            var scheduler2 = new DHXScheduler("sched2");
            var timeline = new TimelineView("timeline", "room_id");
            timeline.RenderMode = TimelineView.RenderModes.Bar;

            var rooms = new List<object>();
            for (var i = 1; i < 10; i++)
            {
                rooms.Add(new { key = i, label = string.Format("Room #{0}", i) });
            }

            timeline.FitEvents = false;
            scheduler2.Views.Add(timeline);
            timeline.AddOptions(rooms);
            timeline.X_Unit = TimelineView.XScaleUnits.Hour;
            timeline.X_Size = 18;
            timeline.AddSecondScale(TimelineView.XScaleUnits.Day, "%j, %M");
            timeline.X_Step = 4;
            scheduler2.InitialView = timeline.Name;

            return View(new mod() { sh1 = scheduler, sh2 = scheduler2 });
        }

    }
}
