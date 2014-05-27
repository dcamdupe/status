using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Site.Models;
using SiteLogic;
using Site.Services;
using System.Web.Security;

namespace Site.Controllers
{
    public class UserController : Controller
    {
        private IAuthenticationService _authenticationService;

        public UserController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(AuthenticationDetails auth)
        {
            if (ModelState.IsValid)
            {
                var user = _authenticationService.AutheticateUser(HttpContext, auth.Login, auth.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(auth.Login, true);
                    Session["UserId"] = user.UserId;
                    return RedirectToAction("index", "");
                }

                return View(auth);
            }

            return View(auth);
        }

        [HttpGet]
        public ActionResult Logout(AuthenticationDetails auth)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index", "");
        }
    }
}
