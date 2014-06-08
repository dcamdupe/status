using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Site.Services;

namespace Site.Controllers
{
    public class SessionController : ApiController
    {
        private IApiServices _apiServices;

        public SessionController(IApiServices apiServices)
        {
            _apiServices = apiServices;
        }

        public string Post(string login, string password)
        {
            return _apiServices.CreateSession(login, password);
        }
    }
}
