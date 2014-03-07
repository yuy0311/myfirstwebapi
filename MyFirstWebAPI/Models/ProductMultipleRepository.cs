using MyFirstWebAPI.Models.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using System.Linq.Expressions;
using System.Data.Entity;
using MyFirstWebAPI.Models.Orders;
using MyFirstWebAPI.Models.Products;

namespace MyFirstWebAPI.Models
{
    public class ProductMultipleRepository : IDisposable, IProductRepository
    {
        private IDBConnection dbconnection;
        private MyFirstWebAPIDBEntities db;
        public ProductMultipleRepository(IDBConnection myconnection)
        {
            this.dbconnection = myconnection;
        }

        public IQueryable<Products.ProductWrapper> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<Products.ProductWrapper> GetProductByID(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<OrderDataWrapper> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<OrderDataWrapper> GetOrderByID(int id)
        {
            EntityConnection connection = new EntityConnection(dbconnection.connectionString(id.ToString()));
            db = new MyFirstWebAPIDBEntities(connection);
             return db.Orders
                .Where(o => o.Id == id)
                .Select(ASOrderDto).FirstOrDefaultAsync();
        }

        public IQueryable<OrderDetail> GetAllOrderDetails()
        {
            throw new NotImplementedException();
        }

        public Task<OrderDataWrapper> GetOrderLagacyByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<OrderDataWrapper>> GetOrdersLagacy()
        {
            throw new NotImplementedException();
        }

        protected void Dispose(bool disposing)
        {
            if (db != null)
            {
                db.Dispose();
                db = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

           private static readonly Expression<Func<OrderDetail, OrderDetailsWrapper>> AsOrderDetailDTO =
            x => new OrderDetailsWrapper
        {
            productid = x.Product.Id,
            price = x.Product.Price,
            cost = x.Product.Cost,
            name = x.Product.Name,
            quantity = x.Quantity
        };
    
        private static readonly Expression<Func<Product, ProductWrapper>> AsProductDto =
            x => new ProductWrapper
            {
                productid = x.Id,
                price = x.Price,
                cost = x.Cost,
                name = x.Name
            };

        private static readonly Expression<Func<Order, OrderDataWrapper>> ASOrderDto =
            x => new OrderDataWrapper
            {
                orderid = x.Id,
                customer  = x.Customer,
                date = x.Date,
                details = x.OrderDetails.AsQueryable().Select(AsOrderDetailDTO)
            };
    }
}