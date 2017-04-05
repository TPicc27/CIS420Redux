using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace CIS420Redux.Models.ViewModels.Student
{
    public class StudentIndexViewModel
    {
        

        public int Id { get; set; }
        [Display(Name ="Student Number")]
        public int StudentNumber { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }
        public int CampusId { get; set; }
        public int ProgramId { get; set; }
        public IEnumerable<Event> AlertList { get; set; }

        public virtual Program Program { get; set; }

        public virtual Campus Campus { get; set; }
    }
}