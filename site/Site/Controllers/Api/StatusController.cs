using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Site.Services;
using Site.Models.Api;

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

        public int Post(NewMessage status)
        {
            if (!_apiServices.IsSessionValid(status.SessionId))
                throw new HttpResponseException(HttpStatusCode.Unauthorized);

            var user = _apiServices.GetUserFromSession(status.SessionId);

            return _statusService.Add(status.Message, user);
        }
    }
}
