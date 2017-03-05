using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CIS420Redux.Models
{
    public class Student
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string MiddleName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }

        public string Email { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        public decimal GPA { get; set; }

        public string Standing { get; set; }
        [DisplayName("Have You Graduated?")]
        public string HasGraduated { get; set; }
        [DisplayName("Campus ID")]
        public string CampusId { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}