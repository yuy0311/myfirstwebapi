using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MyFirstWebAPI.Models.Products;
using System.Linq.Expressions;

namespace MyFirstWebAPI.Models
{
    public class ProductRepository : IDisposable, IProductRepository
    {
        private MyFirstWebAPIDBEntities db = new MyFirstWebAPIDBEntities();
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
                date = x.Date
            };

        public IQueryable<ProductWrapper> GetAllProducts()
        {
            return db.Products.Select(AsProductDto);
        }

        public Task<ProductWrapper> GetProductByID(int id)
        {

            return db.Products
                .Where(p => p.Id == id)
                .Select(AsProductDto)
                .FirstOrDefaultAsync();
        }

        public IQueryable<OrderDataWrapper> GetAllOrders()
        {
            return db.Orders.Select(ASOrderDto);
        }

        public Task<OrderDataWrapper> GetOrderByID(int id)
        {
            return db.Orders
                .Where(o => o.Id == id)
                .Select(ASOrderDto)
                .FirstOrDefaultAsync();
        }

        public IQueryable<OrderDetail> GetAllOrderDetails()
        {
            return db.OrderDetails;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}