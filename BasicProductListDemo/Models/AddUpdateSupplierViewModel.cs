using ProductListMVCDemo.Objects.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Models
{
    public class AddUpdateSupplierViewModel
    {
        public bool IsUpdate { get; set; }

        public Supplier Supplier { get; set; }

        public string ErrorMessage { get; set; }

        public string FormAction { get; set; }

        /// <summary>
        /// Creates the view model for the add/update supplier page.
        /// </summary>
        /// <param name="supplier">
        /// Supplier that is being modified (if any).
        /// In the event of an error, this will be used to hold form data that was posted back
        /// </param>
        /// <param name="isErorr">Error string to display on the page. Set when posting back.</param>
        /// <returns>The generated view model</returns>
        public static AddUpdateSupplierViewModel GetModel(Supplier supplier, string errorMessage = null)
        {
            AddUpdateSupplierViewModel model = new AddUpdateSupplierViewModel();

            model.IsUpdate = supplier?.SupplierID > 0;
            model.Supplier = supplier;
            model.ErrorMessage = errorMessage;

            if (model.IsUpdate)
            {
                model.FormAction = "UpdateSupplier";
            }
            else
            {
                model.FormAction = "AddSupplier";
            }

            return model;
        }
    }
}