using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using SiteLogic;
using DataInterfaces.Models;

namespace Site.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private IAuthentication _authentication;

        public AuthenticationService(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        public User AutheticateUser(HttpContextBase context, string login, string password)
        {
            var user = _authentication.AuthenticateUser(login, password);
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}