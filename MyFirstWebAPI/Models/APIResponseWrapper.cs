using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstWebAPI.Models
{
    public class APIResponseWrapper
    {
        public int resultcode { get; set; }
        public string resultvalue { get; set; }
        public Object data { get; set; }
    }
}