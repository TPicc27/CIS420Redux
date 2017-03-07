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
        [DisplayName("Student Number")]
        public int StudentNumber { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [DisplayName("Date of Birth")]
        public string DateOfBirth { get; set; }

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
        public int CampusId { get; set; }
        [DisplayName("Program ID")]
        public int ProgramId { get; set; }

        public virtual Campus Campus { get; set; }

        public virtual Program Program { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

         public bool CPR_Compliant { get; set; }
        public bool HIPPA_Compliant { get; set; }
        public bool Bloodbourne_Compliant { get; set; }
        public bool Liability_Compliant { get; set; }
        public bool Immunization_Compliant { get; set; }
        public bool Drug_Screen_Compliant { get; set; }
        public bool CNA_Compliant { get; set; }
        public bool Is_Compliant { get; set; }
        public int AdvisorID { get; set; }
    }
}