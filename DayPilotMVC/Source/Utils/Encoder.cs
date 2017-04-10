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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace DayPilot.Web.Mvc.Utils
{
    internal class Encoder
    {
          
        internal static string HtmlEncode(string input)
        {
            if (input == null)
            {
                return null;
            }
            // there used to be Server.HtmlEncode call here but it didn't work properly
            // with some > ASCII characters in connection with validateRequest="true"
            // e.g. Ntilde
            string result = input.Replace("&", "&amp;");
            result = result.Replace("<", "&lt;");
            result = result.Replace(">", "&gt;");
            result = result.Replace("\"", "&quot;");
            return result;
        }


        internal static string UrlEncode(string input)
        {
            if (input == null)
            {
                return null;
            }

            string result = input.Replace("%", "%25");
            return result.Replace("&", "%26");
            // there used to be Server.UrlEncode() call here but it didn't work properly with 
            // characters > ASCII 
        }

        internal static string UrlEncode(IList list)
        {
            StringBuilder sb = new StringBuilder();

            bool isFirst = true;
            foreach (object o in list)
            {
                string item;
                if (o is DateTime)
                {
                    DateTime dt = (DateTime)o;
                    item = dt.ToString("s");
                }
                else if (o == null)
                {
                    item = String.Empty;
                }
                else
                {
                    item = o.ToString();
                }

                if (!isFirst)
                {
                    sb.Append("&");
                }

                sb.Append(UrlEncode(item));

                isFirst = false;
            }

            return sb.ToString();
        }
    }

}
