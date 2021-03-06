﻿using ProductListMVCDemo.Objects;

namespace ProductListMVCDemo.Models
{
    public class AddProductBaseModel : IActionModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        /// <summary>
        /// Validates an action's model to ensure that data is correct
        /// </summary>
        /// <param name="errorMessage">Error message to be displayed to the user. Only set when model is invalid</param>
        /// <returns>True if the model is valid, False otherwise</returns>
        public virtual bool IsValid(out string errorMessage)
        {
            bool isValid = true;
            errorMessage = null;

            if (string.IsNullOrEmpty(Name))
            {
                errorMessage = Errors.ProductsEmptyName;
                isValid = false;
            }
            else if (Name.Length > 50)
            {
                errorMessage = Errors.ProductsNameTooLong;
                isValid = false;
            }
            else if (Price < 0)
            {
                errorMessage = Errors.ProductsNegativePrice;
                isValid = false;
            }
            else if (Quantity < 0)
            {
                errorMessage = Errors.ProductsNegativeQuantity;
                isValid = false;
            }

            return isValid;
        }
    }
}