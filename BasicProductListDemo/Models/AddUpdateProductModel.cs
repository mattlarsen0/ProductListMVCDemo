﻿using ProductListMVCDemo.Objects;
using ProductListMVCDemo.Objects.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Models
{
    public class AddUpdateProductModel : IActionModel
    {
        public int? ProductID { get; set; }

        [EnumDataType(typeof(ProductType))]
        public ProductType ProductType { get; set; }

        /// <summary>
        /// Validates an action's model to ensure that data is correct
        /// </summary>
        /// <param name="errorMessage">Error message to be displayed to the user. Only set when model is invalid</param>
        /// <returns>True if the model is valid, False otherwise</returns>
        public bool IsValid(out string errorMessage)
        {
            bool isValid = true;
            errorMessage = null;

            if (ProductID.HasValue && ProductID <= 0)
            {
                errorMessage = Errors.CannotFindProduct;
                isValid = false;
            }

            return isValid;
        }
    }
}