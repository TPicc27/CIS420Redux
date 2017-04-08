using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Scheduler.MVC5.Global.Auth;

[assembly: OwinStartup(typeof(Scheduler.MVC5.App_Start.Startup))]

namespace Scheduler.MVC5.App_Start
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.CreatePerOwinContext(AppUserIdentityDbContext.Create);
            app.CreatePerOwinContext<CustomUserManager>(CustomUserManager.Create);


            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            //This will used the HTTP header: "Authorization"      Value: "Bearer 1234123412341234asdfasdfasdfasdf"
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }
    }
}
