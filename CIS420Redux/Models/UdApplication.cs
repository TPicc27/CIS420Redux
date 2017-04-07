using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIS420Redux.Models
{
    public class UdApplication
    {
        public int Id { get; set; }
        [DisplayName("Student Number")]
        public string StudentNumber { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Email { get; set; }
        [DisplayName("Street Address")]
        public string Address1 { get; set; }
        [DisplayName("Additional Street Address")]
        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        [DisplayName("Home Phone Number")]
        public string HomePhone { get; set; }
        [DisplayName("Cell Phone Number")]
        public string CellPhone { get; set; }
        [DisplayName("Select a Program")]
        public string SelectProgram { get; set; }
        [DisplayName("What semester are you applying for?")]
        public string Semester { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("What are the Current Courses you are taking?")]
        public string CurrentCourses { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Personal Qualties Essay.  Must be up to 500 words.")]
        public string PersonalQualties { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Nursing or Health Care related experience.")]
        public string HealthCare { get; set; }
        [DisplayName("Have you ever been arrested for, charged with, or convicted of any crime other than a minor traffic violation?")]
        public string Crimes { get; set; }
        [DisplayName("Have you ever been dismissed, suspended, or otherwise disciplined by a college, university, or a graduate/professional school for any reason?")]
        public string SchoolTrouble { get; set; }
        [DisplayName("Have you ever been other than honorably discharged from any armed service of the U.S.?")]
        public string HonorablyDischarge { get; set; }
        [DisplayName("Have you ever been discharged from employment, disciplined, or asked to resign from employment or any service program (VISTA, Peace Corps, etc.) for reasons of dishonesty, malfeasance or any other reason reflecting on your character?")]
        public string DischargedEmployment { get; set; }
        [DisplayName("Have you ever been charged with or found guilty of sexual harassment or racial, ethnic, or gender bias in any proceeding?")]
        public string Harassment { get; set; }
        [DisplayName("Have you ever been subject to disciplinary or legal action related to use of drugs, including alcohol?")]
        public string DrugsOrAlcohol { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("If you answered YES to any question(s) above, please provide an explanation below.")]
        public string DrugsOrAlcoholEssay { get; set; }
        [DisplayName("I hereby declare that the information furnished above is completed and accurate to the best of my knowledge. I also authorize the release of information from any agency or individuals, who may have additional information on me.")]
        public string AccurateKnowledge { get; set; }
        [DisplayName("Accepted/Declined?")]
        public string Status { get; set; }

        public IEnumerable<SelectListItem> StatusList { get; set; }

        public IEnumerable<SelectListItem> StateList { get; set; }
    }
}