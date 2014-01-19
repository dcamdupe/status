using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.Repositories
{
    public class ConnectionDetails
    {
        public ConnectionDetails(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public string ConnectionString { get; set; }
    }
}