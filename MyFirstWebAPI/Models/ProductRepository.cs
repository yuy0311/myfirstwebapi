﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Linq.Expressions;
using System.Threading;
using System.Data.Entity.Core.EntityClient;
using MyFirstWebAPI.Models.Products;
using MyFirstWebAPI.Models.Orders;
using MyFirstWebAPI.Utility;
using MyFirstWebAPI.Models.Connection;
using MyFirstWebAPI.APIInterception;
using Microsoft.Practices.EnterpriseLibrary.Logging.PolicyInjection;

namespace MyFirstWebAPI.Models
{
    public class ProductRepository : IDisposable, IProductRepository
    {
        private MyFirstWebAPIDBEntities db;
        public ProductRepository(IDBConnection myconntection)
        {
             EntityConnection connection = new EntityConnection(myconntection.connectionString());
             db = new MyFirstWebAPIDBEntities(connection);
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
     
        [LogCallHandler(Priority=9)]
        public IQueryable<ProductWrapper> GetAllProducts()
        {
           // return db.Products.Select(AsProductDto);
            throw new NotImplementedException();
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

        [LoggingCallHandler(1)]
        public Task<OrderDataWrapper> GetOrderByID(int id)
        {
            return db.Orders
                .Where(o => o.Id == id)
                .Select(ASOrderDto).FirstOrDefaultAsync();
        }

        public Task<OrderDataWrapper> GetOrderLagacyByID(int id)
        {
            return Task.Run(()=> this._GetOrderLagacyByIDInternal(id));
        }

        private  OrderDataWrapper _GetOrderLagacyByIDInternal(int id)
        {
            Order order = db.Orders.Where(o => o.Id == id).Single();
            OrderDataWrapper odw = new OrderDataWrapper();
            odw.orderid = order.Id;
            odw.customer = order.Customer;
            odw.date = order.Date;
            odw.details = order.OrderDetails.AsQueryable().Include(o => o.Product).Select(AsOrderDetailDTO);
            return odw;
        }

        public Task<ICollection<OrderDataWrapper>> GetOrdersLagacy()
        {
            return null;
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