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
        private HttpRequestBase _requestBase;
        private IStatusService _statusService;
        private HttpSessionStateBase _session;
        public const int ItemsPerPage = 10;

        public StatusController(IStatusService statusService, HttpSessionStateBase session, HttpRequestBase requestBase)
        {
            _statusService = statusService;
            _session = session;
            _requestBase = requestBase;
        }

        [Authorize]
        public ActionResult Index(int? page)
        {
            if (!page.HasValue)
                page = 1;

            var statusList = _statusService.GetHistory((int)_session["UserId"], page.Value, ItemsPerPage);

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
            var viewUser = new ViewUser { UserId = (int)_session["UserId"], IpAddress = _requestBase.UserHostAddress };
            return View("Single", _statusService.Get(statusId, viewUser));
        }

        [Authorize]
        public ActionResult Search(string searchText, int? page)
        {
            if (!page.HasValue)
                page = 1;

            var statusList = _statusService.Search(searchText, page.Value, ItemsPerPage);

            return View("Search", statusList);
        }

        [Authorize]
        public ActionResult AddLike(int statusId)
        {
            _statusService.AddLike(statusId, _requestBase.UserHostAddress, (int)_session["UserId"]);
            return RedirectToAction("Single", new { statusId = statusId });
        }
    }
}
