using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Scheduler.MVC5.Global.Auth
{
    public class SignIn
    {

        public CustomUserManager CustomUserManager { get; private set; }
        public IAuthenticationManager AuthenticationManager { get; private set; }
        public SignIn(CustomUserManager customManager, IAuthenticationManager authManager)
        {
            CustomUserManager = customManager;
            AuthenticationManager = authManager;
        }

        public async Task SignInAsync(CustUserIdentity user, bool isPersistent, bool rememberBrowser)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            var userIdentity = await user.GenerateUserIdentityAsync(CustomUserManager);
            if (rememberBrowser)
            {
                var rememberBrowserIdentity = AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(user.Id.ToString());
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity, rememberBrowserIdentity);
            }
            else
            {
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity);
            }
        }

        public async Task<SignInStatus> PasswordSignIn(string userName, string password, bool isPersistent)
        {
            var user = await CustomUserManager.FindByNameAsync(userName);
            if (user == null)
            {
                return SignInStatus.Failure;
            }
            if (await CustomUserManager.CheckPasswordAsync(user, password))
            {
                return await SignInOrTwoFactor(user, isPersistent);
            }
            if (await CustomUserManager.IsLockedOutAsync(user.Id))
            {
                return SignInStatus.LockedOut;
            }
            
            return SignInStatus.Failure;
        }

        private async Task<SignInStatus> SignInOrTwoFactor(CustUserIdentity user, bool isPersistent)
        {
            if (await CustomUserManager.GetTwoFactorEnabledAsync(user.Id) &&
                !await AuthenticationManager.TwoFactorBrowserRememberedAsync(user.Id.ToString()))
            {
                var identity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorCookie);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                AuthenticationManager.SignIn(identity);
                return SignInStatus.RequiresTwoFactorAuthentication;
            }
            await SignInAsync(user, isPersistent, false);
            return SignInStatus.Success;
        }

        
        public enum SignInStatus
        {
            Success,
            LockedOut,
            RequiresTwoFactorAuthentication,
            Failure
        }

    }
}