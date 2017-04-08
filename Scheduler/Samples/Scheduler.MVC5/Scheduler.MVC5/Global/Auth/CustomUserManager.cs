using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Scheduler.MVC5.Model;

namespace Scheduler.MVC5.Global.Auth
{
    public class CustomUserManager:UserManager<CustUserIdentity,int>
    {
        public CustomUserManager(IUserStore<CustUserIdentity,int> store) : base(store)
        {
        }
        
        public static CustomUserManager Create(IdentityFactoryOptions<CustomUserManager> options, IOwinContext context)
        {
            var manager =
                new CustomUserManager(new UserStoreInt(context.Get<AppUserIdentityDbContext>()));
            
            return manager;
        }
    }

}