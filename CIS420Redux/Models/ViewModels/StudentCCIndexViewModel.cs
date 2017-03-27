using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS420Redux.Models.ViewModels
{
    public class StudentCCIndexViewModel
    {
        public int ID { get; set; }

        public ICollection<ClincalCompliance> TypeList { get; set; }

        public int DocumentId { get; set; }

        public Document Documents { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string IsComplaint { get; set; }

    }
}