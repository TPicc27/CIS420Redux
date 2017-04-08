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

using System.Data;
using System.Web.UI;

namespace DayPilot.Web.Mvc.Data
{
    /// <summary>
    /// A class wrapping the original data source item.
    /// </summary>
    public class DataItemWrapper
    {
        /// Gets the original DataItem object.
        public object Source { get; private set; }

        /// <summary>
        /// Create a new wrapper from an object.
        /// </summary>
        /// <param name="dataItem"></param>
        public DataItemWrapper(object dataItem)
        {
            Source = dataItem;
        }

        /// <summary>
        /// Gets a property value of the original DataItem object.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public object this[string property]
        {
            get
            {
                if (Source is DataRow)
                {
                    DataRow dr = (DataRow) Source;
                    return dr[property];
                }
                return DataBinder.GetPropertyValue(Source, property, null);
            }
        }
    }
}
