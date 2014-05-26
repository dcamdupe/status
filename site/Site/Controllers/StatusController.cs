using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataInterfaces.Repositories;
using Data;
using Site.Models;

namespace Site.Controllers
{
    public class StatusController : Controller
    {
        private IStatusRepository _statusController;

        public StatusController(IStatusRepository statusController)
        {
            _statusController = statusController;
        }

        //
        // GET: /Status/
        [Authorize]
        public ActionResult Index()
        {
            var statusList = _statusController.GetHistory(1, 1, 10)
                .Select(s => new Status {DateAdded = s.DateAdded, ViewCount = s.Views, LikeCount = s.Likes, Message = s.Message, UserId = s.AddedBy.UserId, UserName = s.AddedBy.UserName});

            return View(new StatusList { Status = statusList});
        }

    }
}
