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
    public class ServerSideFilteringController : BaseController
    {
        // GET: ServerSideFiltering
        public class SchedulerFilterModel
        {
            public DHXScheduler Scheduler { get; set; }
            public IEnumerable<Room> Rooms { get; set; }

            public SchedulerFilterModel(DHXScheduler sched, IEnumerable<Room> rooms)
            {
                Scheduler = sched;
                Rooms = rooms;

            }
        }

        public ActionResult Index(FormCollection data)
        {

            var sched = new DHXScheduler(this);

            sched.Extensions.Add(SchedulerExtensions.Extension.Cookie);

            var rooms = Repository.Rooms.ToList();

            int selectedRoom;
            if (Request.QueryString["filter"] != null)
            {
                // parameters will be added to data url
                sched.Data.Loader.AddParameters(Request.QueryString);
                selectedRoom = int.Parse(Request.QueryString["rooms"]);
            }
            else
            {
                selectedRoom = rooms.First().key;
            }


            var unit = new UnitsView("rooms", "room_id") {Label = "Rooms"};
        
            unit.AddOptions(rooms);
            sched.Views.Add(unit);


            sched.Lightbox.AddDefaults();
            var select = new LightboxSelect("room_id", "Room");
            select.AddOptions(rooms);
            sched.Lightbox.Add(select);


            sched.LoadData = true;
            sched.EnableDataprocessor = true;
            sched.InitialView = unit.Name;
            ViewData["rooms"] = selectedRoom;
            sched.InitialDate = new DateTime(2011, 9, 7);
            return View(new SchedulerFilterModel(sched, rooms));

        }
        public ContentResult Data()
        {
            IEnumerable<Event> dataset;

            if (Request.QueryString["rooms"] == null)
                dataset = Repository.Events;
            else
            {
                var currentRoom = int.Parse(Request.QueryString["rooms"]);
                dataset = Repository.Events.Where(ev => ev.room_id == currentRoom);
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