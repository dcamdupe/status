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
        private HttpSessionStateBase _session;

        public UserController(IAuthenticationService authenticationService, HttpSessionStateBase session)
        {
            _authenticationService = authenticationService;
            _session = session;
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
                    FormsAuthentication.SetAuthCookie(auth.Login, false);
                    _session["UserId"] = user.UserId;
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
