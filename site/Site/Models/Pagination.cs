using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class Pagination
    {
        public int PageCount { get; set; }
        public int Page { get; set; }
        public string BaseUrl { get; set; }
    }
}