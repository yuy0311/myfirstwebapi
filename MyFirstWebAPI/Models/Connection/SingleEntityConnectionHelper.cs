using MyFirstWebAPI.Models.Utility;
using MyFirstWebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyFirstWebAPI.Models.Connection
{
    public class SingleEntityConnectionHelper : IDBConnection
    {
        private string context;
        private string module;
        private const string providerName = "System.Data.SqlClient";
        public SingleEntityConnectionHelper(string context,string module)
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
            sqlBuilder.DataSource = AppConfigXMLParser.getXMLValue(module,AppSettingsConstant.SERVER);//serverName; this shall be read from xml file
            sqlBuilder.InitialCatalog = AppConfigXMLParser.getXMLValue(module, AppSettingsConstant.DATABASE);//databaseName;
            //if the integratedSecurity
            Object o = StringAdditions.ConvertToBoolean(AppConfigXMLParser.getXMLValue(module, AppSettingsConstant.ADINTERGRATED));
            if(o != null)
            {
                if ((Boolean)o)
                {
                    sqlBuilder.IntegratedSecurity = true;//true;
                }
                else
                {
                    sqlBuilder.IntegratedSecurity = false;
                    sqlBuilder.UserID = AppConfigXMLParser.getXMLValue(module, AppSettingsConstant.DBUSERNAME);
                    sqlBuilder.Password = AppConfigXMLParser.getXMLValue(module, AppSettingsConstant.DBPWD);
                }
            }
            else
            {
                throw new Exception("AppConfig XML AD Intergration value is correct");
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