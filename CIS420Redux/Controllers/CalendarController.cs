using System.Linq;
using System.Net;
using System.Web.Mvc;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using CIS420Redux.Models;
using EventClickArgs = DayPilot.Web.Mvc.Events.Month.EventClickArgs;
using EventMoveArgs = DayPilot.Web.Mvc.Events.Month.EventMoveArgs;
using InitArgs = DayPilot.Web.Mvc.Events.Month.InitArgs;
using TimeRangeSelectedArgs = DayPilot.Web.Mvc.Events.Month.TimeRangeSelectedArgs;

namespace DeveloperUniversity.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Calendar
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Event");
        }

        public ActionResult Backend()
        {
            return new Dpm().CallBack(this);
        }

        public ActionResult Edit(string id, string titleText)
        {

            var eventToModify = _db.Events.FirstOrDefault(ev => ev.Id.ToString() == id);

            eventToModify.Name = titleText;
            _db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
            //return new Dpm().CallBack(this);
        }
    }


    public class Dpm : DayPilotMonth
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        protected override void OnInit(InitArgs e)
        {
            Events = from ev in _db.Events where !((ev.EndDate <= VisibleStart) || (ev.StartDate >= VisibleEnd)) select ev;

            DataIdField = "Id";
            DataTextField = "Name";
            DataStartField = "StartDate";
            DataEndField = "EndDate";

            Update();
        }

        protected override void OnFinish()
        {
            if (UpdateType == CallBackUpdateType.None)
            {
                return;
            }

            DataIdField = "Id";
            DataStartField = "StartDate";
            DataEndField = "EndDate";
            DataTextField = "Name";


            Events = from e in _db.Events where !((e.EndDate <= VisibleStart) || (e.StartDate >= VisibleEnd)) select e;
        }


        protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
        {
            if (string.IsNullOrEmpty((string)e.Data["name"]))
            {
                return;
            }

            var createdEvent = new Event()
            {
                Name = (string)e.Data["name"],
                StartDate = e.Start,
                EndDate = e.End
            };

            _db.Events.Add(createdEvent);

            _db.SaveChanges();

            Update();
        }

        protected override void OnEventMove(EventMoveArgs e)
        {
            var dbEvent = _db.Events.FirstOrDefault(ev => ev.Id.ToString() == e.Id);

            if (dbEvent != null)
            {

                dbEvent.StartDate = e.NewStart;
                dbEvent.EndDate = e.NewEnd;

                _db.SaveChanges();
            }

            Update();
        }

        protected override void OnEventClick(EventClickArgs e)
        {
            if (string.IsNullOrEmpty(e.Text))
            {
                return;
            }

            var dbEvent = _db.Events.FirstOrDefault(ev => ev.Id.ToString() == e.Id);

            if (dbEvent != null)
            {
                dbEvent.Name = e.Text;
                dbEvent.StartDate = e.Start;
                dbEvent.EndDate = e.End;

                _db.SaveChanges();
            }

            Update();
        }
    }
}