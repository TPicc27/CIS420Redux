using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CIS420Redux.Models
{
    public class CalendarContext : DbContext
    {
        public CalendarContext() : base()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CalendarContext>());
        }
        public System.Data.Entity.DbSet<CIS420Redux.Models.Calendar> Appointments { get; set; }
    }
}