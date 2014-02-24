using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyFirstWebAPI.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAllProducts();
        Task<Product> GetProductByID(int id);
        IQueryable<Order> GetAllOrders();
        IQueryable<OrderDetail> GetAllOrderDetails();
    }
}
