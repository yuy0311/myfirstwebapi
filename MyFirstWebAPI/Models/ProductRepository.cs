using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MyFirstWebAPI.Models
{
    public class ProductRepository : IDisposable, IProductRepository
    {
        private MyFirstWebAPIDBEntities db = new MyFirstWebAPIDBEntities();
        public IQueryable<Product> GetAllProducts()
        {
            return db.Products;
        }

        public Task<Product> GetProductByID(int id)
        {
            return db.Products.Where(u => u.Id == id).FirstOrDefaultAsync();
        }


        public IQueryable<Order> GetAllOrders()
        {
            return db.Orders;
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