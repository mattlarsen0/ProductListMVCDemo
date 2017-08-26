using ProductListMVCDemo.Classes.Products;
using ProductListMVCDemo.Objects.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Models
{
    public class ProductListViewModel
    {
        public ProductListContext ProductContext{ get; set; }

        public ProductBase MostExpensiveProduct { get; set; }

        public ProductBase MostQuantityProduct { get; set; }

        public ProductBase LeastExpensiveProduct { get; set; }

        public ProductBase LeastQuantityProduct { get; set; }

        public static ProductListViewModel GetModel(ProductListContext productContext)
        {
            ProductListViewModel model = new ProductListViewModel();

            // Get products in a single list to find min/max values
            IQueryable<ProductBase> allProducts = productContext.GetAllProducts();
            IEnumerable<ProductBase> allProductsOrderedByPrice = allProducts.OrderBy(p => p.Price);
            IEnumerable<ProductBase> allProductsOrderedByQuantity = allProducts.OrderBy(p => p.Quantity);

            model.ProductContext = productContext;
            model.MostExpensiveProduct = allProductsOrderedByPrice.FirstOrDefault();
            model.LeastExpensiveProduct = allProductsOrderedByPrice.LastOrDefault();
            model.MostQuantityProduct = allProductsOrderedByQuantity.FirstOrDefault();
            model.LeastQuantityProduct = allProductsOrderedByQuantity.LastOrDefault();

            return model;
        }
    }
}