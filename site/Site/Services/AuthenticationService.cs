using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using SiteLogic;

namespace Site.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private IAuthentication _authentication;

        public AuthenticationService(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        public bool AutheticateUser(HttpContextBase context, string login, string password)
        {
            var userId = _authentication.AuthenticateUser(login, password);
            if (userId.HasValue)
            {
                return true;
            }
            return false;
        }
    }
}