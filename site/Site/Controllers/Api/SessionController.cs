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
    public class SessionController : ApiController
    {
        private IApiServices _apiServices;

        public SessionController(IApiServices apiServices)
        {
            _apiServices = apiServices;
        }

        public string Post(LoginDetails login)
        {
            return _apiServices.CreateSession(login.Login, login.Password);
        }

        public void Delete(string sessionId)
        {
            _apiServices.EndSession(sessionId);
        }
    }
}
