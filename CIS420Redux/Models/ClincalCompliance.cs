using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CIS420Redux.Models
{
    public class ClincalCompliance
    { public int ID { get; set; }
      
      public string Type { get; set; }
        [DisplayName("Expiration Date")]
      public DateTime ExpirationDate { get; set; }

      public string Status { get; set; }

      

      




    }
}