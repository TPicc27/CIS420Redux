using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS420Redux.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<CIS420Redux.Models.Student> StudentsList { get; set; }
        public IEnumerable<Event> TodosList { get; set; }
    }
}