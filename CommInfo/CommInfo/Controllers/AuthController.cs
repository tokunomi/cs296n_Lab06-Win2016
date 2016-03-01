using CommInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace CommInfo.Controllers
{
    public class AuthController : Controller
    {
        // Hard coding a User Manager for now, to avoid having to do Dependency Injection
        UserManager<Member> userManager = new UserManager<Member>(
                new UserStore<Member>(new CommInfoContext()));

        // GET: Auth/Login
        public ActionResult LogIn(string returnUrl)
        {
            var model = new LogInViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        // POST: Auth/Login
        [HttpPost]
        public ActionResult LogIn(LogInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Using UserManager (no async)
            var user = userManager.Find(model.Email, model.Password);

            if (user != null)
            {
                var identity = userManager.CreateIdentity(
                    user, DefaultAuthenticationTypes.ApplicationCookie);

                GetAuthenticationManager().SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            // user authN failed
            ModelState.AddModelError("", "Invalid email or password");
            return View();


            // Hard coded user
            // Don't do this in production!
            /*if (model.Email == "admin@admin.com" && model.Password == "password")
            {
                var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, "Admin"),
                new Claim(ClaimTypes.Email, "test@test.com"),
                new Claim(ClaimTypes.Country, "United States")
            },
                    "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            // user authN failed
            ModelState.AddModelError("", "Invalid email or password");
            return View();  */
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }

            return returnUrl;
        }

        // Auth/LogOut
        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }

        // Added from 'ASP.NET Identity Stripped Bare' example code on GitHub
        private IAuthenticationManager GetAuthenticationManager()
        {
            var ctx = Request.GetOwinContext();
            return ctx.Authentication;
        }
        // Also added from GitHub, modified to NOT use async
        private void SignIn(Member user)
        {
            var identity = userManager.CreateIdentity(
                user, DefaultAuthenticationTypes.ApplicationCookie);

            GetAuthenticationManager().SignIn(identity);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new Member
            {
                // the order of the following is how they will display
                //UserName = model.Email,
                NameFirst = model.NameFirst,
                NameLast = model.NameLast,
                Email = model.Email,
                UserName = model.UserName
            };

            var result = userManager.Create(user, model.Password);  // Error: {System.Data.Entity.Validation.DbEntityValidationResult}

            if (result.Succeeded)
            {
                SignIn(user);
                return RedirectToAction("index", "home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && userManager != null)
            {
                userManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

