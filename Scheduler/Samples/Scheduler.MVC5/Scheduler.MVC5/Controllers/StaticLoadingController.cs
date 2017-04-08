using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Controls;
using Scheduler.MVC5.Model.Models;

namespace Scheduler.MVC5.Controllers
{
    public class StaticLoadingController : BaseController
    {
        // GET: StaticLoading
        public ActionResult Index()
        {

            var sched = new DHXScheduler(this);

            var unit = new UnitsView("unit1", "room_id");


            sched.Views.Add(unit);
            var rooms = Repository.Rooms.ToList();

            unit.AddOptions(rooms);

            sched.Data.Parse(Repository.Events);
            sched.EnableDataprocessor = true;
            sched.InitialDate = new DateTime(2011, 9, 5);

            return View(sched);
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {

            var action = new DataAction(actionValues);

            var changedEvent = (Event)DHXEventsHelper.Bind(typeof(Event), actionValues);
            try
            {
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
                        if (!Repository.RemoveEvents((int)action.SourceId))
                        {
                            action.Type = DataActionTypes.Error;
                        }
                        break;
                    default:// "update"                          
                        var eventToUpdate = Repository.Events.SingleOrDefault(ev => ev.id == action.SourceId);
                        if (Repository.UpdateEvents(changedEvent))
                        {
                            action.Type = DataActionTypes.Error;
                        }
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