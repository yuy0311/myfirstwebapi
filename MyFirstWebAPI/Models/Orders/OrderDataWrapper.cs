using MyFirstWebAPI.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstWebAPI.Models
{
    public class OrderDataWrapper
    {
        public int orderid { get; set; }
        public string customer { get; set; }
        public DateTime date { get; set; }
        public IQueryable<OrderDetailsWrapper> details { get; set; }
    }
}