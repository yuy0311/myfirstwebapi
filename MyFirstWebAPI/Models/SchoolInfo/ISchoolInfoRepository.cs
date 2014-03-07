using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstWebAPI.Models
{
    public interface ISchoolInfoRepository
    {
        string getDBConnectionStringViaCampusid(int id);
    }
}