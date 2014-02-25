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
using System.Threading.Tasks;
using MyFirstWebAPI.Models.Products;
using MyFirstWebAPI.Utility;

namespace MyFirstWebAPI.Controllers
{
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        private IProductRepository repository;
        private String name;

        public AdminController(IProductRepository repository, String controllerName)
        {
            this.repository = repository;
            this.name = controllerName;
        }
      
        // GET api/Admin
        [Route("products")]
        public IQueryable<ProductWrapper> GetProdcuts()
        {
            return this.repository.GetAllProducts();
        }

        [Route("products/{id:int}")]
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProductByID(int id)
        {
            var product = await this.repository.GetProductByID(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [Route("orders")]
        public IQueryable<OrderDataWrapper> GetOrders()
        {
            return this.repository.GetAllOrders();
        }

        [Route("orders/{id:int}")]
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> GetOrder(int id)
        {
            
            var order = await this.repository.GetOrderByID(id);
            if (order == null)
            {
                return NotFound();
            }
            return new APIActionResult(order,Request);
            //return Ok(order);
        }

        [Route("orderdetails")]
        public IQueryable<OrderDetail> GetOrderDetails()
        {
            return this.repository.GetAllOrderDetails();
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