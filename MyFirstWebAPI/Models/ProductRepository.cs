using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MyFirstWebAPI.Models.Products;
using System.Linq.Expressions;
using MyFirstWebAPI.Models.Orders;

namespace MyFirstWebAPI.Models
{
    public class ProductRepository : IDisposable, IProductRepository
    {
        private MyFirstWebAPIDBEntities db = new MyFirstWebAPIDBEntities();
             
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
                details = x.OrderDetails.AsQueryable().Include(b=>b.Product).Select(AsOrderDetailDTO)
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

        public Task<Order> GetOrderByID(int id)
        {
            /*
            return db.Orders
                .Where(o => o.Id == id)
                .Select(ASOrderDto)
                .FirstOrDefaultAsync();
             */
            var results = from order in db.Orders
                          from orderdetails in db.OrderDetails
                          where order.Id == orderdetails.OrderId && order.Id == id
                          select order;
            ICollection<Order> i = results.ToList<Order>();
            return results.FirstOrDefaultAsync();
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