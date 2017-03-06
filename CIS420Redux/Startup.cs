using CIS420Redux.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CIS420Redux.Startup))]
namespace CIS420Redux
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }
        public void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //If Admin role doesn't exist, create first Admin Role and a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {
                //First we create Admin role   
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Then we create a Admin user                
                var user = new ApplicationUser();
                user.UserName = "admin@email.com"; //Use same UserName and Email for simplicity. 
                user.Email = "admin@email.com";    //Else you will need to modify the login action in the AccountController
                string userPWD = "Password!1";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            //If Advisor role doesn't exist, create first Advisor Role and a default Advisor User    
            if (!roleManager.RoleExists("Advisor"))
            {
                //First we create Advisor role   
                var role = new IdentityRole();
                role.Name = "Advisor";
                roleManager.Create(role);

                //Then we create a Admin user                
                var user = new ApplicationUser();
                user.UserName = "advisor@email.com"; //Use same UserName and Email for simplicity. 
                user.Email = "advisor@email.com";    //Else you will need to modify the login action in the AccountController
                string userPWD = "Password!1";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, "advisor");
                }
            }

            //If SuperAdmin role doesn't exist, create first SuperAdmin Role and a default SuperAdmin User    
            if (!roleManager.RoleExists("SuperAdmin"))
            {
                //First we create SuperAdmin role   
                var role = new IdentityRole();
                role.Name = "SuperAdmin";
                roleManager.Create(role);

                //Then we create a Admin user                
                var user = new ApplicationUser();
                user.UserName = "superAdmin@email.com"; //Use same UserName and Email for simplicity. 
                user.Email = "superAdmin@email.com";    //Else you will need to modify the login action in the AccountController
                string userPWD = "Password!1";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, "SuperAdmin");
                }
            }

            //If Student role doesn't exist, create first Student Role and a default Student User    
            if (!roleManager.RoleExists("Student"))
            {
                //First we create Student role   
                var role = new IdentityRole();
                role.Name = "Student";
                roleManager.Create(role);

                //Then we create a Admin user                
                var user = new ApplicationUser();
                user.UserName = "student@email.com"; //Use same UserName and Email for simplicity. 
                user.Email = "student@email.com";    //Else you will need to modify the login action in the AccountController
                string userPWD = "Password!1";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, "Student");
                }
            }
        }
    }
}
