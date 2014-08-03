using System.Web;
using System.Net.Http;
using System.ServiceModel.Channels;

namespace Site.Services
{
    public static class WebApiHelpers
    {
        public static string GetClientIp(this HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            
            if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
                return ((RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name]).Address;

            return null;
        }
    }
}