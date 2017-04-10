using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DayPilot.Web.Mvc.Enums.Calendar;
using DayPilot.Web.Mvc.Init;
using DayPilot.Web.Mvc.Utils;

namespace DayPilot.Web.Mvc
{
    public static partial class DayPilotExtensions
    {
        public static MvcHtmlString DayPilotCalendar(this HtmlHelper helper, string id, DayPilotCalendarConfig cfg)
        {
            if (id == null)
            {
                throw new ArgumentException("The 'id' argument is required.");
            }

            if (cfg == null)
            {
                cfg = new DayPilotCalendarConfig();
            }

            string scrollPos = "null";
            if (cfg.HeightSpec != HeightSpec.Full)
            {
                scrollPos = Convert.ToString(cfg.CellHeight * cfg.ScrollPositionHour * 2 + 1);
            }

            var builder = new InitBuilder();
            builder.Open("DayPilot.Calendar", id);

            builder.AppendProp("api", 1);
            builder.AppendProp("borderColor", cfg.BorderColor);
            builder.AppendProp("businessBeginsHour", cfg.BusinessBeginsHour);
            builder.AppendProp("businessEndsHour", cfg.BusinessEndsHour);
            builder.AppendProp("cellBackColor", cfg.CellBackColor);
            builder.AppendProp("cellBorderColor", cfg.CellBorderColor);
            builder.AppendProp("cellHeight", cfg.CellHeight);
            builder.AppendProp("columnMarginRight", cfg.ColumnMarginRight);
            builder.AppendProp("cornerBackColor", cfg.CornerBackColor);
            builder.AppendProp("cssOnly", cfg.CssOnly);
            builder.AppendProp("durationBarVisible", cfg.DurationBarVisible);
            builder.AppendProp("theme", cfg.Theme);
            builder.AppendProp("days", cfg.Days);
            builder.AppendProp("eventBackColor", cfg.EventBackColor);
            builder.AppendProp("eventBorderColor", cfg.EventBorderColor);
            builder.AppendProp("eventFontFamily", cfg.EventFontFamily);
            builder.AppendProp("eventFontSize", cfg.EventFontSize);
            builder.AppendProp("eventFontColor", cfg.EventFontColor);
            builder.AppendProp("eventHeaderFontSize", cfg.EventHeaderFontSize);
            builder.AppendProp("eventHeaderFontColor", cfg.EventHeaderFontColor);
            builder.AppendProp("eventHeaderHeight", cfg.EventHeaderHeight);
            builder.AppendProp("eventHeaderVisible", cfg.EventHeaderVisible);
            builder.AppendProp("headerFontSize", cfg.HeaderFontSize);
            builder.AppendProp("headerFontFamily", cfg.HeaderFontFamily);
            builder.AppendProp("headerFontColor", cfg.HeaderFontColor);
            builder.AppendProp("headerHeight", cfg.HeaderHeight);
            builder.AppendProp("heightSpec", cfg.HeightSpec);
            builder.AppendProp("hourHalfBorderColor", cfg.HourHalfBorderColor);
            builder.AppendProp("hourBorderColor", cfg.HourBorderColor);
            builder.AppendProp("hourFontColor", cfg.HourFontColor);
            builder.AppendProp("hourFontFamily", cfg.HourFontFamily);
            builder.AppendProp("hourNameBackColor", cfg.HourNameBackColor);
            builder.AppendProp("hourNameBorderColor", cfg.HourNameBorderColor);
            builder.AppendProp("hourWidth", cfg.HourWidth);
            builder.AppendProp("initScrollPos", scrollPos);
            builder.AppendProp("loadingLabelText", cfg.LoadingLabelText);
            builder.AppendProp("loadingLabelVisible", cfg.LoadingLabelVisible);
            builder.AppendProp("loadingLabelFontSize", cfg.LoadingLabelFontSize);
            builder.AppendProp("loadingLabelFontFamily", cfg.LoadingLabelFontFamily);
            builder.AppendProp("loadingLabelFontColor", cfg.LoadingLabelFontColor);
            builder.AppendProp("loadingLabelBackColor", cfg.LoadingLabelBackColor);
            builder.AppendProp("locale", Thread.CurrentThread.CurrentCulture.Name.ToLower());

            builder.AppendProp("showToolTip", cfg.ShowToolTip);

            builder.AppendProp("startDate", cfg.StartDate.ToString("s"));
            builder.AppendProp("timeFormat", Hour.DetectTimeFormat(cfg.TimeFormat));
            builder.AppendProp("viewType", cfg.ViewType);
            builder.AppendProp("width", cfg.Width);

            builder.AppendProp("backendUrl", cfg.BackendUrl);
            builder.AppendProp("ajaxError", "function(req) { if (DayPilot.Modal) { new DayPilot.Modal().showHtml(req.responseText); } else { alert('AJAX callback error (500)'); } }", false);

            // event handling types
            builder.AppendProp("eventClickHandling", cfg.EventClickHandling);
            builder.AppendProp("eventMoveHandling", cfg.EventMoveHandling);
            builder.AppendProp("eventResizeHandling", cfg.EventResizeHandling);
            builder.AppendProp("timeRangeSelectedHandling", cfg.TimeRangeSelectedHandling);

            // JavaScript event handlers
            builder.AppendProp("onEventClick", String.Format("function(e) {{ {0}; }}", cfg.EventClickJavaScript), false);
            builder.AppendProp("onEventMove", String.Format("function(e, newStart, newEnd) {{ var newColumn = newResource; {0}; }}", cfg.EventMoveJavaScript), false);
            builder.AppendProp("onEventResize", String.Format("function(e, newStart, newEnd) {{ {0}; }}", cfg.EventResizeJavaScript), false);
            builder.AppendProp("onTimeRangeSelected", String.Format("function(start, end, resource) {{ {0}; }}", cfg.TimeRangeSelectedJavaScript), false);

            builder.AppendSerialized("columns", cfg.GetColumns());

            builder.InjectString(Locale.RegistrationString(Thread.CurrentThread.CurrentCulture.Name.ToLower()));

            builder.Close();
            return MvcHtmlString.Create(builder.ToString());
        }

    }
}
