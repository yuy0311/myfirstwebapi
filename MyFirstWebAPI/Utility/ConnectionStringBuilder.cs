using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyFirstWebAPI.Utility
{
    public class ConnectionStringBuilder
    {
         private const string providerName = "System.Data.SqlClient";

         public static string getEntityConnectionStr(string serverName, string databaseName, string metadata,string userid, string pwd)
        {
            // underlying provider.
            SqlConnectionStringBuilder sqlBuilder =
                new SqlConnectionStringBuilder();

            // Set the properties for the data source.
            sqlBuilder.DataSource = serverName;
            sqlBuilder.InitialCatalog = databaseName;
            sqlBuilder.IntegratedSecurity = false;
            sqlBuilder.UserID = userid;
            sqlBuilder.Password = pwd;

            // Build the SqlConnection connection string.
            string providerString = sqlBuilder.ToString();

            // Initialize the EntityConnectionStringBuilder.
            EntityConnectionStringBuilder entityBuilder =
                new EntityConnectionStringBuilder();

            //Set the provider name.
            entityBuilder.Provider = providerName;

            // Set the provider-specific connection string.
            entityBuilder.ProviderConnectionString = providerString;

            // Set the Metadata location.

            entityBuilder.Metadata = String.Format(@"res://*/{0}.csdl|
                            res://*/{0}.ssdl|
                            res://*/{0}.msl", metadata);
             string s = entityBuilder.ToString();
            return entityBuilder.ToString();
        }
    }
}