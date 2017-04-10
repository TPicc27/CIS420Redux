using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Controls;
using DHTMLX.Scheduler.Data;
using Scheduler.MVC5.Model.Models;

namespace Scheduler.MVC5.Controllers
{
    public class PartialViewInLightboxController : BaseController
    {
        // GET: PartialViewInLightbox
        public ActionResult Index()
        {
            var sched = new DHXScheduler(this) {LoadData = true, EnableDataprocessor = true};
            sched.Views.Add(new AgendaView());

            //Need to implement setValue/getValue interface, and form will be fully integrated into the scheduler
            var box = sched.Lightbox.SetExternalLightboxForm("PartialViewInLightbox/Form", 500, 150);
            box.ClassName = "custom_lightbox";
            sched.InitialDate = new DateTime(2011, 9, 5);

            //try in new skin
            //sched.Skin = DHXScheduler.Skins.Terrace;

            return View(sched);
        }

        public ActionResult Form()
        {
            return View();
        }
        public ContentResult Data()
        {
            var data = new SchedulerAjaxData(Repository.Events);

            return (data);
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);

            try
            {
                var changedEvent = (Event)DHXEventsHelper.Bind(typeof(Event), actionValues);
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        if (!Repository.CreateEvents(changedEvent))
                        {
                            action.Type = DataActionTypes.Error;
                        }
                        break;
                    case DataActionTypes.Delete:
                        changedEvent = Repository.Events.SingleOrDefault(ev => ev.id == action.SourceId);
                        if (!Repository.RemoveEvents((int) action.SourceId))
                        {
                            action.Type = DataActionTypes.Error;
                        }
                        break;
                    default:// "update"                          
                        var eventToUpdate = Repository.Events.SingleOrDefault(ev => ev.id == action.SourceId);
                        if (!Repository.UpdateEvents(changedEvent))
                        {
                            action.Type = DataActionTypes.Error;
                        }
                        DHXEventsHelper.Update(eventToUpdate, changedEvent, new List<string>() { "id" });
                        break;
                }
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