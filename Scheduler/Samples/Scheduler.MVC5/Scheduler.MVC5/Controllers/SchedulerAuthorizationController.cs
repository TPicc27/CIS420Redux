using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Authentication;
using DHTMLX.Scheduler.Data;
using Scheduler.MVC5.Global.Auth;
using Scheduler.MVC5.Model.Models;

namespace Scheduler.MVC5.Controllers
{
    public class SchedulerAuthorizationController : BaseController
    {
        // GET: SchedulerAuthorization
        public ActionResult Index()
        {
            var sched = new DHXScheduler(this) {LoadData = true, EnableDataprocessor = true};
            
            if (Request.IsAuthenticated)
            {
                var user = Repository.Users.SingleOrDefault(u => u.UserName == HttpContext.User.Identity.Name);
                sched.SetUserDetails(user, "Id");//pass dictionary<string, object> or any object which can be serialized to json(without circular references)
                sched.Authentication.EventUserIdKey = "user_id";//set field in event which will be compared to user id(same as sched.Authentication.UserIdKey by default)    
            }
            sched.SetEditMode(EditModes.OwnEventsOnly, EditModes.AuthenticatedOnly);

            sched.InitialDate = new DateTime(2011, 9, 26);
            return View(sched);
        }

        public ContentResult Data()
        {
            var data = new SchedulerAjaxData(Repository.Events);
            return (data);
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {

            var action = new DataAction(actionValues);
            var changedEvent = (Event)DHXEventsHelper.Bind(typeof(Event), actionValues);
            var data = new AppUserIdentityDbContext();
            var custUserIdentity = data.Users.SingleOrDefault(u => u.UserName == HttpContext.User.Identity.Name);
            if (custUserIdentity != null && (Request.IsAuthenticated && changedEvent.user_id == custUserIdentity.Id))
            {
                try
                {
                    switch (action.Type)
                    {
                        case DataActionTypes.Insert:
                            changedEvent.room_id = Repository.Rooms.First().key;
                            Repository.CreateEvents(changedEvent);
                            break;
                        case DataActionTypes.Delete:
                            //changedEvent = Repository.Events.SingleOrDefault(ev => ev.id == action.SourceId);
                            Repository.RemoveEvents((int) action.SourceId);
                            break;
                        default:// "update"                          
                            var eventToUpdate = Repository.Events.SingleOrDefault(ev => ev.id == action.SourceId);
                            Repository.UpdateEvents(eventToUpdate);
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
            }
            else
            {
                action.Type = DataActionTypes.Error;
            }
            return (new AjaxSaveResponse(action));
        }
    }
}