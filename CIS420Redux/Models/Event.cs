using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CIS420Redux.Models
{
    public class Event
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
        [DisplayName("Is Recruitment?")]
        public bool IsRecruitment { get; set; }
        [DisplayName("Student ID")]
        public int StudentId { get; set; }

        public virtual Student Students { get; set; }
    }
}