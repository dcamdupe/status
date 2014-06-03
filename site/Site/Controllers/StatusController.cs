using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataInterfaces.Repositories;
using Data;
using Site.Models;
using Site.Services;

namespace Site.Controllers
{
    public class StatusController : Controller
    {
        private IStatusService _statusService;
        private HttpSessionStateBase _session;
        public const int ItemsPerPage = 10;

        public StatusController(IStatusService statusService, HttpSessionStateBase session)
        {
            _statusService = statusService;
            _session = session;
        }

        [Authorize]
        public ActionResult Index(int? pageNumber)
        {
            if (!pageNumber.HasValue)
                pageNumber = 1;

            var statusList = _statusService.GetHistory((int)_session["UserId"], pageNumber.Value, ItemsPerPage);

            return View("History", statusList);
        }

        [Authorize]
        public ActionResult New(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return View("New");

            var statusId = _statusService.Add(message, (int)_session["UserId"]);

            return RedirectToAction("Single", new { statusId = statusId });
        }

        [Authorize]
        public ActionResult Single(int statusId)
        {
            return View("Single", _statusService.Get(statusId));
        }

        [Authorize]
        public ActionResult Search(string text, int pageNumber)
        {
            throw new NotImplementedException();
        }
    }
}
