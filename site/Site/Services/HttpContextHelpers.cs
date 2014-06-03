using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Services
{
    public static class HttpContextHelpers
    {
        public static bool IsAuthenicated(HttpContextBase context)
        {
            if (System.Web.HttpContext.Current.User == null)
                return false;

            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                return false;

            return context.Session["UserId"] != null;
        }
    }
}