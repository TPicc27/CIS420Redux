using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS420Redux.Models
{
    public class Advisor
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Title { get; set; }
        public string Room { get; set; }
        public string Availability { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
