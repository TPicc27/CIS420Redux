//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace CIS420Redux.Controllers
//{
//    public class DayPilotController : Controller
//    {
//        // GET: DayPilot
//        public ActionResult Index()
//        {
//            return View();
//        }
//        public ActionResult Backend()
//        {
//            return new Dpm().CallBack(this);
//        }
//    }
//    class Dpm : DayPilotMonth
//    {
//        protected override void OnInit(InitArgs e)
//        {
//            var db = new DataClasses1DataContext();
//            Events = from ev in db.events select ev;

//            DataIdField = "_id";
//            DataTextField = "_text";
//            DataStartField = "_eventstart";
//            DataEndField = "_eventend";

//            Update();
//        }

//        protected override void OnEventMove(EventMoveArgs e)
//        {
//            var toBeResized = (from ev in db.Events where ev.id == Convert.ToInt32(e.Id) select ev).First();
//            toBeResized.eventstart = e.NewStart;
//            toBeResized.eventend = e.NewEnd;
//            db.SubmitChanges();
//            Update();
//        }

//        protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
//        {
//            var toBeCreated = new Event { eventstart = e.Start, eventend = e.End, text = (string)e.Data["name"] };
//            db.Events.InsertOnSubmit(toBeCreated);
//            db.SubmitChanges();
//            Update();
//        }
//}
