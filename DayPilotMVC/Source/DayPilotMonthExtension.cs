/*
Copyright © 2005 - 2017 Annpoint, s.r.o.

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
using System.Threading;
using System.Web.Mvc;
using DayPilot.Web.Mvc.Enums.Calendar;
using DayPilot.Web.Mvc.Init;
using DayPilot.Web.Mvc.Utils;

namespace DayPilot.Web.Mvc
{
    public static partial class DayPilotExtensions
    {
        public static MvcHtmlString DayPilotMonth(this HtmlHelper helper, string id, DayPilotMonthConfig cfg)
        {
            if (id == null)
            {
                throw new ArgumentException("The 'id' argument is required.");
            }

            if (cfg == null)
            {
                cfg = new DayPilotMonthConfig();
            }

            if (String.IsNullOrEmpty(cfg.BackendUrl))
            {
                throw new ArgumentException("BackendUrl property must be specified.");
            }


            var builder = new InitBuilder();
            builder.Open("DayPilot.Month", id);

            builder.AppendProp("api", 1);
            builder.AppendProp("backendUrl", cfg.BackendUrl);
            builder.AppendProp("ajaxError", "function(req) { if (DayPilot.Modal) { new DayPilot.Modal().showHtml(req.responseText); } else { alert('AJAX callback error (500)'); } }", false);
            builder.AppendProp("backColor", cfg.BackColor);
            builder.AppendProp("borderColor", cfg.BorderColor);
            builder.AppendProp("cellHeaderBackColor", cfg.CellHeaderBackColor);
            builder.AppendProp("cellHeaderFontColor", cfg.CellHeaderFontColor);
            builder.AppendProp("cellHeaderFontFamily", cfg.CellHeaderFontFamily);
            builder.AppendProp("cellHeaderFontSize", cfg.CellHeaderFontSize);
            builder.AppendProp("cellHeight", cfg.CellHeight);
            builder.AppendProp("cellHeaderHeight", cfg.CellHeaderHeight);
            builder.AppendProp("cssOnly", cfg.CssOnly);
            //builder.AppendSerialized("dayNames", Week.GetDayNames());
            builder.AppendProp("eventBackColor", cfg.EventBackColor);
            builder.AppendProp("eventBorderColor", cfg.EventBorderColor);
            builder.AppendProp("eventFontColor", cfg.EventFontColor);
            builder.AppendProp("eventFontFamily", cfg.EventFontFamily);
            builder.AppendProp("eventFontSize", cfg.EventFontSize);
            builder.AppendProp("eventHeight", cfg.EventHeight);
            builder.AppendProp("innerBorderColor", cfg.InnerBorderColor);
            builder.AppendProp("headerBackColor", cfg.HeaderBackColor);
            builder.AppendProp("headerFontColor", cfg.HeaderFontColor);
            builder.AppendProp("headerFontSize", cfg.HeaderFontSize);
            builder.AppendProp("headerHeight", cfg.HeaderHeight);
            builder.AppendProp("hideUntilInit", cfg.HideUntilInit);
            builder.AppendProp("locale", Thread.CurrentThread.CurrentCulture.Name.ToLower());
            builder.AppendProp("nonBusinessBackColor", cfg.NonBusinessBackColor);
            //builder.AppendSerialized("monthNames", Year.GetMonthNames());
            builder.AppendProp("showToolTip", cfg.ShowToolTip);
            builder.AppendProp("timeFormat", cfg.TimeFormat);
            builder.AppendProp("weekStarts", cfg.WeekStartInt);
            builder.AppendProp("width", cfg.Width);
            builder.AppendProp("startDate", cfg.StartDate.ToString("s"));
            builder.AppendProp("theme", cfg.Theme);

            // event handling types
            builder.AppendProp("eventClickHandling", cfg.EventClickHandling);
            builder.AppendProp("eventDoubleClickHandling", cfg.EventClickHandling);
            builder.AppendProp("eventMoveHandling", cfg.EventMoveHandling);
            builder.AppendProp("eventResizeHandling", cfg.EventResizeHandling);
            builder.AppendProp("timeRangeSelectedHandling", cfg.TimeRangeSelectedHandling);

            // JavaScript event handlers
            builder.AppendProp("onEventClick", String.Format("function(e) {{ {0}; }}", cfg.EventClickJavaScript), false);
            builder.AppendProp("onEventDoubleClick", String.Format("function(e) {{ {0}; }}", cfg.EventDoubleClickJavaScript), false);
            builder.AppendProp("onEventSelect", String.Format("function(e, change) {{ {0}; }}", cfg.EventSelectJavaScript), false);
            builder.AppendProp("onEventMove", String.Format("function(e, newStart, newEnd, ctrl, shift) {{ {0}; }}", cfg.EventMoveJavaScript), false);
            builder.AppendProp("onEventResize", String.Format("function(e, newStart, newEnd) {{ {0}; }}", cfg.EventResizeJavaScript), false);
            builder.AppendProp("onEventRightClick", String.Format("function(e) {{ {0}; }}", cfg.EventRightClickJavaScript), false);
            builder.AppendProp("onHeaderClick", String.Format("function(e) {{ var day = e.day; {0}; }}", cfg.HeaderClickJavaScript), false);
            builder.AppendProp("onTimeRangeDoubleClick", String.Format("function(start, end) {{ {0}; }}", cfg.TimeRangeDoubleClickJavaScript), false);
            builder.AppendProp("onTimeRangeSelected", String.Format("function(start, end) {{ {0}; }}", cfg.TimeRangeSelectedJavaScript), false);

            builder.InjectString(Locale.RegistrationString(Thread.CurrentThread.CurrentCulture.Name.ToLower()));

            builder.Close();
            return MvcHtmlString.Create(builder.ToString());

        }
    }
}
