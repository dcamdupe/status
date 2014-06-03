using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataInterfaces.Models
{
    public class Session
    {
        public string SessionId { get; set; }
        public DateTime Expires { get; set; }
    }
}
