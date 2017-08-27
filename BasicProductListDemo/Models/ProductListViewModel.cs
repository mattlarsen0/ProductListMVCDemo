
using ProductListMVCDemo.Objects.Products;
using ProductListMVCDemo.Objects.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProductListMVCDemo.Objects;

namespace ProductListMVCDemo.Models
{
    public class ProductListViewModel
    {
        public List<ProductBase> AllProducts { get; set; }

        public ProductBase MostExpensiveProduct { get; set; }

        public ProductBase MostQuantityProduct { get; set; }

        public ProductBase LeastExpensiveProduct { get; set; }

        public ProductBase LeastQuantityProduct { get; set; }

        public bool IsError { get; private set; }

        public string Message { get; private set; }

        /// <summary>
        /// Gets the view model for the main product list
        /// </summary>
        /// <param name="productContext">Database context for products</param>
        /// <param name="addedProduct">If true, a success message will be shown. Set this to true when adding a product and redirecting to this list</param>
        /// <param name="errorMessage">If set, an error message will be displayed</param>
        /// <returns>The generated view model</returns>
        public static ProductListViewModel GetModel(ProductListContext productContext, bool addedProduct = false, string errorMessage = null)
        {
            ProductListViewModel model = new ProductListViewModel();
            List<ProductBase> allProductsList = productContext.AllProducts.Where(p => !p.Removed).ToList();

            // Get products in a single list to find min/max values
            IEnumerable<ProductBase> allProductsOrderedByPrice = allProductsList.OrderBy(p => p.Price);
            IEnumerable<ProductBase> allProductsOrderedByQuantity = allProductsList.OrderBy(p => p.Quantity);

            model.AllProducts = allProductsList;
            model.MostExpensiveProduct = allProductsOrderedByPrice.FirstOrDefault();
            model.LeastExpensiveProduct = allProductsOrderedByPrice.LastOrDefault();
            model.MostQuantityProduct = allProductsOrderedByQuantity.FirstOrDefault();
            model.LeastQuantityProduct = allProductsOrderedByQuantity.LastOrDefault();
            model.IsError = false;

            if (addedProduct)
            {
                model.Message = General.AddedProductSuccess;
            }
            else
            {
                model.IsError = true;
                model.Message = errorMessage;
            }

            return model;
        }
    }
}