using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CIS420Redux.Models
{
    public class Program
    {
        public int Id { get; set; }
        [DisplayName("Program Name:")]
        public string Name { get; set; }
    }
}