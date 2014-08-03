using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataInterfaces.Repositories;
using DataInterfaces.Models;
using SiteLogic;

namespace Site.Services
{
    public class ApiServices : IApiServices
    {
        private readonly IApiSessionRepository _apiRepository;
        private readonly IAuthentication _authService;

        public ApiServices(IApiSessionRepository apiRepository, IAuthentication authService)
        {
            _apiRepository = apiRepository;
            _authService = authService;
        }

        public string CreateSession(string login, string password)
        {
            var user = _authService.AuthenticateUser(login, password);
            if (user != null)
            {
                return _apiRepository.GetSession(user.UserId).SessionId;
            }

            return "";
        }

        public bool IsSessionValid(string sessionId)
        {
            var session = _apiRepository.IsSessionValid(sessionId);

            if (session != null)
            {
                _apiRepository.RefreshExpiry(session.SessionId);
                return true;
            }
            return false;
        }

        public void EndSession(string sessionId)
        {
            _apiRepository.EndSession(sessionId);
        }

        public int GetUserFromSession(string sessionId)
        {
            return _apiRepository.GetUserFromSession(sessionId);
        }
    }
}