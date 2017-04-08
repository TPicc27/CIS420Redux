using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Scheduler.MVC5.Model.Models;

namespace Scheduler.MVC5.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SchedulerContext : DbContext
    {
        public SchedulerContext()
            : base("name=SchedulerContext")
        
        {
            
        }

         public virtual DbSet<ColoredEvent> ColoredEvents { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Grid> Grids { get; set; }
        public virtual DbSet<Recurring> Recurrings { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<ValidEvent> ValidEvents { get; set; }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<ColoredEvent>()
                .Property(e => e.text)
                .IsUnicode(false);

            modelBuilder.Entity<Event>()
                .Property(e => e.text)
                .IsUnicode(false);

            modelBuilder.Entity<Grid>()
                .Property(e => e.details)
                .IsUnicode(false);

            modelBuilder.Entity<Recurring>()
                .Property(e => e.text)
                .IsUnicode(false);
        }
    }

}
