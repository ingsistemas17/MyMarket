using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyMarket.Models
{
    public class ExampleContext : IdentityDbContext<IdentityUser>
    {
        public ExampleContext() : base("ContextBD")
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<BuyingProduct> BuyingProducts { get; set; }

        public virtual DbSet<WareHouse> WareHouses { get; set; }

        public virtual DbSet<Receipt> Receipts { get; set; }

        public virtual DbSet<SaleProduct> SaleProducts { get; set; }
    }
}