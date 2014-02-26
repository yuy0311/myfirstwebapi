using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstWebAPI.Models.Orders
{
    public class OrderDetailsWrapper
    {
        public int productid { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public decimal cost { get; set; }
        public int quantity { get; set; } 
    }
}