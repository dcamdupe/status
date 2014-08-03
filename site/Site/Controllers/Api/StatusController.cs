using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Site.Services;
using Site.Models.Api;
using Site.Models;

namespace Site.Controllers.Api
{
    public class StatusController : ApiController
    {
        private IApiServices _apiServices;
        private IStatusService _statusService;

        public StatusController(IApiServices apiServices, IStatusService statusService)
        {
            _apiServices = apiServices;
            _statusService = statusService;
        }

        public int Post(NewStatus status)
        {
            if (!_apiServices.IsSessionValid(status.SessionId))
                throw new HttpResponseException(HttpStatusCode.Unauthorized);

            var user = _apiServices.GetUserFromSession(status.SessionId);

            return _statusService.Add(status.Message, user);
        }

        public Status Get(int id, [FromUri] string SessionId)
        {
            if (!_apiServices.IsSessionValid(SessionId))
                throw new HttpResponseException(HttpStatusCode.Unauthorized);

            var viewUser = new ViewUser { UserId = _apiServices.GetUserFromSession(SessionId), IpAddress = Request.GetClientIp() };

            return _statusService.Get(id, viewUser);
        }

        [HttpGet]
        public SearchResults Search([FromUri] string SessionId, [FromUri] string SearchText, [FromUri] int PageNumber)
        {
            if (!_apiServices.IsSessionValid(SessionId))
                throw new HttpResponseException(HttpStatusCode.Unauthorized);

            return _statusService.Search(SearchText, PageNumber, 15);
        }

        [HttpGet]
        public StatusList History([FromUri] string SessionId, [FromUri] int PageNumber)
        {
            if (!_apiServices.IsSessionValid(SessionId))
                throw new HttpResponseException(HttpStatusCode.Unauthorized);

            var user = _apiServices.GetUserFromSession(SessionId);

            return _statusService.GetHistory(user, PageNumber, 15);
        }

        [HttpPost]
        public void Like(LikeStatus Status)
        {
            if (!_apiServices.IsSessionValid(Status.SessionId))
                throw new HttpResponseException(HttpStatusCode.Unauthorized);

            _statusService.AddLike(Status.StatusId, Request.GetClientIp(), _apiServices.GetUserFromSession(Status.SessionId));
        }
    }
}
