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

        public string ProductTypeStr { get; set; }

        public string FormProductAction { get; set; }

        /// <summary>
        /// Creates the view model for the add/update product page.
        /// </summary>
        /// <param name="product">
        /// Product that is being modified (if any).
        /// In the event of an error, this will be used to hold form data that was posted back
        /// </param>
        /// <param name="productType">Type of the product being created or modified</param>
        /// <param name="isErorr">Error string to display on the page. Set when posting back.</param>
        /// <returns>The generated view model</returns>
        public static AddUpdateProductViewModel GetModel(ProductBase product, ProductType productType, string errorMessage = null)
        {
            AddUpdateProductViewModel model = new AddUpdateProductViewModel();

            model.IsUpdate = product?.ProductID > 0;
            model.Product = product;
            model.ProductType = productType;
            model.ErrorMessage = errorMessage;

            switch (model.ProductType)
            {
                case ProductType.Game:
                    if (model.IsUpdate)
                    {
                        model.FormProductAction = "UpdateGameProduct";
                    }
                    else
                    {
                        model.FormProductAction = "AddGameProduct";
                    }
                    model.ProductTypeStr = "Game";
                    break;
                case ProductType.Car:
                    if (model.IsUpdate)
                    {
                        model.FormProductAction = "UpdateCarProduct";
                    }
                    else
                    {
                        model.FormProductAction = "AddCarProduct";
                    }
                    model.ProductTypeStr = "Car";
                    break;
            }

            return model;
        }
    }
}