using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using DHTMLX.Scheduler.Controls;
using DHTMLX.Common;
using SchedulerTest.Models;

namespace SchedulerTest.Controllers
{

    /// <summary>
    /// check available skins and localizations
    /// </summary>
    public class SkinLocaleController : Controller
    {
        //
        // GET: /SkinLocale/
        public class LocaleData
        {
            public DHXScheduler scheduler;
            public string locale;
            public string skin;
            public LocaleData(DHXScheduler sched, string loc, string sk)
            {
                scheduler = sched;
                locale = loc;
                skin = sk;
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


            var rooms = new DHXSchedulerDataContext().Rooms.ToList();

            var unit = new UnitsView("unit1", "room_id");
            unit.AddOptions(rooms);//parse model objects
            scheduler.Views.Add(unit);

            var timeline = new TimelineView("timeline", "room_id");
            timeline.RenderMode = TimelineView.RenderModes.Bar;
            timeline.FitEvents = false;
            timeline.AddOptions(rooms);
            scheduler.Views.Add(timeline);


            scheduler.EnableDataprocessor = true;
            scheduler.LoadData = true;
            scheduler.InitialDate = new DateTime(2011, 9, 19);
            return View(new LocaleData(scheduler, request_lang, request_skin));
        }
        public ContentResult Data()
        {

            var data = new SchedulerAjaxData((new DHXSchedulerDataContext()).Events);
    

            return data;
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {

            var action = new DataAction(actionValues);

            DHXSchedulerDataContext data = new DHXSchedulerDataContext();
            var changedEvent = DHXEventsHelper.Bind<Event>(actionValues);
            try
            {
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
