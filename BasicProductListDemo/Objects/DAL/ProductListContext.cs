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

        public DbSet<Supplier> Suppliers { get; set; }

        public void AddGameProduct(string name, decimal price, int quantity, GameProductType gameType, int yearOfRelease, int recommendedAge, int supplierID)
        {
            GameProduct newProduct = new GameProduct
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                GameType = gameType,
                YearOfRelease = yearOfRelease,
                RecommendedAge = recommendedAge,
                SupplierID = supplierID
            };

            GameProducts.Add(newProduct);
        }

        public void AddCarProduct(string name, decimal price, int quantity, int year, string color, int supplierID)
        {
            CarProduct newProduct = new CarProduct
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                Year = year,
                Color = color,
                SupplierID = supplierID
            };

            CarProducts.Add(newProduct);
        }

        public void UpdateGameProduct(int productID, string name, decimal price, int quantity, GameProductType gameType, int yearOfRelease, int recommendedAge, int supplierID)
        {
            GameProduct productToUpdate = GameProducts.FirstOrDefault(p => p.ProductID == productID);

            if (productToUpdate != null)
            {
                productToUpdate.Name = name;
                productToUpdate.Price = price;
                productToUpdate.Quantity = quantity;
                productToUpdate.GameType = gameType;
                productToUpdate.YearOfRelease = yearOfRelease;
                productToUpdate.RecommendedAge = recommendedAge;
                productToUpdate.SupplierID = supplierID;
            }
        }

        internal void UpdateCarProduct(int productID, string name, decimal price, int quantity, int year, string color, int supplierID)
        {
            CarProduct productToUpdate = CarProducts.FirstOrDefault(p => p.ProductID == productID);

            if (productToUpdate != null)
            {
                productToUpdate.Name = name;
                productToUpdate.Price = price;
                productToUpdate.Quantity = quantity;
                productToUpdate.Year = year;
                productToUpdate.Color = color;
                productToUpdate.SupplierID = supplierID;
            }
        }

        public void AddSupplier(string name, string phoneNumber)
        {
            Supplier supplier = new Supplier
            {
                Name = name,
                PhoneNumber = phoneNumber
            };

            Suppliers.Add(supplier);
        }

        public void UpdateSupplier(int supplierID, string name, string phoneNumber)
        {
            Supplier supplierToUpdate = Suppliers.FirstOrDefault(s => s.SupplierID == supplierID);

            if (supplierToUpdate != null)
            {
                supplierToUpdate.Name = name;
                supplierToUpdate.PhoneNumber = phoneNumber;
            }
        }
    }
}