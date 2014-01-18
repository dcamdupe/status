using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataInterfaces.Models
{
    public class Status
    {
        public int StatusId { get; set; }
        public string Message { get; set; }
        public DateTime DateAdded { get; set; }
        public User AddedBy { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
    }
}
