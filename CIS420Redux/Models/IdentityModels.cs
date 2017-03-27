using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CIS420Redux.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
#if DEBUG
            ////This will create database if one doesn't exist.
            Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());
            ////This will drop and re-create the database if model changes.
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
#endif
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Student> Students { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<Program> Program { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Campus> Campus { get; set; }

        public DbSet<Event> Events { get; set; }


        public System.Data.Entity.DbSet<CIS420Redux.Models.Admin> Admins { get; set; }

        public System.Data.Entity.DbSet<CIS420Redux.Models.Advisor> Advisors { get; set; }

        public System.Data.Entity.DbSet<CIS420Redux.Models.UdApplication> UDApplications { get; set; }

        public System.Data.Entity.DbSet<CIS420Redux.Models.Alert> Alerts { get; set; }

        public System.Data.Entity.DbSet<CIS420Redux.Models.Todo> Todoes { get; set; }

        public System.Data.Entity.DbSet<CIS420Redux.Models.ClincalCompliance> ClincalCompliances { get; set; }

        public System.Data.Entity.DbSet<CIS420Redux.Models.POS> POS { get; set; }

        public System.Data.Entity.DbSet<CIS420Redux.Models.Document> Documents { get; set; }
    }
}