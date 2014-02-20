using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstWebAPI.Models
{
    public class ProductRepository : IDisposable, IProductRepository
    {
        private MyFirstWebAPIDBEntities db = new MyFirstWebAPIDBEntities();
        public IQueryable<Product> GetAll()
        {
            return db.Products;
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