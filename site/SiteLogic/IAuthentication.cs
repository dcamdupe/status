using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataInterfaces.Models;

namespace SiteLogic
{
    public interface IAuthentication
    {
        User AuthenticateUser(string login, string password);
    }
}
