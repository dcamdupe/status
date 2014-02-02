using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteLogic
{
    public interface IAuthentication
    {
        int? AuthenticateUser(string login, string password);
    }
}
