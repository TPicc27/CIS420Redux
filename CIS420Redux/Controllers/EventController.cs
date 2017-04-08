using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;
using CIS420Redux.Models;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;

namespace CIS420Redux.Controllers
{
    public class EventController : Controller
    {
        //Refer to this link in order to set up the Calendar.
        //http://scheduler-net.com/docs/simple-.net-mvc-application-with-scheduler.html#step_2_add_the_scheduler_reference

        public readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
            {
            return View();

         }

        public ActionResult Calendar()
        {

            return View();
        }

        public JsonResult Data()
        {
            //Using Dxhtml JavaScript Edition (open source)
            var events = _db.Events;

            var formatedEvents = new List<object>();
            foreach (var ev in events)
            {
                var formattingEvent = new
                {
                    id = ev.Id,
                    start_date = ev.StartDate.ToString(),
                    end_date = ev.EndDate.ToString(),
                    //start_date = ev.start_date.Date.ToString("yyyy-MM-dd"),
                    //end_date = ev.end_date.Date.ToString("yyyy-MM-dd"),
                    text = ev.Name
                };
                formatedEvents.Add(formattingEvent);
            }



            return Json(formatedEvents, JsonRequestBehavior.AllowGet);

            //Using Dxhtml MVC Scheduler Edition (free trial)
            //events for loading to scheduler
            //return new SchedulerAjaxData(_db.Events);
        }

        public ActionResult Save(string id, string text, string start_date, string end_date)
        {

            var existingEvent = _db.Events.FirstOrDefault(e => e.Id.ToString() == id);
            var newStartDate = Convert.ToDateTime(start_date);
            var newEndDate = Convert.ToDateTime(end_date);


            if (existingEvent != null)
            {
                existingEvent.StartDate = newStartDate;
                existingEvent.EndDate = newEndDate;
                existingEvent.Name = text;
            }
            else
            {

                var newEvent = new Event()
                {
                    StartDate = newStartDate,
                    EndDate = newEndDate,
                    Name = text
                };
                _db.Events.Add(newEvent);
            }

            _db.SaveChanges();



            return View("Index");
        }

        public ActionResult Delete(string id, string text, string start_date, string end_date)
        {

            var existingEvent = _db.Events.FirstOrDefault(e => e.Id.ToString() == id);
            var newStartDate = Convert.ToDateTime(start_date);
            var newEndDate = Convert.ToDateTime(end_date);


            if (existingEvent != null)
            {
                _db.Events.Remove(existingEvent);
                _db.SaveChanges();
            }

            return View("Index");
        }

    }
}