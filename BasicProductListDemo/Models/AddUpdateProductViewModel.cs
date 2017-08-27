using ProductListMVCDemo.Objects.Enums;
using ProductListMVCDemo.Objects.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Models
{
    public class AddUpdateProductViewModel
    {
        public bool IsUpdate { get; set; }

        public ProductBase Product { get; set; }

        public ProductType ProductType { get; set; }

        public string ErrorMessage { get; set; }

        /// <summary>
        /// Creates the view model for the add/update product page.
        /// </summary>
        /// <param name="product">Product that is being modified (if any)</param>
        /// <param name="productType">Type of the product being created or modified</param>
        /// <param name="isErorr">Error string to display on the page. Set when posting back.</param>
        /// <returns>The generated view model</returns>
        public static AddUpdateProductViewModel GetModel(ProductBase product, ProductType productType, string errorMessage = null)
        {
            AddUpdateProductViewModel model = new AddUpdateProductViewModel();

            model.IsUpdate = product != null;
            model.Product = product;
            model.ProductType = productType;
            model.ErrorMessage = errorMessage;

            return model;
        }
    }
}