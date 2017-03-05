using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CIS420Redux.Models.ViewModels.Student
{
    public class StudentIndexViewModel
    {
        public IEnumerable<CIS420Redux.Models.Student> StudentsList { get; set; }
        public IEnumerable<Event> TodosList { get; set; }
    }
}