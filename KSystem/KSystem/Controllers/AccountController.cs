using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KSystem.Model;
using KSystem.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using Microsoft.Owin.Security;
using System.Net;
using Microsoft.Owin;
namespace KSystem.Controllers
{
    [Authorize]
    public class AccountController : AuthenticateController
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                KSystemUser user = UserManager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    ClaimsIdentity identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthentificationManager.SignIn(identity);
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Неверный логин или пароль");
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            AuthentificationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }
    }
}
