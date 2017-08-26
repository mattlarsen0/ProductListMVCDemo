using ProductListMVCDemo.Objects.Products;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Objects.DAL
{
    public class ProductListContext : DbContext
    {
        public ProductListContext() : base("ProductListContext")
        {
        }

        public DbSet<GameProduct> GameProducts { get; set; }

        public DbSet<CarProduct> CarProducts { get; set; }

        public DbSet<ProductBase> AllProducts { get; set; }
    }
}