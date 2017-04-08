﻿/*
Copyright © 2005 - 2014 Annpoint, s.r.o.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

-------------------------------------------------------------------------

NOTE: Reuse requires the following acknowledgement (see also NOTICE):
This product includes DayPilot (http://www.daypilot.org) developed by Annpoint, s.r.o.
*/

using System;
using System.Data;
using System.Web.Mvc;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Data;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Month;
using DayPilot.Web.Mvc.Json;

namespace MvcApplication1.Controllers
{
    [HandleError]
    public class MonthController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Light()
        {
            return RedirectToAction("ThemeTransparent");

        }
        public ActionResult Red()
        {
            return RedirectToAction("Index");

        }
        public ActionResult Green()
        {
            return RedirectToAction("ThemeGreen");
        }

        public ActionResult ThemeGreen()
        {
            return View();
        }

        public ActionResult ThemeWhite()
        {
            return View();
        }

        public ActionResult ThemeTransparent()
        {
            return View();
        }

        public ActionResult ThemeBlue()
        {
            return View();
        }

        public ActionResult ThemeGoogleLike()
        {
            return View();
        }

        public ActionResult ThemeTraditional()
        {
            return View();
        }

        public ActionResult NextPrevious()
        {
            return View();
        }
        public ActionResult JQuery()
        {
            return View();
        }
        public ActionResult EventCreating()
        {
            return View();
        }
        public ActionResult EventMoving()
        {
            return View();
        }
        public ActionResult EventResizing()
        {
            return View();
        }
        public ActionResult DropDown()
        {
            return View();
        }

        public ActionResult EventActiveAreas()
        {
            return View();
        }

        public ActionResult EventContextMenu()
        {
            return View();
        }

        public ActionResult EventSelecting()
        {
            return View();
        }

        public ActionResult EventBubble()
        {
            return View();
        }

        public ActionResult EventDoubleClick()
        {
            return View();
        }

        public ActionResult EventStartEndTime()
        {
            return View();
        }

        public ActionResult EventMoveToPosition()
        {
            return View();
        }

        public ActionResult RecurringEvents()
        {
            return View();
        }

        public ActionResult Weeks()
        {
            return View();
        }

        public ActionResult Today()
        {
            return View();
        }

        public ActionResult Weekends()
        {
            return View();
        }

        public ActionResult EventCssContinue()
        {
            return View();
        }

        public ActionResult AutoRefresh()
        {
            return View();
        }

        public ActionResult NotifyEventModel()
        {
            return View();
        }

        public ActionResult Message()
        {
            return View();
        }

        public ActionResult Height100Pct()
        {
            return View();
        }

        public ActionResult EventCustomization()
        {
            return View();
        }



        public ActionResult Backend()
        {
            return new Dpm().CallBack(this);
        }

        public class Dpm : DayPilotMonth
        {
            protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
            {
                string name = (string)e.Data["name"];
                if (String.IsNullOrEmpty(name))
                {
                    name = "(default)";
                }
                new EventManager(Controller).EventCreate(e.Start, e.End, name);
                Update();
            }

            protected override void OnEventMove(EventMoveArgs e)
            {
                if (new EventManager(Controller).Get(e.Id) != null)
                {
                    new EventManager(Controller).EventMove(e.Id, e.NewStart, e.NewEnd);
                }

                Update();
            }

            protected override void OnEventResize(EventResizeArgs e)
            {
                new EventManager(Controller).EventMove(e.Id, e.NewStart, e.NewEnd);
                Update();
            }

            private int i = 0;
            protected override void OnBeforeEventRender(BeforeEventRenderArgs e)
            {
                if (Id == "dp_customization")
                {
                    // alternating color
                    int colorIndex = i % 4;
                    string[] backColors = { "#FFE599", "#9FC5E8", "#B6D7A8", "#EA9999" };
                    string[] borderColors = { "#F1C232", "#3D85C6", "#6AA84F", "#CC0000" };
                    e.BackgroundColor = backColors[colorIndex];
                    e.BorderColor = borderColors[colorIndex];
                    e.FontColor = "#000";
                    i++;
                }
            }

            protected override void OnCommand(CommandArgs e)
            {
                switch (e.Command)
                {
                    case "navigate":
                        StartDate = (DateTime) e.Data["start"];
                        Update(CallBackUpdateType.Full);
                        break;

                    case "previous":
                        StartDate = StartDate.AddMonths(-1);
                        Update(CallBackUpdateType.Full);
                        break;

                    case "next":
                        StartDate = StartDate.AddMonths(1);
                        Update(CallBackUpdateType.Full);
                        break;

                    case "today":
                        StartDate = DateTime.Today;
                        Update(CallBackUpdateType.Full);
                        break;

                    case "refresh":
                        Update();
                        break;
                }
            }

            protected override void OnInit(InitArgs initArgs)
            {
                Update(CallBackUpdateType.Full);
            }

            protected override void OnFinish()
            {
                // only load the data if an update was requested by an Update() call
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                // this select is a really bad example, no where clause
                Events = new EventManager(Controller).Data.AsEnumerable();

                DataStartField = "start";
                DataEndField = "end";
                DataTextField = "text";
                DataIdField = "id";
            }

        }

    }
}
