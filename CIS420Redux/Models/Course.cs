using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CIS420Redux.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }
        [DisplayName("Catalog Number")]
        public string CatalogNumber { get; set; }

        public string Credits { get; set; }
        [DisplayName("Program ID")]
        public int ProgramId { get; set; }

        public virtual Program Program { get; set; }
    }
}