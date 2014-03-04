using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Models.Connection
{
    public interface IDBConnection
    {
        string connectionString(); 
    }
}
