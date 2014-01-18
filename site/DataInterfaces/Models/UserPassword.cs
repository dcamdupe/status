using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataInterfaces.Models
{
    public class UserPassword
    {
        public string Salt { get; set; }
        public string Password { get; set; }
    }
}
