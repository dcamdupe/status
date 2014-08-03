using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models.Api
{
    public class NewStatus : BaseApiMessage
    {
        public string Message { get; set; }
    }
}