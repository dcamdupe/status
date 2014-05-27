using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DataInterfaces.Models;

namespace Site.Services
{
    public interface IAuthenticationService
    {
        User AutheticateUser(HttpContextBase context, string login, string password);
    }
}
