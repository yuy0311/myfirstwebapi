﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyFirstWebAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyFirstWebAPIDBEntities : DbContext
    {
        private const string connectionstring = @"YANG-WINX7\YANGSQLEXPRESS;initial catalog=MyFirstWebAPI;user id=sa;password=1q2w3e4r;MultipleActiveResultSets=True;App=EntityFramework";
        public MyFirstWebAPIDBEntities()
            : base("MyFirstWebAPIDBEntities")
        {
           // this.Database.Connection.ConnectionString = connectionstring;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}
