using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataInterfaces.Repositories;
using Data;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Status/

        public ActionResult Index()
        {
            return View();
        }

    }
}
