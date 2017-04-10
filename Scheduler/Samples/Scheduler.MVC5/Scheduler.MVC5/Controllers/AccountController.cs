using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Scheduler.MVC5.Global.Auth;
using Scheduler.MVC5.Models;
using CustUserIdentity = Scheduler.MVC5.Global.Auth.CustUserIdentity;

namespace Scheduler.MVC5.Controllers
{
    public class AccountController : Controller
    {
        private CustomUserManager _customUserManager;

        public CustomUserManager CustomUserManager
        {
            get { return _customUserManager ?? HttpContext.GetOwinContext().GetUserManager<CustomUserManager>(); }
            private set { _customUserManager = value; }
        }

        // GET: Account

        public AccountController()
        {
        }
        public AccountController(CustomUserManager customUserManager)
        {
            _customUserManager = customUserManager;
        }

        private SignIn _helper;

        private SignIn SignInHelper
        {
            get { return _helper ?? (_helper = new SignIn(CustomUserManager, AuthenticationManager)); }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInHelper.PasswordSignIn(model.UserName, model.Password, model.RememberMe);
                switch (result)
                {
                    case SignIn.SignInStatus.Success:
                        return RedirectToLocal(returnUrl);
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }
            }

            return View(model);
        }

        //get
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //register the user
                try
                {
                    if (ModelState.IsValid)
                    {
                        var user = new CustUserIdentity { UserName = model.UserName};
                        var result = await CustomUserManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            await SignInAsync(user, isPersistent: false);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            AddErrors(result);
                        }
                    }


                    return View(model);
                }
                catch (Exception e)
                {
                    //ModelState.AddModelError();
                }
            }

            return View(model);
        }



        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing && _customUserManager != null)
            {
                _customUserManager.Dispose();
                _customUserManager = null;
            }
            base.Dispose(disposing);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(CustUserIdentity user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            var identity = await CustomUserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
           
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}