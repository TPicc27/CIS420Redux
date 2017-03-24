using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS420Redux.Models.ViewModels
{
    public class StudentCCIndexViewModel
    {
        public int ID { get; set; }

        public IEnumerable<ClincalCompliance> TypeList { get; set; }

        public string isComplaint { get; set; }

    }
}