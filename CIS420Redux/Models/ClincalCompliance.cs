using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIS420Redux.Models
{
    public class ClincalCompliance
    { public int ID { get; set; }
      
      public string Type { get; set; }
      [DisplayName("Expiration Date")]
      public DateTime ExpirationDate { get; set; }
      [DisplayName("Has Date Expired?")]
      public bool IsExpired { get; set; }
      [DisplayName("Are You Compliant?")]
      public bool IsCompliant { get; set; }
      [DisplayName("Student ID")]
      public int StudentId { get; set; }

      public virtual Student Student { get; set; }

     public IEnumerable<SelectListItem> Types { get; set; }

    }
}