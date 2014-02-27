using MyFirstWebAPI.Models.Orders;
using MyFirstWebAPI.Models.Products;
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
        IQueryable<ProductWrapper> GetAllProducts();
        Task<ProductWrapper> GetProductByID(int id);
        IQueryable<OrderDataWrapper> GetAllOrders();
        Task<OrderDataWrapper> GetOrderByID(int id);
        IQueryable<OrderDetail> GetAllOrderDetails();

        Task<OrderDataWrapper> GetOrderLagacyByID(int id);
        ICollection<OrderDataWrapper> GetOrdersLagacy();
    }
}
