using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Controls;
using DHTMLX.Scheduler.Data;
using Scheduler.MVC5.Model.Models;

namespace Scheduler.MVC5.Controllers
{
    public class ServerSideFilteringAjaxController : BaseController
    {
        // GET: ServerSideFilteringAjax
        public class SchedulerFilterModel
        {
            public DHXScheduler Scheduler { get; set; }
            public IEnumerable<Room> Rooms { get; set; }
            public SchedulerFilterModel(DHXScheduler sched, IEnumerable<Room> rooms)
            {
                this.Scheduler = sched;
                this.Rooms = rooms;
            }
        }
        public ActionResult Index(FormCollection data)
        {

            var sched = new DHXScheduler(this);

            var rooms = Repository.Rooms.ToList();

            var unit = new UnitsView("rooms", "room_id") {Label = "Rooms"};
            unit.AddOptions(rooms);
    
           
            sched.Views.Add(unit);

            sched.InitialView = unit.Name;
            sched.InitialDate = new DateTime(2011, 9, 7);
            sched.LoadData = true;
            sched.EnableDataprocessor = true;

            return View(new SchedulerFilterModel(sched, rooms));

        }

        public ContentResult Data()
        {
            IEnumerable<Event> dataset;

            if (this.Request.QueryString["rooms"] == null)
                dataset = Repository.Events.ToList();
            else
            {
                var currentRoom = int.Parse(this.Request.QueryString["rooms"]);
                dataset = Repository.Events.Where(ev => ev.room_id == currentRoom).ToList();
                //from ev in dc.Events where ev.room_id == current_room select ev;
            }

            var data = new SchedulerAjaxData(dataset);


            return (data);
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