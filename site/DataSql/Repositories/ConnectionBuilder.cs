using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.EntityClient;

namespace Data.Repositories
{
    public class ConnectionBuilder
    {
        public static statusContainer Create(string connectionString)
        {
            var entityBuilder = new EntityConnectionStringBuilder();

            entityBuilder.Provider = "System.Data.SqlClient";
            entityBuilder.ProviderConnectionString = connectionString;

            entityBuilder.Metadata = @"res://*";

            return new statusContainer(entityBuilder.ToString());   
        }
    }
}
