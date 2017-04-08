using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS420Redux.Models.ViewModels
{
    public class StudentCCIndexViewModel
    {
        public int ID { get; set; }

        //public ICollection<ClincalCompliance> TypeList { get; set; }

        public int DocumentId { get; set; }

        public Document Document { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsExpired { get; set; }
        public bool IsComplaint { get; set; }
        public string Type { get; set; }
        public int StudentNumber { get; set; }
       

    }
}