using ProductListMVCDemo.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Models
{
    public class AddUpdateSupplierModel : IActionModel
    {
        public int? SupplierID { get; set; }

        /// <summary>
        /// Validates an action's model to ensure that data is correct
        /// </summary>
        /// <param name="errorMessage">Error message to be displayed to the user. Only set when model is invalid</param>
        /// <returns>True if the model is valid, False otherwise</returns>
        public bool IsValid(out string errorMessage)
        {
            bool isValid = true;
            errorMessage = null;

            if (SupplierID.HasValue && SupplierID <= 0)
            {
                errorMessage = Errors.CannotFindProduct;
                isValid = false;
            }

            return isValid;
        }
    }
}