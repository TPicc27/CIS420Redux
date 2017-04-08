using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Scheduler.MVC5.Global.Auth;

namespace Scheduler.MVC5
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Database.SetInitializer<AppUserIdentityDbContext>(new CustUserIdentityDbcontextInitializer());


            AppUserIdentityDbContext db = new AppUserIdentityDbContext();
            db.Database.Initialize(true);
        }
    }
}
