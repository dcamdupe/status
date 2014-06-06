using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Site.Models;
using SiteLogic;
using Site.Services;
using System.Web.Security;
using log4net;

namespace Site.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly HttpSessionStateBase _session;
        private readonly ILog _log;

        public UserController(IAuthenticationService authenticationService, HttpSessionStateBase session, ILog log)
        {
            _authenticationService = authenticationService;
            _session = session;
            _log = log;
        }

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
                    _log.Info(string.Format("login suceeded, {0}", auth.Login));
                    return RedirectToAction("index", "");
                }

                _log.Info(string.Format("login failed, {0}", auth.Login));

                return View(auth);
            }
            _log.Info("Failed login attempt, model invalid");

            return View(auth);
        }

        [HttpGet]
        public ActionResult Logout(AuthenticationDetails auth)
        {
            _session.Abandon();
            return RedirectToAction("index", "");
        }
    }
}
