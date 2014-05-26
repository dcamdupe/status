using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class Status
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public int ViewCount { get; set; }
        public int LikeCount { get; set; }
        public DateTime DateAdded { get; set; }
    }
}