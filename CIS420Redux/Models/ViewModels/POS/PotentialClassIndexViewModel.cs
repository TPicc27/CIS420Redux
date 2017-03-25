using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS420Redux.Models.ViewModels.POS
{
    public class PotentialClassIndexViewModel
    {
        public int Id { get; set; }

        public CIS420Redux.Models.POS CourseList { get; set; }

        public CIS420Redux.Models.Student posImage { get; set; }
    }
}