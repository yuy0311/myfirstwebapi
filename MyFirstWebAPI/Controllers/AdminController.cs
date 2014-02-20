using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MyFirstWebAPI.Models;

namespace MyFirstWebAPI.Controllers
{
    public class AdminController : ApiController
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repository)
        {
            this.repository = repository;
        }
      
        // GET api/Admin
        public IQueryable<Product> GetProdcuts()
        {
            return this.repository.GetAll();
        }
        /*
        // GET api/Admin/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // GET api/Admin/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product p = db.Products.Find(id);
            if(p == null)
            {
                return NotFound();
            }
            return Ok(p);
        }
   

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.Id == id) > 0;
        }
         *  * */


    }

}