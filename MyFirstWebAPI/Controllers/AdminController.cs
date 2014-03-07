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
using System.Diagnostics;
using MyFirstWebAPI.APIInterception;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Newtonsoft.Json;
namespace MyFirstWebAPI.Controllers
{
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        private IProductRepository repository;
        private string controllername;
        public AdminController(IProductRepository repository, string controllername)
        {
            this.repository = repository;
            this.controllername = controllername;
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
        public  async Task<HttpResponseMessage> GetOrder(int id)
        {
            APIResponseWrapper ordersReturn = new APIResponseWrapper();
            var order = await this.repository.GetOrderByID(id);
            if (order == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ordersReturn);
            }
            ordersReturn.data = order;
            //return new APIActionResult(order,Request);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, ordersReturn);
            return response;
        }

        [Route("orderLegacys/{id:int}")]
        [ResponseType(typeof(Order))]
        public async Task<HttpResponseMessage> GetOrderByIDLegacy(int id)
        {
            APIResponseWrapper ordersReturn = new APIResponseWrapper();
            var order = await this.repository.GetOrderLagacyByID(id);
            if (order == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ordersReturn);
            }
            ordersReturn.data = order;
            return Request.CreateResponse(HttpStatusCode.OK, ordersReturn);
        }

        [Route("orderLegacys")]
        public async Task<HttpResponseMessage> GetOrderLegacy(int id)
        {
            APIResponseWrapper ordersReturn = new APIResponseWrapper();
            var order = await this.repository.GetOrdersLagacy();
            if (order == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ordersReturn);
            }
            ordersReturn.data = order;
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