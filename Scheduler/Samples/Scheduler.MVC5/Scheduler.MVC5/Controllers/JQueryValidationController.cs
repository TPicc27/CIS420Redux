using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using Scheduler.MVC5.Model.Models;

namespace Scheduler.MVC5.Controllers
{
    public class JQueryValidationController : BaseController
    {
        // GET: JQueryValidation
        public ActionResult Index()
        {
            var sched = new DHXScheduler(this);

            sched.LoadData = true;
            sched.EnableDataprocessor = true;

            sched.Lightbox.SetExternalLightbox("JQueryValidation/LightboxControl", 400, 140);

            sched.InitialDate = new DateTime(2011, 9, 5);

            //try in new skin
            //sched.Skin = DHXScheduler.Skins.Terrace;

            return View(sched);
        }



        public ActionResult LightboxControl(ValidEvent ev)
        {
    
            var current = Repository.ValidEvents.SingleOrDefault(e => e.id == ev.id) ?? ev;
            return View(current);
        }

        public ContentResult Data()
        {
            return (new SchedulerAjaxData(Repository.ValidEvents));
        }

        public ActionResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            var changedEvent = (ValidEvent)DHXEventsHelper.Bind(typeof(ValidEvent), actionValues);
            if (action.Type != DataActionTypes.Error)
            {
                //handle changes done without lightbox - dnd, resize..
                return NativeSave(action, changedEvent, actionValues);
            }
            else
            {
                return CustomFormSave(action, changedEvent, actionValues);
            }
        }

        public ContentResult CustomFormSave(DataAction action, ValidEvent changedEvent, FormCollection actionValues)
        {
            if (actionValues["actionType"] != null)
            {
                var actionType = actionValues["actionType"].ToLower();
                
                try
                {
                    if (actionType == "save")
                    {

                        if (Repository.ValidEvents.SingleOrDefault(ev => ev.id == action.SourceId) != null)
                        {
                            //update event
                            var eventToUpdate = Repository.ValidEvents.SingleOrDefault(ev => ev.id == action.SourceId);
                            Repository.UpdateValidEvent(changedEvent);
                            DHXEventsHelper.Update(eventToUpdate, changedEvent, new List<string>() { "id" });

                            action.Type = DataActionTypes.Update;
                        }
                        else
                        {
                            //create event                           
                            Repository.CreateValidEvent(changedEvent);
                            action.Type = DataActionTypes.Insert;
                        }
                    }
                    else if (actionType == "delete")
                    {

                        changedEvent = Repository.ValidEvents.SingleOrDefault(ev => ev.id == action.SourceId);
                        Repository.RemoveValidEvent((int)action.SourceId);

                        action.Type = DataActionTypes.Delete;
                    }
                    //data.SubmitChanges();
                }

                catch
                {
                    action.Type = DataActionTypes.Error;
                }
            }



            return (new SchedulerFormResponseScript(action, changedEvent));
        }

        public ContentResult NativeSave(DataAction action, ValidEvent changedEvent, FormCollection actionValues)
        {
            try
            {
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        Repository.CreateValidEvent(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        changedEvent = Repository.ValidEvents.SingleOrDefault(ev => ev.id == action.SourceId);
                        Repository.RemoveValidEvent((int)action.SourceId);
                        break;
                    default:// "update"                          
                        var eventToUpdate = Repository.ValidEvents.SingleOrDefault(ev => ev.id == action.SourceId);
                        Repository.UpdateValidEvent(changedEvent);
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