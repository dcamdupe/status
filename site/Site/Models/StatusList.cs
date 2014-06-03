using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class StatusList
    {
        public IEnumerable<Status> Status { get; set; }
        public int Page { get; set; }
        public int ItemsPerPage {get;set;}
        public int PageCount { get; set; }
    }
}