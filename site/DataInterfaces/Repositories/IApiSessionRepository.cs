using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataInterfaces.Models;

namespace DataInterfaces.Repositories
{
    public interface IApiSessionRepository
    {
        Session GetSession(int userId);
        Session IsSessionValid(string sessionId);
        int GetUserFromSession(string sessionId);
        void RefreshExpiry(string sessionId);
        void EndSession(string sessionId);
    }
}
