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
        public List<Supplier> Suppliers { get; set; }

        public List<ProductBase> AllProducts { get; set; }

        public ProductBase MostExpensiveProduct { get; set; }

        public ProductBase MostQuantityProduct { get; set; }

        public ProductBase LeastExpensiveProduct { get; set; }

        public ProductBase LeastQuantityProduct { get; set; }

        public bool IsErrorMessage { get; set; }

        public string Message { get; set; }

        public Supplier MostProductSupplier { get; set; }

        public Supplier LeastProductSupplier { get; set; }

        /// <summary>
        /// Dictionary of total supplier value (price * quantity), Keyed by supplier id
        /// </summary>
        public Dictionary<int, decimal> SupplierProductValue { get; set; }

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
            model.Suppliers = productContext.Suppliers.Where(s => !s.Removed).ToList();

            List<ProductBase> allProductsList = productContext.AllProducts.Where(p => !p.Removed).ToList();

            // Get products in a single list to find min/max values
            IEnumerable<ProductBase> allProductsOrderedByPrice = allProductsList.OrderBy(p => p.Price);
            IEnumerable<ProductBase> allProductsOrderedByQuantity = allProductsList.OrderBy(p => p.Quantity);

            model.AllProducts = allProductsList;
            model.MostExpensiveProduct = allProductsOrderedByPrice.LastOrDefault();
            model.LeastExpensiveProduct = allProductsOrderedByPrice.FirstOrDefault();
            model.MostQuantityProduct = allProductsOrderedByQuantity.LastOrDefault();
            model.LeastQuantityProduct = allProductsOrderedByQuantity.FirstOrDefault();

            // lookup supplier with most products
            IEnumerable<int> supplierIDsOrderedByProductCount = from p in allProductsList
                                                                group p by p.SupplierID into g
                                                                orderby g.Sum(p => p.Quantity) descending
                                                                select g.Key;

            int mostProductSupplierID = supplierIDsOrderedByProductCount.FirstOrDefault();
            int leastProductSupplierID = supplierIDsOrderedByProductCount.LastOrDefault();

            model.MostProductSupplier = model.Suppliers.FirstOrDefault(s => s.SupplierID == mostProductSupplierID);
            model.LeastProductSupplier = model.Suppliers.FirstOrDefault(s => s.SupplierID == leastProductSupplierID);

            model.SupplierProductValue = allProductsList.GroupBy(p => p.SupplierID)
                                                        .ToDictionary(g => g.Key, g => g.Sum(p => p.Quantity * p.Price));

            if (addedProduct)
            {
                model.IsErrorMessage = false;
                model.Message = General.AddedSuccess;
            }
            else
            {
                model.IsErrorMessage = true;
                model.Message = errorMessage;
            }

            return model;
        }
    }
}