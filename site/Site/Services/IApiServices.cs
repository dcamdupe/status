using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Site.Services
{
    public interface IApiServices
    {
        string CreateSession(string login, string password);
        bool IsSessionValid(string sessionId);
        void EndSession(string sessionId);
    }
}
