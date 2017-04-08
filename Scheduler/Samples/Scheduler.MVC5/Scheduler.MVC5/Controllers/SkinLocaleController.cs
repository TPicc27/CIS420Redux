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
    public class SkinLocaleController : BaseController
    {
        // GET: SkinLocale
        public class LocaleData
        {
            public DHXScheduler Scheduler;
            public string Locale;
            public string Skin;
            public LocaleData(DHXScheduler sched, string loc, string sk)
            {
                Scheduler = sched;
                Locale = loc;
                Skin = sk;
            }
        }
        public ActionResult Index()
        {
            var locale = SchedulerLocalization.Localizations.English;
            var skin = DHXScheduler.Skins.Standart;
            var request_lang = this.Request.QueryString["language"];
            var request_skin = this.Request.QueryString["skin"];

            if (!string.IsNullOrEmpty(request_lang))
            {
                locale = (SchedulerLocalization.Localizations)Enum.Parse(typeof(SchedulerLocalization.Localizations), request_lang);
            }
            if (!string.IsNullOrEmpty(request_skin))
            {
                skin = (DHXScheduler.Skins)Enum.Parse(typeof(DHXScheduler.Skins), request_skin);
            }


            var scheduler = new DHXScheduler(this);
            scheduler.Skin = skin;
            scheduler.Localization.Set(locale);


            scheduler.InitialDate = new DateTime(2011, 11, 24);

            scheduler.XY.scroll_width = 0;
            scheduler.Config.first_hour = 8;
            scheduler.Config.last_hour = 19;
            scheduler.Config.time_step = 30;
            scheduler.Config.multi_day = true;
            scheduler.Config.limit_time_select = true;

            scheduler.Localization.Directory = "locale";


            var rooms = Repository.Rooms.ToList();

            var unit = new UnitsView("unit1", "room_id");
          
            unit.AddOptions(rooms);
            scheduler.Views.Add(unit);

            var timeline = new TimelineView("timeline", "room_id");
            timeline.RenderMode = TimelineView.RenderModes.Bar;
            timeline.FitEvents = false;
          
            timeline.AddOptions(rooms);
            scheduler.Views.Add(timeline);


            scheduler.EnableDataprocessor = true;
            scheduler.LoadData = true;
            scheduler.InitialDate = new DateTime(2011, 9, 19);
            return View(new LocaleData(scheduler, locale.ToString(), skin.ToString()));

        }
        public ContentResult Data()
        {

            var data = new SchedulerAjaxData(Repository.Events);
            return data;
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
               // data.SubmitChanges();
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