using ProductListMVCDemo.Objects.Products;
using ProductListMVCDemo.Objects.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Models
{
    public class ProductListViewModel
    {
        public List<ProductBase> AllProducts { get; set; }

        public ProductBase MostExpensiveProduct { get; set; }

        public ProductBase MostQuantityProduct { get; set; }

        public ProductBase LeastExpensiveProduct { get; set; }

        public ProductBase LeastQuantityProduct { get; set; }

        public static ProductListViewModel GetModel(ProductListContext productContext)
        {
            ProductListViewModel model = new ProductListViewModel();
            List<ProductBase> allProductsList = productContext.AllProducts.ToList();

            // Get products in a single list to find min/max values
            IEnumerable<ProductBase> allProductsOrderedByPrice = allProductsList.OrderBy(p => p.Price);
            IEnumerable<ProductBase> allProductsOrderedByQuantity = allProductsList.OrderBy(p => p.Quantity);

            model.AllProducts = allProductsList;
            model.MostExpensiveProduct = allProductsOrderedByPrice.FirstOrDefault();
            model.LeastExpensiveProduct = allProductsOrderedByPrice.LastOrDefault();
            model.MostQuantityProduct = allProductsOrderedByQuantity.FirstOrDefault();
            model.LeastQuantityProduct = allProductsOrderedByQuantity.LastOrDefault();

            return model;
        }
    }
}