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
    public class AddRangeController : BaseController
    {
        // GET: AddRange
        public ActionResult Details()
        {
            var sched = new DHXScheduler(this)
            {
                XY = {scroll_width = 0},
                Config = {first_hour = 8, last_hour = 19, time_step = 30, limit_time_select = true}
            };
            var text = new LightboxText("text", "Description") {Height = 50};
            sched.Lightbox.Add(text);

            var select = new LightboxSelect("color", "Priority");
            select.AddOptions(new List<object>{
                new { key = "#ccc", label = "Low" },
                new { key = "#76B007", label = "Medium" },
                new { key = "#FE7510", label = "Hight" }
            });
            sched.Lightbox.Add(select);

            var check = new LightboxRadio("category", "Category");
            check.AddOption(new LightboxSelectOption("job", "Job"));
            check.AddOption(new LightboxSelectOption("family", "Family"));
            check.AddOption(new LightboxSelectOption("other", "Other"));
            sched.Lightbox.Add(check);

            sched.Lightbox.Add(new LightboxMiniCalendar("time", "Time:"));

            sched.Lightbox.Add(new LightboxCheckbox("remind", "Remind"));
            sched.LoadData = true;
            sched.EnableDataprocessor = true;
            sched.InitialDate = new DateTime(2011, 9, 11);

            return View(sched);
        }

        public ActionResult Wide()
        {
            var sched = new DHXScheduler(this) {Config = {wide_form = true}, XY = {scroll_width = 0}};
            
            sched.Config.first_hour = 8;
            sched.Config.last_hour = 19;
            sched.Config.time_step = 30;
            sched.Config.limit_time_select = true;

            var text = new LightboxText("text", "Description") {Height = 50};
            sched.Lightbox.Add(text);

            var select = new LightboxSelect("color", "Priority");
            select.AddOptions(new List<object>{
                new { key = "#ccc", label = "Low" },
                new { key = "#76B007", label = "Medium" },
                new { key = "#FE7510", label = "Hight" }
            });
            sched.Lightbox.Add(select);
            
            var check = new LightboxRadio("category", "Category");
            check.AddOption(new LightboxSelectOption("job", "Job"));
            check.AddOption(new LightboxSelectOption("family", "Family"));
            check.AddOption(new LightboxSelectOption("other", "Other"));
            sched.Lightbox.Add(check);

            sched.Lightbox.Add(new LightboxMiniCalendar("time", "Time:"));

            sched.Lightbox.Add(new LightboxCheckbox("remind", "Remind"));
            sched.LoadData = true;
            sched.EnableDataprocessor = true;
            sched.InitialDate = new DateTime(2011, 9, 11);

            return View(sched);
        }



        public ActionResult Index()
        {
            var sched = new DHXScheduler(this);
            var unit = new UnitsView("unit1", "room_id");
            sched.Views.Add(unit);
            
            //can add IEnumerable of objects, native units or dictionary

            var rooms = Repository.Rooms.ToList();

            unit.AddOptions(rooms);//parse model objects
            sched.Config.details_on_create = true;
            sched.Config.details_on_dblclick = true;

            var timeline = new TimelineView("timeline", "room_id") {RenderMode = TimelineView.RenderModes.Bar};
            sched.XY.scale_height = 40;
            timeline.X_Date = "%d<br>%D";
            timeline.FitEvents = false;
            sched.Views.Add(timeline);
            
            timeline.AddOptions(rooms);
    
            var select = new LightboxSelect("color", "Priority");
            select.AddOptions(new List<object>{
                new { key = "#ccc", label = "Low" },
                new { key = "#76B007", label = "Medium" },
                new { key = "#FE7510", label = "Hight" }
            });
            sched.Lightbox.Add(select);


            select = new LightboxSelect("room_id", "Room id");
            select.AddOptions(rooms);

            sched.Lightbox.Add(select);

            sched.LoadData = true;
            sched.EnableDataprocessor = true;
            sched.InitialDate = new DateTime(2011, 9, 5);
            return View(sched);
        }

        public ContentResult Data()
        {
            var data = new SchedulerAjaxData(Repository.ColoredEvents);

            return (data);
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            var changedEvent = (ColoredEvent)DHXEventsHelper.Bind(typeof(ColoredEvent), actionValues);
        
            try
            {
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        if (!Repository.CreateColoredEvent(changedEvent))
                        {
                            Repository.UpdateColoredEvent(changedEvent);
                        } 
                        break;
                    case DataActionTypes.Delete:
                        if (!Repository.RemoveColoredEvent((int) action.SourceId))
                        {
                            Repository.UpdateColoredEvent(changedEvent);
                        }
                        break;
                    default:// "update"                          
                        var eventToUpdate = Repository.ColoredEvents.SingleOrDefault(ev => ev.id == action.SourceId);
                        if (!Repository.UpdateColoredEvent(changedEvent))
                        {
                            Repository.UpdateColoredEvent(changedEvent);
                        }
                        DHXEventsHelper.Update(eventToUpdate, changedEvent, new List<string> { "id" });
                        break;
                }
                //data.SubmitChanges();
                action.TargetId = changedEvent.id;
            }
            catch
            {
                action.Type = DataActionTypes.Error;
            }

            return (new AjaxSaveResponse(action));
        }
    }
}