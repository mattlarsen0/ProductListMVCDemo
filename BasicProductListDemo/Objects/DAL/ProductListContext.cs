using ProductListMVCDemo.Classes.Products;
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

        /// <summary>
        /// Helper method that concatenates all products to a base class IQueryable
        /// </summary>
        /// <returns>All products in a concatenated IQueryable</returns>
        public IQueryable<ProductBase> GetAllProducts()
        {
            IQueryable<ProductBase> gameProductsBaseCast = GameProducts.Cast<ProductBase>();
            IQueryable<ProductBase> carProductsBaseCast = CarProducts.Cast<ProductBase>();

            IQueryable<ProductBase> result = gameProductsBaseCast.Concat(carProductsBaseCast);

            return result;
        }
    }
}