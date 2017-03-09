using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS420Redux.Models.ViewModels.Advisor
{
    public class StudentRecordViewModel
    {
        public IEnumerable<Models.Student> StudentRecordsList { get; set; }
        public IEnumerable<Todo> AdvisorTodosList { get; set; }
    }
}