using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using site.Models;

namespace site.Controllers
{
    public class UserController : Controller
    {
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
                // TODO: take them to a login page
                return View(auth);
            }

            return View(auth);
        }
    }
}
