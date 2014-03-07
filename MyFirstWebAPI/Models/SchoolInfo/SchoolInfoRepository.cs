using MyFirstWebAPI.Models.Connection;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MyFirstWebAPI.Models.SchoolInfo
{
    public class SchoolInfoRepository : ISchoolInfoRepository
    {
        MyFirstWebAPIDBEntities entities;
        public SchoolInfoRepository(IDBConnection myconnection)
        {
            EntityConnection connection = new EntityConnection(myconnection.connectionString());
            Debug.WriteLine("School" + myconnection.connectionString());
            entities = new MyFirstWebAPIDBEntities(connection);
        }

        public string getDBConnectionStringViaCampusid(int id)
        {
            throw new NotImplementedException();
        }
    }
}