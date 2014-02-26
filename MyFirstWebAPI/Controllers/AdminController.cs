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
        public HttpResponseMessage GetOrders()
        {
            APIResponseWrapper ordersReturn = new APIResponseWrapper();
            ordersReturn.data = this.repository.GetAllOrders();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, ordersReturn);
            return response;
        }

        [Route("orders/{id:int}")]
        [ResponseType(typeof(Order))]
        public async Task<HttpResponseMessage> GetOrder(int id)
        {
            APIResponseWrapper ordersReturn = new APIResponseWrapper();
            var order = await this.repository.GetOrderByID(id);
            if (order == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ordersReturn);
            }
            ordersReturn.data = order;
            //return new APIActionResult(order,Request);
            return Request.CreateResponse(HttpStatusCode.OK, ordersReturn);
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