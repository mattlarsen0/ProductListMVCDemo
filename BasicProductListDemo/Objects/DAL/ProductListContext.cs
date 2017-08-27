using ProductListMVCDemo.Objects.Enums;
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

        public void AddGameProduct(string name, decimal price, int quantity, GameProductType gameType, int yearOfRelease, int RecommendedAge)
        {
            GameProduct newProduct = new GameProduct
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                GameType = gameType,
                YearOfRelease = yearOfRelease,
                RecommendedAge = RecommendedAge
            };

            GameProducts.Add(newProduct);
        }

        public void AddCarProduct(string name, decimal price, int quantity, int year, string color)
        {
            CarProduct newProduct = new CarProduct
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                Year = year,
                Color = color
            };

            CarProducts.Add(newProduct);
        }
    }
}