using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstWebAPI.Models.Connection
{
    public class SQLConnectionHelper : IConnection
    {
        private string module;
        
        public SQLConnectionHelper(string module)
        {
            this.module = module;
        }

        public string connectionString()
        {
            return null;
        }
    }
}