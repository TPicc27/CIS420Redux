using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using DHTMLX.Scheduler.Controls;
using Scheduler.MVC5.Model.Models;

namespace Scheduler.MVC5.Controllers
{
    public class MVCFormInLightboxController : BaseController
    {
        // GET: MVCFormInLightbox
        public ActionResult Index()
        {
            var sched = new DHXScheduler(this);

            sched.LoadData = true;
            sched.EnableDataprocessor = true;

            var timeline = new TimelineView("timeline", "room_id") { RenderMode = TimelineView.RenderModes.Bar };
            sched.XY.scale_height = 40;
            timeline.X_Date = "%d<br>%D";
            timeline.FitEvents = false;
            sched.Views.Add(timeline);

            timeline.AddOptions(new List<object> { 
                new {key= 1, label = "first"},
                new {key= 2, label = "second"},
                new {key= 3, label = "third"},
                new {key= 4, label = "fourth"}
            });
            //view, width, height
            var box = sched.Lightbox.SetExternalLightbox("MVCFormInLightbox/LightboxControl", 420, 195);
            //css class to be applied to the form
            box.ClassName = "custom_lightbox";
            sched.InitialDate = new DateTime(2011, 9, 5);

            //try in new skin
            //sched.Skin = DHXScheduler.Skins.Terrace;

            return View(sched);
        }

        public ActionResult LightboxControl(Event ev)
        {
            var current = Repository.Events.SingleOrDefault(e => e.id == ev.id);
            if (current == null)
                current = ev;
            return View(current);
        }

        public ContentResult Data()
        {
            var data = new SchedulerAjaxData(Repository.Events);

            return (data);
        }

        public ActionResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            var changedEvent = (Event)DHXEventsHelper.Bind(typeof(Event), actionValues);
            if (action.Type != DataActionTypes.Error)
            {
                //process resize, d'n'd operations...
                return NativeSave(changedEvent, actionValues);
            }
            else
            {
                //custom form operation
                return CustomSave(changedEvent, actionValues);
            }

        }

        public ActionResult CustomSave(Event changedEvent, FormCollection actionValues)
        {

            var action = new DataAction(DataActionTypes.Update, changedEvent.id, changedEvent.id);
            if (actionValues["actionButton"] != null)
            {
                try
                {
                    if (actionValues["actionButton"] == "Save")
                    {

                        if (Repository.Events.SingleOrDefault(ev => ev.id == action.SourceId) != null)
                        {
                            var eventToUpdate = Repository.Events.SingleOrDefault(ev => ev.id == action.SourceId);
                            Repository.UpdateEvents(changedEvent);
                            DHXEventsHelper.Update(eventToUpdate, changedEvent, new List<string>() { "id" });
                        }
                        else
                        {
                            action.Type = DataActionTypes.Insert;

                            Repository.CreateEvents(changedEvent);
                        }
                    }
                    else if (actionValues["actionButton"] == "Delete")
                    {
                        action.Type = DataActionTypes.Delete;
                        changedEvent = Repository.Events.SingleOrDefault(ev => ev.id == action.SourceId);
                        Repository.RemoveEvents((int)action.SourceId);
                    }
                   // data.SubmitChanges();
                }

                catch
                {
                    action.Type = DataActionTypes.Error;
                }
            }
            else
            {
                action.Type = DataActionTypes.Error;
            }


            return (new SchedulerFormResponseScript(action, changedEvent));

        }

        public ContentResult NativeSave(Event changedEvent, FormCollection actionValues)
        {

            var action = new DataAction(actionValues);

            try
            {
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        Repository.CreateEvents(changedEvent);
                        //data.Events.InsertOnSubmit(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        changedEvent = Repository.Events.SingleOrDefault(ev => ev.id == action.SourceId);
                        Repository.RemoveEvents((int)action.SourceId);
                        break;
                    default:// "update"                          
                        var eventToUpdate = Repository.Events.SingleOrDefault(ev => ev.id == action.SourceId);
                        Repository.UpdateEvents(changedEvent);
                        DHXEventsHelper.Update(eventToUpdate, changedEvent, new List<string>() { "id" });
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