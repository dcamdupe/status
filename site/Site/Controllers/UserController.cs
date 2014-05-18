using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Site.Models;
using SiteLogic;
using Site.Services;

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
                if (_authenticationService.AutheticateUser(HttpContext, auth.Login, auth.Password))
                {
                    var userId = HttpContext.Session["userId"];
                    return RedirectToAction("index", "");
                }

                return View(auth);
            }

            return View(auth);
        }

        [HttpGet]
        public ActionResult Logout(AuthenticationDetails auth)
        {
            this.Session.Abandon();
            return RedirectToAction("index", "");
        }
    }
}
