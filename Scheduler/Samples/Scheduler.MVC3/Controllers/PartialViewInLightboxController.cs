﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SchedulerTest.Models;
using System.Web.Security;

using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Controls;
using DHTMLX.Scheduler.Data;
using DHTMLX.Common;

namespace SchedulerTest.Controllers
{
    /// <summary>
    /// Use custom form instead of native details form.
    /// 
    /// </summary>
    public class PartialViewInLightboxController : Controller
    {

        public ActionResult Index()
        {

            var sched = new DHXScheduler(this);
   
            
            sched.LoadData = true;
            sched.EnableDataprocessor = true;
            sched.Views.Add(new AgendaView());

            //Need to implement setValue/getValue interface, and form will be fully integrated into the scheduler
            var box = sched.Lightbox.SetExternalLightboxForm("PartialViewInLightbox/Form", 500, 150);    
            box.ClassName = "custom_lightbox";
            sched.InitialDate = new DateTime(2011, 9, 5);



            return View(sched);
        }

        public ActionResult Form()
        {
            return View();
        }
        public ContentResult Data()
        {
            var data = new SchedulerAjaxData((new DHXSchedulerDataContext()).Events);
           
            return (data);
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {

            var action = new DataAction(actionValues);

            DHXSchedulerDataContext data = new DHXSchedulerDataContext();
            try
            {
                var changedEvent = DHXEventsHelper.Bind<Event>(actionValues);
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        data.Events.InsertOnSubmit(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        changedEvent = data.Events.SingleOrDefault(ev => ev.id == action.SourceId);
                        data.Events.DeleteOnSubmit(changedEvent);
                        break;
                    default:// "update"                          
                        var eventToUpdate = data.Events.SingleOrDefault(ev => ev.id == action.SourceId);
                        DHXEventsHelper.Update(eventToUpdate, changedEvent, new List<string>() { "id" });
                        break;
                }
                data.SubmitChanges();
                action.TargetId = changedEvent.id;
            }
            catch (Exception a)
            {
                action.Type = DataActionTypes.Error;
            }

            return (new AjaxSaveResponse(action));
        }
    }
}
