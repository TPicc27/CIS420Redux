using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using Scheduler.MVC5.Model.Models;

namespace Scheduler.MVC5.Controllers
{
    public class RecurringEventsController : BaseController
    {
        // GET: RecurringEvents
        public ActionResult Index()
        {
            var scheduler = new DHXScheduler(this) {InitialDate = new DateTime(2011, 10, 1)};

            scheduler.Extensions.Add(SchedulerExtensions.Extension.Recurring);
            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;


            return View(scheduler);
        }
        public ActionResult Data()
        {
            //recurring events have 3 additional mandatory properties
            // string rec_type
            // Nullable<long> event_length
            // Nullable<int> event_pid
            return new SchedulerAjaxData(Repository.Recurrings);
        }




        public ActionResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);

            try
            {
                var changedEvent = (Recurring)DHXEventsHelper.Bind(typeof(Recurring), actionValues);
                //operations with recurring events require some additional handling
                bool isFinished = deleteRelated(action, changedEvent);
                if (!isFinished)
                {
                    switch (action.Type)
                    {

                        case DataActionTypes.Insert:
                            Repository.CreateRecurring(changedEvent);
                            if (changedEvent.rec_type == "none")//delete one event from the serie
                                action.Type = DataActionTypes.Delete;
                            break;
                        case DataActionTypes.Delete:
                            changedEvent = Repository.Recurrings.SingleOrDefault(ev => ev.id == action.SourceId);
                            Repository.RemoveRecurring((int)action.SourceId);
                            break;
                        default:// "update"   
                            var eventToUpdate = Repository.Recurrings.SingleOrDefault(ev => ev.id == action.SourceId);
                            DHXEventsHelper.Update(eventToUpdate, changedEvent, new List<string>() { "id" });
                            break;
                    }
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
        protected bool deleteRelated(DataAction action, Recurring changedEvent)
        {
            bool finished = false;
            if ((action.Type == DataActionTypes.Delete || action.Type == DataActionTypes.Update) && !string.IsNullOrEmpty(changedEvent.rec_type))
            {
                Repository.RemoveRecurringCondition(Repository.Recurrings.Where(ev => ev.event_pid == changedEvent.id));
                //Repository.Recurrings.DeleteAllOnSubmit(from ev in context.Recurrings where ev.event_pid == changedEvent.id select ev);
            }
            if (action.Type == DataActionTypes.Delete && (changedEvent.event_pid != 0 && changedEvent.event_pid != null))
            {
                Recurring changed = Repository.Recurrings.First(ev => ev.id == action.TargetId);
                    //(from ev in context.Recurrings where ev.id == action.TargetId select ev).Single();
                changed.rec_type = "none";
                finished = true;
            }
            return finished;
        }
    }
}