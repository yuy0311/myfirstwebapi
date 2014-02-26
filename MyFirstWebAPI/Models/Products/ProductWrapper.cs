using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstWebAPI.Models.Products
{
    public class ProductWrapper
    {
        public int productid { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public decimal cost { get; set; }
    }
}