using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Scheduler.MVC5.Global.Auth
{
    public class UserRoleInt : IdentityUserRole<int>
    {
    }

    public class UserClaimInt : IdentityUserClaim<int>
    {
    }

    public class UserLoginInt : IdentityUserLogin<int>
    {
    }

    public class RoleInt : IdentityRole<int, UserRoleInt>
    {
        public RoleInt() { }
        public RoleInt(string name) { Name = name; }
    }

    public class UserStoreInt : UserStore<CustUserIdentity, RoleInt, int, UserLoginInt, UserRoleInt, UserClaimInt>
    {
        public UserStoreInt(DbContext context) : base(context)
        {
        }
    }

    public class CustUserIdentity : IdentityUser<int, UserLoginInt, UserRoleInt, UserClaimInt>
    {
        public string Domain { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TimezoneOffSetInMinutes { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(CustomUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class AppUserIdentityDbContext : IdentityDbContext<CustUserIdentity, RoleInt, int, UserLoginInt, UserRoleInt, UserClaimInt>
    {
        public AppUserIdentityDbContext()
            : base("SchedulerContext")
        {
        }

        public static AppUserIdentityDbContext Create()
        {
            return new AppUserIdentityDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustUserIdentity>().ToTable("Users");
            modelBuilder.Entity<RoleInt>().ToTable("Roles");
            modelBuilder.Entity<UserRoleInt>().ToTable("UserRoles");
            modelBuilder.Entity<UserLoginInt>().ToTable("UserLogins");
            modelBuilder.Entity<UserClaimInt>().ToTable("UserClaims");
        }
    }

    
}