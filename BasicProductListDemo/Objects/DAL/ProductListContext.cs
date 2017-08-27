using ProductListMVCDemo.Objects.Products;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ProductListMVCDemo.Objects.Enums;

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

        public void AddGameProduct(string name, decimal price, int quantity, GameProductType gameType, int yearOfRelease, int reccomendedAge)
        {
            GameProduct newProduct = new GameProduct
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                GameType = gameType,
                YearOfRelease = yearOfRelease,
                ReccomendedAge = reccomendedAge
            };

            GameProducts.Add(newProduct);
        }
    }
}