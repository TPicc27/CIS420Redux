using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS420Redux.Models.ViewModels.Advisor
{
    public class AdvisorIndexViewModel
    {
        public IEnumerable<CIS420Redux.Models.Student> NCStudentsList { get; set; }
        public IEnumerable<Event> AlertList { get; set; }
        public IEnumerable<Todo> AdvisorTodosList { get; set; }

    }
}