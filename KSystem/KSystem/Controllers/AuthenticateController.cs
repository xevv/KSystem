using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using KSystem.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace KSystem.Controllers
{
    public class AuthenticateController : Controller
    {
        public string UserId
        {
            get
            {
                return User.Identity.GetUserId();
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return User.Identity.IsAuthenticated;
            }
        }

        private KSystemUserManager _userManager { get; set; }

        public KSystemUserManager UserManager
        {
            get
            {
                if (IsAuthenticated)
                    return _userManager ?? HttpContext.GetOwinContext().GetUserManager<KSystemUserManager>();
                else
                    return null;
            }
        }


        public IAuthenticationManager AuthentificationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        public T GetSession<T>() where T : class, new()
        {
            string sessionKey = typeof(T).Name;
            T result = HttpContext.Session[sessionKey] as T;
            if (result == null)
            {
                result = new T();
                HttpContext.Session[sessionKey] = result;
                return result;
            }
            else
            {
                return result;
            }
        }

        public void SetSession<T>(object item) where T : class
        {
            HttpContext.Session[typeof(T).Name] = item;
        }

    }
}