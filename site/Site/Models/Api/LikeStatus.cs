using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models.Api
{
    public class LikeStatus : BaseApiMessage
    {
        public int StatusId { get; set; }
    }
}