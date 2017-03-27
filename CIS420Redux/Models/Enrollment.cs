using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS420Redux.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public int StudentId { get; set; }

        public int ProgramId { get; set; }

        public string Semester { get; set; }

        public string Grade { get; set; }


        public virtual Course Course { get; set; }

        public virtual Student Student { get; set; }

        public virtual Program Program { get; set; }
    }
}