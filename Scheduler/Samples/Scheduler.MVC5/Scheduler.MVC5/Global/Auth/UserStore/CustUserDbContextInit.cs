using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Scheduler.MVC5.Global.Auth
{
    public class CustUserIdentityDbcontextInitializer : CreateDatabaseIfNotExists<AppUserIdentityDbContext>
    {
        protected override void Seed(AppUserIdentityDbContext context)
        {
            //InitializeIdentityForEF(context);
            base.Seed(context);
        }

        private void InitializeIdentityForEF(AppUserIdentityDbContext context)
        {
            var UserManager = new UserManager<CustUserIdentity,int>(new UserStoreInt(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            string name = "test";
            string password = "123456";
        
            //Create Role Test and User Test
           
            UserManager.Create(new CustUserIdentity()
            {
                UserName = name,
                Domain = "ITEEDEE",
                FirstName = "Test User"
            }, password);

            //Create Role Admin if it does not exist
            

            //Create User=Admin with password=123456
            var user = new CustUserIdentity();
            user.UserName = name;
            user.Domain = "ITEEDEE";
            user.FirstName = "Admin User";
            var adminresult = UserManager.CreateAsync(user);

            //Add User Admin to Role Admin
            if (adminresult.IsCompleted)
            {
                var result = UserManager.AddToRoleAsync(user.Id, name);
            }
        }
    }
}