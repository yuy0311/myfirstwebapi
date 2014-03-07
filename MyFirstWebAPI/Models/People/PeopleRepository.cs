using MyFirstWebAPI.Models.Connection;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MyFirstWebAPI.Models.People
{
    public class PeopleRepository : IPeopleRepository
    {
        MyFirstWebAPIDBEntities db;
        public PeopleRepository(IDBConnection myconnection)
        {
            EntityConnection connection = new EntityConnection(myconnection.connectionString());
            Debug.WriteLine("People:" + myconnection.connectionString());
            db = new MyFirstWebAPIDBEntities(connection);
        }

        public int getStudentCampusID(int studentid)
        {
            throw new NotImplementedException();
        }
    }
}