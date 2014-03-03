using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyFirstWebAPI.Models.Connection
{
    public class EntityConnectionHelper : IConnection
    {
        private string context;
        private string module;
        private const string providerName = "System.Data.SqlClient";
        public EntityConnectionHelper(string context,string module)
        {
            this.context = context;
            this.module = module;
        }

        public string connectionString()
        {
            // underlying provider.
            SqlConnectionStringBuilder sqlBuilder =
                new SqlConnectionStringBuilder();
            // Set the properties for the data source.
            sqlBuilder.DataSource = "";//serverName; this shall be read from xml file
            sqlBuilder.InitialCatalog = "";//databaseName;
            //if the integratedSecurity
            if(true)
            {
                sqlBuilder.IntegratedSecurity = true;//true;
            }
            else
            {
                sqlBuilder.IntegratedSecurity = false;
                sqlBuilder.UserID = "";//userid;
                sqlBuilder.Password = "";//pwd;
            }


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
                            res://*/{0}.msl", this.context);
            string s = entityBuilder.ToString();
            return entityBuilder.ToString();
        }
    }
}