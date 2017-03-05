using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS420Redux.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }

        public string CatlogNumber { get; set; }

        public string Credits { get; set; }

        public string ProgramId { get; set; }

        public string CampusId { get; set; }
    }
}