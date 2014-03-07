using MyFirstWebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstWebAPI.Models.Connection
{
    public class MultiplyEntityConnectionHelper : IDBConnection
    {
        private IPeopleRepository peopeRepo;
        private ISchoolInfoRepository schoolRepo;
        public MultiplyEntityConnectionHelper(IPeopleRepository prepo, ISchoolInfoRepository srepo)
        {
            this.peopeRepo = prepo;
            this.schoolRepo = srepo;
        }

        public string connectionString(string requestid)
        {
            /*
            int requestidint = Convert.ToInt32(requestid);
            int campusid = peopeRepo.getStudentCampusID(requestidint);
            return schoolRepo.getDBConnectionStringViaCampusid(campusid);
             * */
            return ConnectionStringBuilder.getEntityConnectionStr(@"YANG-WINX7\YANGSQLEXPRESS", "MyFirstWebAPIDB2", "Models.WebAPIModel", "sa", "1q2w3e4r");
        }
    }
}