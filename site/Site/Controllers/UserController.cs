using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using site.Models;
using SiteLogic;

namespace site.Controllers
{
    public class UserController : Controller
    {
        private Authentication _authentication;

        public UserController(Authentication authentication)
        {
            _authentication = authentication;
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
                var result = _authentication.AuthenticateUser(auth.Login, auth.Password);

                return View(auth);
            }

            return View(auth);
        }
    }
}
