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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DayPilot.Web.Mvc.Data;
using DayPilot.Web.Mvc.Json;

namespace DayPilot.Web.Mvc.Events.Month
{
    public class BeforeEventRenderArgs
    {

        /// <summary>
        /// Event id.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Event text.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Event start.
        /// </summary>
        public DateTime Start { get; private set; }

        /// <summary>
        /// Event end.
        /// </summary>
        public DateTime End { get; private set; }

        /// <summary>
        /// Event ToolTip.
        /// </summary>
        public string ToolTip { get; set; }

        /// <summary>
        /// Event HTML.
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// Custom background color.
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Custom CssClass to be added to the default class.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Custom border color.
        /// </summary>
        public string BorderColor { get; set; }

        /// <summary>
        /// Custom event font color. Ignored in CssOnly mode.
        /// </summary>
        public string FontColor { get; set; }

        /// <summary>
        /// DataSource element containing the source data for this event.
        /// </summary>
        public DataItemWrapper DataItem { get; private set; }

        internal BeforeEventRenderArgs(Event e)
        {
            Id = e.Id;
            Text = e.Text;
            Start = e.Start;
            End = e.End;

            DataItem = new DataItemWrapper(e.Source);
        }
    }
}
